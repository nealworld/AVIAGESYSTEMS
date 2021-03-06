﻿using System;
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
            base.DataContext = ELBViewModel.mSingleton.mConfigurationViewModel;
            RemarkPanel.DataContext = ELBViewModel.mSingleton;

            GetCurrentConfigration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.GetCurrentConfigration_Click);
            NewConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.NewConfiguration_Click);
            OpenConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.OpenConfiguration_Click);
            SaveConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.SaveConfiguration_Click);
            CompareConfiguration.Click += new RoutedEventHandler(ELBViewModel.mSingleton.CompareConfiguration_Click);
            remarkSave.Click += new RoutedEventHandler(ELBViewModel.mSingleton.remarkSave_Click);
            remarkCancel.Click += new RoutedEventHandler(ELBViewModel.mSingleton.remarkCancel_Click);  
        }

        private void openRemark_Click(object sender, RoutedEventArgs e)
        {
            RemarkPanel.Visibility = Visibility.Visible;
            ConfigurationTab.Visibility = Visibility.Collapsed;
        }

        private void ShowConfiguration_click(object sender, RoutedEventArgs e)
        {
            RemarkPanel.Visibility = Visibility.Collapsed;
            ConfigurationTab.Visibility = Visibility.Visible;
        }


    }
}
