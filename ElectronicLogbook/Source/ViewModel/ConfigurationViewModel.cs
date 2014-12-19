﻿using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;
using System;
using System.Windows;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ConfigurationViewModel : ViewModel, ISerializable
    {
        private bool _IsEditable;
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

        public ConfigurationViewModel() 
        {
            initialize();
        }

        public void initialize() 
        {
            mIsEditable = false;
            mIsReadOnly = true;
            mAirCraftEquipmentConfigViewModelList = new ObservableCollection<AirCraftEquipmentConfigViewModel>();
            mVAISParticipantListViewModel = new ObservableCollection<VAISParticipantViewModel>();
            mDeviceDriverListViewModel = new ObservableCollection<DeviceDriverViewModel>();
            mThirdPartySoftwareListViewModel = new ObservableCollection<ThirdPartySoftwareViewModel>();
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mAirCraftEquipmentConfigViewModelList",mAirCraftEquipmentConfigViewModelList);
            info.AddValue("mVAISParticipantListViewModel",mVAISParticipantListViewModel);
            info.AddValue("mDeviceDriverListViewModel",mDeviceDriverListViewModel);
            info.AddValue("mThirdPartySoftwareListViewModel",mThirdPartySoftwareListViewModel);
        }

        public ConfigurationViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mAirCraftEquipmentConfigViewModelList = (ObservableCollection<AirCraftEquipmentConfigViewModel>)
                info.GetValue("mAirCraftEquipmentConfigViewModelList", typeof(ObservableCollection<AirCraftEquipmentConfigViewModel>));
            mVAISParticipantListViewModel = (ObservableCollection<VAISParticipantViewModel>)
                info.GetValue("mVAISParticipantListViewModel", typeof(ObservableCollection<VAISParticipantViewModel>));
            mDeviceDriverListViewModel = (ObservableCollection<DeviceDriverViewModel>)
                info.GetValue("mDeviceDriverListViewModel", typeof(ObservableCollection<DeviceDriverViewModel>));
            mThirdPartySoftwareListViewModel = (ObservableCollection<ThirdPartySoftwareViewModel>)
                info.GetValue("mThirdPartySoftwareListViewModel", typeof(ObservableCollection<ThirdPartySoftwareViewModel>));

        }


        public void Compare(ConfigurationViewModel aTargetConfiguration)
        {
            CompareAirCraftEquipmentConfigList(this.mAirCraftEquipmentConfigViewModelList, aTargetConfiguration.mAirCraftEquipmentConfigViewModelList);
            CompareVAISParticipant(this.mVAISParticipantListViewModel, aTargetConfiguration.mVAISParticipantListViewModel);
            CompareThirdPartySoftware(this.mThirdPartySoftwareListViewModel, aTargetConfiguration.mThirdPartySoftwareListViewModel);
            CompareDeviceDriver(this.mDeviceDriverListViewModel, aTargetConfiguration.mDeviceDriverListViewModel);
        }

        private void CompareDeviceDriver(ObservableCollection<DeviceDriverViewModel> observableCollection1, ObservableCollection<DeviceDriverViewModel> observableCollection2)
        {
        }

        private void CompareThirdPartySoftware(ObservableCollection<ThirdPartySoftwareViewModel> observableCollection1, ObservableCollection<ThirdPartySoftwareViewModel> observableCollection2)
        {
        }

        private void CompareVAISParticipant(ObservableCollection<VAISParticipantViewModel> observableCollection1, ObservableCollection<VAISParticipantViewModel> observableCollection2)
        {
        }

        private void CompareAirCraftEquipmentConfigList(ObservableCollection<AirCraftEquipmentConfigViewModel> aSourceList,
            ObservableCollection<AirCraftEquipmentConfigViewModel> aTargetList)
        {
            foreach (AirCraftEquipmentConfigViewModel lSourceRecord in aSourceList)
            {
                if (aTargetList.Contains(lSourceRecord))
                {
                    lSourceRecord.Compare(aTargetList[aTargetList.IndexOf(lSourceRecord)]);
                }
                else
                {
                    lSourceRecord.mCompareResult = Utility.Deleted;
                }
            }

            foreach (AirCraftEquipmentConfigViewModel lTargetRecord in aTargetList)
            {
                if (!aSourceList.Contains(lTargetRecord))
                {
                    lTargetRecord.mCompareResult = Utility.New;
                    aSourceList.Add(lTargetRecord);
                }
            }

            foreach (AirCraftEquipmentConfigViewModel lElement in aSourceList) 
            {
                lElement.mConfigName += lElement.mCompareResult;
            }
        }
    }
}
