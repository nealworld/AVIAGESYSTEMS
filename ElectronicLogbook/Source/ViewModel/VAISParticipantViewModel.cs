using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;
using System.Windows;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class VAISParticipantViewModel : ViewModel, ISerializable, IEquatable<VAISParticipantViewModel>
    {
        private String _ParticipantName;
        public String mParticipantName 
        {
            set 
            {
                _ParticipantName = value;
                this.OnPropertyChanged("mParticipantName");
            }
            get { return _ParticipantName; }
        }

        private String _ParticipantPartNumber;
        public String mParticipantPartNumber 
        {
            set 
            {
                _ParticipantPartNumber = value;
                this.OnPropertyChanged("mParticipantPartNumber");
            }
            get { return _ParticipantPartNumber; }
        }

        private String _ParticipantVersionNumber;
        public String mParticipantVersionNumber
        { 
            set 
            {
                _ParticipantVersionNumber = value;
                this.OnPropertyChanged("mParticipantVersionNumber");
            }
            get{return _ParticipantVersionNumber;}
        }

        private String _ParticipantDescription;
        public String mParticipantDescription 
        {
            set 
            {
                _ParticipantDescription = value;
                this.OnPropertyChanged("mParticipantDescription");
            }
            get { return _ParticipantDescription; }
        }

        private String _ParticipantLocation;
        public String mParticipantLocation 
        {
            set 
            {
                _ParticipantLocation = value;
                this.OnPropertyChanged("mParticipantLocation");
            }
            get { return _ParticipantLocation; }
        }

        public VAISParticipantViewModel(VAISParticipant aVAISParticipant) 
        {
            mParticipantName = aVAISParticipant.mParticipantName;
            mParticipantPartNumber = aVAISParticipant.mParticipantName;
            mParticipantVersionNumber = aVAISParticipant.mParticipantVersionNumber;
            mParticipantDescription = aVAISParticipant.mParticipantDescription;
            mParticipantLocation = aVAISParticipant.mParticipantLocation;
        }

        public VAISParticipantViewModel()
        {
            mParticipantName = String.Empty;
            mParticipantPartNumber = String.Empty;
            mParticipantVersionNumber = String.Empty;
            mParticipantDescription = String.Empty;
            mParticipantLocation = String.Empty;
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mParticipantName", mParticipantName);
            info.AddValue("mParticipantPartNumber",mParticipantPartNumber);
            info.AddValue("mParticipantVersionNumber",mParticipantVersionNumber);
            info.AddValue("mParticipantDescription",mParticipantDescription);
            info.AddValue("mParticipantLocation",mParticipantLocation);
        }

        public VAISParticipantViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mParticipantName = (String)info.GetValue("mParticipantName", typeof(String));
            mParticipantPartNumber = (String)info.GetValue("mParticipantPartNumber", typeof(String));
            mParticipantVersionNumber = (String)info.GetValue("mParticipantVersionNumber", typeof(String));
            mParticipantDescription = (String)info.GetValue("mParticipantDescription", typeof(String));
            mParticipantLocation = (String)info.GetValue("mParticipantLocation", typeof(String));
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
            return this.mParticipantName.GetHashCode();
        }

        public bool Equals(VAISParticipantViewModel aOther)
        {
            if (aOther == null) return false;
            return (this.mParticipantName).Equals(aOther.mParticipantName);
        }
        public override string ToString()
        {
            return mParticipantName;
        }
        public override Boolean Compare(ViewModel aTarget)
        {
            Boolean lIsChanged = false;
            if ((aTarget as VAISParticipantViewModel).mParticipantDescription != this.mParticipantDescription)
            {
                this.mCompareResult = Utility.Modified;
                this.mParticipantDescription += "(" + (aTarget as VAISParticipantViewModel).mParticipantDescription + ")";
                lIsChanged = true;
            }
            if ((aTarget as VAISParticipantViewModel).mParticipantLocation != this.mParticipantLocation)
            {
                this.mCompareResult = Utility.Modified;
                this.mParticipantLocation += "(" + (aTarget as VAISParticipantViewModel).mParticipantLocation + ")";
                lIsChanged = true;
            }
            if ((aTarget as VAISParticipantViewModel).mParticipantName != this.mParticipantName)
            {
                this.mCompareResult = Utility.Modified;
                this.mParticipantName += "(" + (aTarget as VAISParticipantViewModel).mParticipantName + ")";
                lIsChanged = true;
            }
            if ((aTarget as VAISParticipantViewModel).mParticipantPartNumber != this.mParticipantPartNumber)
            {
                this.mCompareResult = Utility.Modified;
                this.mParticipantPartNumber += "(" + (aTarget as VAISParticipantViewModel).mParticipantPartNumber + ")";
                lIsChanged = true;
            }
            if ((aTarget as VAISParticipantViewModel).mParticipantVersionNumber != this.mParticipantVersionNumber)
            {
                this.mCompareResult = Utility.Modified;
                this.mParticipantDescription += "(" + (aTarget as VAISParticipantViewModel).mParticipantVersionNumber + ")";
                lIsChanged = true;
            }
            return lIsChanged;
        }

    }
}
