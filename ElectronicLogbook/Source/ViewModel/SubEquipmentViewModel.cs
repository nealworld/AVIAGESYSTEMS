using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Security;
using System.Windows;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SubEquipmentViewModel : TreeViewItemViewModel, ISerializable,
        IEquatable<SubEquipmentViewModel>
    {
        private Visibility _HWPartCompareResultColumnVisibility;
        public Visibility mHWPartCompareResultColumnVisibility
        {
            set
            {
                _HWPartCompareResultColumnVisibility = value;
                this.OnPropertyChanged("mHWPartCompareResultColumnVisibility");
            }
            get
            {
                return _HWPartCompareResultColumnVisibility;
            }
        }

        private Visibility _ConfigInfoCompareResultColumnVisibility;
        public Visibility mConfigInfoCompareResultColumnVisibility
        {
            set
            {
                _ConfigInfoCompareResultColumnVisibility = value;
                this.OnPropertyChanged("mConfigInfoCompareResultColumnVisibility");
            }
            get
            {
                return _ConfigInfoCompareResultColumnVisibility;
            }
        }

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
            mHWPartCompareResultColumnVisibility = Visibility.Hidden;
            mConfigInfoCompareResultColumnVisibility = Visibility.Hidden;
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
            base.IsSelected = true;
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mConfigInfoCompareResultColumnVisibility",mConfigInfoCompareResultColumnVisibility);
            info.AddValue("mHWPartCompareResultColumnVisibility", mHWPartCompareResultColumnVisibility);
            info.AddValue("mEquipmentID",mEquipmentID);
            info.AddValue("mHWPartList", mHWPartList);
            info.AddValue("mSWConfigList", mSWConfigList);
            info.AddValue("mConfigInfoList", mConfigInfoList);
            info.AddValue("mChildren", mChildren);
            info.AddValue("mParent", mParent);
        }

        public SubEquipmentViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mConfigInfoCompareResultColumnVisibility = (Visibility)info.GetValue("mConfigInfoCompareResultColumnVisibility", typeof(Visibility));
            mHWPartCompareResultColumnVisibility = (Visibility)info.GetValue("mHWPartCompareResultColumnVisibility", typeof(Visibility));
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
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

        public override Boolean Compare(ViewModel lTarget)
        {
            Boolean lIsChanged = false;
            lIsChanged |= Compare(this.mHWPartList, (lTarget as SubEquipmentViewModel).mHWPartList);
            lIsChanged |= Compare(this.mSWConfigList, (lTarget as SubEquipmentViewModel).mSWConfigList);
            lIsChanged |= Compare(this.mConfigInfoList, (lTarget as SubEquipmentViewModel).mConfigInfoList);
            return lIsChanged;
        }

        private Boolean Compare<T>(ObservableCollection<T> aSourceList, ObservableCollection<T> aTargetList)
        {
            Boolean lIsChanged = false;
            foreach (T lSourceElement in aSourceList)
            {

                if (!aTargetList.Contains(lSourceElement))
                {
                    (lSourceElement as ViewModel).mCompareResult = Utility.Deleted;
                    this.mCompareResult = Utility.Modified;
                    this.mParent.mCompareResult = Utility.Modified;
                    lIsChanged = true;
                }
            }

            foreach (T lTargetElement in aTargetList)
            {
                if (!aSourceList.Contains(lTargetElement))
                {
                    (lTargetElement as ViewModel).mCompareResult = Utility.New;
                    aSourceList.Add(lTargetElement);
                    this.mCompareResult = Utility.Modified;
                    this.mParent.mCompareResult = Utility.Modified;
                    lIsChanged = true;
                }
            }

            if(aSourceList is ObservableCollection<SWConfigViewModel>)
            {
                foreach (SWConfigViewModel lElement in aSourceList as ObservableCollection<SWConfigViewModel>)
                {
                    lElement.mSWConfigIndex += lElement.mCompareResult;
                }
            }

            return lIsChanged;
        }
    }
}
