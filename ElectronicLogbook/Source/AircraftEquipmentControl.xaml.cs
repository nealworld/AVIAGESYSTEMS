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
            TextBlock tb =   (TextBlock)((ContextMenu)(((MenuItem)sender).Parent)).PlacementTarget;
            tb.Text = "neal is gooda!";
           
            foreach (AirCraftEquipmentConfigViewModel lAirCraftEquipmentConfigViewModel in 
                ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList) 
            {
                foreach (SubEquipmentViewModel lSubEquipment in lAirCraftEquipmentConfigViewModel.mChildren )
                {
                    System.Diagnostics.Debug.WriteLine(lSubEquipment.mEquipmentID);
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


        }

        private void CreateNewEquipment(object sender, RoutedEventArgs e) 
        {
            ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModelList.Add(
                new AirCraftEquipmentConfigViewModel(new AirCraftEquipmentConfig("neal1",new List<SubEquipment>())));
        }
    }
}
