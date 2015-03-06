using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace ElectronicLogbook
{
    public class Utility
    {
        public static void MakeDateDir(String aDir)
        {
            if (!Directory.Exists(aDir))
            {
                Directory.CreateDirectory(aDir);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

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

        public static readonly String Modified = "(Modified)";
        public static readonly String New = "(New in Current)";
        public static readonly String Missing = "(Missing in Previous)";

        internal static bool SerializeToXML<T>(T aValue, string aFileName)
        {
            try
            {
                var serializer = new XmlSerializer(aValue.GetType());

                using (var writer = new StreamWriter(aFileName))
                {
                    serializer.Serialize(writer, aValue);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        internal static bool DeSerializeFromXML<T>(ref T aValue, string aFileName)
        {
            try
            {
                System.Type lType = aValue.GetType();
                XmlSerializer serializer = new
                XmlSerializer(lType);

                // A FileStream is needed to read the XML document.
                FileStream lfilestream = new FileStream(aFileName, FileMode.Open);
                XmlReader lxmlReader = XmlReader.Create(lfilestream);

                // Use the Deserialize method to restore the object's state.
                aValue = (T)serializer.Deserialize(lxmlReader);
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
