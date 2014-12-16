using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ThirdPartySoftwareViewModel : ViewModel, ISerializable
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

        
        public ThirdPartySoftwareViewModel(ThirdPartySoftware aThirdPartySoftware)
        {
            mSoftwareName = aThirdPartySoftware.mSoftwareName;
            mSoftwareVersionNumber = aThirdPartySoftware.mSoftwareVersionNumber;
            mSoftwareLocation = aThirdPartySoftware.mSoftwareLocation;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mSoftwareName", mSoftwareName);
            info.AddValue("mSoftwareVersionNumber",mSoftwareVersionNumber);
            info.AddValue("mSoftwareLocation",mSoftwareLocation);
        }

        //Deserialization constructor.
        public ThirdPartySoftwareViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mSoftwareName = (String)info.GetValue("mSoftwareName", typeof(String));
            mSoftwareVersionNumber = (String)info.GetValue("mSoftwareVersionNumber", typeof(String));
            mSoftwareLocation = (String)info.GetValue("mSoftwareLocation", typeof(String));
        }
    }
}
