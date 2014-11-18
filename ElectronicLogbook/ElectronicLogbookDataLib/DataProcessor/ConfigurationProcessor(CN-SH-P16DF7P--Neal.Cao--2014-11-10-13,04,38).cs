using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using GEAviation.CommonSim;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbookDataLib.DataProcessor
{
    public class ConfigurationProcessor
    {
        private static string mELBConfig = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ElectronicLogbook_Config.txt";
        private static List<A664ACRMessagePeriodicInput> mAllEquipmentMsgList = new List<A664ACRMessagePeriodicInput>();
        static public UtilityParticipant Participant { get; set; }
        private ConfigurationProcessor mConfigProcessor { private set; private get; }

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
                                    lACRMessage.registerMessage(App.Participant);
                                    mAllEquipmentMsgList.Add(lACRMessage);
                                }
                            }
                        }

                        lContent = lOutfile.ReadLine();
                    }

                }
            }
        }

        private ConfigurationProcessor() {
                
        }

        public static ConfigurationProcessor getInstance() 
        {
            if (mConfigProcessor != null) 
            {
                return 
            }
        }
        public AirCraftEquipmentConfig GetConfiguration() 
        {
            
        }
        
    }
}
