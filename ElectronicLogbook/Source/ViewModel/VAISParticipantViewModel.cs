using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class VAISParticipantViewModel : ViewModel, ISerializable
    {
        private String _ParticipantName;
        public String mParticipantName 
        {
            set 
            {
                _ParticipantName = value;
                this.OnPropertyChanged("mParticipantName");
            }
            get { return mParticipantName; }
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
                this.OnPropertyChanged("_ParticipantLocation");
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

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mParticipantName", mParticipantName);
            info.AddValue("mParticipantPartNumber",mParticipantPartNumber);
            info.AddValue("mParticipantVersionNumber",mParticipantVersionNumber);
            info.AddValue("mParticipantDescription",mParticipantDescription);
            info.AddValue("mParticipantLocation",mParticipantLocation);
        }

        //Deserialization constructor.
        public VAISParticipantViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mParticipantName = (String)info.GetValue("mParticipantName", typeof(String));
            mParticipantPartNumber = (String)info.GetValue("mParticipantPartNumber", typeof(String));
            mParticipantVersionNumber = (String)info.GetValue("mParticipantVersionNumber", typeof(String));
            mParticipantDescription = (String)info.GetValue("mParticipantDescription", typeof(String));
            mParticipantLocation = (String)info.GetValue("mParticipantLocation", typeof(String));
        }
    }
}
