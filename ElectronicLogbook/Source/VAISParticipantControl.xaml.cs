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
using ElectronicLogbookDataLib.DataProcessor;
using ElectronicLogbookDataLib;
namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for VAISParticipantControl.xaml
    /// </summary>
    public partial class VAISParticipantControl : UserControl
    {
        public VAISParticipantControl()
        {
            InitializeComponent();

            base.DataContext = ElectronicLogbook.ViewModel.ELBViewModel.getInstance().mVAISParticipantListViewModel;
            List<VAISParticipant> ss = new List<VAISParticipant>();
            ss.Add(
                new VAISParticipant {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfsh",
                    mParticipantLocation = "r32d43",
                    mParticipantName = "rt4t4ffft",
                    mParticipantPartNumber = "2e2",
                    mParticipantVersionNumber = "d24#d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "$$$$$$$$%",
                    mParticipantLocation = "RRRRRRRRRRR$$$$$$$$$$",
                    mParticipantName = "erwerwegfreg",
                    mParticipantPartNumber = "555555",
                    mParticipantVersionNumber = "4323222222222222222"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );
            ss.Add(
                new VAISParticipant
                {
                    mParticipantDescription = "dfh",
                    mParticipantLocation = "r343",
                    mParticipantName = "rt4t4t",
                    mParticipantPartNumber = "swe2e2",
                    mParticipantVersionNumber = "d2d32d2"
                }
            );

            //base.DataContext = ss;
        }


    }
}
