using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;


namespace I_SIVB_ReflectMemoryConverter.src.Configuration_src
{
    /// <summary>
    /// class ConfigReader is to parse the configfile  
    /// </summary>
    /// <remarks>
    /// class ConfigReader is to parse the configfile
    /// </remarks>
    /// <exception>
    /// null
    /// </exception>
    /// <returns>
    /// null
    /// </returns>
    class ConfigReader
    {
        private SharedConfig mSharedConfig = null;

        private VaisConfig mVaisConfig;

        private RfmConfig mRfmConfig;

        private string mConfigPath;

        /// <summary>
        /// constructor  
        /// </summary>
        /// <remarks>
        /// constructor     
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// null
        /// </returns>
        public ConfigReader( string aConfigPath )
        {
            mConfigPath = aConfigPath;
        }

        /// <summary>
        /// get the mSharedConfig which was set by config_app  
        /// </summary>
        /// <remarks>
        /// get the mSharedConfig which was set by config_app     
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// SharedConfig
        /// </returns>
        public SharedConfig SharedConfig
        {
            get
            {
                return ( mSharedConfig );
            }
        }

        /// <summary>
        /// get the mVaisConfig which was set by config_app  
        /// </summary>
        /// <remarks>
        /// get the mVaisConfig which was set by config_app     
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// VaisConfig
        /// </returns>
        public VaisConfig VaisConfig
        {
            get
            {
                return ( mVaisConfig );
            }
        }

        /// <summary>
        /// get the mRfmConfig which was set by config_app  
        /// </summary>
        /// <remarks>
        /// get the mRfmConfig which was set by config_app     
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// RfmConfig
        /// </returns>
        public RfmConfig RfmConfig
        {
            get
            {
                return ( mRfmConfig );
            }
        }

        private static string mTempXmlName = "Temp.xml";

        private bool InitXmlFile( string aPath )
        {
            bool lResult = true;
            string lStr = null;
            StreamReader lsr = new StreamReader( aPath, Encoding.Default );

            int lIndex = 0;
            try
            {
                StreamWriter lFile = new StreamWriter( mTempXmlName );
                lFile.WriteLine( "<VaisConfig_temp>" );
                lFile.WriteLine( "<VaisIcd>" );
                while ( ( lStr = lsr.ReadLine() ) != null )
                {
                    lIndex++;
                    if ( lIndex > 2 )
                    {
                        lFile.WriteLine( lStr );
                    }
                }

                lFile.WriteLine( "</VaisConfig_temp>" );
                lsr.Close();
                lFile.Close();
                lFile.Dispose();
            }
            catch ( Exception e )
            {
                LogGlobalManager.LogMgr.PrintLine( "Can not generate the temp vais config xml!" );
                LogGlobalManager.LogMgr.PrintLine( e.Message );
                lResult = false;
            }
            return ( lResult );
        }

