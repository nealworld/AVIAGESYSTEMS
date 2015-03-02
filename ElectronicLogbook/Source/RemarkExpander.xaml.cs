using ElectronicLogbook.ViewModel;
using System;
using System.Collections.Generic;
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

            addButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.addNewRemark);
            deleteButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.deleteRemark);
            saveButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.saveRemark);
        }

    }
}
