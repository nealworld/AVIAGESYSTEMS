using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ThirdPartySoftwareViewModel : ViewModel, ISerializable, IEquatable<ThirdPartySoftwareViewModel>
    {
        private String _SoftwareName;
        public String mSoftwareName {
            set 
            {
                _SoftwareName = value;
                this.OnPropertyChanged("mSoftwareName");
            }
            get { return _SoftwareName; }
        }

        private String _SoftwareVersionNumber;
        public String mSoftwareVersionNumber 
        {
            set 
            {
                _SoftwareVersionNumber = value;
                this.OnPropertyChanged("mSoftwareVersionNumber");
            }
            get { return _SoftwareVersionNumber; }
        }

        private String _SoftwareLocation;
        public String mSoftwareLocation 
        {
            set 
            {
                _SoftwareLocation = value;
                this.OnPropertyChanged("mSoftwareLocation");
            }
            get { return _SoftwareLocation; }
        }

        public ThirdPartySoftwareViewModel()
        {
            mSoftwareName = String.Empty;
            mSoftwareVersionNumber = String.Empty;
            mSoftwareLocation = String.Empty;
        }

        public ThirdPartySoftwareViewModel(ThirdPartySoftware aThirdPartySoftware)
        {
            mSoftwareName = aThirdPartySoftware.mSoftwareName;
            mSoftwareVersionNumber = aThirdPartySoftware.mSoftwareVersionNumber;
            mSoftwareLocation = aThirdPartySoftware.mSoftwareLocation;
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mSoftwareName", mSoftwareName);
            info.AddValue("mSoftwareVersionNumber",mSoftwareVersionNumber);
            info.AddValue("mSoftwareLocation",mSoftwareLocation);
        }

        public ThirdPartySoftwareViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mSoftwareName = (String)info.GetValue("mSoftwareName", typeof(String));
            mSoftwareVersionNumber = (String)info.GetValue("mSoftwareVersionNumber", typeof(String));
            mSoftwareLocation = (String)info.GetValue("mSoftwareLocation", typeof(String));
        }

        public override bool Equals(object aobj)
        {
            if (aobj == null) return false;
            SubEquipmentViewModel lobj = aobj as SubEquipmentViewModel;
            if (lobj == null) return false;
            else return Equals(lobj);
        }

        public override int GetHashCode()
        {
            return this.mSoftwareName.GetHashCode();
        }

        public bool Equals(ThirdPartySoftwareViewModel aOther)
        {
            if (aOther == null) return false;
            return (this.mSoftwareName).Equals(aOther.mSoftwareName);
        }

        public override Boolean Compare(ViewModel aTarget)
        {
            Boolean lIsChanged = false;

            if ((aTarget as ThirdPartySoftwareViewModel).mSoftwareLocation != this.mSoftwareLocation)
            {
                this.mCompareResult = Utility.Modified;
                this.mSoftwareLocation += "(" + (aTarget as ThirdPartySoftwareViewModel).mSoftwareLocation + ")";
                lIsChanged = true;
            }
            if ((aTarget as ThirdPartySoftwareViewModel).mSoftwareVersionNumber != this.mSoftwareVersionNumber)
            {
                this.mCompareResult = Utility.Modified;
                this.mSoftwareVersionNumber += "(" + (aTarget as ThirdPartySoftwareViewModel).mSoftwareVersionNumber + ")";
                lIsChanged = true;
            }

            return lIsChanged;
        }
    }
}
