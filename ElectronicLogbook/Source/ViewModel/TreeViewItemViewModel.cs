using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class TreeViewItemViewModel : ViewModel, ISerializable
    {
        public TreeViewItemViewModel(TreeViewItemViewModel parent)
        {
            _parent = parent;
            _children = new ObservableCollection<TreeViewItemViewModel>();
        }

        public TreeViewItemViewModel()
        {
        }


        private ObservableCollection<TreeViewItemViewModel> _children;
        public ObservableCollection<TreeViewItemViewModel> mChildren
        {
            get { return _children; }
            set {
                _children = value;
                this.OnPropertyChanged("Children");
            }
        }


        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
        private TreeViewItemViewModel _parent;
        public TreeViewItemViewModel mParent
        {
            get { return _parent; }
            set { _parent = value; }
        }




        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) 
        {
            info.AddValue("mChildren",mChildren);
            info.AddValue("mParent",mParent);
        }

        //Deserialization constructor.
        public TreeViewItemViewModel(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mChildren = (ObservableCollection<TreeViewItemViewModel>)info.
                GetValue("mChildren", typeof(ObservableCollection<TreeViewItemViewModel>));
            mParent = (TreeViewItemViewModel)info.GetValue("mParent", typeof(TreeViewItemViewModel));
        }
    }
}
