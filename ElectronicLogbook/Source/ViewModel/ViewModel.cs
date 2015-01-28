using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Windows;
using System.Xml.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    [XmlInclude(typeof(AirCraftEquipmentConfigViewModel))]
    [XmlInclude(typeof(ConfigInfoViewModel))]
    [XmlInclude(typeof(ConfigurationViewModel))]
    [XmlInclude(typeof(DeviceDriverViewModel))]
    [XmlInclude(typeof(ELBViewModel))]
    [XmlInclude(typeof(HWPartViewModel))]
    [XmlInclude(typeof(SWConfigViewModel))]
    [XmlInclude(typeof(SWPartViewModel))]
    [XmlInclude(typeof(ThirdPartySoftwareViewModel))]
    [XmlInclude(typeof(TreeViewItemViewModel))]
    [XmlInclude(typeof(VAISParticipantViewModel ))]
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private bool _isInEditMode;

        [XmlIgnore()]
        public bool IsInEditMode
        {
            get { return _isInEditMode; }
            set
            {
                if (value != _isInEditMode)
                {
                    _isInEditMode = value;
                    this.OnPropertyChanged("IsInEditMode");
                }
            }
        }

        private String _CompareResult;

        [XmlIgnore()]
        public String mCompareResult 
        { 
            set
            {
                _CompareResult = value;
                this.OnPropertyChanged("mCompareResult");
            }
            get 
            {
                return _CompareResult;
            } 
        }

        public ViewModel() 
        {
            this.mCompareResult = String.Empty;
            this.IsInEditMode = false;
        }

        public abstract Boolean Compare(ViewModel aViewModel);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
