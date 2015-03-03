using ElectronicLogbook.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for RemarkExpander.xaml
    /// </summary>
    public partial class RemarkExpander : UserControl
    {
        public RemarkExpander()
        {
            InitializeComponent();

            addButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.addNewRemark);
            deleteButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.deleteRemark);
            saveButton.Click += new RoutedEventHandler(
                ELBViewModel.mSingleton.mDailyRemarkHandler.saveRemark);

            PrintLogicalTree(0, this);
        }

        public String GetTime() {
            return TimeTextBox.Text;
        }

        void PrintLogicalTree(int depth, object obj)
        {
            // Print the object with preceding spaces that represent its depth
            Debug.WriteLine(new string(' ', depth) + obj);
            // Sometimes leaf nodes aren’t DependencyObjects (e.g. strings)
            if (!(obj is DependencyObject)) return;
            // Recursive call for each logical child
            foreach (object child in LogicalTreeHelper.GetChildren(
            obj as DependencyObject))
            PrintLogicalTree(depth + 1, child);
        }

        void PrintVisualTree(int depth, DependencyObject obj)
        {
            // Print the object with preceding spaces that represent its depth
            Debug.WriteLine(new string(' ', depth) + obj);
            // Recursive call for each visual child
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }


    }
}
