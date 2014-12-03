using ElectronicLogbook.ViewModel;
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
                    System.Diagnostics.Debug.WriteLine(lAirCraftEquipmentConfigViewModel.mConfigName);
                    foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren)
                    {
                        System.Diagnostics.Debug.WriteLine("    " + lSubEquipment.mEquipmentID);

                    }
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //EditableTextBlock etb = (EditableTextBlock)((ContextMenu)(((MenuItem)sender).Parent)).PlacementTarget;
            //etb.SetValue(EditableTextBlock.TextProperty, "hahaha");
            //System.Diagnostics.Debug.WriteLine("    " + lSubEquipment.mEquipmentID);
            //etb.IsInEditMode = true;
            /*etb.Text = "neal is gooda!";
           
            foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in 
                ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList) 
            {
                System.Diagnostics.Debug.WriteLine(lAirCraftEquipmentConfigViewModel.mConfigName);
                foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren )
                {
                    System.Diagnostics.Debug.WriteLine("    " +　lSubEquipment.mEquipmentID);
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

        private void CreateNewEquipment(object sender, RoutedEventArgs e) 
        {
            ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList.Add(
                new AirCraftEquipmentConfigViewModel(new AirCraftEquipmentConfig("neal1",new List<SubEquipment>())));
        }

        private void EditableTextBlock_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(" enter EditableTextBlock_KeyDown");
            EditableTextBlock etb = sender as EditableTextBlock;

            // Finally make sure that we are
            // allowed to edit the TextBlock
            if (etb.IsEditable) 
            {
                if (e.Key == Key.F2)
                {
                    etb.IsInEditMode = true;
                }
            }
        }
    }
}
