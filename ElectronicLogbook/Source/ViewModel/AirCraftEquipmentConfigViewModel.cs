using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class AirCraftEquipmentConfigViewModel : TreeViewItemViewModel
    {
        private String _ConfigName;
        public String mConfigName{
            set
            {
                _ConfigName = value;
                this.OnPropertyChanged("mConfigName");
            }
            get 
            {
                return _ConfigName;
            }
        }
        private void Initialize(AirCraftEquipmentConfig aAirCraftEquipmentConfig)
        {
            mConfigName = aAirCraftEquipmentConfig.mConfigName;
            foreach (SubEquipment lSubEquipment in aAirCraftEquipmentConfig.mSubEquipmentList)
            {
                mChildren.Add(new SubEquipmentViewModel(lSubEquipment, this));
            }
        }
        public AirCraftEquipmentConfigViewModel(AirCraftEquipmentConfig aAirCraftEquipmentConfig)
            :base(null)
        {
            Initialize(aAirCraftEquipmentConfig);
        }

        public AirCraftEquipmentConfigViewModel(AirCraftEquipmentConfig aAirCraftEquipmentConfig, bool aIsInEditMode)
            : base(null)
        {
            Initialize(aAirCraftEquipmentConfig);
            base.IsInEditMode = aIsInEditMode;

        }

    }
}
