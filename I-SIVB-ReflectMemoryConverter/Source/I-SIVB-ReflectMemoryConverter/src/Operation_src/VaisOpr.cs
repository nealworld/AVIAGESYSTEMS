using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;

namespace I_SIVB_ReflectMemoryConverter.src.Operation_src
{
    /// <summary>
    /// class VaisOpr is to operate the vais  
    /// </summary>

    class VaisOpr
    {
        private SharedConfig mMessgaconfig = null;

        private VaisConfig mVaisConfig;

        private ParticipantString mParticipantConfig = null;

        private uint mPeriod;

        /// <summary>
        /// get the mPeriod which was set by VaisOpr constructor  
        /// </summary>

        public uint Period
        {
            get
            {
                return ( mPeriod );
            }
        }

        private List<Parameter> mData4Messages = null;

        private List<Parameter> mData1Messages = null;

        private Participant mParticipant = null;

        /// <summary>
        /// get the mParticipant which was set by createParticipant  
        /// </summary>
        /// <returns>
        /// Participant
        /// </returns>
        public Participant Participant
        {
            get
            {
                return ( mParticipant );
            }
        }

        private Collection mData1Bus = null;

        private Collection mData4Bus = null;

        private byte[] mData1;

        private byte[] mData4;

        /// <summary>
        /// get the mData4 which was set by readData4  
        /// </summary>
        /// <returns>
        /// byte[]
        /// </returns>
        public byte[] Data4ReadIn
        {
            get
            {
                return ( mData4 );
            }
        }

        /// <summary>
        /// constructor of VaisOpr class
        /// </summary>

        public VaisOpr( VaisConfig lViasConfig, SharedConfig lMessageConfig )
        {
            mVaisConfig = lViasConfig;
            mMessgaconfig = lMessageConfig;
            mPeriod = Convert.ToUInt32( lViasConfig.RefreshRate );
            mParticipantConfig = lViasConfig.ParticipantConfig;
        }


        private void CreateParticipant()
        {
            ParticipantInformation lParticipantInfo = new ParticipantInformation();
            lParticipantInfo.Name = mParticipantConfig.Name;//"TTTTTT";
            lParticipantInfo.Description = mParticipantConfig.Description;
            lParticipantInfo.PartNumber = mParticipantConfig.PartNumber;
            lParticipantInfo.Version = mParticipantConfig.Version;
            mParticipant = new Participant( lParticipantInfo );
            mParticipant.ConnectToMesh();
            return;
        }

        /// <summary>
        /// try to initialize the vais 
        /// </summary>
        /// <remarks>
        /// try to initialize the vais with the configurations 
        /// </remarks>
        /// <returns>
        /// bool 
        /// </returns>
        public bool VaisInit()
        {
            bool lResult = false;
            CreateParticipant();
            lResult = InitVaisHandles( mMessgaconfig.MessageConfigs[0], true );
            if ( lResult == true )
            {
                lResult = InitVaisHandles( mMessgaconfig.MessageConfigs[1], false );
            }
            return ( lResult );
        }

        private bool InitVaisHandles( VaisMessageConfig aVaisConfig, bool aDirection )
        {
            bool lResult = true;
            VaisMessageConfig lVaisMessageConfig = aVaisConfig;
            Collection lCollection = mParticipant.GetCollection( lVaisMessageConfig.name );
            List<Parameter> lparameters = new List<Parameter>();
            if ( lCollection != null )
            {
                foreach ( VaisParamConfig lParam in lVaisMessageConfig.parameters )
                {
                    string lName = lParam.name;
                    Parameter lParameter = lCollection.GetParameter( lName );
                    if ( lParameter != null )
                    {
                        lparameters.Add( lParameter );
                    }
                    else
                    {
                        lResult = false;
                        LogGlobalManager.LogMgr.PrintLine( "can not get the NPD parameter: " + lParameter.Name );
                    }
                }
            }
            else
            {
                lResult = false;
                LogGlobalManager.LogMgr.PrintLine( "can not get the NPD Message: " + lVaisMessageConfig.name );
            }
            if ( aDirection == true )//transmit
            {
                mData1Bus = lCollection;
                mData1Messages = lparameters;
                mData1 = new byte[RfmVaisDataConvert.getVaislength( mData1Messages )];
            }
            else
            {
                mData4Bus = lCollection;
                mData4Messages = lparameters;
                mData4 = new byte[RfmVaisDataConvert.getVaislength( mData4Messages )];
            }
            return ( lResult );
        }

