#define Test
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;
using Rfm2gDriverCsharp;
using I_SIVB_ReflectMemoryConverter.src.Configuration_src;
using I_SIVB_ReflectMemoryConverter.src.Operation_src;

namespace I_SIVB_ReflectMemoryConverter.src
{
    class Program
    {
        static void Main( string[] args )
        {
#if Test
            ParticipantInformation lParticipantInfo = new ParticipantInformation();
            lParticipantInfo.Name = "Test_rfm_vais_ready in define Test Status";
            lParticipantInfo.Description = "a";

            Participant msParticipant = new Participant( lParticipantInfo );
            msParticipant.ConnectToMesh();
            Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gOpen(1);
            Rfm2gDriverCsharp.Rfm2gDriverCsharp.CSharpRFM2gSetDMAByteSwap( true ) ;
            
#endif
            /// <args>
            /// <args[0] value ="path of the configuration file" />
            /// <args[1] value ="path of the log file" optional ="true"/>
            int lArgsLength = args.Length;
            if ( lArgsLength >= 2 )
            {
                LogGlobalManager.LogMgr = new LogManager( args[2] );
            }
            if ( lArgsLength >= 1 )
            {
                Program mProgram = new Program();
                if ( args != null )
                {
                    mProgram.Initialize( args[0] );
                }
                else
                {
                    mProgram.Initialize( "" );
                }
                mProgram.mAdapter.Run();
            }
            else
            {
                LogGlobalManager.LogMgr.PrintForQuit( "No configuration file specified" );
            }
            return;
           
        }
        private RfmOpr mRfmOpr;

        private VaisOpr mVaisOpr;

        private Adapter mAdapter;

        /// <summary>
        /// System initialization   
        /// </summary>
        /// <param name="aConfigPath"></param>
        private void Initialize( string aConfigPath )
        {
            ConfigReader lConfigReader = new ConfigReader( aConfigPath );

            SharedConfig lSharedConfig = null;
            VaisConfig lVaisConfig;
            RfmConfig lRfmConfig;
            if ( lConfigReader.ConfigApp() == true )
            {
                lSharedConfig = lConfigReader.SharedConfig;
                lVaisConfig = lConfigReader.VaisConfig;
                lRfmConfig = lConfigReader.RfmConfig;

                RfmOpr lRfmOpr = new RfmOpr( lRfmConfig, lSharedConfig );
                mRfmOpr = lRfmOpr;
#if Test
                lRfmOpr.RmfInit();
#else
                if ( lRfmOpr.RmfInit() != true )
                {
                    LogGlobalManager.LogMgr.PrintForQuit( "Error happened and keyboard input will stop the application" );
                }
                else
#endif
                {
                    VaisOpr lVaisOpr = new VaisOpr( lVaisConfig, lSharedConfig );
                    if ( lVaisOpr.VaisInit() != true )
                    {
                        LogGlobalManager.LogMgr.PrintForQuit( "Error happened and keyboard input will stop the application" );
                    }
                    else
                    {
                        mVaisOpr = lVaisOpr;
                        mAdapter = new Adapter( lVaisOpr, mRfmOpr );
                    }
                }
            }
            else
            {
                LogGlobalManager.LogMgr.PrintForQuit( "ConfigReader parses the xml config error!" );
            }
        }
    }
}
