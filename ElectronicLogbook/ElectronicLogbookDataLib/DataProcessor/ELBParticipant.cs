using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GEAviation.CommonSim;
using System.Windows;
using System.Windows.Controls;

namespace ElectronicLogbookDataLib.DataProcessor
{
    class ELBParticipant
    {

        public UtilityParticipant mUtilityParticipant{get; private set;}

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly ELBParticipant instance = new ELBParticipant();
        }

        public static ELBParticipant mSingleton { get { return Nested.instance; } }

        private ELBParticipant() 
        {
            mUtilityParticipant = GetParticipant();
            if (mUtilityParticipant == null)
            {
                return;
            }

            mUtilityParticipant.EnterManualMode();
            mUtilityParticipant.SetState(CommonSimTypes.Status.Running);
        }

        private UtilityParticipant GetParticipant()
        {
            System.Diagnostics.Debug.WriteLine("enter GetParticipant");
            string lSimulationName = "Electronic_Logbook";
            string lSimulationDescription = "Electronic_Logbook";
            string lSimulationPartNumber = "1002197-002";
            string lSimulationVersionInformation = "01";
            //create the participant
            return RegisterParticipant(lSimulationName, lSimulationDescription,
                                        lSimulationPartNumber, lSimulationVersionInformation);
        }

        private UtilityParticipant RegisterParticipant(string aParticipantName, string aDescription,
                                    string aPartNumber, string aVersion)
        {
            var lParticipantInfo = new ParticipantInformation()
            {
                Name = aParticipantName,
                Description = aDescription,
                PartNumber = aPartNumber,
                Version = aVersion
            };

            UtilityParticipant lParticipant = null;
            try
            {
                // create participant for the GUI
                bool lIsConnected = false;
                int lNextIdentifier = 2;
                while (!lIsConnected)
                {
                    try
                    {
                        // create as Utility Participant (the current CommonSimDotNet
                        // implementation does not currently support Simulation
                        // Participants as there is no way for them to enter normal mode).
                        lParticipant = new UtilityParticipant(lParticipantInfo);
                        lParticipant.ConnectToMesh();
                        lIsConnected = true;
                    }
                    catch (CSI_ObjectAlreadyExistsException)
                    {
                        // if there is already a participant with this name connected to
                        // the mesh, append a number and try to connect again
                        lParticipantInfo.Name = String.Format("{0}{1}", aParticipantName,
                                                              lNextIdentifier);
                        lNextIdentifier++;

                        if (lParticipant != null)
                        {
                            lParticipant.Dispose();
                            lParticipant = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (lParticipant != null)
                {
                    lParticipant.Dispose();
                    lParticipant = null;
                }
                MessageBox.Show("Cannot connect to CSI mesh. Error message: " + e.Message,
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            return lParticipant;

        }
    }
}
