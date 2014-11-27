using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using ElectronicLogbookDataLib.DataProcessor;
using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class AirCraftEquipmentConfigViewModel
    {
        #region abandon
        /* public List<AirCraftEquipmentConfig> mAirCraftEquipmentConfigList { set; get; }

        public AirCraftEquipmentConfigViewModel() { }
        public AirCraftEquipmentConfigViewModel(List<AirCraftEquipmentConfig> aAirCraftEquipmentConfigList) 
        {
            if (aAirCraftEquipmentConfigList != null)
            {
                mAirCraftEquipmentConfigList = aAirCraftEquipmentConfigList;
            }
            else 
            {
                mAirCraftEquipmentConfigList = new List<AirCraftEquipmentConfig>();
            }
        }*/
        #endregion

        public List<AirCraftEquipmentConfigViewModel> mAirCraftEquipmentConfigViewModelList { set; get; }
        public String mNodeName { set; get; }
        public List<SubEquipmentViewModel> mSubEquipmentViewModelList { set; get; }
        
        public AirCraftEquipmentConfigViewModel()
        {
            mAirCraftEquipmentConfigViewModelList = new List<AirCraftEquipmentConfigViewModel>();
            ConfigurationProcessor lConfigurationProcessor = ConfigurationProcessor.GetInstance();
            
            List<AirCraftEquipmentConfig> lAirCraftEquipmentConfigList = lConfigurationProcessor.GetAirCraftEquipmentConfigList();
            foreach (AirCraftEquipmentConfig lAirCraftEquipmentConfig in lAirCraftEquipmentConfigList) 
            {
                mAirCraftEquipmentConfigViewModelList.Add(new AirCraftEquipmentConfigViewModel(lAirCraftEquipmentConfig));
            }

        }

        public AirCraftEquipmentConfigViewModel(List<AirCraftEquipmentConfig> aAirCraftEquipmentConfigList)
        {
            mAirCraftEquipmentConfigViewModelList = new List<AirCraftEquipmentConfigViewModel>();

            foreach (AirCraftEquipmentConfig lAirCraftEquipmentConfig in aAirCraftEquipmentConfigList)
            {
                mAirCraftEquipmentConfigViewModelList.Add(new AirCraftEquipmentConfigViewModel(lAirCraftEquipmentConfig));
            }
        }


        public AirCraftEquipmentConfigViewModel(AirCraftEquipmentConfig aAirCraftEquipmentConfig)

        {
            mNodeName = aAirCraftEquipmentConfig.mConfigName;
            mSubEquipmentViewModelList =  new List<SubEquipmentViewModel>();
            foreach (SubEquipment lSubEquipment in aAirCraftEquipmentConfig.mSubEquipmentList)
            {
                mSubEquipmentViewModelList.Add(new SubEquipmentViewModel(lSubEquipment));
            }
        }
    }
}
