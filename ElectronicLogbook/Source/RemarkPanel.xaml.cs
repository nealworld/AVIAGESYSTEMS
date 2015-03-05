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
        // Dummy columns for layers 0 and 1:
        public RemarkPanel()
        {
            InitializeComponent();

        }

        // Toggle between docked and undocked states (Pane 1)
        public void pane1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPane(1);
        }

        // Show Pane 1 when hovering over its button
        public void pane1Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer1.Visibility = Visibility.Visible;

            // Adjust Z order to ensure the pane is on top:
            parentGrid.Children.Remove(layer1);
            parentGrid.Children.Add(layer1);

        }


        // Hide any undocked panes when the mouse enters Layer 0
        public void layer1_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
        }



        // Docks a pane, which hides the corresponding pane button
        public void DockPane(int paneNumber)
        {
            pane1Button.Visibility = Visibility.Collapsed;
            pane1PinImage.Source = new BitmapImage(new Uri("resources/pin.gif", UriKind.Relative));

            parentGrid.Children.Remove(layer1);
            layer0.Children.Add(layer1);
            Grid.SetColumn(layer1,0);
        }

        // Undocks a pane, which reveals the corresponding pane button
        public void UndockPane(int paneNumber)
        {
            layer0.Children.Remove(layer1);
            parentGrid.Children.Add(layer1);
            layer1.Visibility = Visibility.Collapsed;
            pane1Button.Visibility = Visibility.Visible;
            pane1PinImage.Source = new BitmapImage(new Uri("resources/pinHorizontal.gif", UriKind.Relative));
        }
    }
}
