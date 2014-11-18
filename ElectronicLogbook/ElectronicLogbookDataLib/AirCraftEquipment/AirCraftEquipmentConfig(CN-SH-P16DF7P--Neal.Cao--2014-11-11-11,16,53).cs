using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class AirCraftEquipmentConfig
    {
        public readonly String mConfigName { set; get; }
        public readonly int mEquipmentID{set;get;}
        public readonly List<HWPart> mHWPart { set; get; }
        public readonly List<SWPart> mSWConfig { set; get; }
        public readonly List<OtherItem> mOtherItemList { set; get; }

        public AirCraftEquipmentConfig() { }
        public AirCraftEquipmentConfig(String aConfigName, int aEquipmentID, List<HWPart> aHWPart, List<SWPart> aSWConfig, List<OtherItem> aOtherItemList) 
        {
            mConfigName = aConfigName;
            mEquipmentID = aEquipmentID;
            mHWPart = aHWPart;
            mSWConfig = aSWConfig;
            mOtherItemList = aOtherItemList;
        }
    }
}