        /// <summary>
        /// parse the xml configfile  
        /// </summary>
        /// <remarks>
        /// parse the xml configfile, set the value of mSharedConfig,mVaisConfig and mRfmConfig.
        /// </remarks>
        /// <exception>
        /// null
        /// </exception>
        /// <returns>
        /// bool
        /// </returns>
        public bool ConfigApp()
        {
            bool lConfigResult = true;
            try
            {

                XmlDocument lAppconfig = new XmlDocument();
                lAppconfig.Load( mConfigPath );

                #region mVaisConfig initialization
                mVaisConfig = new VaisConfig();
                mVaisConfig.RefreshRate = lAppconfig.SelectSingleNode( @"I-SIVBconfig/RefreshRate" ).Attributes["value"].Value;

                ParticipantString lpartistr = new ParticipantString();
                XmlNode lParticipantNode = lAppconfig.SelectSingleNode( @"I-SIVBconfig/I-SIVBParticipant" );
                lpartistr.Name = lParticipantNode.Attributes["name"].Value;
                lpartistr.Description = lParticipantNode.Attributes["description"].Value;
                lpartistr.PartNumber = lParticipantNode.Attributes["partNumber"].Value;
                lpartistr.Version = lParticipantNode.Attributes["version"].Value;
                mVaisConfig.ParticipantConfig = lpartistr;
                #endregion mVaisConfig initialization End

                #region mRfmConfig initialization
                mRfmConfig = new RfmConfig();
                RfmDatablockAddresses lDatablocks = new RfmDatablockAddresses();
                lDatablocks.Value = lAppconfig.SelectSingleNode( @"I-SIVBconfig/SourceSelection" ).Attributes["firstSelection"].Value;


                lDatablocks.data1IronBirdAddress = new DataAddress();
                lDatablocks.data1FCSMiniRIGAddress = new DataAddress();

                XmlNode lData1IBSourceNode = null;
                XmlNode lData1FCSSourceNode = null;
                lData1IBSourceNode = lAppconfig.SelectSingleNode( @"I-SIVBconfig/SourceSelection/IronBirdData1" );
                lData1FCSSourceNode = lAppconfig.SelectSingleNode( @"I-SIVBconfig/SourceSelection/FCSMiniRigData1" );

                lDatablocks.data1IronBirdAddress.offset = lData1IBSourceNode.ChildNodes[0].Attributes["offset"].Value;
                lDatablocks.data1IronBirdAddress.end = lData1IBSourceNode.ChildNodes[0].Attributes["end"].Value;

                lDatablocks.data1FCSMiniRIGAddress.offset = lData1FCSSourceNode.ChildNodes[0].Attributes["offset"].Value;
                lDatablocks.data1FCSMiniRIGAddress.end = lData1FCSSourceNode.ChildNodes[0].Attributes["end"].Value;



                lDatablocks.data4Address = new DataAddress();
                XmlNode lData4Node = null;
                lData4Node = lAppconfig.SelectSingleNode( @"I-SIVBconfig/Data4Message" );
                lDatablocks.data4Address.offset = lData4Node.ChildNodes[0].Attributes["offset"].Value;
                lDatablocks.data4Address.end = lData4Node.ChildNodes[0].Attributes["end"].Value;



                mRfmConfig.DeviceID = lAppconfig.SelectSingleNode( @"I-SIVBconfig/RfmDevice" ).Attributes["id"].Value;
                mRfmConfig.ByteSwap = lAppconfig.SelectSingleNode( @"I-SIVBconfig/RfmDevice" ).Attributes["byteSwap"].Value;
                mRfmConfig.DataBlocks = lDatablocks;
                #endregion mRfmConfig initialization End

                #region mSharedConfig Initialization
                string lMessagePath = lAppconfig.SelectSingleNode( @"I-SIVBconfig/VaisConfigPath" ).Attributes["value"].Value;
                VaisMessageConfig[] lMessages = new VaisMessageConfig[2];
                lMessages[0] = new VaisMessageConfig();
                lMessages[1] = new VaisMessageConfig();
                mSharedConfig = new SharedConfig();

                lConfigResult = ConfigVaisMessages( lMessagePath, ref lMessages );
                mSharedConfig.MessageConfigs = lMessages;
                #endregion mSharedConfig Initialization End
            }
            catch ( Exception e )
            {
                LogGlobalManager.LogMgr.PrintLine( e.Message );
                lConfigResult = false;
            }
            return ( lConfigResult );
        }

        private bool ConfigVaisMessages( string aPath, ref VaisMessageConfig[] Messages )
        {
            bool lResult = true;
            if ( Messages != null )
            {
                XmlDocument lXML = new XmlDocument();
                bool lInitXml = false; ;
                lInitXml = InitXmlFile( aPath );
                if ( lInitXml == true )
                {
                    lXML.Load( mTempXmlName );
                    XmlNodeList lNodelist = lXML.SelectNodes( @"VaisConfig_temp/VaisIcd/NonProtocolMessage" );
                    if ( lNodelist.Count == 2 )
                    {
                        int lMessageIndex = 0;
                        foreach ( XmlNode lMessage in lNodelist )
                        {
                            Messages[lMessageIndex].name = lMessage.Attributes["MsgName"].Value;
                            foreach ( XmlNode lParam in lMessage )
                            {
                                VaisParamConfig lparamConfig = new VaisParamConfig();
                                try
                                {
                                    lparamConfig.name = lParam.Attributes["ParamName"].Value;
                                    lparamConfig.type = lParam.Attributes["DataFormatType"].Value;
                                }
                                catch ( Exception e )
                                {
                                    LogGlobalManager.LogMgr.PrintLine( e.Message );
                                    lparamConfig.type = "double";
                                    LogGlobalManager.LogMgr.PrintLine( "Not define the DataFormatType, use double for default value" );
                                }
                                finally
                                {
                                    Messages[lMessageIndex].parameters.Add( lparamConfig );
                                }
                            }
                            lMessageIndex++;

                        }
                    }
                    else
                    {
                        lResult = false;
                        LogGlobalManager.LogMgr.PrintLine( "VaisConfig_temp/VaisIcd/NonProtocolMessage : nodes number is not equal to 2 ! " );
                    }
                }
                else
                {
                    lResult = false;
                }

            }
            else
            {
                lResult = false;
            }
            return ( lResult );
        }

    }
}
