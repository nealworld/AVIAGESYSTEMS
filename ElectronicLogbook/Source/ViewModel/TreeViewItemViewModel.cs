using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace ElectronicLogbook.ViewModel
{
    public class TreeViewItemViewModel : ViewModel
    {
        #region Data
        private ObservableCollection<TreeViewItemViewModel> _children;
        private TreeViewItemViewModel _parent;
        private bool _isSelected;

        #endregion // Data

        #region Constructors

        public TreeViewItemViewModel(TreeViewItemViewModel parent)
        {
            _parent = parent;
            _children = new ObservableCollection<TreeViewItemViewModel>();
        }

        public TreeViewItemViewModel()
        {
        }

        #endregion // Constructors

        #region Presentation Members

        #region Children

        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> mChildren
        {
            get { return _children; }
            set {
                _children = value;
                this.OnPropertyChanged("Children");
            }
        }

        #endregion // Children

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
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

        #endregion // IsSelected

        #region Parent

        public TreeViewItemViewModel mParent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #endregion // Presentation Members
    }
}
