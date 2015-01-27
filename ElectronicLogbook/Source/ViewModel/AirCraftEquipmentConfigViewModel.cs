using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.Collections.ObjectModel;
using System.Security;
using System.Windows;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class AirCraftEquipmentConfigViewModel : TreeViewItemViewModel, ISerializable,
        IEquatable<AirCraftEquipmentConfigViewModel>

    {
        private String _ConfigName;

        [XmlAttribute("ConfigName")]
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

        public AirCraftEquipmentConfigViewModel() { }


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
            base.IsSelected = true;
            this.IsExpanded = true;
        }

        [SecurityCritical]
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

        public override Boolean Compare(ViewModel aTarget)
        {
            Boolean lIsChanged = false;
            foreach (SubEquipmentViewModel lSourceSubEquipment in this.mChildren) 
            {
                if((aTarget as AirCraftEquipmentConfigViewModel).mChildren.Contains(lSourceSubEquipment)) {
                    lIsChanged |= lSourceSubEquipment.Compare(
                        (aTarget as AirCraftEquipmentConfigViewModel).mChildren[(aTarget as AirCraftEquipmentConfigViewModel).mChildren.IndexOf(lSourceSubEquipment)]);
                }
                else
                {
                    lSourceSubEquipment.mCompareResult = Utility.Deleted;
                    this.mCompareResult = Utility.Modified;
                    lIsChanged = true;
                }
            }

            foreach (SubEquipmentViewModel lTargetSubEquipment in (aTarget as AirCraftEquipmentConfigViewModel).mChildren) 
            {
                if (!this.mChildren.Contains(lTargetSubEquipment)) 
                {
                    this.mCompareResult = Utility.Modified;
                    lTargetSubEquipment.mCompareResult = Utility.New;
                    this.mChildren.Add(lTargetSubEquipment);
                    lIsChanged = true;
                }
            }


            foreach (SubEquipmentViewModel lElement in this.mChildren)
            {
                lElement.mEquipmentID += lElement.mCompareResult;
                setVisibility(lElement);
            }

            return lIsChanged;
        }

        private void setVisibility(SubEquipmentViewModel aSubEquipment) 
        {
            if (aSubEquipment.mCompareResult == Utility.Modified)
            {
                foreach (HWPartViewModel lHWPart in aSubEquipment.mHWPartList)
                {
                    if (lHWPart.mCompareResult != String.Empty) {
                        aSubEquipment.mHWPartCompareResultColumnVisibility = Visibility.Visible;
                        break;
                    }
                }
                foreach (ConfigInfoViewModel lConfigInfo in aSubEquipment.mConfigInfoList)
                {
                    if (lConfigInfo.mCompareResult != String.Empty) {
                        aSubEquipment.mConfigInfoCompareResultColumnVisibility = Visibility.Visible;
                        break;
                    }
                }
            }
        }
    }
}
