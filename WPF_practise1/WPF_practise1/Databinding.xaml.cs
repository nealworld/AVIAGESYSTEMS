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
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace WPF_practise1
{
    /// <summary>
    /// Interaction logic for Databinding.xaml
    /// </summary>
    public partial class Databinding : Window
    {
        public String name = "neal";
        public void changeName(String text) {
        }
        public void First_Thread()
        {
            while (true)
            {
                Thread.Sleep(5000);
                /*Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action<String>(changeName),
                    "neal");*/
                name = "aa";
            }
        }

        public Databinding()
        {
            ThreadStart thr_start_func = new ThreadStart(First_Thread);
            Thread fThread = new Thread(thr_start_func);
            fThread.Name = "first_thread";
            fThread.Start(); //starting the thread



            InitializeComponent();

        }
    }
}
