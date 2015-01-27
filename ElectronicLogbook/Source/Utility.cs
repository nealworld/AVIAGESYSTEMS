using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace ElectronicLogbook
{
    public class Utility
    {
        public static bool Serialize<T>(T aValue, String aFile)
        {
            if (aValue == null)
            {
                return false;
            }
            try
            {
                Stream lStream = File.Open(aFile, FileMode.Create);
                BinaryFormatter lFormatter = new BinaryFormatter();
                lFormatter.Serialize(lStream, aValue);
                lStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public static bool DeSerialize<T>(ref T aValue, String aFile)
        {
            if (aValue == null)
            {
                return false;
            }
            try
            {
                Stream lStream = File.Open(aFile, FileMode.Open);
                BinaryFormatter lFormatter = new BinaryFormatter();
                aValue = (T)lFormatter.Deserialize(lStream);
                lStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public static readonly String Deleted = "(Deleted)";
        public static readonly String Modified = "(Modified)";
        public static readonly String New = "(New)";


        internal static bool SerializeToXML(ViewModel.ConfigurationViewModel aConfigurationViewModel, string aFileName)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ViewModel.ConfigurationViewModel));

                using (var writer = new StreamWriter(aFileName))
                {
                    serializer.Serialize(writer, aConfigurationViewModel);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        internal static bool DeSerializeFromXML(ref ViewModel.ConfigurationViewModel aConfigurationViewModel, string aFileName)
        {
            try
            {
                XmlSerializer serializer = new
                XmlSerializer(typeof(ViewModel.ConfigurationViewModel));

                // A FileStream is needed to read the XML document.
                FileStream lfilestream = new FileStream(aFileName, FileMode.Open);
                XmlReader lxmlReader = XmlReader.Create(lfilestream);

                // Use the Deserialize method to restore the object's state.
                aConfigurationViewModel = (ViewModel.ConfigurationViewModel)serializer.Deserialize(lxmlReader);
                lfilestream.Close();
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
