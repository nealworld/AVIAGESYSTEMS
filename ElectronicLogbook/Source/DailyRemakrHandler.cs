using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ElectronicLogbook.ViewModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ElectronicLogbook
{
    public class DailyRemakrHandler : INotifyPropertyChanged
    {

        private const String dirName = "ELBRemark";
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

        private ObservableCollection<DailyRemarkViewModel> _DailyRemark;
        public ObservableCollection<DailyRemarkViewModel> mDailyRemarks
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
            MakeTopDir();
            mRemarkDates = new ObservableCollection<DateViewModel>();
            mDailyRemarks = new ObservableCollection<DailyRemarkViewModel>();
            GetAllRemarkDates();
            createTodayRemark();
        }

        private void createTodayRemark()
        {
            String lYear = String.Empty;
            String lMonth = String.Empty;
            String lDay = String.Empty;
            
            parseDate(ref lYear, ref lMonth, ref lDay);
            MakeDateDir(lYear,lMonth,lDay);

            DailyRemarkViewModel lTodayRemark = new DailyRemarkViewModel("New Remark");
            lTodayRemark.mTime = DateTime.UtcNow.ToLocalTime().ToString();
            mDailyRemarks.Add(lTodayRemark);

        }

        private void MakeDateDir(string lYear, string lMonth, string lDay)
        {
            if (!Directory.Exists(dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay)) {
                Directory.CreateDirectory(dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay);
            }
        }

        private void parseDate(ref string aYear, ref string aMonth, ref string aDay)
        {
            String lToday = DateTime.Today.ToShortDateString();
            String[] lDate = lToday.Split('/');
            aYear = lDate[2];
            aMonth = lDate[0];
            aDay = lDate[1];
        }

        private void MakeTopDir()
        {
            try
            {
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
            }
            catch(Exception e) {
                MessageBox.Show("Can not create remark dirctory!", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }



        internal void addNewRemark(object sender, System.Windows.RoutedEventArgs e)
        {
            createTodayRemark();
        }

        internal void deleteRemark(object sender, System.Windows.RoutedEventArgs e)
        {
            RemarkExpander temp = (RemarkExpander)((sender as Button).Parent as UserControl);
            if (temp != null) {
                System.Diagnostics.Debug.WriteLine(temp.TemplatedParent.GetType().ToString());
            }
        }

        internal void saveRemark(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
