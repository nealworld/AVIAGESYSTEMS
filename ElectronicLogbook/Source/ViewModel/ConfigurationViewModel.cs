using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;
using System;
using System.Windows;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    [XmlRoot("SystemConfiguration")]
    public class ConfigurationViewModel : ViewModel, ISerializable
    {
        private bool _IsEditable;

        [XmlIgnore()]
        public bool mIsEditable
        {
            set
            {
                _IsEditable = value;
                this.OnPropertyChanged("mIsEditable");
            }
            get
            {
                return _IsEditable;
            }
        }

        private bool _isReadOnly;

        [XmlIgnore()]
        public bool mIsReadOnly
        {
            set
            {
                _isReadOnly = value;
                this.OnPropertyChanged("mIsReadOnly");
            }
            get
            {
                return _isReadOnly;
            }
        }

        private ObservableCollection<AirCraftEquipmentConfigViewModel> _AirCraftEquipmentConfigList;

        [XmlArrayItem("AirCraftEquipmentConfig", typeof(AirCraftEquipmentConfigViewModel))]
        [XmlArray("AirCraftEquipmentConfigList")]
        public ObservableCollection<AirCraftEquipmentConfigViewModel> mAirCraftEquipmentConfigViewModelList
        {
            set
            {
                _AirCraftEquipmentConfigList = value;
                this.OnPropertyChanged("mAirCraftEquipmentConfigViewModelList");
            }
            get
            {
                return _AirCraftEquipmentConfigList;
            }
        }

        private ObservableCollection<VAISParticipantViewModel> _VAISParticipantList;

        [XmlArrayItem("VAISParticipant", typeof(VAISParticipantViewModel))]
        [XmlArray("VAISParticipantList")]
        public ObservableCollection<VAISParticipantViewModel> mVAISParticipantListViewModel
        {
            set
            {
                _VAISParticipantList = value;
                this.OnPropertyChanged("mVAISParticipantListViewModel");
            }
            get
            {
                return _VAISParticipantList;
            }
        }

        private ObservableCollection<DeviceDriverViewModel> _DeviceDriverList;

        [XmlArrayItem("DeviceDriver", typeof(DeviceDriverViewModel))]
        [XmlArray("DeviceDriverList")]
        public ObservableCollection<DeviceDriverViewModel> mDeviceDriverListViewModel
        {
            set
            {
                _DeviceDriverList = value;
                this.OnPropertyChanged("mDeviceDriverListViewModel");
            }
            get
            {
                return _DeviceDriverList;
            }
        }

        private ObservableCollection<ThirdPartySoftwareViewModel> _ThirdPartySoftwareList;

        [XmlArrayItem("ThirdPartySoftware", typeof(ThirdPartySoftwareViewModel))]
        [XmlArray("ThirdPartySoftwareList")]
        public ObservableCollection<ThirdPartySoftwareViewModel> mThirdPartySoftwareListViewModel
        {
            set
            {
                _ThirdPartySoftwareList = value;
                this.OnPropertyChanged("mThirdPartySoftwareListViewModel");
            }
            get
            {
                return _ThirdPartySoftwareList;
            }
        }

        private Visibility _VAISParticipantCompareResultColumnVisibility;

        [XmlIgnore()]
        public Visibility mVAISParticipantCompareResultColumnVisibility
        {
            set
            {
                _VAISParticipantCompareResultColumnVisibility = value;
                this.OnPropertyChanged("mVAISParticipantCompareResultColumnVisibility");
            }
            get
            {
                return _VAISParticipantCompareResultColumnVisibility;
            }
        }

        private Visibility _DeviceDriverCompareResultColumnVisibility;

        [XmlIgnore()]
        public Visibility mDeviceDriverCompareResultColumnVisibility
        {
            set
            {
                _DeviceDriverCompareResultColumnVisibility = value;
                this.OnPropertyChanged("mDeviceDriverCompareResultColumnVisibility");
            }
            get
            {
                return _DeviceDriverCompareResultColumnVisibility;
            }
        }

        private Visibility _ThirdPartySoftwareCompareResultColumnVisibility;

        [XmlIgnore()]
        public Visibility mThirdPartySoftwareCompareResultColumnVisibility
        {
            set
            {
                _ThirdPartySoftwareCompareResultColumnVisibility = value;
                this.OnPropertyChanged("mThirdPartySoftwareCompareResultColumnVisibility");
            }
            get
            {
                return _ThirdPartySoftwareCompareResultColumnVisibility;
            }
        }

        public ConfigurationViewModel() 
        {
            initialize();
        }

        public void initialize() 
        {
            mIsEditable = false;
            mIsReadOnly = true;
            mVAISParticipantCompareResultColumnVisibility = Visibility.Hidden;
            mDeviceDriverCompareResultColumnVisibility = Visibility.Hidden;
            mThirdPartySoftwareCompareResultColumnVisibility = Visibility.Hidden;
            mAirCraftEquipmentConfigViewModelList = new ObservableCollection<AirCraftEquipmentConfigViewModel>();
            mVAISParticipantListViewModel = new ObservableCollection<VAISParticipantViewModel>();
            mDeviceDriverListViewModel = new ObservableCollection<DeviceDriverViewModel>();
            mThirdPartySoftwareListViewModel = new ObservableCollection<ThirdPartySoftwareViewModel>();
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mVAISParticipantCompareResultColumnVisibility", mVAISParticipantCompareResultColumnVisibility);
            info.AddValue("mDeviceDriverCompareResultColumnVisibility", mDeviceDriverCompareResultColumnVisibility);
            info.AddValue("mThirdPartySoftwareCompareResultColumnVisibility", mThirdPartySoftwareCompareResultColumnVisibility);
            info.AddValue("mAirCraftEquipmentConfigViewModelList",mAirCraftEquipmentConfigViewModelList);
            info.AddValue("mVAISParticipantListViewModel",mVAISParticipantListViewModel);
            info.AddValue("mDeviceDriverListViewModel",mDeviceDriverListViewModel);
            info.AddValue("mThirdPartySoftwareListViewModel",mThirdPartySoftwareListViewModel);
        }

        public ConfigurationViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mVAISParticipantCompareResultColumnVisibility = (Visibility)info.GetValue("mVAISParticipantCompareResultColumnVisibility", typeof(Visibility));
            mDeviceDriverCompareResultColumnVisibility = (Visibility)info.GetValue("mDeviceDriverCompareResultColumnVisibility", typeof(Visibility));
            mThirdPartySoftwareCompareResultColumnVisibility = (Visibility)info.GetValue("mThirdPartySoftwareCompareResultColumnVisibility", typeof(Visibility));
            mAirCraftEquipmentConfigViewModelList = (ObservableCollection<AirCraftEquipmentConfigViewModel>)
                info.GetValue("mAirCraftEquipmentConfigViewModelList", typeof(ObservableCollection<AirCraftEquipmentConfigViewModel>));
            mVAISParticipantListViewModel = (ObservableCollection<VAISParticipantViewModel>)
                info.GetValue("mVAISParticipantListViewModel", typeof(ObservableCollection<VAISParticipantViewModel>));
            mDeviceDriverListViewModel = (ObservableCollection<DeviceDriverViewModel>)
                info.GetValue("mDeviceDriverListViewModel", typeof(ObservableCollection<DeviceDriverViewModel>));
            mThirdPartySoftwareListViewModel = (ObservableCollection<ThirdPartySoftwareViewModel>)
                info.GetValue("mThirdPartySoftwareListViewModel", typeof(ObservableCollection<ThirdPartySoftwareViewModel>));

        }

        public override Boolean Compare(ViewModel aTarget)
        {
            Boolean lIsAirCraftEquipmentConfigChanged = false;
            Boolean lIsVAISParticipantChanged = false;
            Boolean lIsThirdPartySoftwareChanged = false;
            Boolean lIsDeviceDriverChanged = false;

            lIsAirCraftEquipmentConfigChanged = Compare(this.mAirCraftEquipmentConfigViewModelList, (aTarget as ConfigurationViewModel).mAirCraftEquipmentConfigViewModelList);
            lIsVAISParticipantChanged = Compare(this.mVAISParticipantListViewModel, (aTarget as ConfigurationViewModel).mVAISParticipantListViewModel);
            lIsThirdPartySoftwareChanged = Compare(this.mThirdPartySoftwareListViewModel, (aTarget as ConfigurationViewModel).mThirdPartySoftwareListViewModel);
            lIsDeviceDriverChanged = Compare(this.mDeviceDriverListViewModel, (aTarget as ConfigurationViewModel).mDeviceDriverListViewModel);

            if (lIsVAISParticipantChanged) 
            {
                mVAISParticipantCompareResultColumnVisibility = Visibility.Visible;
            }

            if (lIsThirdPartySoftwareChanged) 
            {
                mThirdPartySoftwareCompareResultColumnVisibility = Visibility.Visible;
            }

            if (lIsDeviceDriverChanged) 
            {
                mDeviceDriverCompareResultColumnVisibility = Visibility.Visible;
            }

            return lIsAirCraftEquipmentConfigChanged | lIsVAISParticipantChanged | lIsThirdPartySoftwareChanged | lIsDeviceDriverChanged;
        }

        private Boolean Compare<T>(ObservableCollection<T> aSourceList, ObservableCollection<T> aTargetList) 
        {
            Boolean lIsChanged = false;

            foreach (T lSourceElement in aSourceList)
            {
                if (aTargetList.Contains(lSourceElement))
                {
                    lIsChanged |= (lSourceElement as ViewModel).Compare(aTargetList[aTargetList.IndexOf(lSourceElement)] as ViewModel);
                }
                else
                {
                    (lSourceElement as ViewModel).mCompareResult = Utility.Missing;
                    lIsChanged = true;
                }
            }

            foreach (T lTargetElement in aTargetList)
            {
                if (!aSourceList.Contains(lTargetElement))
                {
                    (lTargetElement as ViewModel).mCompareResult = Utility.New;
                    aSourceList.Add(lTargetElement);
                    lIsChanged = true;
                }
            }

            if (aSourceList is ObservableCollection<AirCraftEquipmentConfigViewModel>) 
            {
                foreach (AirCraftEquipmentConfigViewModel lElement in aSourceList as ObservableCollection<AirCraftEquipmentConfigViewModel>)
                {
                    lElement.mConfigName += lElement.mCompareResult;
                }
            }

            return lIsChanged;
        }
    }
}
