using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SubEquipmentViewModel : TreeViewItemViewModel, ISerializable,
        IEquatable<SubEquipmentViewModel>, IComparable<SubEquipmentViewModel>
    {
        private String _EquipmentID;
        public String mEquipmentID 
        {
            set 
            {
                _EquipmentID = value;
                this.OnPropertyChanged("mEquipmentID");
            }
            get 
            {
                return _EquipmentID;
            }
        }

        private ObservableCollection<HWPartViewModel> _HWPartList;
        public ObservableCollection<HWPartViewModel> mHWPartList 
        {
            set 
            {
                _HWPartList = value;
                this.OnPropertyChanged("mHWPartList");
            }
            get 
            {
                return _HWPartList;
            }
        }

        private ObservableCollection<SWConfigViewModel> _SWConfigList;
        public ObservableCollection<SWConfigViewModel> mSWConfigList 
        {
            set 
            {
                _SWConfigList = value;
                this.OnPropertyChanged("mSWConfigList");
            }
            get 
            {
                return _SWConfigList;
            }
        }

        private ObservableCollection<ConfigInfoViewModel> _ConfigInfoList;
        public ObservableCollection<ConfigInfoViewModel> mConfigInfoList 
        {
            set
            {
                _ConfigInfoList = value;
                this.OnPropertyChanged("mConfigInfoList");
            }
            get 
            {
                return _ConfigInfoList;
            }
        }

        private void Initialize(SubEquipment aSubEquipment, TreeViewItemViewModel aParent) 
        {
            mEquipmentID = aSubEquipment.mEquipmentID;

            mHWPartList = new ObservableCollection<HWPartViewModel>();
            foreach (HWPart lHWPart in aSubEquipment.mHWPartList)
            {
                mHWPartList.Add(new HWPartViewModel(lHWPart));
            }

            mSWConfigList = new ObservableCollection<SWConfigViewModel>();
            foreach (SWConfig lSWConfig in aSubEquipment.mSWConfigList)
            {
                mSWConfigList.Add(new SWConfigViewModel(lSWConfig));
            }

            mConfigInfoList = new ObservableCollection<ConfigInfoViewModel>();
            foreach (ConfigInfo lConfigInfo in aSubEquipment.mConfigInfoList)
            {
                mConfigInfoList.Add(new ConfigInfoViewModel(lConfigInfo));
            }
        }
        public SubEquipmentViewModel(SubEquipment aSubEquipment, TreeViewItemViewModel aParent)
        : base(aParent)
        {
            Initialize(aSubEquipment, aParent);
        }
        public SubEquipmentViewModel(SubEquipment aSubEquipment, TreeViewItemViewModel aParent, bool aIsInEditMode)
            : base(aParent)
        {
            Initialize(aSubEquipment, aParent);
            base.IsInEditMode = aIsInEditMode;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mEquipmentID",mEquipmentID);
            info.AddValue("mHWPartList", mHWPartList);
            info.AddValue("mSWConfigList", mSWConfigList);
            info.AddValue("mConfigInfoList", mConfigInfoList);
            info.AddValue("mChildren", mChildren);
            info.AddValue("mParent", mParent);
        }

        //Deserialization constructor.
        public SubEquipmentViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mEquipmentID = (String)info.GetValue("mEquipmentID", typeof(String));
            mHWPartList = (ObservableCollection<HWPartViewModel>)
                info.GetValue("mHWPartList", typeof(ObservableCollection<HWPartViewModel>));
            mSWConfigList = (ObservableCollection<SWConfigViewModel>)
                info.GetValue("mSWConfigList", typeof(ObservableCollection<SWConfigViewModel>));
            mConfigInfoList = (ObservableCollection<ConfigInfoViewModel>)
                info.GetValue("mConfigInfoList", typeof(ObservableCollection<ConfigInfoViewModel>));
            mChildren = (ObservableCollection<TreeViewItemViewModel>)info.
                GetValue("mChildren", typeof(ObservableCollection<TreeViewItemViewModel>));
            mParent = (TreeViewItemViewModel)info.GetValue("mParent", typeof(TreeViewItemViewModel));
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
            return mEquipmentID.GetHashCode();
        }

        public bool Equals(SubEquipmentViewModel aOther)
        {
            if (aOther == null) return false;
            return (this.mEquipmentID.Equals(aOther.mEquipmentID));
        }

        public int CompareTo(SubEquipmentViewModel aOther)
        {
            if (aOther == null) return 1;
            return this.mEquipmentID.CompareTo(aOther.mEquipmentID);
        }

        public void Compare(SubEquipmentViewModel lTargetSubEquipment)
        {
            throw new NotImplementedException();
        }
    }
}
