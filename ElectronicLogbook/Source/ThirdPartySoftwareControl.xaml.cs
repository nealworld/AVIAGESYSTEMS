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
    /// Interaction logic for ThirdPartySoftwareControl.xaml
    /// </summary>
    public partial class ThirdPartySoftwareControl : UserControl
    {
        public ThirdPartySoftwareControl()
        {
            InitializeComponent();

            base.DataContext = ELBViewModel.getInstance().mThirdPartySoftwareListViewModel;
        }
    }
}
