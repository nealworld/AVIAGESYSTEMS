using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ElectronicLogbook.ViewModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows;

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

        private void createTodayRemark()
        {
            String lYear = String.Empty;
            String lMonth = String.Empty;
            String lDay = String.Empty;
            
            DailyRemarkViewModel lTodayRemark = new DailyRemarkViewModel("New Remark");
            lTodayRemark.mTime = DateTime.UtcNow.ToLocalTime().ToString();
            mDailyRemarks.Add(lTodayRemark);

            parseDate(ref lYear, ref lMonth, ref lDay, lTodayRemark.mTime);
            MakeDateDir(lYear,lMonth,lDay);

        }

        private void MakeDateDir(string lYear, string lMonth, string lDay)
        {
            if (!Directory.Exists(dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay)) {
                Directory.CreateDirectory(dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay);
            }
        }

        private void parseDate(ref string aYear, ref string aMonth, ref string aDay, String aDate)
        {
            String[] lDate = aDate.Split('/');
            aYear = (lDate[2].Split(' '))[0];
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
                System.Windows.Forms.MessageBox.Show("Can not create remark dirctory!", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }

        private RemarkExpander GetRemarkExpanderInstance(object asender)
        {
            DockPanel dp = LogicalTreeHelper.GetParent(asender as DependencyObject) as DockPanel;
            Expander lexpander = (Expander)LogicalTreeHelper.GetParent(dp);
            RemarkExpander lremarkExpander = LogicalTreeHelper.GetParent(lexpander) as RemarkExpander;
            return lremarkExpander;
        }

        private void deleteRemarkFile(DailyRemarkViewModel aDailyRemark)
        {
            String lYear = String.Empty;
            String lMonth = String.Empty;
            String lDay = String.Empty;
            parseDate(ref lYear, ref lMonth, ref lDay, aDailyRemark.mTime);

            String lfile = dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay + "\\" + aDailyRemark.mTestName;
            try
            {
                if (File.Exists(lfile))
                {
                    File.Delete(lfile);
                    String lDayDir = dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay;
                    String lMonthDir = dirName + "\\" + lYear + "\\" + lMonth;
                    String lYearDir = dirName + "\\" + lYear;
                    DeleteEmptyDirs(lDayDir, lMonthDir, lYearDir);
                }
            }
            catch (Exception e) {
                System.Windows.Forms.MessageBox.Show("Delete file failed!", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void deleteRecordFromDateTree(DailyRemarkViewModel aDailyRemark)
        {
            String lYear = String.Empty;
            String lMonth = String.Empty;
            String lDay = String.Empty;
            parseDate(ref lYear, ref lMonth, ref lDay, aDailyRemark.mTime);

            String lfile = dirName + "\\" + lYear + "\\" + lMonth + "\\" + lDay + "\\" + aDailyRemark.mTestName;

            if (File.Exists(lfile)) {
                foreach (DateViewModel lYearDate in mRemarkDates) {
                    if (lYearDate.mTime == lYear)
                    {
                        foreach (DateViewModel lMonthDate in lYearDate.mChildren) {
                            if (lMonthDate.mTime == lMonth) {
                                foreach (DateViewModel lDayDate in lMonthDate.mChildren) {
                                    if (lDayDate.mTime == lDay) {
                                        lDayDate.mFileName.Remove(aDailyRemark.mTestName);
                                        if (lDayDate.mFileName.Count == 0) {
                                            lMonthDate.mChildren.Remove(lDayDate);
                                            if (lMonthDate.mChildren.Count == 0) {
                                                lYearDate.mChildren.Remove(lMonthDate);
                                                if (lYearDate.mChildren.Count == 0) {
                                                    mRemarkDates.Remove(lYearDate);
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void DeleteEmptyDirs(string aDayDir, string aMonthDir, string aYearDir)
        {
            if (Directory.GetFiles(aDayDir).Length == 0) {
                Directory.Delete(aDayDir);
                if (Directory.GetDirectories(aMonthDir).Length == 0) {
                    Directory.Delete(aMonthDir);
                    if (Directory.GetDirectories(aYearDir).Length == 0) {
                        Directory.Delete(aYearDir);
                    }
                }
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

        internal void addNewRemark(object sender, System.Windows.RoutedEventArgs e)
        {
            createTodayRemark();
        }

        internal void deleteRemark(object sender, System.Windows.RoutedEventArgs e)
        {

            RemarkExpander lremarkExpander = GetRemarkExpanderInstance(sender);

            if (lremarkExpander != null)
            {
                foreach (DailyRemarkViewModel ltemp in mDailyRemarks)
                {
                    if (lremarkExpander.GetTime() == ltemp.mTime)
                    {
                        mDailyRemarks.Remove(ltemp);
                        deleteRemarkFile(ltemp);
                        deleteRecordFromDateTree(ltemp);
                        break;
                    }
                }
            }

        }

        internal void saveRemark(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
