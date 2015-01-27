using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Security;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SWConfigViewModel : ViewModel, ISerializable, IEquatable<SWConfigViewModel>
    {
        private bool _isSelected;

        [XmlIgnore()]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        private String _SWConfigIndex;

        [XmlAttribute("SWConfigIndex")]
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

        [XmlAttribute("SWLocationID")]
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

        [XmlAttribute("SWLocationDescription")]
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

        [XmlArrayItem("SWPart", typeof(SWPartViewModel))]
        [XmlArray("SWPartList")]
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

        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }

        public SWConfigViewModel() { }

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

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mSWConfigIndex",mSWConfigIndex);
            info.AddValue("mSWLocationID", mSWLocationID);
            info.AddValue("mSWLocationDescription", mSWLocationDescription);
            info.AddValue("mSWPartList", mSWPartList);
        }

        public SWConfigViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mSWConfigIndex = (String)info.GetValue("mSWConfigIndex", typeof(String));
            mSWLocationID = (String)info.GetValue("mSWLocationID", typeof(String));
            mSWLocationDescription = (String)info.GetValue("mSWLocationDescription", typeof(String));
            mSWPartList = (ObservableCollection<SWPartViewModel>)
                info.GetValue("mSWPartList", typeof(ObservableCollection<SWPartViewModel>));
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
            return (mSWConfigIndex + mSWLocationID + mSWPartList.ToString()).GetHashCode();
        }

        public bool Equals(SWConfigViewModel aOther)
        {
            if (aOther == null) return false;
            return (this.mSWConfigIndex + this.mSWLocationID + this.mSWPartList.ToString()).
                Equals(aOther.mSWConfigIndex + aOther.mSWLocationID + aOther.mSWPartList.ToString());
        }

        
    }
}
