using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class SubEquipment
    {
        public String mEquipmentID { set; get; }
        public List<HWPart> mHWPartList { set; get; }
        public List<SWConfig> mSWConfigList { set; get; }
        public List<ConfigInfo> mConfigInfoList { set; get; }

        public SubEquipment() {
            mEquipmentID = String.Empty;
            mHWPartList = new List<HWPart>();
            mSWConfigList = new List<SWConfig>();
            mConfigInfoList = new List<ConfigInfo>();
        }
        public SubEquipment(String aEquipmentID, List<HWPart> aHWPartList, List<SWConfig> aSWConfigList, List<ConfigInfo> aConfigInfoList) 
        {
            mEquipmentID = aEquipmentID;
            mHWPartList = aHWPartList;
            mSWConfigList = aSWConfigList;
            mConfigInfoList = aConfigInfoList;
        }
    }
}
