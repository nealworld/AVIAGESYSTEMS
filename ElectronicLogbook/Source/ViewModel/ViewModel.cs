using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security;
using System.Windows;

namespace ElectronicLogbook.ViewModel
{
    [System.Serializable()]
    public abstract class ViewModel : INotifyPropertyChanged
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

        private Visibility _IsComparing;
        public Visibility mIsComparing
        {
            set
            {
                _IsComparing = value;
                this.OnPropertyChanged("mIsComparing");
            }
            get
            {
                return _IsComparing;
            }
        }

        public ViewModel() 
        {
            this.mCompareResult = String.Empty;
            this.IsInEditMode = false;
            this.mIsComparing = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*[SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        { 
        }

        //Deserialization constructor.
        public ViewModel(SerializationInfo info, StreamingContext ctxt)
        {
        }*/
    }
}
