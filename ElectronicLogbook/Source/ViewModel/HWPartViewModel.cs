using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class HWPartViewModel : ViewModel, ISerializable
    {
        private String _HWPartIndex;
        public String mHWPartIndex 
        { 
            set
            {
                _HWPartIndex = value;
                this.OnPropertyChanged("mHWPartIndex");
            }
            get 
            {
                return _HWPartIndex;
            }
        }

        private String _HWPartNumber;
        public String mHWPartNumber 
        {
            set 
            {
                _HWPartNumber = value;
                this.OnPropertyChanged("mHWPartNumber");
            }
            get 
            {
                return _HWPartNumber;
            }
        }

        private String _HWPartDescription;
        public String mHWPartDescription 
        {
            set 
            {
                _HWPartDescription = value;
                this.OnPropertyChanged("mHWPartDescription");
            }
            get 
            {
                return _HWPartDescription;
            }
        }

        private String _HWPartStatus;
        public String mHWPartStatus 
        {
            set 
            {
                _HWPartStatus = value;
                this.OnPropertyChanged("mHWPartStatus");
            }
            get 
            {
                return _HWPartStatus;
            }
        }

        private String _HWPartSerialNumber;
        public String mHWPartSerialNumber 
        {
            set 
            {
                _HWPartSerialNumber = value;
                this.OnPropertyChanged("mHWPartSerialNumber");
            }
            get 
            {
                return _HWPartSerialNumber;
            }
        }

        public HWPartViewModel() {
            mHWPartDescription = String.Empty;
            mHWPartIndex = String.Empty;
            mHWPartNumber = String.Empty;
            mHWPartSerialNumber = String.Empty;
            mHWPartStatus = String.Empty;
        }
        public HWPartViewModel(HWPart aHWPart) 
        {
            mHWPartDescription = aHWPart.mHWPartDescription;
            mHWPartIndex = aHWPart.mHWPartIndex;
            mHWPartNumber = aHWPart.mHWPartNumber;
            mHWPartSerialNumber = aHWPart.mHWPartSerialNumber;
            mHWPartStatus = aHWPart.mHWPartStatus;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mHWPartIndex",mHWPartIndex);
            info.AddValue("mHWPartNumber", mHWPartNumber);
            info.AddValue("mHWPartDescription", mHWPartDescription);
            info.AddValue("mHWPartStatus", mHWPartStatus);
            info.AddValue("mHWPartSerialNumber", mHWPartSerialNumber);
        }

        public HWPartViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mHWPartIndex = (String)info.GetValue("mHWPartIndex", typeof(String));
            mHWPartNumber = (String)info.GetValue("mHWPartNumber", typeof(String));
            mHWPartDescription = (String)info.GetValue("mHWPartDescription", typeof(String));
            mHWPartStatus = (String)info.GetValue("mHWPartStatus", typeof(String));
            mHWPartSerialNumber = (String)info.GetValue("mHWPartSerialNumber", typeof(String));
        }
    }
}
