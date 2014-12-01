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
        public ObservableCollection<HWPartViewModel> mHWPartList { set; get; }
        public List<SWConfig> mSWConfigList { set; get; }
        public List<ConfigInfo> mConfigInfoList { set; get; }
        public SubEquipmentViewModel(SubEquipment aSubEquipment, TreeViewItemViewModel aParent)
        : base(aParent)
        {
            mSubEquipment = aSubEquipment;
        }
    }
}
