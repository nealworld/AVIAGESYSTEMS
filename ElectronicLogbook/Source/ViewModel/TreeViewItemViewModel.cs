using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security;
namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    public class TreeViewItemViewModel : ViewModel
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
                    if (value && this.mParent != null) {
                        this.mParent.IsSelected = false;
                    }
                }
            }
        }
        private TreeViewItemViewModel _parent;
        public TreeViewItemViewModel mParent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public override Boolean Compare(ViewModel aViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
