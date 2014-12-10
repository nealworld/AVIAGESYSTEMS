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
using System.Windows.Shapes;
using ElectronicLogbook.ViewModel;
using ElectronicLogbookDataLib;
namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigurationWindow.DataContext = ELBViewModel.mSingleton;

            ShowCurrentConfigMenuItem.Click += new RoutedEventHandler(ELBViewModel.mSingleton.GetCurrentConfiguration_MenuItemClick);
            ShowExpectConfigMenuItem.Click += new RoutedEventHandler(ELBViewModel.mSingleton.GetExpectConfiguration_MenuItemClick );
        }
    }
}
