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
    /// Interaction logic for DateTreeView.xaml
    /// </summary>
    public partial class DateTreeView : UserControl
    {
        public DateTreeView()
        {
            InitializeComponent();

            base.DataContext = ElectronicLogbook.ViewModel.ELBViewModel.mSingleton;
        }
    }
}
