using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ConfigInfoViewModel : ViewModel, ISerializable
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

        public ConfigInfoViewModel(ConfigInfo aConfigInfo)
        {
            mItemIndex = aConfigInfo.mItemIndex;
            mItemInfo = aConfigInfo.mItemInfo;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mItemIndex",mItemIndex);
            info.AddValue("mItemInfo", mItemInfo);
        }

        //Deserialization constructor.
        public ConfigInfoViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mItemIndex = (String)info.GetValue("mItemIndex", typeof(String));
            mItemInfo = (String)info.GetValue("mItemInfo", typeof(String));
        }
    }
}
