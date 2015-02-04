using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace ElectronicLogbook
{
    public class PartialEditableTextBox : TextBox
    {

        public PartialEditableTextBox()
        {
            TextChanged += new TextChangedEventHandler(MyTextBox_TextChanged);
           /* PreviewKeyDown += new KeyEventHandler(MyTextBox_PreviewKeyDown);
            PreviewTextInput += new TextCompositionEventHandler(MyTextBox_PreviewTextInput);
            /*DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(OnPaste));*/
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!IsValidPositionForEdit())
            {
                e.CancelCommand(); // do not allow pasting 
            }
        }

        void MyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            this.Text = ReadOnlyTextChunk + this.Text;
        }

        void MyTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            this.Text = ReadOnlyTextChunk + this.Text;
        }

        private bool IsValidPositionForEdit()
        {
            return SelectionStart <= this.before || SelectionStart >= this.before + ReadOnlyTextChunk.Length;
        }

        public static readonly DependencyProperty ReadOnlyTextChunkProperty = DependencyProperty.Register(
            "ReadOnlyTextChunk", typeof(string), typeof(PartialEditableTextBox), new PropertyMetadata(""));
        public string ReadOnlyTextChunk
        {
            get { return (string)GetValue(ReadOnlyTextChunkProperty); }
            set { SetValue(ReadOnlyTextChunkProperty, value); }
        }

        public static readonly DependencyProperty EditableTextProperty = DependencyProperty.Register(
            "EditableText", typeof(string), typeof(PartialEditableTextBox), new PropertyMetadata(""));
        public string EditableText
        {
            get { return (string)GetValue(EditableTextProperty); }
            set { SetValue(EditableTextProperty, value); }
        }

      /*  public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Text = ReadOnlyTextChunk;
            this.before = 0;
            this.after = ReadOnlyTextChunk.Length;
        }
        */
        void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          /*  foreach (TextChange ch in e.Changes)
            {
                if (ch.Offset <= this.before) // before text was modified
                {
                    this.before += ch.AddedLength - ch.RemovedLength;
                }
                else if (ch.Offset >= this.before + ReadOnlyTextChunk.Length) // after text was modified
                {
                    this.after += ch.AddedLength - ch.RemovedLength;
                }
            }*/

            this.Text = ReadOnlyTextChunk + this.Text;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
           // if (e.Key == Key.Tab) // jump to after part
          //  {
                if (SelectionStart <= this.before)
                {
                    SelectionStart = this.before + ReadOnlyTextChunk.Length;
                    e.Handled = true;
                }
                else
                {
                    base.OnKeyDown(e);
                }
         //   }
        }

        private int before; // length of before part
        private int after; // length of after part
    }
}
