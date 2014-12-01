using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    public class ConfigInfoViewModel : ViewModel
    {
        private String _ItemIndex;
        public String mItemIndex 
        {
            set 
            {
                _ItemIndex = value;
                this.OnPropertyChanged("mItemIndex");
            }
            get 
            {
                return _ItemIndex;
            }
        }

        private String _ItemInfo;
        public String mItemInfo 
        {
            set 
            {
                _ItemInfo = value;
                this.OnPropertyChanged("mItemInfo");
            }
            get 
            {
                return _ItemInfo;
            }
        }

        public ConfigInfoViewModel(ConfigInfo aConfigInfo)
        {
            mItemIndex = aConfigInfo.mItemIndex;
            mItemInfo = aConfigInfo.mItemInfo;
        }
    }
}
