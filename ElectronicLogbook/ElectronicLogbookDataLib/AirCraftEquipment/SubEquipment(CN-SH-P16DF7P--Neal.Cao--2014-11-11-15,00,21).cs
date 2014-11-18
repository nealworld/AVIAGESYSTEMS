using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class SubEquipment
    {
        public String mEquipmentID { set; get; }
        public List<HWPart> mHWPart { set; get; }
        public List<SWConfig> mSWConfig { set; get; }
        public List<ConfigInfo> mConfigInfoList { set; get; }

        public SubEquipment() {
            mEquipmentID = String.Empty;
            mHWPart = new List<HWPart>();
            mSWConfig = new List<SWConfig>();
            mConfigInfoList = new List<ConfigInfo>();
        }
        public SubEquipment(String aEquipmentID, List<HWPart> aHWPart, List<SWConfig> aSWConfig, List<ConfigInfo> aOtherItemList) 
        {
            mEquipmentID = aEquipmentID;
            mHWPart = aHWPart;
            mSWConfig = aSWConfig;
            mConfigInfoList = aOtherItemList;
        }
    }
}
