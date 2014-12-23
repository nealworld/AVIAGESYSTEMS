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
