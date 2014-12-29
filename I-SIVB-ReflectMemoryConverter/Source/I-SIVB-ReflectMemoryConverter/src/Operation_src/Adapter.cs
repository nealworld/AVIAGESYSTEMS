using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;

namespace I_SIVB_ReflectMemoryConverter.src.Operation_src
{
    /// <summary>
    /// class Opr is to manage the data handling circle with both VaisOpr and RfmOpr 
    /// </summary>
    /// <remarks>
    /// class Opr is to manage the data handling circle with both VaisOpr and RfmOpr 
    /// </remarks>
    /// <exception>
    /// null
    /// </exception>
    /// <returns>
    /// null
    /// </returns>
    class Adapter
    {
        private VaisOpr mVaisOpr;

        private RfmOpr mRfmOpr;

        private uint mPeriod;

        private Participant mParticipant = null;

        private DataSwap mDataSwap;

        /// <summary>
        /// constructor of mRfmOpr class
        /// </summary>
        /// <remarks>
        /// constructor of mRfmOpr class
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// null
        /// </returns>
        public Adapter( VaisOpr aVaisOpr, RfmOpr aRfmOpr )
        {
            mVaisOpr = aVaisOpr;
            mRfmOpr = aRfmOpr;
            mParticipant = aVaisOpr.Participant;
            mPeriod = aVaisOpr.Period;
            mDataSwap = aRfmOpr.DataSwap;
        }

        /// <summary>
        /// call the function of EnterNormalMode 
        /// </summary>
        /// <remarks>
        /// call the function of EnterNormalMode which will trigger the comupute delegate function run in period
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// void
        /// </returns>
        public void Run()
        {
            mParticipant.EnterNormalMode( mPeriod, new Participant.ComputeDelegate( ComputeCallBack ) );
            return;
        }
        private bool GetData1Ready()
        {
            bool lReady = false;
            return ( lReady );
        }
        private void SetData4Ready()
        {
            mRfmOpr.RfmSetData4Status( DataSourceStatus.Ready );
            // DataSourceStatus.Ready;
        }
        private void SetData4Paused()
        {
            mRfmOpr.RfmSetData4Status( DataSourceStatus.Pause );
            // DataSourceStatus.Pause;
        }
        private void SetData4Shutdown()
        {
            mRfmOpr.RfmSetData4Status( DataSourceStatus.Shutdown );
            // DataSourceStatus.Shutdown;
        }
        private void Process()
        {
            if ( mRfmOpr.RfmRead() == true )
            {
                byte[] lBytes = mRfmOpr.Data1ReadIn;
                byte[] lBytesConverted = RfmVaisDataConvert.RfmToVaisbytes( lBytes, mDataSwap );
                mVaisOpr.VaisSendData1( lBytesConverted );
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( "Can not read data1 Through VMIC" );
                LogGlobalManager.LogMgr.PrintLine( "VAIS May not Send messages" );
            }
            if ( mVaisOpr.VaisReadData4() == true )
            {
                byte[] lBytes = mVaisOpr.Data4ReadIn;
                mRfmOpr.Data4ToWrite = RfmVaisDataConvert.VaisToRfmbytes( lBytes, mDataSwap );
                if ( mRfmOpr.RfmWrite() != true )
                {
                    LogGlobalManager.LogMgr.PrintLine( "Can not write data to VMIC" );
                }
            }
            else
            {
                LogGlobalManager.LogMgr.PrintLine( "Can not read data4 from VAIS" );
            }


        }

        private void DeleteProcess()
        {
            mRfmOpr.RfmDelete();
        }

        private bool ComputeCallBack( Participant aParticipant, CommonSimTypes.Status aStatus, float aWrapper )
        {
            bool lResult = true;
            switch ( aStatus )
            {
                case CommonSimTypes.Status.Running:
                    if ( GetData1Ready() )
                    {
                        Process();
                        SetData4Ready();
                    };
                    break;
                case CommonSimTypes.Status.Paused:
                    SetData4Paused();
                    break;
                case CommonSimTypes.Status.Shutdown:
                    lResult = false;
                    SetData4Shutdown();
                    DeleteProcess();
                    break;

                default:
                    break;
            }

            return lResult;
        }
    }
}
