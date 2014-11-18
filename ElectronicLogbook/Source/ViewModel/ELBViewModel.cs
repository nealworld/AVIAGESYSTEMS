using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ElectronicLogbookDataLib;
using ElectronicLogbookDataLib.DataProcessor;
namespace ElectronicLogbook.ViewModel
{
    public class ELBViewModel : INotifyPropertyChanged
    {
        private static ELBViewModel mSingleton;
        public static ELBViewModel getInstance()
        {
            if (mSingleton == null) 
            {
                mSingleton = new ELBViewModel();
            }
            return mSingleton;
        } 

        private AirCraftEquipmentConfigViewModel _AirCraftEquipmentConfig;
        public AirCraftEquipmentConfigViewModel mAirCraftEquipmentConfigViewModel 
        {
            private set
            {
                _AirCraftEquipmentConfig = value;
                this.OnPropertyChanged("mAirCraftEquipmentConfigViewModel");
            }
            get 
            {
                return _AirCraftEquipmentConfig;
            }
        }

        private List<VAISParticipant> _VAISParticipantList;
        public List<VAISParticipant> mVAISParticipantListViewModel 
        {
            private set
            {
                _VAISParticipantList = value;
                this.OnPropertyChanged("mVAISParticipantListViewModel");
            }
            get
            {
                return _VAISParticipantList;
            }
        }

        private List<DeviceDriver> _DeviceDriverList;
        public List<DeviceDriver> mDeviceDriverListViewModel 
        {
            private set
            {
                _DeviceDriverList = value;
                this.OnPropertyChanged("mDeviceDriverListViewModel");
            }
            get
            {
                return _DeviceDriverList;
            }
        }

        private List<ThirdPartySoftware> _ThirdPartySoftwareList;
        public List<ThirdPartySoftware> mThirdPartySoftwareListViewModel 
        {
            private set
            {
                _ThirdPartySoftwareList = value;
                this.OnPropertyChanged("mThirdPartySoftwareListViewModel");
            }
            get
            {
                return _ThirdPartySoftwareList;
            }
        }


        public ELBViewModel() 
        {
            ConfigurationProcessor lConfigurationProcessor = ConfigurationProcessor.GetInstance();
            mAirCraftEquipmentConfigViewModel = new AirCraftEquipmentConfigViewModel(
                lConfigurationProcessor.GetAirCraftEquipmentConfigList());
            mVAISParticipantListViewModel = lConfigurationProcessor.GetVAISParticipantList();
            getDeviceDriverAndThirdPartySoftware(lConfigurationProcessor);
        }

        private void getDeviceDriverAndThirdPartySoftware(ConfigurationProcessor aConfigurationProcessor)
        {
            string lDriverConfig = "";
            string l3rdPartySW = "";
            aConfigurationProcessor.GetDriverAnd3rdPartyConfig(out lDriverConfig, out l3rdPartySW);
            ConvertStrToDriver(lDriverConfig);
            ConverStrToThirdPartySoftware(l3rdPartySW);
        }

        private void ConverStrToThirdPartySoftware(string a3rdPartySW)
        {
            List<ThirdPartySoftware> lThirdPartySoftwareList = new List<ThirdPartySoftware>();
            String[] lThirdPartySoftwares = a3rdPartySW.Split('\n');
            foreach (String lStr in lThirdPartySoftwares)
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
            mThirdPartySoftwareListViewModel = lThirdPartySoftwareList;
        }

        private void ConvertStrToDriver(string aDriverConfig)
        {
            List<DeviceDriver> lDeviceDriverList = new List<DeviceDriver>();
            String[] lDeviceDrivers = aDriverConfig.Split('\n');
            foreach (String lStr in lDeviceDrivers)
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
            mDeviceDriverListViewModel = lDeviceDriverList;
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}