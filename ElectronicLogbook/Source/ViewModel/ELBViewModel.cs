using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ElectronicLogbookDataLib;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.IO;
namespace ElectronicLogbook.ViewModel
{
    public class ELBViewModel : ViewModel
    {
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly ELBViewModel instance = new ELBViewModel();
        }

        public static ELBViewModel mSingleton { get { return Nested.instance; } }
        
        private ConfigurationViewModel _ConfigurationViewModel;
        public ConfigurationViewModel mConfigurationViewModel 
        {
            get 
            {
                return _ConfigurationViewModel;
            }
            set 
            {
                _ConfigurationViewModel = value;
                this.OnPropertyChanged("mConfigurationViewModel");
            }
        }

        private String _CurrentFile;
        public String mCurrentFile {
            get
            {
                return _CurrentFile;
            }
            set
            {
                _CurrentFile = value;
                this.OnPropertyChanged("mCurrentFile");
            }
        }

        private String _ComparingFile;
        public String mComparingFile {
            get
            {
                return _ComparingFile;
            }
            set
            {
                _ComparingFile = value;
                this.OnPropertyChanged("mComparingFile");
            }
        }

        public DailyRemakrHandler mDailyRemarkHandler{get;set;}

        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }

        private ELBViewModel() 
        {
            mCurrentFile = String.Empty;
            mComparingFile = String.Empty;
            mConfigurationViewModel = new ConfigurationViewModel();
            mDailyRemarkHandler = new DailyRemakrHandler();
        }

        public void GetCurrentConfigration_Click(object sender, RoutedEventArgs e)
        {
            CollectConfigurationFromSystem();
            mCurrentFile = String.Empty;
            mComparingFile = String.Empty;
        }

        public void NewConfiguration_Click(object sender, RoutedEventArgs e)
        {
            mConfigurationViewModel.initialize();
            mConfigurationViewModel.mIsReadOnly = false;
            mConfigurationViewModel.mIsEditable = true;
            mCurrentFile = String.Empty;
            mComparingFile = String.Empty;
        }

        public void OpenConfiguration_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog lDlg = new Microsoft.Win32.OpenFileDialog();
            lDlg.FileName = "Configuration"; 
            lDlg.DefaultExt = ".cnf";
            lDlg.Filter = "Configuration files(*.cnf)|*.cnf|XML Files(*.xml)|*.xml";
            Nullable<bool> lResult = lDlg.ShowDialog();
            if (lResult == true)
            {
                String lFileName = lDlg.FileName;
                if (lFileName.Contains(".xml")) {
                    CollectConfigurationFromXMLFile(lFileName);
                }
                else if (lFileName.Contains(".cnf")) {
                    CollectConfigurationFromFile(lFileName);
                }
                mConfigurationViewModel.mIsEditable = true;
                mConfigurationViewModel.mIsReadOnly = false;
                mCurrentFile = lFileName;
            }
        }

        public void SaveConfiguration_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog lDlg = new Microsoft.Win32.SaveFileDialog();
            lDlg.FileName = "Configuration";
            lDlg.DefaultExt = ".cnf";
            lDlg.Filter = "Configuration files(*.cnf)|*.cnf|XML Files(*.xml)|*.xml";
            Nullable<bool> lResult = lDlg.ShowDialog();
            if (lResult == true)
            {
                String lFileName = lDlg.FileName;
                if (lFileName.Contains(".cnf"))
                {
                    if (!Utility.Serialize(mConfigurationViewModel, lFileName))
                    {
                        String messageBoxText = "Save failed!";
                        String caption = "ELB";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                }
                else if (lFileName.Contains(".xml")) {
                    if (!Utility.SerializeToXML(mConfigurationViewModel, lFileName))
                    {
                        String messageBoxText = "Save failed!";
                        String caption = "ELB";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                }
            }
        }

        public void CompareConfiguration_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog lDlg = new Microsoft.Win32.OpenFileDialog();
            lDlg.FileName = "Configuration";
            lDlg.DefaultExt = ".cnf";
            lDlg.Filter = "Configuration files(*.cnf)|*.cnf|XML Files(*.xml)|*.xml";
            Nullable<bool> lResult = lDlg.ShowDialog();
            if (lResult == true)
            {
                String lFileName = lDlg.FileName;
                ConfigurationViewModel lConfigurationViewModel = new ConfigurationViewModel();

                if (lFileName.Contains(".cnf"))
                {
                    if (!Utility.DeSerialize(ref lConfigurationViewModel, lFileName))
                    {
                        String messageBoxText = "Open failed!";
                        String caption = "ELB";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show(messageBoxText, caption, button, icon);

                        return;
                    }
                }
                else if(lFileName.Contains(".xml")) {
                    if (!Utility.DeSerializeFromXML(ref lConfigurationViewModel, lFileName))
                    {
                        String messageBoxText = "Open failed!";
                        String caption = "ELB";
                        MessageBoxButton button = MessageBoxButton.OKCancel;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show(messageBoxText, caption, button, icon);

                        return;
                    }

                    foreach (AirCraftEquipmentConfigViewModel lelement in lConfigurationViewModel.mAirCraftEquipmentConfigViewModelList)
                    {
                        foreach (SubEquipmentViewModel lsubelement in lelement.mChildren)
                        {
                            lsubelement.mParent = lelement;
                        }
                    }
                }

                performCompare(mConfigurationViewModel, lConfigurationViewModel);
                mConfigurationViewModel.mIsEditable = false;
                mConfigurationViewModel.mIsReadOnly = true;
                mComparingFile = lFileName;
      
            }
        }

        private void performCompare(ConfigurationViewModel aSourceConfiguration, ConfigurationViewModel aTargetConfiguration)
        {
            aSourceConfiguration.Compare(aTargetConfiguration);
        }

        private void CollectConfigurationFromSystem()
        {
            mConfigurationViewModel.initialize();
            mConfigurationViewModel.mIsEditable = false;
            mConfigurationViewModel.mIsReadOnly = true;

            ConfigurationProcessor lConfigurationProcessor = ConfigurationProcessor.mSingleton;
            foreach (AirCraftEquipmentConfig lAirCraftEquipmentConfig in
                lConfigurationProcessor.GetAirCraftEquipmentConfigList()/* GetList()*/)
            {
                mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList.Add
                    (new AirCraftEquipmentConfigViewModel(lAirCraftEquipmentConfig));
                
            }

            foreach (VAISParticipant lElement in lConfigurationProcessor.GetVAISParticipantList()) 
            {
                mConfigurationViewModel.mVAISParticipantListViewModel.Add(new VAISParticipantViewModel(lElement));
            }

            
            string lDriverConfig = String.Empty;
            string l3rdPartySW = String.Empty;
            lConfigurationProcessor.GetDriverAnd3rdPartyConfig(out lDriverConfig, out l3rdPartySW);

            foreach (DeviceDriver lElement in ConvertStrToDriver(lDriverConfig)) 
            {
                mConfigurationViewModel.mDeviceDriverListViewModel.Add(new DeviceDriverViewModel(lElement));
            }

            foreach (ThirdPartySoftware lElement in ConverStrToThirdPartySoftware(l3rdPartySW)) 
            {
                mConfigurationViewModel.mThirdPartySoftwareListViewModel.Add(new ThirdPartySoftwareViewModel(lElement));
            }
        }

        private void CollectConfigurationFromXMLFile(string aFileName)
        {
            ConfigurationViewModel lConfigurationViewModel = new ConfigurationViewModel();
            if (Utility.DeSerializeFromXML(ref lConfigurationViewModel, aFileName))
            {
                mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList = lConfigurationViewModel.mAirCraftEquipmentConfigViewModelList;
                mConfigurationViewModel.mDeviceDriverListViewModel = lConfigurationViewModel.mDeviceDriverListViewModel;
                mConfigurationViewModel.mThirdPartySoftwareListViewModel = lConfigurationViewModel.mThirdPartySoftwareListViewModel;
                mConfigurationViewModel.mVAISParticipantListViewModel = lConfigurationViewModel.mVAISParticipantListViewModel;

                foreach (AirCraftEquipmentConfigViewModel lelement in mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList) {
                    foreach (SubEquipmentViewModel lsubelement in lelement.mChildren) {
                        lsubelement.mParent = lelement;
                    }
                }
            }
            else
            {
                String messageBoxText = "Open failed!";
                String caption = "ELB";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private void CollectConfigurationFromFile(String aFileName) 
        {
            ConfigurationViewModel lConfigurationViewModel = new ConfigurationViewModel();
            if(Utility.DeSerialize(ref lConfigurationViewModel, aFileName))
            {
                mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList = lConfigurationViewModel.mAirCraftEquipmentConfigViewModelList;
                mConfigurationViewModel.mDeviceDriverListViewModel = lConfigurationViewModel.mDeviceDriverListViewModel;
                mConfigurationViewModel.mThirdPartySoftwareListViewModel = lConfigurationViewModel.mThirdPartySoftwareListViewModel;
                mConfigurationViewModel.mVAISParticipantListViewModel = lConfigurationViewModel.mVAISParticipantListViewModel;
            }
            else
            {
                String messageBoxText = "Open failed!";
                String caption = "ELB";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        private List<AirCraftEquipmentConfig> GetList()
        {
            List<AirCraftEquipmentConfig> myList = new List<AirCraftEquipmentConfig>();

            AirCraftEquipmentConfig lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM1";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            SubEquipment lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 1";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWsssnfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWc2222ig2",
                    mSWLocationID = "2qq3",
                    mSWLocationDescription = "d1dd1",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWweig4",
                    mSWLocationID = "24sd4",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ss",
                    mItemInfo = "ss2222"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s1s",
                   mItemInfo = "ss224sssghfgde22"
               });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 2";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_11", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_21", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWcw22nfig1",
                    mSWLocationID = "2sd",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SW434v3Part1", "22", "3v353"), new SWPart("SWPar3v45t2", "24 f fr32", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWcss3bbbbgggonfig2",
                    mSWLocationID = "2dee444444444443",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "S==++onfig4",
                    mSWLocationID = "2{{j,44",
                    mSWLocationDescription = "d4267567;'p[1cdd",
                    mSWPartList = { new SWPart("SWPax34rt1", "2123x12", "3t3"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssxs3v423d33",
                    mItemInfo = "ss222gv234c2gfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s1s",
                   mItemInfo = "sl0000000ghfgde22"
               });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssxsd33",
                    mItemInfo = "ss222gvgfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s12",
                   mItemInfo = "slllde22"
               });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssx2dd33",
                    mItemInfo = "ss2aafgfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "seq2er1s",
                   mItemInfo = "sl00aaqde22"
               });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 3";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            /*lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 4";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_114s1", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_214s1", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            myList.Add(lAirCraftEquipmentConfig1);

            lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM2";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 1";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1222", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2222", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 2";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1333", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2333", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 3";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1444", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2444", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);*/
            myList.Add(lAirCraftEquipmentConfig1);

            return myList;
        }

        private List<VAISParticipant> GetVAISParticipantList() 
        {
            List<VAISParticipant> ss = new List<VAISParticipant>();
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfsh",
                    mParticipantLocation = "r32d43",
                    mParticipantName = "rt4t4ffft",
                    mParticipantPartNumber = "2e2",
                    mParticipantVersionNumber = "d24#d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "$$$$$$$$%",
                    mParticipantLocation = "RRRRRRRRRRR$$$$$$$$$$",
                    mParticipantName = "erwerwegfreg",
                    mParticipantPartNumber = "555555",
                    mParticipantVersionNumber = "4323222222222222222"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            return ss;
        }

        private ObservableCollection<ThirdPartySoftware> ConverStrToThirdPartySoftware(string a3rdPartySW)
        {
            ObservableCollection<ThirdPartySoftware> lThirdPartySoftwareList = new ObservableCollection<ThirdPartySoftware>();
            String[] lThirdPartySoftwares = a3rdPartySW.Split('\n');
            foreach (String lStr in lThirdPartySoftwares)
            {
                if (lStr != string.Empty)
                {
                    String[] lThirdPartySoftwareRecord = lStr.Split(',');
                    lThirdPartySoftwareList.Add(
                        new ThirdPartySoftware
                        {
                            mSoftwareName = lThirdPartySoftwareRecord[0],
                            mSoftwareVersionNumber = lThirdPartySoftwareRecord[1],
                            mSoftwareLocation = lThirdPartySoftwareRecord[2]
                        });
                }
            }
            return lThirdPartySoftwareList;
        }

        private ObservableCollection<DeviceDriver> ConvertStrToDriver(string aDriverConfig)
        {
            ObservableCollection<DeviceDriver> lDeviceDriverList = new ObservableCollection<DeviceDriver>();
            String[] lDeviceDrivers = aDriverConfig.Split('\n');
            foreach (String lStr in lDeviceDrivers)
            {
                if (lStr != String.Empty)
                {
                    String[] lOneDeviceDriverRecord = lStr.Split(',');
                    lDeviceDriverList.Add(
                        new DeviceDriver
                        {
                            mDeviceName = lOneDeviceDriverRecord[0],
                            mDriverName = lOneDeviceDriverRecord[1],
                            mDriverVersionNumber = lOneDeviceDriverRecord[2],
                            mDriverLocation = lOneDeviceDriverRecord[3]
                        });
                }
            }
            return lDeviceDriverList;
        }
    }
}