using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class SWPart
    {
        public String mSWPartIndex { get; set; }
        public String mLSAPPartNumber { get; set; }
        public String mLSAPDescription { get; set; }

        public SWPart() 
        {
            mSWPartIndex = String.Empty;
            mLSAPPartNumber = String.Empty;
            mLSAPDescription = String.Empty;
        }

        public SWPart(String aSWPartIndex, String aLSAPPartNumber, String aLSAPDescription) 
        {
            mSWPartIndex = aSWPartIndex;
            mLSAPPartNumber = aLSAPPartNumber;
            mLSAPDescription = aLSAPDescription;
        }
    }
}
