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
    /// Interaction logic for RemarkPanel.xaml
    /// </summary>
    public partial class RemarkPanel : UserControl
    {
        public RemarkPanel()
        {
            InitializeComponent();

            base.DataContext = ELBViewModel.mSingleton;

            remarkSave.Click += new RoutedEventHandler(ELBViewModel.mSingleton.remarkSave_Click);
            remarkCancel.Click += new RoutedEventHandler(ELBViewModel.mSingleton.remarkCancel_Click);  
        }
    }
}
