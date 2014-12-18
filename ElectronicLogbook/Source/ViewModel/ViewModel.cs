using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public class ViewModel : INotifyPropertyChanged, ISerializable
    {
        private bool _isInEditMode;
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
            mCompareResult = String.Empty;
            this.IsInEditMode = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
        }

        //Deserialization constructor.
        public ViewModel(SerializationInfo info, StreamingContext ctxt)
        {
        }
    }
}
