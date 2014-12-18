using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.Collections.ObjectModel;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class AirCraftEquipmentConfigViewModel : TreeViewItemViewModel, ISerializable,
        IEquatable<AirCraftEquipmentConfigViewModel>, IComparable<AirCraftEquipmentConfigViewModel>

    {
        private String _ConfigName;
        public String mConfigName{
            set
            {
                _ConfigName = value;
                this.OnPropertyChanged("mConfigName");
            }
            get 
            {
                return _ConfigName;
            }
        }

        private void Initialize(AirCraftEquipmentConfig aAirCraftEquipmentConfig)
        {
            mConfigName = aAirCraftEquipmentConfig.mConfigName;
            foreach (SubEquipment lSubEquipment in aAirCraftEquipmentConfig.mSubEquipmentList)
            {
                mChildren.Add(new SubEquipmentViewModel(lSubEquipment, this));
            }
        }
        public AirCraftEquipmentConfigViewModel(AirCraftEquipmentConfig aAirCraftEquipmentConfig)
            :base(null)
        {
            Initialize(aAirCraftEquipmentConfig);
        }

        public AirCraftEquipmentConfigViewModel(AirCraftEquipmentConfig aAirCraftEquipmentConfig, bool aIsInEditMode)
            : base(null)
        {
            Initialize(aAirCraftEquipmentConfig);
            base.IsInEditMode = aIsInEditMode;

        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mConfigName",mConfigName);
            info.AddValue("mChildren", mChildren);
            info.AddValue("mParent", mParent);
        }

        public AirCraftEquipmentConfigViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mConfigName = (String)info.GetValue("mConfigName", typeof(String));
            mChildren = (ObservableCollection<TreeViewItemViewModel>)info.
                GetValue("mChildren", typeof(ObservableCollection<TreeViewItemViewModel>));
            mParent = (TreeViewItemViewModel)info.GetValue("mParent", typeof(TreeViewItemViewModel));
        }

        public override bool Equals(object aobj)
        {
            if (aobj == null) return false;
            AirCraftEquipmentConfigViewModel lobj = aobj as AirCraftEquipmentConfigViewModel;
            if (lobj == null) return false;
            else return Equals(lobj);
        }

        public override int GetHashCode()
        {
            return mConfigName.GetHashCode();
        }

        public bool Equals(AirCraftEquipmentConfigViewModel aOther)
        {
            if (aOther == null) return false;
            return (this.mConfigName.Equals(aOther.mConfigName));
        }

        public int CompareTo(AirCraftEquipmentConfigViewModel aOther)
        {
            if (aOther == null) return 1;
            return this.mConfigName.CompareTo(aOther.mConfigName);
        }

        public void Compare(AirCraftEquipmentConfigViewModel aTarget)
        {
            foreach (SubEquipmentViewModel lSourceSubEquipment in this.mChildren) 
            {
                bool lFound = false;
                if(aTarget.mChildren.Contains(lSourceSubEquipment)) {
                    lSourceSubEquipment.Compare(
                        aTarget.mChildren[aTarget.mChildren.IndexOf(lSourceSubEquipment)] 
                        as SubEquipmentViewModel);
                    lFound = true;
                }
                if (!lFound) 
                {
                    lSourceSubEquipment.mEquipmentID += Utility.Deleted;
                    mCompareResult = Utility.Modified;
                }
            }

            foreach (SubEquipmentViewModel lTargetSubEquipment in aTarget.mChildren) 
            {
                if (!this.mChildren.Contains(lTargetSubEquipment)) 
                {
                    mCompareResult = Utility.Modified;
                    lTargetSubEquipment.mEquipmentID += Utility.New;
                    this.mChildren.Add(lTargetSubEquipment);
                }
            }

            if (mCompareResult != String.Empty) 
            {
                this.mConfigName += mCompareResult;
            }
        }
    }
}
