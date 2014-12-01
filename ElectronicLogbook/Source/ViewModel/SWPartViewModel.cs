using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    public class SWPartViewModel : ViewModel
    {
        private String _SWPartIndex;
        public String mSWPartIndex {
            get 
            {
                return _SWPartIndex;
            }
            set 
            {
                _SWPartIndex = value;
                this.OnPropertyChanged("mSWPartIndex");
            }
        }

        private String _LSAPPartNumber;
        public String mLSAPPartNumber 
        {
            get 
            {
                return _LSAPPartNumber;
            }
            set 
            {
                _LSAPPartNumber = value;
                this.OnPropertyChanged("mLSAPPartNumber");
            }
        }

        private String _LSAPDescription;
        public String mLSAPDescription 
        {
            get 
            {
                return _LSAPDescription;
            }
            set 
            {
                _LSAPDescription = value;
                this.OnPropertyChanged("mLSAPDescription");
            }
        }

        public SWPartViewModel(SWPart aSWPart) 
        {
            mSWPartIndex = aSWPart.mSWPartIndex;
            mLSAPPartNumber = aSWPart.mLSAPPartNumber;
            mLSAPDescription = aSWPart.mLSAPDescription;
        }

    }
}
