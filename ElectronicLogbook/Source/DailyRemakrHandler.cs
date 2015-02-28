using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ElectronicLogbook.ViewModel;
using System.ComponentModel;
using System.IO;

namespace ElectronicLogbook
{
    public class DailyRemakrHandler : INotifyPropertyChanged
    {
        private ObservableCollection<DateViewModel> _RemarkDates;
        public ObservableCollection<DateViewModel> mRemarkDates
        {
            get { return _RemarkDates; }
            set
            {
                _RemarkDates = value;
                this.OnPropertyChanged("mRemarkDates");
            }
        }

        private ObservableCollection<DailyRemark> _DailyRemark;
        public ObservableCollection<DailyRemark> mDailyRemarks
        {
            get { return _DailyRemark; }
            set
            {
                _DailyRemark = value;
                this.OnPropertyChanged("mDailyRemarks");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void GetAllRemarkDates()
        {
            // throw new NotImplementedException();
            String lyear = String.Empty;
            String lmonth = String.Empty;
            String lday = String.Empty;
            DateViewModel lDate_year;
            DateViewModel lDate_month;
            DateViewModel lDate_day;
            char[] lsplit = { '-', ',' };
            foreach (String line in File.ReadLines("RemarkDates.txt"))
            {
                String[] tokens = line.Split(lsplit);
                if (tokens[0] != lyear)
                {
                    lDate_year = new DateViewModel(null, tokens[0]);
                    lDate_month = new DateViewModel(lDate_year, tokens[1]);
                    lDate_day = new DateViewModel(lDate_month, tokens[2]);
                    lDate_year.mChildren.Add(lDate_month);
                    lDate_month.mChildren.Add(lDate_day);
                    lDate_day.mFileName.Add(tokens[3]);
                    mRemarkDates.Add(lDate_year);
                }
                else if (tokens[1] != lmonth)
                {
                    lDate_year = mRemarkDates[mRemarkDates.Count - 1];
                    lDate_month = new DateViewModel(lDate_year, tokens[1]);
                    lDate_day = new DateViewModel(lDate_month, tokens[2]);
                    lDate_year.mChildren.Add(lDate_month);
                    lDate_month.mChildren.Add(lDate_day);
                    lDate_day.mFileName.Add(tokens[3]);
                }
                else if (tokens[2] != lday)
                {
                    lDate_year = mRemarkDates[mRemarkDates.Count - 1];
                    lDate_month = lDate_year.mChildren[lDate_year.mChildren.Count - 1] as DateViewModel;
                    lDate_day = new DateViewModel(lDate_month, tokens[2]);
                    lDate_month.mChildren.Add(lDate_day);
                    lDate_day.mFileName.Add(tokens[3]);
                }
                else
                {
                    lDate_year = mRemarkDates[mRemarkDates.Count - 1];
                    lDate_month = lDate_year.mChildren[lDate_year.mChildren.Count - 1] as DateViewModel;
                    lDate_day = lDate_month.mChildren[lDate_month.mChildren.Count - 1] as DateViewModel;
                    lDate_day.mFileName.Add(tokens[3]);
                }
                lDate_year = mRemarkDates[mRemarkDates.Count - 1];
                lDate_month = lDate_year.mChildren[lDate_year.mChildren.Count - 1] as DateViewModel;
                lDate_day = lDate_month.mChildren[lDate_month.mChildren.Count - 1] as DateViewModel;
                lyear = lDate_year.mTime;
                lmonth = lDate_month.mTime;
                lday = lDate_day.mTime;
            }
        }

        public DailyRemakrHandler() 
        {
            mRemarkDates = new ObservableCollection<DateViewModel>();
            mDailyRemarks = new ObservableCollection<DailyRemark>();
            GetAllRemarkDates();
            DailyRemark TEMP = new DailyRemark("Test1");
            TEMP.mTime = "2015-01-30";
            mDailyRemarks.Add(TEMP);

            TEMP = new DailyRemark("Test2");
            TEMP.mTime = "2015-02-23";
            mDailyRemarks.Add(TEMP);

            TEMP = new DailyRemark("Test3");
            TEMP.mTime = "2015-02-24";
            mDailyRemarks.Add(TEMP);
        }
    }
}