        private int SetVal( Parameter aParam, int aIndex )
        {
            int lIndex = aIndex;

            switch ( aParam.GetDataType() )
            {
                case CommonSimTypes.ValueType.Bool:
                    aParam.SetValue( BitConverter.ToBoolean( mData1, lIndex ) );
                    lIndex++;
                    break;
                case CommonSimTypes.ValueType.Char:
                    aParam.SetValue( BitConverter.ToChar( mData1, lIndex ) );
                    lIndex++;
                    break;
                case CommonSimTypes.ValueType.Double:
                    aParam.SetValue( BitConverter.ToDouble( mData1, lIndex ) );
                    lIndex = lIndex + 8;
                    break;
                case CommonSimTypes.ValueType.Float:
                    aParam.SetValue( BitConverter.ToSingle( mData1, lIndex ) );
                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.Int16:
                    aParam.SetValue( BitConverter.ToInt16( mData1, lIndex ) );
                    lIndex = lIndex + 2;
                    break;
                case CommonSimTypes.ValueType.Int32:
                    aParam.SetValue( BitConverter.ToInt32( mData1, lIndex ) );
                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.Int64:
                    aParam.SetValue( BitConverter.ToInt64( mData1, lIndex ) );
                    lIndex = lIndex + 8;
                    break;

                case CommonSimTypes.ValueType.Int8:
                    aParam.SetValue( BitConverter.ToChar( mData1, lIndex ) );
                    lIndex = lIndex++;
                    break;
                case CommonSimTypes.ValueType.UInt16:
                    aParam.SetValue( BitConverter.ToChar( mData1, lIndex ) );
                    lIndex = lIndex + 2;
                    break;
                case CommonSimTypes.ValueType.UInt32:
                    aParam.SetValue( BitConverter.ToUInt32( mData1, lIndex ) );
                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.UInt64:
                    aParam.SetValue( BitConverter.ToUInt64( mData1, lIndex ) );
                    lIndex = lIndex + 8;
                    break;
                case CommonSimTypes.ValueType.UInt8:
                    aParam.SetValue( BitConverter.ToChar( mData1, lIndex ) );
                    lIndex = lIndex++;
                    break;
                default:
                    throw new Exception( aParam.GetDataType().ToString() + " is not supported yet." );

            }
            return ( lIndex );
        }

        private int GetVal( Parameter aParam, int aIndex )
        {
            int lOffset = aIndex;
            int lIndex = aIndex;
            byte[] lByte;
            switch ( aParam.GetDataType() )
            {
                case CommonSimTypes.ValueType.Bool:
                    lByte = BitConverter.GetBytes( aParam.GetValueBool() );
                    lIndex++;
                    break;
                case CommonSimTypes.ValueType.Char:
                    lByte = BitConverter.GetBytes( aParam.GetValueChar() );
                    lIndex++;
                    break;
                case CommonSimTypes.ValueType.Double:
                    lByte = BitConverter.GetBytes( aParam.GetValueDouble() );
                    lIndex = lIndex + 8;
                    break;
                case CommonSimTypes.ValueType.Float:
                    lByte = BitConverter.GetBytes( aParam.GetValueFloat() );
                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.Int16:
                    lByte = BitConverter.GetBytes( aParam.GetValueInt16() );
                    lIndex = lIndex + 2;
                    break;
                case CommonSimTypes.ValueType.Int32:
                    lByte = BitConverter.GetBytes( aParam.GetValueInt32() );
                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.Int64:
                    lByte = BitConverter.GetBytes( aParam.GetValueInt64() );
                    lIndex = lIndex + 8;
                    break;
                case CommonSimTypes.ValueType.Int8:
                    lByte = BitConverter.GetBytes( aParam.GetValueChar() );
                    lIndex = lIndex++;
                    break;
                case CommonSimTypes.ValueType.UInt16:
                    lByte = BitConverter.GetBytes( aParam.GetValueUInt16() );
                    lIndex = lIndex + 2;
                    break;
                case CommonSimTypes.ValueType.UInt32:
                    lByte = BitConverter.GetBytes( aParam.GetValueUInt32() );

                    lIndex = lIndex + 4;
                    break;
                case CommonSimTypes.ValueType.UInt64:
                    lByte = BitConverter.GetBytes( aParam.GetValueUInt64() );
                    lIndex = lIndex + 8;
                    break;
                case CommonSimTypes.ValueType.UInt8:
                    lByte = BitConverter.GetBytes( aParam.GetValueChar() );
                    lIndex = lIndex++;
                    break;
                default:
                    throw new Exception( aParam.GetDataType().ToString() + " is not supported yet." );

            }
            for ( int ldst = lOffset; ldst < lIndex; ldst++ )
            {
                mData4[ldst] = lByte[ldst - lOffset];
            }
            return ( lIndex );
        }

        /// <summary>
        /// Use data1 to update Vais Parameters' value 
        /// </summary>
        /// <remarks>
        /// Use data1 to update Vais Parameters' value  
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// void 
        /// </returns>
        public void VaisSendData1( byte[] aDatas )
        {
            mData1 = aDatas;
            int lNumbVaisParam = mData1Messages.Count;
            int lIndex = 0;
            for ( int lNum = 0; lNum < lNumbVaisParam; lNum++ )
            {
                lIndex = SetVal( mData1Messages[lNum], lIndex );
            }
        }

        /// <summary>
        /// try to read in the Vais parameters and update the data4  
        /// </summary>
        /// <remarks>
        /// try to read in the Vais parameters and update the data4   
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool 
        /// </returns>
        public bool VaisReadData4()
        {
            int lIndex = 0;
            bool lResult = false;
            while ( ( lResult = mData4Bus.Receive() ) )
            {
                foreach ( Parameter lParam in mData4Bus.Parameters )
                {
                    lIndex = GetVal( lParam, lIndex );
                }
            };
            return ( lResult );
        }

        private void beganReceiveMessages()
        {
            // mData1Bus.SetPeriod( mPeriod );
            //  mData4Bus.StartPeriodic(CommonSimTypes.Direction.Subscribe);
            //mData4Bus.Receive();//?
        }

        private void beganSendMessages()
        {
            // mData1Bus.StartPeriodic( CommonSimTypes.Direction.Publish );
        }

    }
}
