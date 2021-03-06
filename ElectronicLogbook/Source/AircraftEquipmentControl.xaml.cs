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
            TopGrid.DataContext = ELBViewModel.mSingleton.mConfigurationViewModel;

           /* ThreadStart thr_start_func = new ThreadStart(First_Thread);
            Thread fThread = new Thread(thr_start_func) { IsBackground=true};
            fThread.Name = "first_thread";
            fThread.Start(); */ 
        }

        public void First_Thread()
        {
            
            while (true)
            {
                Thread.Sleep(15000);
                //ELBViewModel.mSingleton.mConfigurationViewModel.mIsEditable = true;
                //ELBViewModel.mSingleton.mConfigurationViewModel.mIsReadOnly = false;
                foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in
                ELBViewModel.mSingleton.mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList)
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

                        foreach (SWConfigViewModel lSWConfigViewModel in lSubEquipment.mSWConfigList)
                        {
                            string display = string.Empty;
                            display = "    " +
                                lSWConfigViewModel.mSWConfigIndex + "," + lSWConfigViewModel.mSWLocationDescription
                                + lSWConfigViewModel.mSWLocationID ;

                            System.Diagnostics.Debug.WriteLine(display);
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
            ELBViewModel.mSingleton.mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList.Add(
                new AirCraftEquipmentConfigViewModel(new AirCraftEquipmentConfig("New Equipment",new List<SubEquipment>()),true));
            EquipmentRoot.IsExpanded = true;
        }

        private void CreateNewSubEquipment(object sender, RoutedEventArgs e)
        {
            AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as AirCraftEquipmentConfigViewModel;
            SubEquipment lnew = new SubEquipment();
            lnew.mEquipmentID = "New SubEquipment";
            lAirCraftEquipmentConfigViewModel.mChildren.Add(new SubEquipmentViewModel(lnew, lAirCraftEquipmentConfigViewModel, true));

        }

        private void CreateNewSWConfig(object sender, RoutedEventArgs e)
        {
            if (AircraftEquipmentConfigTreeView.SelectedItem is SubEquipmentViewModel)
            {
                SubEquipmentViewModel lSubEquipmentViewModel = AircraftEquipmentConfigTreeView.SelectedItem as SubEquipmentViewModel;
                SWConfigViewModel lSWConfigViewModel = new SWConfigViewModel(new SWConfig());
                lSWConfigViewModel.mSWConfigIndex = "New SWConfig Index";
                lSubEquipmentViewModel.mSWConfigList.Add(lSWConfigViewModel);
                lSWConfigViewModel.IsInEditMode = true;
                lSWConfigViewModel.IsSelected = true;
                SWConfigRoot.IsExpanded = true;
            }
        }

        private void AircraftEquipmentConfigTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (AircraftEquipmentConfigTreeView.SelectedItem is TreeViewItemViewModel && e.Key == Key.F2 && ELBViewModel.mSingleton.mConfigurationViewModel.mIsEditable)
            {
                TreeViewItemViewModel lTreeViewItemModel = AircraftEquipmentConfigTreeView.SelectedItem as TreeViewItemViewModel;
                lTreeViewItemModel.IsInEditMode=true;
            }
        }

        private void SWConfigListTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (SWConfigListTreeView.SelectedItem is ViewModel.ViewModel && e.Key == Key.F2 && ELBViewModel.mSingleton.mConfigurationViewModel.mIsEditable)
            {
                ViewModel.ViewModel lViewModel = SWConfigListTreeView.SelectedItem as ViewModel.ViewModel;
                lViewModel.IsInEditMode = true;
            }
        }

        private void RemoveEquipment(object sender, RoutedEventArgs e)
        {
            AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as AirCraftEquipmentConfigViewModel;
            ELBViewModel.mSingleton.mConfigurationViewModel.mAirCraftEquipmentConfigViewModelList.Remove(lAirCraftEquipmentConfigViewModel);
        }

        private void RemoveSubEquipment(object sender, RoutedEventArgs e) 
        {
            SubEquipmentViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as SubEquipmentViewModel;
            lAirCraftEquipmentConfigViewModel.mParent.mChildren.Remove(lAirCraftEquipmentConfigViewModel);
        }

        private void RemoveSWConfig(object sender, RoutedEventArgs e) 
        {
            SWConfigViewModel lSWConfigViewModel = SWConfigListTreeView.SelectedItem as SWConfigViewModel;
            SubEquipmentViewModel lAirCraftEquipmentConfigViewModel = AircraftEquipmentConfigTreeView.SelectedItem as SubEquipmentViewModel;
            lAirCraftEquipmentConfigViewModel.mSWConfigList.Remove(lSWConfigViewModel);
        }
    }
}
