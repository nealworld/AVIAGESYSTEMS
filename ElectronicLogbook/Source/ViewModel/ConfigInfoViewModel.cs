using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ConfigInfoViewModel : ViewModel, ISerializable, IEquatable<HWPartViewModel>
    {
        private String _ItemIndex;
        public String mItemIndex 
        {
            set 
            {
                _ItemIndex = value;
                this.OnPropertyChanged("mItemIndex");
            }
            get 
            {
                return _ItemIndex;
            }
        }

        private String _ItemInfo;
        public String mItemInfo 
        {
            set 
            {
                _ItemInfo = value;
                this.OnPropertyChanged("mItemInfo");
            }
            get 
            {
                return _ItemInfo;
            }
        }

        public ConfigInfoViewModel() {
            mItemIndex = String.Empty;
            mItemInfo = String.Empty;
        }
        public ConfigInfoViewModel(ConfigInfo aConfigInfo)
        {
            mItemIndex = aConfigInfo.mItemIndex;
            mItemInfo = aConfigInfo.mItemInfo;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mItemIndex",mItemIndex);
            info.AddValue("mItemInfo", mItemInfo);
        }

        public ConfigInfoViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mItemIndex = (String)info.GetValue("mItemIndex", typeof(String));
            mItemInfo = (String)info.GetValue("mItemInfo", typeof(String));
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
            return (mItemIndex + mItemInfo).GetHashCode();
        }

        public bool Equals(ConfigInfoViewModel aOther)
        {
            if (aOther == null) return false;
            return (mItemIndex + mItemInfo).Equals(aOther.mItemIndex + aOther.mItemInfo );
        }
    }
}
