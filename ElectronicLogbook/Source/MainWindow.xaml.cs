using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElectronicLogbook.ViewModel;
using ElectronicLogbookDataLib;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows;
namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConfigurationPanel mConfigurationPanel;
        RemarkPanel mRemarkPanel;
        UserSeesionHandler mUsersession;

        public MainWindow()
        {
            mUsersession = new UserSeesionHandler();
            if (!mUsersession.StartSession())
            {
                System.Windows.Forms.Application.Exit();
            }

            InitializeComponent();
            base.DataContext = ELBViewModel.mSingleton;
            GetCurrentConfigration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.GetCurrentConfigration_Click);
            NewConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.NewConfiguration_Click);
            OpenConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.OpenConfiguration_Click);
            SaveConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.SaveConfiguration_Click);
            CompareConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.CompareConfiguration_Click);

            mConfigurationPanel = new ConfigurationPanel();
            mRemarkPanel = new RemarkPanel();
            this.Closing +=MainWindow_Closing;
            this.ContentControl.Content = mConfigurationPanel;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ELBViewModel.mSingleton.mDailyRemarkHandler.isAnyUnsavedReamrk())
            {
                if (System.Windows.Forms.MessageBox.Show("Are you sure to dump unsaved remark files?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            if (!e.Cancel)
            {
                mUsersession.EndSession();
            }
        }

        private void openRemark_Click(object sender, RoutedEventArgs e)
        {
            this.ContentControl.Content = mRemarkPanel;
        }

        private void ShowConfiguration_click(object sender, RoutedEventArgs e)
        {
            this.ContentControl.Content = mConfigurationPanel;
        }


    }
}
