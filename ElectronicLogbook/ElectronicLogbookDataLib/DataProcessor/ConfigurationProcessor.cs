using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Threading;
using GEAviation.CommonSim;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicLogbookDataLib.DataProcessor
{
    public class ConfigurationProcessor
    {
        private string mELBConfig = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ElectronicLogbook_Config.txt";
        private List<A664ACRMessagePeriodicInput> mAllEquipmentMsgList = new List<A664ACRMessagePeriodicInput>();
        private static ConfigurationProcessor mConfigProcessor;
        private ELBParticipant mELBParticipant;
        private List<string> m3rdPartySWCheckList = new List<string>();

        private Error LoadELBConfig() {
            if (!System.IO.File.Exists(mELBConfig))
            {
                return new Error(mELBConfig + " doesn't exist!");
            }
            else
            {
                using (StreamReader lOutfile = new StreamReader(mELBConfig))
                {
                    string lContent = lOutfile.ReadLine();
                    int lConfigType = 0;
                    while (lContent != null)
                    {
                        if (lContent.Length == 0)
                        {
                            lContent = lOutfile.ReadLine();
                            continue;
                        }

                        if (lContent.Equals("<LastSetting>"))
                        {
                            lConfigType = 1;
                        }
                        else if (lContent.Equals("<3rdPartySWCheckList>"))
                        {
                            lConfigType = 2;
                        }
                        else if (lContent.Equals("<EquipmentDefine>"))
                        {
                            lConfigType = 3;
                        }
                        else if (lContent.Equals("<BeyondComparePath>"))
                        {
                            lConfigType = 4;
                        }
                        else
                        {
                            if (lConfigType == 2)
                            {
                                m3rdPartySWCheckList.Add(lContent);
                            }
                            else if (lConfigType == 3)
                            {
                                string[] lSubStrings = lContent.Split(',');
                                //there are should 3 items in each line
                                if (lSubStrings.Length == 2)
                                {
                                    A664ACRMessagePeriodicInput lACRMessage = new A664ACRMessagePeriodicInput(lSubStrings[0], lSubStrings[1]);
                                    Error lResult = lACRMessage.registerMessage(mELBParticipant.mUtilityParticipant);
                                    if (lResult != Error.OK)
                                    {
                                        MessageBox.Show(lResult.mErrorInfo, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        mAllEquipmentMsgList.Add(lACRMessage);
                                    }
                                }
                            }
                        }
                        lContent = lOutfile.ReadLine();
                    }
                }
            }
            return Error.OK;
        }

        private ConfigurationProcessor() {
            mELBParticipant = ELBParticipant.getInstance();
            LoadELBConfig();
            ASN1_Decoder_ConfigReport.cASN1Format.BuildValueTree();
        }

        

        public static ConfigurationProcessor GetInstance() 
        {
            if (mConfigProcessor == null)
            {
                mConfigProcessor = new ConfigurationProcessor();
            }
            return mConfigProcessor; 
        }

        public List<AirCraftEquipmentConfig> GetAirCraftEquipmentConfigList() 
        {
            List<AirCraftEquipmentConfig> lAirCraftEquipmentConfigList = new List<AirCraftEquipmentConfig>();

            AirCraftEquipmentConfig lAirCraftEquipmentConfig = new AirCraftEquipmentConfig();
            int lTryTimes = 0;//try to get message from equipment, but just try 3 times.
            List<bool> lMessageReceived = new List<bool>();
            int lMessageBusCount = mAllEquipmentMsgList.Count;
            int i = 0;
            while (i++ < lMessageBusCount)
                lMessageReceived.Add(false);//initialize all to "false"

            while (lMessageBusCount != 0 && lTryTimes < 3)
            {
                i = -1;
                while (i < lMessageReceived.Count - 1)
                {
                    i++;
                    if (lMessageReceived[i])//this message was received in last loop
                        continue;

                    A664ACRMessagePeriodicInput lMessageBus = mAllEquipmentMsgList[i];
                    //receive messages from equipment, one message from each equipment is enough
                    if (lMessageBus.GetMessage())
                    {
                        //the content is in ASN.1 format
                        int lCount = lMessageBus.RawData.Count();
                        //let ASN.1 decoder process it first.
                        ASN1_Decoder_ConfigReport.cTopASN1Wrapper.process(lMessageBus.RawData, lCount);
                        List<string> lErrors = new List<string>();
                        if (ASN1_Decoder_ConfigReport.cTopASN1Wrapper.validate(lErrors) == false)//if there is any error
                        {
                            if (lErrors.Count > 0)
                            {
                                //don't block the main thread
                                lMessageBus.Participant.SendHealthMessage(GEAviation.CommonSim.CommonSimTypes.HealthLevel.Warning, lErrors[0]);
                                //MessageBox.Show(lErrors[0]);
                            }
                            MessageBox.Show("The Configuration Report from " + lMessageBus.mLRUName + " is wrong! \n",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {

                            //use the message to generate the equipment config report
                            lAirCraftEquipmentConfig.mConfigName = lMessageBus.mLRUName;
                            GetDecodedMessage(ref lAirCraftEquipmentConfig, ASN1_Decoder_ConfigReport.cTopASN1Wrapper, ASN1_Decoder_ConfigReport.cASN1Format, "");
                            lAirCraftEquipmentConfigList.Add(lAirCraftEquipmentConfig);
                        }

                        lMessageReceived[i] = true;
                        lMessageBusCount--;
                    }
                }

                lTryTimes++;

                if (lMessageBusCount != 0 && lTryTimes < 3)
                    Thread.Sleep(500);//sleep a while for others to send the messages out.
            }

            return lAirCraftEquipmentConfigList;
        }

        public List<VAISParticipant> GetVAISParticipantList()
        {
            List<VAISParticipant> lResult = new List<VAISParticipant>();
            foreach (GEAviation.CommonSim.Runtime lRuntime in mELBParticipant.mUtilityParticipant.Runtimes)
            {
                string lLocation = lRuntime.Name;
                foreach (GEAviation.CommonSim.RemoteParticipant participant in lRuntime.RemoteParticipants)
                {
                    lResult.Add(new VAISParticipant
                    {
                        mParticipantName = participant.Name,
                        mParticipantPartNumber = participant.ReadTag("PartNumber"),
                        mParticipantVersionNumber = participant.ReadTag("VersionInformation"),
                        mParticipantDescription = participant.ReadTag("Description"),
                        mParticipantLocation = lLocation
                    });
                }
            }
            return lResult;
        }

        public void GetDriverAnd3rdPartyConfig(out string aDriverConfig, out string a3rdPartySW)
        {
            List<bool> lMessageReceived = new List<bool>();
            List<GEAviation.CommonSim.Collection> lStationBusList = new List<GEAviation.CommonSim.Collection>();
            aDriverConfig = "";
            a3rdPartySW = "";
            foreach (GEAviation.CommonSim.Runtime lRuntime in mELBParticipant.mUtilityParticipant.Runtimes)//all stations
            {
                //the name should be same as the name used in "ElectronicLogbook_Slave"
                string lNPDMessageReceivedName = "NPD_ElectronicLogbookSlave_Message_" + lRuntime.Name;
                GEAviation.CommonSim.Collection lNPDMessageRecieved = mELBParticipant.mUtilityParticipant.CreateCollection(lNPDMessageReceivedName);
                if (lNPDMessageRecieved != null)
                {
                    lNPDMessageRecieved.CreateParameter("NPD_Parameter_Driver", GEAviation.CommonSim.CommonSimTypes.ValueType.CharArray);
                    lNPDMessageRecieved.CreateParameter("NPD_Parameter_3rdParty", GEAviation.CommonSim.CommonSimTypes.ValueType.CharArray);

                    lNPDMessageRecieved.Subscribe(GEAviation.CommonSim.CommonSimTypes.QueueType.Snapshot);
                    lMessageReceived.Add(false);
                    lStationBusList.Add(lNPDMessageRecieved);
                }

                string lNPDMessageName = lRuntime.Name + ".Startup";
                GEAviation.CommonSim.Collection lNPDMessage = mELBParticipant.mUtilityParticipant.GetCollection(lNPDMessageName);
                if (lNPDMessage != null)
                {
                    //send message to the station runtion to invoke "ElectronicLogbookSlave.exe"
                    lNPDMessage.Publish();
                    GEAviation.CommonSim.Parameter lAppName = lNPDMessage.GetParameter("ExecutablePath");
                    GEAviation.CommonSim.Parameter lArgument = lNPDMessage.GetParameter("ArgumentString");

                    lAppName.SetValue(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ElectronicLogbookSlave.exe");
                    lArgument.SetValue(lRuntime.Name);

                    lNPDMessage.Send();
                }
            }

            int lTryTimes = 0;//try to get message from each station, but just try 3 times.
            int lMessageBusCount = lMessageReceived.Count;
            while (lMessageBusCount != 0 && lTryTimes < 3)
            {
                Thread.Sleep(500);//sleep a while for others to send the messages out.

                int i = -1;
                while (i < lMessageReceived.Count - 1)
                {
                    i++;
                    if (lMessageReceived[i])//this message was received in last loop
                        continue;

                    GEAviation.CommonSim.Collection lNPDMessage = lStationBusList[i];
                    //receive messages from station
                    if (lNPDMessage.Receive())
                    {
                        aDriverConfig += lNPDMessage.GetParameter("NPD_Parameter_Driver").GetValueAsString();

                        string l3rdPartySWList = lNPDMessage.GetParameter("NPD_Parameter_3rdParty").GetValueAsString();
                        a3rdPartySW += Filter3rdPartySWFromConfigFile(l3rdPartySWList, lNPDMessage.Name.Replace("NPD_ElectronicLogbookSlave_Message_", ""));

                        lMessageReceived[i] = true;
                        lMessageBusCount--;
                    }
                }

                lTryTimes++;
            }

        }

        /// <summary>
        /// The "ElectronicLogbookSlave.exe" sends all the running process names back, but only the desired 
        /// 3rd party SW should be shown on GUI.
        /// </summary>
        private string Filter3rdPartySWFromConfigFile(string aProcessList, string aLocation)
        {
            string lResult = "";

            foreach (string lSWName in m3rdPartySWCheckList)
            {
                int lStartIndex = aProcessList.IndexOf(lSWName, StringComparison.OrdinalIgnoreCase);
                if (lStartIndex >= 0)
                {
                    string lSW = aProcessList.Substring(lStartIndex);
                    int lEndIndex = lSW.IndexOf(';');

                    lResult += lSW.Substring(0, lEndIndex);
                    lResult += ',';
                    lResult += aLocation;
                    lResult += '\n';
                }
            }

            return lResult;
        }

        private void GetDecodedMessage(ref AirCraftEquipmentConfig aAirCraftEquipmentConfig, CSharpWrapper_ASN1Decoder.ASN1_Wrapper aWrapNode, ASN1_Decoder_ConfigReport aGrammarNode, string aPrefix)
        {
            CSharpWrapper_ASN1Decoder.Decoder_Err_Result lErr = new CSharpWrapper_ASN1Decoder.Decoder_Err_Result();
            lErr.mValue = 1;

            switch (aGrammarNode.ValueType)
            {
                case ASN1_Decoder_ConfigReport.TagValueType.intType:
                    int lIValue = aWrapNode.getIntTag(aGrammarNode.Tag, lErr);
                    if (lErr.mValue == CSharpWrapper_ASN1Decoder.Decoder_Err_Result.Decode_Success)
                    {
                        if (aGrammarNode.DisplayName == "Equipment_ID") {
                            SubEquipment lSubEquipment = new SubEquipment();
                            lSubEquipment.mEquipmentID = "Equipment_ID: " + lIValue;
                            aAirCraftEquipmentConfig.mSubEquipmentList.Add(lSubEquipment);
                        }
                    }
                    break;
                case ASN1_Decoder_ConfigReport.TagValueType.stringType:
                    string lStrValue = aWrapNode.getStringTag(aGrammarNode.Tag, lErr);
                    if (lErr.mValue == CSharpWrapper_ASN1Decoder.Decoder_Err_Result.Decode_Success)
                    {
                        addPlainNodes(ref aAirCraftEquipmentConfig, aGrammarNode.DisplayName, lStrValue);
                    }

                    break;
                case ASN1_Decoder_ConfigReport.TagValueType.nestedType:
                    CSharpWrapper_ASN1Decoder.ASN1_Wrapper lASN1Node = aWrapNode.getNestedTag(aGrammarNode.Tag, lErr);

                    if (lErr.mValue == CSharpWrapper_ASN1Decoder.Decoder_Err_Result.Decode_Success)
                    {
                        bool bSkip = false;
                        if (aGrammarNode.DisplayName.Equals("Config Report (All)") || aGrammarNode.DisplayName.Equals("Config Report (This LRU)"))
                        {
                            bSkip = true;
                        }
                        if (!bSkip)
                        {
                            addNestedNodes(ref aAirCraftEquipmentConfig, aGrammarNode.DisplayName);
                        }

                        int[] lTagList = lASN1Node.getParsedTagList();//get all the tags shown in the message
                        int lCount = lTagList.Length;
                        int i = 0;
                        while (i < lCount)//this value may be constructed by multiple "TLV"
                        {
                            int lTag = lTagList[i];
                            Object temp = aGrammarNode.NestedASN1[lTag];
                            if (temp != null)
                            {
                                ASN1_Decoder_ConfigReport lGrammarNoder = temp as ASN1_Decoder_ConfigReport;
                                GetDecodedMessage(ref aAirCraftEquipmentConfig,lASN1Node, lGrammarNoder, aPrefix);
                            }
                            i++;
                        }
                    }
                    break;
            }
        }

        private void addPlainNodes(ref AirCraftEquipmentConfig aAirCraftEquipmentConfig, string aPlainNodeName, string aStrValue)
        {
            int lCount = aAirCraftEquipmentConfig.mSubEquipmentList.Count;
            int lHWPartListCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList.Count;
            int lSWConfigListCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount-1].mSWConfigList.Count;
            int lSWPartListCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigListCount - 1].mSWPartList.Count;
            int lConfigInfoListCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mConfigInfoList.Count;
            switch (aPlainNodeName)
            {
                case "HW_Part_Number":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList[lHWPartListCount - 1].mHWPartNumber = aStrValue;
                    break;
                case "HW_Part_Description":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList[lHWPartListCount - 1].mHWPartDescription = aStrValue;
                    break;
                case "HW_Part_Status":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList[lHWPartListCount - 1].mHWPartStatus = aStrValue;
                    break;
                case "HW_Part_Serial_Number":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList[lHWPartListCount - 1].mHWPartSerialNumber = aStrValue;
                    break;
                case "SW_Location_ID":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigListCount - 1].mSWLocationID = aStrValue;
                    break;
                case "SW_Location_Description":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigListCount - 1].mSWLocationDescription = aStrValue;
                    break;
                case "LSAP_Part_Number":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigListCount - 1].
                        mSWPartList[lSWConfigListCount-1].mLSAPPartNumber = aStrValue;
                    break;
                case "LSAP_Description":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigListCount - 1].
                        mSWPartList[lSWConfigListCount - 1].mLSAPDescription = aStrValue;
                    break;
                case "Config_Info":
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mConfigInfoList[lConfigInfoListCount - 1].mItemInfo = aStrValue;
                    break;
            }
        }

        private void addNestedNodes(ref AirCraftEquipmentConfig aAirCraftEquipmentConfig, String aTagName)
        {
            int lCount = aAirCraftEquipmentConfig.mSubEquipmentList.Count;
            switch (aTagName)
            {
                case "HW_Part":
                    HWPart lHWPart = new HWPart();
                    lHWPart.mHWPartIndex = "HW_Part_" + aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mHWPartList.Count;
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mHWPartList.Add(lHWPart);
                    break;
                case "SW_Config":
                    SWConfig lSWConfig = new SWConfig();
                    lSWConfig.mSWConfigIndex = "SW_Config_" + aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mSWConfigList.Count;
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList.Add(lSWConfig);
                    break;
                case "SW_Part":
                    SWPart lSWPart = new SWPart();
                    int lSWConfigCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mSWConfigList.Count;
                    lSWPart.mSWPartIndex = "SW_Part_" + aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mSWConfigList[lSWConfigCount].mSWPartList.Count;
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount - 1].mSWConfigList[lSWConfigCount - 1].mSWPartList.Add(lSWPart);
                    break;
                case "Config_Info":
                    ConfigInfo lConfigInfo = new ConfigInfo();
                    int lConfigInfoCount = aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mConfigInfoList.Count;
                    lConfigInfo.mItemIndex = "Config_Info_" + lConfigInfoCount;
                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mConfigInfoList.Add(lConfigInfo);
                    break;
            }
        }
        
    }
}
