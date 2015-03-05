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