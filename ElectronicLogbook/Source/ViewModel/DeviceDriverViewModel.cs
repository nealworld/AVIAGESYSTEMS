using System;
using System.Runtime.Serialization;
using ElectronicLogbookDataLib;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class DeviceDriverViewModel : ViewModel, ISerializable
    {
        private String _DeviceName; 
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

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mDeviceName", mDeviceName);
            info.AddValue("mDriverName",mDriverName);
            info.AddValue("mDriverVersionNumber",mDriverVersionNumber);
            info.AddValue("mDriverLocation",mDriverLocation);
        }

        //Deserialization constructor.
        public DeviceDriverViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mDeviceName = (String)info.GetValue("mDeviceName", typeof(String));
            mDriverName = (String)info.GetValue("mDriverName", typeof(String));
            mDriverVersionNumber = (String)info.GetValue("mDriverVersionNumber", typeof(String));
            mDriverLocation = (String)info.GetValue("mDriverLocation", typeof(String));
        }
    }
}
