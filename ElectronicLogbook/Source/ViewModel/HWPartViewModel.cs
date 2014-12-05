using System;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    public class HWPartViewModel : ViewModel
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
            mHWPartDescription = string.Empty;
            mHWPartIndex = string.Empty;
            mHWPartNumber = string.Empty;
            mHWPartSerialNumber = string.Empty;
            mHWPartStatus = string.Empty;
        }
        public HWPartViewModel(HWPart aHWPart) 
        {
            mHWPartDescription = aHWPart.mHWPartDescription;
            mHWPartIndex = aHWPart.mHWPartIndex;
            mHWPartNumber = aHWPart.mHWPartNumber;
            mHWPartSerialNumber = aHWPart.mHWPartSerialNumber;
            mHWPartStatus = aHWPart.mHWPartStatus;
        }
    }
}
