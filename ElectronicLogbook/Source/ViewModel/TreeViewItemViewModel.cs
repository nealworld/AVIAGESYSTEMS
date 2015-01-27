using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Security;
using System.Xml.Serialization;
namespace ElectronicLogbook.ViewModel
{
    [Serializable()]
    [XmlInclude(typeof(AirCraftEquipmentConfigViewModel))]
    [XmlInclude(typeof(SubEquipmentViewModel))]
    public class TreeViewItemViewModel : ViewModel
    {
        public TreeViewItemViewModel(TreeViewItemViewModel parent)
        {
            _parent = parent;
            _children = new ObservableCollection<TreeViewItemViewModel>();
            IsExpanded = true;
        }

        public TreeViewItemViewModel()
        {
            IsExpanded = true;
        }


        private ObservableCollection<TreeViewItemViewModel> _children;

        [XmlArrayItem("Child", typeof(TreeViewItemViewModel))]
        [XmlArray("Children")]
        public ObservableCollection<TreeViewItemViewModel> mChildren
        {
            get { return _children; }
            set {
                _children = value;
                this.OnPropertyChanged("Children");
            }
        }


        private bool _isSelected;

        [XmlIgnore()]
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

        private bool _isExpanded;

        [XmlIgnore()]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        private TreeViewItemViewModel _parent;

       [XmlIgnore()]
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
