using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;
using System.Security;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class DeviceDriverViewModel : ViewModel, ISerializable, IEquatable<DeviceDriverViewModel>
    {
        private String _DeviceName;

        [XmlAttribute("DeviceName")]
        public String mDeviceName 
        {
            set 
            {
                _DeviceName = value;
                this.OnPropertyChanged("mDeviceName");
            }
            get { return _DeviceName; }
        }

        private String _DriverName;

        [XmlAttribute("DriverName")]
        public String mDriverName 
        {
            set 
            {
                _DriverName = value;
                this.OnPropertyChanged("mDriverName");
            }
            get { return _DriverName; }
        }

        private String _DriverVersionNumber;

        [XmlAttribute("DriverVersionNumber")]
        public String mDriverVersionNumber 
        {
            set 
            {
                _DriverVersionNumber = value;
                this.OnPropertyChanged("mDriverVersionNumber");
            }
            get { return _DriverVersionNumber; }
        }

        private String _DriverLocation;

        [XmlAttribute("DriverLocation")]
        public String mDriverLocation 
        {
            set 
            {
                _DriverLocation = value;
                this.OnPropertyChanged("mDriverLocation");
            }
            get { return _DriverLocation; }
        }

        public DeviceDriverViewModel(DeviceDriver aDeviceDriver) 
        {
            mDeviceName = aDeviceDriver.mDeviceName;
            mDriverName = aDeviceDriver.mDriverName;
            mDriverVersionNumber = aDeviceDriver.mDriverVersionNumber;
            mDriverLocation = aDeviceDriver.mDriverLocation;
        }

        public DeviceDriverViewModel() {
            mCompareResult = String.Empty;
            mDeviceName = String.Empty;
            mDriverName = String.Empty;
            mDriverVersionNumber = String.Empty;
            mDriverLocation = String.Empty;
        }

        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mCompareResult", mCompareResult);
            info.AddValue("mDeviceName", mDeviceName);
            info.AddValue("mDriverName",mDriverName);
            info.AddValue("mDriverVersionNumber",mDriverVersionNumber);
            info.AddValue("mDriverLocation",mDriverLocation);
        }

        public DeviceDriverViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            mCompareResult = (String)info.GetValue("mCompareResult", typeof(String));
            mDeviceName = (String)info.GetValue("mDeviceName", typeof(String));
            mDriverName = (String)info.GetValue("mDriverName", typeof(String));
            mDriverVersionNumber = (String)info.GetValue("mDriverVersionNumber", typeof(String));
            mDriverLocation = (String)info.GetValue("mDriverLocation", typeof(String));
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
            return mDeviceName.GetHashCode();
        }

        public bool Equals(DeviceDriverViewModel aOther)
        {
            if (aOther == null) return false;
            return (mDeviceName + mDriverName). Equals(aOther.mDeviceName + aOther.mDriverName);
        }

        public override Boolean Compare(ViewModel aTarget)
        {
            Boolean lIsChanged = false;

            if ((aTarget as DeviceDriverViewModel).mDriverLocation != this.mDriverLocation) 
            {
                this.mCompareResult = Utility.Modified;
                this.mDriverLocation += "(" + (aTarget as DeviceDriverViewModel).mDriverLocation + ")";
                lIsChanged = true;
            }

            if ((aTarget as DeviceDriverViewModel).mDriverVersionNumber != this.mDriverVersionNumber)
            {
                this.mCompareResult = Utility.Modified;
                this.mDriverVersionNumber += "(" + (aTarget as DeviceDriverViewModel).mDriverVersionNumber + ")";
                lIsChanged = true;
            }

            return lIsChanged;
        }
    }
}
