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
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.Threading;
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
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditableTextBlock etb = (EditableTextBlock)((ContextMenu)(((MenuItem)sender).Parent)).PlacementTarget;
            etb.IsInEditMode = true;
            etb.Text = "neal is gooda!";
           
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
            }

            Thread.Sleep(5000);

            foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in
                ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList)
            {
                foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren)
                {
                    lSubEquipment.mEquipmentID = "neal is kioo!";
                }
            }

        }

        private void CreateNewEquipment(object sender, RoutedEventArgs e) 
        {
            ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList.Add(
                new AirCraftEquipmentConfigViewModel(new AirCraftEquipmentConfig("neal1",new List<SubEquipment>())));
        }
    }
}
