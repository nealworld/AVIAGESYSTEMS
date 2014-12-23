using System.Windows.Controls;
using ElectronicLogbook.ViewModel;
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
            VAISParticipantPanel.DataContext = ELBViewModel.mSingleton.mConfigurationViewModel;
        }


    }
}
