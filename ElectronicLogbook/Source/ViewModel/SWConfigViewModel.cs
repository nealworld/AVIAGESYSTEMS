using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook.ViewModel
{
    public class SWConfigViewModel : ViewModel
    {
        private String _SWConfigIndex;
        public String mSWConfigIndex 
        {
            set 
            {
                _SWConfigIndex = value;
                this.OnPropertyChanged("mSWConfigIndex");
            }
            get 
            {
                return _SWConfigIndex;
            }
        }

        private String _SWLocationID;
        public String mSWLocationID 
        {
            set
            {
                _SWLocationID = value;
                this.OnPropertyChanged("mSWLocationID");
            }
            get 
            {
                return _SWLocationID;
            }
        }

        private String _SWLocationDescription;
        public String mSWLocationDescription 
        {
            set 
            {
                _SWLocationDescription = value;
                this.OnPropertyChanged("mSWLocationDescription");
            }
            get 
            {
                return _SWLocationDescription;
            }
        }

        private ObservableCollection<SWPartViewModel> _SWPartList;
        public ObservableCollection<SWPartViewModel> mSWPartList 
        {
            set 
            {
                _SWPartList = value;
                this.OnPropertyChanged("mSWPartList");
            }
            get 
            {
                return _SWPartList;
            }
        }

        public SWConfigViewModel(SWConfig aSWConfig) 
        {
            mSWConfigIndex = aSWConfig.mSWConfigIndex;
            mSWLocationID = aSWConfig.mSWLocationID;
            mSWLocationDescription = aSWConfig.mSWLocationDescription;
            mSWPartList = new ObservableCollection<SWPartViewModel>();
            foreach (SWPart lSWPart in aSWConfig.mSWPartList) 
            {
                mSWPartList.Add(new SWPartViewModel(lSWPart));
            }
        }
    }
}
