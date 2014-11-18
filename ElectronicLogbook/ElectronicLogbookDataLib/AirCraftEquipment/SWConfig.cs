using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class SWConfig
    {
        public String mSWConfigIndex { set; get; }
        public String mSWLocationID { set; get; }
        public String mSWLocationDescription { set; get; }
        public List<SWPart> mSWPartList { set; get; }

        public SWConfig() 
        {
            mSWConfigIndex = String.Empty;
            mSWLocationID = String.Empty;
            mSWLocationDescription = String.Empty;
            mSWPartList = new List<SWPart>();
        }
        public SWConfig(String aSWConfigIndex, String aSWLocationID, String aSWLocationDescription, List<SWPart> aSWPartList) 
        {
            mSWConfigIndex = aSWConfigIndex;
            mSWLocationID = aSWLocationID;
            mSWLocationDescription = aSWLocationDescription;
            mSWPartList = aSWPartList;
        }
    }
}
