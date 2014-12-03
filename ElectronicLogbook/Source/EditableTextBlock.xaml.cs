using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock : UserControl, INotifyPropertyChanged
    {
        public EditableTextBlock()
        {
            InitializeComponent();
            base.Focusable = true;
            base.FocusVisualStyle = null;
        }

        #region Member Variables

        // We keep the old text when we go into editmode
        // in case the user aborts with the escape key
        private string oldText;

        #endregion Member Variables

        #region Properties

        public string Text
        {
            get {
                System.Diagnostics.Debug.WriteLine("get EditableTextBlock.Text:" + (string)GetValue(TextProperty));
                return (string)GetValue(TextProperty); 
            }
            set {
                System.Diagnostics.Debug.WriteLine("set EditableTextBlock.Text:" + value); 
                SetValue(TextProperty, value);
                //this.OnPropertyChanged("Text");
            }
        }

        public bool IsEditable
        {
            get {
                System.Diagnostics.Debug.WriteLine("get EditableTextBlock.IsEditable:" + (bool)GetValue(IsEditableProperty)); 
                return (bool)GetValue(IsEditableProperty);
            }
            set {
                System.Diagnostics.Debug.WriteLine("set EditableTextBlock.IsEditable:" + value); 
                SetValue(IsEditableProperty, value); 
            }
        }

        public bool IsInEditMode
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("get EditableTextBlock.IsInEditMode:" + (bool)GetValue(IsInEditModeProperty)); 
                if (IsEditable)
                    return (bool)GetValue(IsInEditModeProperty);
                else
                    return false;
            }
            set
            {
                System.Diagnostics.Debug.WriteLine("set EditableTextBlock.IsInEditMode:" + value); 
                if (IsEditable)
                {
                    if (value) oldText = Text;
                    SetValue(IsInEditModeProperty, value);
                    //this.OnPropertyChanged("IsInEditMode");
                }
            }
        }

        public string TextFormat
        {
            get {
                System.Diagnostics.Debug.WriteLine("get EditableTextBlock.TextFormat:" + (string)GetValue(TextFormatProperty)); 
                return (string)GetValue(TextFormatProperty); }
            set
            {
                System.Diagnostics.Debug.WriteLine("set EditableTextBlock.TextFormat:" + value); 
                if (value == "") value = "{0}";
                SetValue(TextFormatProperty, value);
            }
        }

        public string FormattedText
        {
            get {
                System.Diagnostics.Debug.WriteLine("get EditableTextBlock.FormattedText:" + String.Format(TextFormat, Text)); 
                return String.Format(TextFormat, Text); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(EditableTextBlock),
            new PropertyMetadata(""));

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(
            "IsEditable",
            typeof(bool),
            typeof(EditableTextBlock),
            new PropertyMetadata(true));

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register(
            "IsInEditMode",
            typeof(bool),
            typeof(EditableTextBlock),
            new PropertyMetadata(false));

        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register(
            "TextFormat",
            typeof(string),
            typeof(EditableTextBlock),
            new PropertyMetadata("{0}"));

        #endregion Properties

        #region Event Handlers

        // Invoked when we enter edit mode.
        void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Give the TextBox input focus
            txt.Focus();

            txt.SelectAll();
        }

        // Invoked when we exit edit mode.
        void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsInEditMode = false;
        }

        // Invoked when the user edits the annotation.
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.IsInEditMode = false;
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                this.IsInEditMode = false;
                Text = oldText;
                e.Handled = true;
            }
        }

        void EdittableTextBlock_KeyDown(object sender, KeyEventArgs e) 
        {
            if (this.IsEditable) 
            {
                if (e.Key == Key.F2)
                {
                    this.IsInEditMode = true;
                }
            }
        }

        private void mainControl_Loaded(object sender, RoutedEventArgs e)
        {
            /*if (this.IsEditable) 
            {
                this.IsInEditMode = true;
            }*/
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Event Handlers
    }
}
