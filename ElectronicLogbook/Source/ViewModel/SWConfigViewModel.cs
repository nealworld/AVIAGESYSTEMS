using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SWConfigViewModel : ViewModel, ISerializable
    {
        private String _SWConfigIndex;
        public String mSWConfigIndex 
        {
            set 
            {
                _SWConfigIndex = value;
                this.OnPropertyChanged("mSWConfigIndex");
            }
            get 
            {
                return _SWConfigIndex;
            }
        }

        private String _SWLocationID;
        public String mSWLocationID 
        {
            set
            {
                _SWLocationID = value;
                this.OnPropertyChanged("mSWLocationID");
            }
            get 
            {
                return _SWLocationID;
            }
        }

        private String _SWLocationDescription;
        public String mSWLocationDescription 
        {
            set 
            {
                _SWLocationDescription = value;
                this.OnPropertyChanged("mSWLocationDescription");
            }
            get 
            {
                return _SWLocationDescription;
            }
        }

        private ObservableCollection<SWPartViewModel> _SWPartList;
        public ObservableCollection<SWPartViewModel> mSWPartList 
        {
            set 
            {
                _SWPartList = value;
                this.OnPropertyChanged("mSWPartList");
            }
            get 
            {
                return _SWPartList;
            }
        }

        public SWConfigViewModel(SWConfig aSWConfig) 
        {
            mSWConfigIndex = aSWConfig.mSWConfigIndex;
            mSWLocationID = aSWConfig.mSWLocationID;
            mSWLocationDescription = aSWConfig.mSWLocationDescription;
            mSWPartList = new ObservableCollection<SWPartViewModel>();
            foreach (SWPart lSWPart in aSWConfig.mSWPartList) 
            {
                mSWPartList.Add(new SWPartViewModel(lSWPart));
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mSWConfigIndex",mSWConfigIndex);
            info.AddValue("mSWLocationID", mSWLocationID);
            info.AddValue("mSWLocationDescription", mSWLocationDescription);
            info.AddValue("mSWPartList", mSWPartList);
        }

        //Deserialization constructor.
        public SWConfigViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mSWConfigIndex = (String)info.GetValue("mSWConfigIndex", typeof(String));
            mSWLocationID = (String)info.GetValue("mSWLocationID", typeof(String));
            mSWLocationDescription = (String)info.GetValue("mSWLocationDescription", typeof(String));
            mSWPartList = (ObservableCollection<SWPartViewModel>)
                info.GetValue("mSWPartList", typeof(ObservableCollection<SWPartViewModel>));
        }
    }
}
