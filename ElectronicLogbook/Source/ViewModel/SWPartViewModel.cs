using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Security;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SWPartViewModel : ViewModel, ISerializable
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

        public SWPartViewModel() 
        {
            mSWPartIndex = String.Empty;
            mLSAPPartNumber = String.Empty;
            mLSAPDescription = String.Empty;
        }

        public SWPartViewModel(SWPart aSWPart) 
        {
            mSWPartIndex = aSWPart.mSWPartIndex;
            mLSAPPartNumber = aSWPart.mLSAPPartNumber;
            mLSAPDescription = aSWPart.mLSAPDescription;
        }

        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mSWPartIndex",mSWPartIndex);
            info.AddValue("mLSAPPartNumber", mLSAPPartNumber);
            info.AddValue("mLSAPDescription", mLSAPDescription);
        }

        //Deserialization constructor.
        public SWPartViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mSWPartIndex = (String)info.GetValue("mSWPartIndex", typeof(String));
            mLSAPPartNumber = (String)info.GetValue("mLSAPPartNumber", typeof(String));
            mLSAPDescription = (String)info.GetValue("mLSAPDescription", typeof(String));
        }

        public override string ToString()
        {
            return mSWPartIndex + mLSAPPartNumber + mLSAPDescription;
        }
    }
}
