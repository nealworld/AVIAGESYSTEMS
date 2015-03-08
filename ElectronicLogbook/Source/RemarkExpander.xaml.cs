using ElectronicLogbook.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for RemarkExpander.xaml
    /// </summary>
    public partial class RemarkExpander : UserControl
    {
        public RemarkExpander()
        {
            InitializeComponent();
        }

        private void addNewRemarkExpander(object sender, System.Windows.RoutedEventArgs e) {
            ELBViewModel.mSingleton.mDailyRemarkHandler.addNewRemark();
        }

        private void deleteRemarkExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            ELBViewModel.mSingleton.mDailyRemarkHandler.deleteRemark(TimeTextBox.Text);
        }

        private void saveRemarkExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            ELBViewModel.mSingleton.mDailyRemarkHandler.saveRemark(TimeTextBox.Text);
        }

    }
}
