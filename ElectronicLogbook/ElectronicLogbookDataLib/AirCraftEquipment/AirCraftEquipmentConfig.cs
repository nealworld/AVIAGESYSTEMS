using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class AirCraftEquipmentConfig
    {
        public String mConfigName { set; get; }
        public List<SubEquipment> mSubEquipmentList { set; get; }

        public AirCraftEquipmentConfig() {
            mConfigName = String.Empty;
            mSubEquipmentList = new List<SubEquipment>();
        }
        public AirCraftEquipmentConfig(String aConfigName, List<SubEquipment> aSubEquipmentList) 
        {
            mConfigName = aConfigName;
            mSubEquipmentList = aSubEquipmentList;
        }
    }
}
