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
using ElectronicLogbook.ViewModel;
namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for DateTreeView.xaml
    /// </summary>
    public partial class DateTreeView : UserControl
    {
        public DateTreeView()
        {
            InitializeComponent();

            base.DataContext = ELBViewModel.mSingleton;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ELBViewModel.mSingleton.mDailyRemarkHandler.ShowRemarksOfSelectedDay(mDateTreeView.SelectedItem as DateViewModel);
        }
    }
}
