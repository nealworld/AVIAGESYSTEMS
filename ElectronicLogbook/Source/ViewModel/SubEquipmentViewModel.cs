using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class SubEquipmentViewModel : TreeViewItemViewModel
    {
        private String _EquipmentID;
        public String mEquipmentID 
        {
            set 
            {
                _EquipmentID = value;
                this.OnPropertyChanged("mEquipmentID");
            }
            get 
            {
                return _EquipmentID;
            }
        }

        private ObservableCollection<HWPartViewModel> _HWPartList;
        public ObservableCollection<HWPartViewModel> mHWPartList 
        {
            set 
            {
                _HWPartList = value;
                this.OnPropertyChanged("mHWPartList");
            }
            get 
            {
                return _HWPartList;
            }
        }

        private ObservableCollection<SWConfigViewModel> _SWConfigList;
        public ObservableCollection<SWConfigViewModel> mSWConfigList 
        {
            set 
            {
                _SWConfigList = value;
                this.OnPropertyChanged("mSWConfigList");
            }
            get 
            {
                return _SWConfigList;
            }
        }

        private ObservableCollection<ConfigInfoViewModel> _ConfigInfoList;
        public ObservableCollection<ConfigInfoViewModel> mConfigInfoList 
        {
            set
            {
                _ConfigInfoList = value;
                this.OnPropertyChanged("mConfigInfoList");
            }
            get 
            {
                return _ConfigInfoList;
            }
        }
        public SubEquipmentViewModel(SubEquipment aSubEquipment, TreeViewItemViewModel aParent)
        : base(aParent)
        {
            mEquipmentID = aSubEquipment.mEquipmentID;

            mHWPartList = new ObservableCollection<HWPartViewModel>();
            foreach (HWPart lHWPart in aSubEquipment.mHWPartList) 
            {
                mHWPartList.Add(new HWPartViewModel(lHWPart));
            }

            mSWConfigList = new ObservableCollection<SWConfigViewModel>();
            foreach (SWConfig lSWConfig in aSubEquipment.mSWConfigList)
            {
                mSWConfigList.Add(new SWConfigViewModel(lSWConfig));
            }

            mConfigInfoList = new ObservableCollection<ConfigInfoViewModel>();
            foreach (ConfigInfo lConfigInfo in aSubEquipment.mConfigInfoList)
            {
                mConfigInfoList.Add(new ConfigInfoViewModel(lConfigInfo));
            }
        }
    }
}
