using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ElectronicLogbook.ViewModel
{
    public class DateViewModel : TreeViewItemViewModel
    {
        private String _time;
        public String mTime {
            get { return _time; }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    this.OnPropertyChanged("mTime");
                }
            }
        }

        private ObservableCollection<String> _filename;
        public ObservableCollection<String> mFileName
        {
            get { return _filename; }
            set
            {
                _filename = value;
                this.OnPropertyChanged("mFileName");
            }
        } 

        public DateViewModel(TreeViewItemViewModel aParent, String aTime)
            : base(aParent)
        {
            mTime = aTime;
            mFileName = new ObservableCollection<string>();
        }
    }
}
