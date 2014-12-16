using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
                //Open the file written above and read values from it.
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
    }
}
