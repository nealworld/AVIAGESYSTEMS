using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.AirCraftEquipment
{
    public class HWPart
    {
        public String mHWPartIndex { set; get; }
        public String mHWPartNumber { set; get; }
        public String mHWPartDescription { set; get; }
        public String mHWPartStatus { set; get; }
        public String mHWPartSerialNumber { set; get; }

        public HWPart() {
            mHWPartIndex = String.Empty;
            mHWPartNumber = String.Empty;
            mHWPartDescription = String.Empty;
            mHWPartStatus = String.Empty;
            mHWPartSerialNumber = String.Empty;
        }
        public HWPart(String aHWPartIndex, String aHWPartNumber, String aHWPartDescription, String aHWPartStatus, String aHWPartSerialNumber)
        {
            mHWPartIndex = aHWPartIndex;
            mHWPartNumber = aHWPartNumber;
            mHWPartDescription = aHWPartDescription;
            mHWPartStatus = aHWPartStatus;
            mHWPartSerialNumber = aHWPartSerialNumber;
        } 
        
    }
}
