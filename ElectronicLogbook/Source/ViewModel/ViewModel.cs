using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region IsInEditMode
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
        #endregion

        public ViewModel() 
        {
            this.IsInEditMode = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
