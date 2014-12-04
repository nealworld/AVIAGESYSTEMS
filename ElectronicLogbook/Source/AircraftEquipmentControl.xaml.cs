﻿using ElectronicLogbook.ViewModel;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AircraftEquipmentControl.xaml
    /// </summary>
    public partial class AircraftEquipmentControl : UserControl
    {
        //AirCraftEquipmentConfigViewModel mAirCraftEquipmentConfigViewModel;
        public AircraftEquipmentControl()
        {
            InitializeComponent();
                   
            ThreadStart thr_start_func = new ThreadStart(First_Thread);
            Thread fThread = new Thread(thr_start_func) { IsBackground=true};
            fThread.Name = "first_thread";
            fThread.Start(); //starting the thread
        }

        public void First_Thread()
        {
            while (true)
            {
                Thread.Sleep(5000);
                foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in
                ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList)
                {
                    System.Diagnostics.Debug.WriteLine(lAirCraftEquipmentConfigViewModel.mConfigName + "    " +lAirCraftEquipmentConfigViewModel.IsInEditMode);
                    foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren)
                    {
                        System.Diagnostics.Debug.WriteLine("    " + lSubEquipment.mEquipmentID + "  " + lSubEquipment.IsInEditMode);
                        foreach (HWPartViewModel lHWPartViewModel in lSubEquipment.mHWPartList) 
                        {
                            System.Diagnostics.Debug.WriteLine("    " +
                                lHWPartViewModel.mHWPartIndex + "," + lHWPartViewModel.mHWPartNumber
                                + lHWPartViewModel.mHWPartDescription + "," + lHWPartViewModel.mHWPartSerialNumber+","
                                + lHWPartViewModel.mHWPartStatus);
                        }

                    }
                }

                /*foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in
                ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList)
                {
                    System.Diagnostics.Debug.WriteLine(lAirCraftEquipmentConfigViewModel.mConfigName);
                    foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren)
                    {
                        System.Diagnostics.Debug.WriteLine("    " + lSubEquipment.mEquipmentID);
                        etb.Text = "neal is goodassss!";

                    }
                }

                foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in
                    ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList)
                {
                    foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren)
                    {
                        lSubEquipment.mEquipmentID = "neal is bada!";
                    }
                }*/
            }
        }

        private void Rename(object sender, RoutedEventArgs e)
        {
            EditableTextBlock letb = (EditableTextBlock)((ContextMenu)(((MenuItem)sender).Parent)).PlacementTarget;
            letb.IsInEditMode = true;
        }

        private void CreateNewEquipment(object sender, RoutedEventArgs e) 
        {
            ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList.Add(
                new AirCraftEquipmentConfigViewModel(new AirCraftEquipmentConfig("NewEquipment",new List<SubEquipment>()),true));
        }

        private void CreateNewSubEquipment(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" enter xxxx ");
            AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as AirCraftEquipmentConfigViewModel;
            SubEquipment lnew = new SubEquipment();
            lnew.mEquipmentID = "NewSubEquipment";
            lAirCraftEquipmentConfigViewModel.mChildren.Add(new SubEquipmentViewModel(lnew, lAirCraftEquipmentConfigViewModel, true));

        }


        private void TreeViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (AircraftEquipmentConfigTreeView.SelectedItem is TreeViewItemViewModel && e.Key == Key.F2)
            {
                System.Diagnostics.Debug.WriteLine(" enter xxxx ");
                TreeViewItemViewModel lTreeViewItemModel = AircraftEquipmentConfigTreeView.SelectedItem as TreeViewItemViewModel;
                lTreeViewItemModel.IsInEditMode=true;
            }

        }

        private void RemoveEquipment(object sender, RoutedEventArgs e)
        {
            AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as AirCraftEquipmentConfigViewModel;
            ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList.Remove(lAirCraftEquipmentConfigViewModel);
        }

        private void RemoveSubEquipment(object sender, RoutedEventArgs e) 
        {
            SubEquipmentViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as SubEquipmentViewModel;
            lAirCraftEquipmentConfigViewModel.mParent.mChildren.Remove(lAirCraftEquipmentConfigViewModel);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HWPart_DataGrid.Items.Add(new HWPartViewModel(new HWPart()));
        }
    }
}
