using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class ConfigInfo
    {
        public String mItemIndex { set; get; }
        public String mItemInfo { set; get; }

        public ConfigInfo() 
        {
            mItemIndex = String.Empty;
            mItemInfo = String.Empty;
        }
        public ConfigInfo(String aIndex, String aInfo) 
        {
            mItemIndex = aIndex;
            mItemInfo = aInfo;
        }
    }
}
