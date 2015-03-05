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

        private const String mDirName = "ELBRemark";
        private const String mRemarkDateFile = "RemarkDates.txt";
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
            String lyear = String.Empty;
            String lmonth = String.Empty;
            String lday = String.Empty;
            DateViewModel lDate_year;
            DateViewModel lDate_month;
            DateViewModel lDate_day;
            char[] lsplit = { '-', ',' };

            if (!File.Exists(mDirName + "\\" + mRemarkDateFile)) {
                return;
            }

            foreach (String line in File.ReadLines(mDirName + "\\" + mRemarkDateFile))
            {
                if (line == String.Empty) continue;

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

        private void CreateTodayRemark()
        {
            DailyRemarkViewModel lTodayRemark = new DailyRemarkViewModel("New Remark");
            lTodayRemark.mTime = DateTime.UtcNow.ToLocalTime().ToString();
            mDailyRemarks.Add(lTodayRemark);

            MakeDateDir(mDirName+"\\"+lTodayRemark.mRemarkFileDir);

        }

        private void MakeDateDir(String aDir)
        {
            if (!Directory.Exists(aDir))
            {
                Directory.CreateDirectory(aDir);
            }
        }

        private bool isFileExist(DailyRemarkViewModel aRemak)
        {
            if (Directory.Exists(mDirName+"\\"+aRemak.mRemarkFileDir))
            {
                String[] lFiles = Directory.GetFiles(mDirName + "\\" + aRemak.mRemarkFileDir);
                foreach (String lFileName in lFiles) {
                    DailyRemarkViewModel ltemp = new DailyRemarkViewModel();
                    Utility.DeSerializeFromXML(ref ltemp,lFileName);
                    if (ltemp.mTime == aRemak.mTime)
                    {
                        return true;
                    }
                }
                return false;
            }
            else {
                return false;
            }
        }

        /*
         This is a temp solution that save the remark dates to file at once when the remark dates are changed
        */
        private void SaveRemarkDatesToFile()
        {
            FileStream lfs = new FileStream(mDirName + "\\" + mRemarkDateFile, FileMode.Create);
            StreamWriter lsw = new StreamWriter(lfs);
            String[] lContent = ConvertRemarkDateToString(mRemarkDates).Split(';');
            foreach(String lTemp in lContent)
            {
                lsw.WriteLine(lTemp);
            }
            lsw.Flush();
            lsw.Close();
            lfs.Close();  
        }

        private string ConvertRemarkDateToString(ObservableCollection<DateViewModel> mRemarkDates)
        {
            String lResult = String.Empty;

            foreach (DateViewModel lYearDate in mRemarkDates)
            {
                foreach (DateViewModel lMonthDate in lYearDate.mChildren)
                {
                    foreach (DateViewModel lDayDate in lMonthDate.mChildren)
                    {
                        foreach (String lfile in lDayDate.mFileName) 
                        {
                            lResult += lYearDate.mTime + "-" + lMonthDate.mTime +
                                "-" + lDayDate.mTime + "," + lfile + ';';
                        }
                    }
                }
            }

            return lResult;
        }

        private void MakeTopDir()
        {
            try
            {
                if (!Directory.Exists(mDirName))
                {
                    Directory.CreateDirectory(mDirName);
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
            String lfile = mDirName+"\\"+aDailyRemark.mRemarkFileDir + "\\" + aDailyRemark.mTestName;
            String[] lDateStr = (aDailyRemark.mRemarkFileDir).Split('\\');
            try
            {
                if (File.Exists(lfile))
                {
                    File.Delete(lfile);
                    String lDayDir = mDirName + "\\" + lDateStr[0] + "\\" + lDateStr[1] + "\\" + lDateStr[2];
                    String lMonthDir = mDirName + "\\" + lDateStr[0] + "\\" + lDateStr[1];
                    String lYearDir = mDirName + "\\" + lDateStr[0];
                    DeleteEmptyDirs(lDayDir, lMonthDir, lYearDir);
                }
            }
            catch (Exception e) {
                System.Windows.Forms.MessageBox.Show("Delete file failed!", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void deleteRemarkDate(DailyRemarkViewModel aDailyRemark)
        {
            String[] lDateStr = (aDailyRemark.mRemarkFileDir).Split('\\');
            String lYear = lDateStr[0];
            String lMonth = lDateStr[1];
            String lDay = lDateStr[2];

            foreach (DateViewModel lYearDate in mRemarkDates) {
                if (lYearDate.mTime == lYear)
                {
                    foreach (DateViewModel lMonthDate in lYearDate.mChildren) {
                        if (lMonthDate.mTime == lMonth) {
                            foreach (DateViewModel lDayDate in lMonthDate.mChildren) {
                                if (lDayDate.mTime == lDay) {
                                    if (lDayDate.mFileName.Remove(aDailyRemark.mTestName))
                                    {
                                        if (lDayDate.mFileName.Count == 0)
                                        {
                                            lMonthDate.mChildren.Remove(lDayDate);
                                            if (lMonthDate.mChildren.Count == 0)
                                            {
                                                lYearDate.mChildren.Remove(lMonthDate);
                                                if (lYearDate.mChildren.Count == 0)
                                                {
                                                    mRemarkDates.Remove(lYearDate);
                                                }
                                            }
                                        }
                                        SaveRemarkDatesToFile();
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

        private bool SaveRemarkFile(DailyRemarkViewModel aDailyRemark)
        {

            if (!Directory.Exists(mDirName + "\\" + aDailyRemark.mRemarkFileDir))
            {
                Directory.CreateDirectory(mDirName + "\\" + aDailyRemark.mRemarkFileDir);
            }

            if (Utility.SerializeToXML(aDailyRemark, mDirName + "\\" + aDailyRemark.mRemarkFileDir + "\\" + aDailyRemark.mTestName))
            {
                aDailyRemark.mIsModified = String.Empty;
                return true;
            }
            else {
                System.Windows.Forms.MessageBox.Show("Save file failed!", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return false;
            }
        }

        /*
         * return false means the remark date has been modifed
         * return true means the remark date has NOT been modifed
         */
        private bool SaveRemarkDate(DailyRemarkViewModel aDailyRemark)
        {
            String[] lDateStr = (aDailyRemark.mRemarkFileDir).Split('\\');
            String lYear = lDateStr[0];
            String lMonth = lDateStr[1];
            String lDay = lDateStr[2];

            DateViewModel lYearFound = null;
            DateViewModel lMonthFound = null;
            DateViewModel lDayFound = null;
            String lFileFound = String.Empty;

            foreach (DateViewModel lYearDate in mRemarkDates)
            {
                if (lYearDate.mTime == lYear)
                {
                    lYearFound = lYearDate;
                    break;
                }
            }

            if (lYearFound == null)
            {
                lYearFound = new DateViewModel(null, lYear);
                lMonthFound = new DateViewModel(lYearFound, lMonth);
                lDayFound = new DateViewModel(lMonthFound, lDay);
                lDayFound.mFileName.Add(aDailyRemark.mTestName);
                lYearFound.mChildren.Add(lMonthFound);
                lMonthFound.mChildren.Add(lDayFound);
                mRemarkDates.Add(lYearFound);
                return false;
            }

            foreach (DateViewModel lMonthDate in lYearFound.mChildren)
            {
                if (lMonthDate.mTime == lMonth)
                {
                    lMonthFound = lMonthDate;
                    break;
                }
            }

            if (lMonthFound == null) 
            {
                lMonthFound = new DateViewModel(lYearFound, lMonth);
                lDayFound = new DateViewModel(lMonthFound, lDay);
                lDayFound.mFileName.Add(aDailyRemark.mTestName);
                mRemarkDates.Add(lYearFound);
                return false;
            }

            foreach (DateViewModel lDatDate in lMonthFound.mChildren)
            {
                if (lDatDate.mTime == lDay)
                {
                    lDayFound = lDatDate;
                    break;
                }
            }

            if (lDayFound == null) 
            {
                lDayFound = new DateViewModel(lMonthFound, lDay);
                lDayFound.mFileName.Add(aDailyRemark.mTestName);
                mRemarkDates.Add(lYearFound);
                return false;
            }

            foreach (String lFileName in lDayFound.mFileName)
            {
                if (lFileName == aDailyRemark.mTestName)
                {
                    lFileFound = lFileName;
                    break;
                }
            }

            if (lFileFound == String.Empty) {
                lDayFound.mFileName.Add(aDailyRemark.mTestName);
                return false;
            }

            return true;
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

        public bool isAnyUnsavedReamrk() {
            bool lresult = false;

            foreach (DailyRemarkViewModel ltemp in mDailyRemarks)
            {
                if (ltemp.mIsModified != String.Empty)
                {
                    lresult = true;
                    break;
                }
            }

            return lresult;
        }

        private bool ClearCurrentDailyRemarks()
        {
            bool lClear = true;
            if (isAnyUnsavedReamrk())
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure to dump unsaved remark files?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    lClear = false;
                }
            }

            if (lClear)
            {
                mDailyRemarks.Clear();
            }

            return lClear;
        }

        public DailyRemakrHandler()
        {
            MakeTopDir();
            mRemarkDates = new ObservableCollection<DateViewModel>();
            mDailyRemarks = new ObservableCollection<DailyRemarkViewModel>();
            GetAllRemarkDates();
            CreateTodayRemark();
        }

        internal void addNewRemarkExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateTodayRemark();
        }

        internal void deleteRemarkExpander(object sender, System.Windows.RoutedEventArgs e)
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
                        deleteRemarkDate(ltemp);
                        break;
                    }
                }
            }

            if (mDailyRemarks.Count == 0) {
                CreateTodayRemark();
            }
        }

        internal void saveRemarkExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            RemarkExpander lremarkExpander = GetRemarkExpanderInstance(sender);

            if (lremarkExpander != null) {
                foreach (DailyRemarkViewModel ltemp in mDailyRemarks)
                {
                    if (lremarkExpander.GetTime() == ltemp.mTime)
                    {
                        if (!isFileExist(ltemp))
                        {
                            String value = "New Remark";
                            while (Utility.InputBox("New document", "New document name:", ref value) == DialogResult.OK)
                            {
                                String[] lfiles = Directory.GetFiles(mDirName + "\\" + ltemp.mRemarkFileDir);
                                bool lfound = false;
                                foreach (String lfile in lfiles)
                                {
                                    if (lfile.Split('\\')[4] == value)
                                    {
                                        lfound = true;
                                        break;
                                    }
                                }
                                if (lfound)
                                {
                                    System.Windows.Forms.MessageBox.Show("File already exist", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    ltemp.mTestName = value;
                                    if (SaveRemarkFile(ltemp))
                                    {
                                        if (!SaveRemarkDate(ltemp))
                                        {
                                            SaveRemarkDatesToFile();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (SaveRemarkFile(ltemp))
                            {
                                if (!SaveRemarkDate(ltemp))
                                {
                                    SaveRemarkDatesToFile();
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        public void ShowRemarksOfSelectedDay(System.Windows.Controls.TreeView aTreeView)
        {
            DateViewModel lDate = aTreeView.SelectedItem as DateViewModel;

            if (lDate != null) {
                if (lDate.mParent == null) return;
                if (lDate.mParent.mParent == null) return;
                if (!ClearCurrentDailyRemarks())
                {
                    return;
                }
                if (lDate.mFileName.Count != 0)
                {
                    String lDir = mDirName + "\\" + (lDate.mParent.mParent as DateViewModel).mTime + "\\"
                        + (lDate.mParent as DateViewModel).mTime + "\\" +
                        lDate.mTime;
                    foreach (String lfile in lDate.mFileName)
                    {
                        DailyRemarkViewModel lremark = new DailyRemarkViewModel();
                        if (Utility.DeSerializeFromXML(ref lremark, lDir + "\\" + lfile))
                        {
                            lremark.mIsModified = String.Empty;
                            mDailyRemarks.Add(lremark);
                        }
                    }
                }
            }
        }
    }
}
