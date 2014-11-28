using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class AirCraftEquipmentConfigViewModel
    {
        public List<AirCraftEquipmentConfig> mAirCraftEquipmentConfigList { set; get; }
        public String mName;
        
        public AirCraftEquipmentConfigViewModel()
        {
            mName = "Equipment List";
        }
    }
}
