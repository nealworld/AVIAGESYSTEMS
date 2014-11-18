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
                                //m3rdPartySWCheckList.Add(lContent);
                            }
                            else if (lConfigType == 3)
                            {
                                string[] lSubStrings = lContent.Split(',');
                                //there are should 3 items in each line
                                if (lSubStrings.Length == 2)
                                {
                                    A664ACRMessagePeriodicInput lACRMessage = new A664ACRMessagePeriodicInput(lSubStrings[0], lSubStrings[1]);
                                    Error lResult = lACRMessage.registerMessage(mParticipant);
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

            ELBParticipant.CreateParticipantAndRun();

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
                            lSubEquipment.mEquipmentID = lIValue.ToString();
                            aAirCraftEquipmentConfig.mSubEquipmentList.Add(lSubEquipment);
                        }
                    }

                    break;
                case ASN1_Decoder_ConfigReport.TagValueType.stringType:
                    string lStrValue = aWrapNode.getStringTag(aGrammarNode.Tag, lErr);
                    if (lErr.mValue == CSharpWrapper_ASN1Decoder.Decoder_Err_Result.Decode_Success)
                    {
                        return aPrefix + aGrammarNode.DisplayName + ": " + lStrValue + "\n";
                    }

                    break;
                case ASN1_Decoder_ConfigReport.TagValueType.nestedType:
                    CSharpWrapper_ASN1Decoder.ASN1_Wrapper lASN1Node = aWrapNode.getNestedTag(aGrammarNode.Tag, lErr);

                    if (lErr.mValue == CSharpWrapper_ASN1Decoder.Decoder_Err_Result.Decode_Success)
                    {
                        bool bSkip = false;
                        if (aGrammarNode.DisplayName.Equals("Config Report (All)") || aGrammarNode.DisplayName.Equals("Config Report (This LRU)"))
                        {
                            //skip these 2 levels
                            bSkip = true;
                        }

                        string lResult = "";
                        if (!bSkip)
                        {
                            int lCount = aAirCraftEquipmentConfig.mSubEquipmentList.Count;
                            switch (aGrammarNode.DisplayName)
                            {
                                case "HW_Part":                                   
                                    HWPart lHWPart = new HWPart();
                                    lHWPart.mHWPartIndex = "HW_Part_" + aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mHWPart.Count;
                                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount-1].mHWPart.Add(lHWPart);
                                    break;
                                case "SW_Config":
                                    SWConfig lSWConfig = new SWConfig();
                                    lSWConfig.mSWConfigIndex = "SW_Config_" + aAirCraftEquipmentConfig.mSubEquipmentList[lCount].mSWConfig.Count;
                                    aAirCraftEquipmentConfig.mSubEquipmentList[lCount-1].mSWConfig.Add(lSWConfig);
                                    break;
                                case "SW_Part":

                            }
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
                                lResult += GetDecodedMessage(lASN1Node, lGrammarNoder, aPrefix);
                            }
                            i++;
                        }
                    }
                    break;
            }
        }
        
    }
}
