using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectronicLogbookDataLib.AirCraftEquipment;
using System.ComponentModel;

namespace ElectronicLogbook.ViewModel
{
    public class SubEquipmentViewModel
    {
        public String mNodeName { set; get; }
        public SubEquipment mSubEquipment { set; get; }

        public SubEquipmentViewModel(SubEquipment lSubEquipment)
        {
            this.mSubEquipment = lSubEquipment;
            this.mNodeName = "EquipmentID: " + lSubEquipment.mEquipmentID;
        }
    }
}
