using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.Collections.ObjectModel;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class AirCraftEquipmentConfigViewModel : TreeViewItemViewModel, ISerializable 
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

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mConfigName",mConfigName);
        }

        //Deserialization constructor.
        public AirCraftEquipmentConfigViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mConfigName = (String)info.GetValue("mConfigName", typeof(String));
        }

    }
}
