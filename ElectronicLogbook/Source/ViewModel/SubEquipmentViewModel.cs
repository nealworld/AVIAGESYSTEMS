using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Security;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class SubEquipmentViewModel : TreeViewItemViewModel, ISerializable,
        IEquatable<SubEquipmentViewModel>
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

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mEquipmentID",mEquipmentID);
            info.AddValue("mHWPartList", mHWPartList);
            info.AddValue("mSWConfigList", mSWConfigList);
            info.AddValue("mConfigInfoList", mConfigInfoList);
            info.AddValue("mChildren", mChildren);
            info.AddValue("mParent", mParent);
        }

        public SubEquipmentViewModel(SerializationInfo info, StreamingContext ctxt)
        {
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

        public void Compare(SubEquipmentViewModel lTargetSubEquipment)
        {
            CompareLists(this.mHWPartList, lTargetSubEquipment.mHWPartList);
            CompareLists(this.mSWConfigList, lTargetSubEquipment.mSWConfigList);
            CompareLists(this.mConfigInfoList, lTargetSubEquipment.mConfigInfoList);
        }
/*
        private bool CompareConfigInfoList(ObservableCollection<ConfigInfoViewModel> aSourceList, 
            ObservableCollection<ConfigInfoViewModel> aTargetList)
        {
            bool lResult = true;
            foreach (ConfigInfoViewModel lSourceConfigInfo in aSourceList)
            {
                if (!aTargetList.Contains(lSourceConfigInfo))
                {
                    lSourceConfigInfo.mCompareResult = Utility.Deleted;
                    lResult = false;
                }
            }

            foreach (ConfigInfoViewModel lTargetConfigInfo in aTargetList)
            {
                if (!aSourceList.Contains(lTargetConfigInfo))
                {
                    lTargetConfigInfo.mCompareResult = Utility.New;
                    aSourceList.Add(lTargetConfigInfo);
                    lResult = false;
                }
            }

            return lResult;
        }

        private bool CompareSWConfigList(ObservableCollection<SWConfigViewModel> aSourceList, 
            ObservableCollection<SWConfigViewModel> aTargetList)
        {
            bool lResult = true;
            foreach (SWConfigViewModel lSourceSWConfig in aSourceList)
            {
                if (!aTargetList.Contains(lSourceSWConfig))
                {
                    lSourceSWConfig.mCompareResult = Utility.Deleted;
                    lResult = false;
                }
            }

            foreach (SWConfigViewModel lTargetSWConfig in aTargetList)
            {
                if (!aSourceList.Contains(lTargetSWConfig))
                {
                    lTargetSWConfig.mCompareResult = Utility.New;
                    aSourceList.Add(lTargetSWConfig);
                    lResult = false;
                }
            }

            return lResult;
        }

        private bool CompareHWPartList(ObservableCollection<HWPartViewModel> aSourceList, 
            ObservableCollection<HWPartViewModel> aTargetList)
        {
            bool lResult = true;
            foreach (HWPartViewModel lSourceHWPart in aSourceList)
            {
                if (!aTargetList.Contains(lSourceHWPart))
                {
                    lSourceHWPart.mCompareResult = Utility.Deleted;
                    lResult = false;
                }
            }

            foreach (HWPartViewModel lTargetHWPart in aTargetList)
            {
                if (!aSourceList.Contains(lTargetHWPart))
                {
                    lTargetHWPart.mCompareResult = Utility.New;
                    aSourceList.Add(lTargetHWPart);
                    lResult = false;
                }
            }

            return lResult;
        }
*/
        private void CompareLists<T>(ObservableCollection<T> aSourceList, ObservableCollection<T> aTargetList)
        {
            foreach (T lSourceElement in aSourceList)
            {

                if (!aTargetList.Contains(lSourceElement))
                {
                    (lSourceElement as ViewModel).mCompareResult = Utility.Deleted;
                    this.mCompareResult = Utility.Modified;
                    this.mParent.mCompareResult = Utility.Modified;
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
                }
            }

            if(aSourceList is ObservableCollection<SWConfigViewModel>)
            {
                foreach (SWConfigViewModel lElement in aSourceList as ObservableCollection<SWConfigViewModel>)
                {
                    lElement.mSWConfigIndex += lElement.mCompareResult;
                }
            }
        }
    }
}
