using System;
using System.Collections.Generic;
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
using ElectronicLogbook.ViewModel;
using ElectronicLogbookDataLib.AirCraftEquipment;

namespace ElectronicLogbook
{
    /// <summary>
    /// Interaction logic for AircraftEquipmentControl.xaml
    /// </summary>
    public partial class AircraftEquipmentControl : UserControl
    {
        //AirCraftEquipmentConfigViewModel mAirCraftEquipmentConfigViewModel;
        public AircraftEquipmentControl()
        {
            InitializeComponent();


            //mAirCraftEquipmentConfigViewModel = new AirCraftEquipmentConfigViewModel(GetList());
            //base.DataContext = new AirCraftEquipmentConfigViewModel(GetList());

            base.DataContext = ELBViewModel.getInstance().mAirCraftEquipmentConfigViewModel;
        }

        private List<AirCraftEquipmentConfig> GetList()
        {
            List<AirCraftEquipmentConfig> myList = new List<AirCraftEquipmentConfig>();

            AirCraftEquipmentConfig lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM1";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            SubEquipment lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 1";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1","1","1","1","1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWsssnfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWc2222ig2",
                    mSWLocationID = "2qq3",
                    mSWLocationDescription = "d1dd1",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWweig4",
                    mSWLocationID = "24sd4",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo 
                { 
                    mItemIndex="ss",
                    mItemInfo="ss2222"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s1s",
                   mItemInfo = "ss224sssghfgde22"
               });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 2";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_11", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_21", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWcw22nfig1",
                    mSWLocationID = "2sd",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SW434v3Part1", "22", "3v353"), new SWPart("SWPar3v45t2", "24 f fr32", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWcss3bbbbgggonfig2",
                    mSWLocationID = "2dee444444444443",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "S==++onfig4",
                    mSWLocationID = "2{{j,44",
                    mSWLocationDescription = "d4267567;'p[1cdd",
                    mSWPartList = { new SWPart("SWPax34rt1", "2123x12", "3t3"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssxs3v423d33",
                    mItemInfo = "ss222gv234c2gfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s1s",
                   mItemInfo = "sl0000000ghfgde22"
               });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssxsd33",
                    mItemInfo = "ss222gvgfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "s12",
                   mItemInfo = "slllde22"
               });
            lSubEquipment.mConfigInfoList.Add(
                new ConfigInfo
                {
                    mItemIndex = "ssx2dd33",
                    mItemInfo = "ss2aafgfgf2"
                });
            lSubEquipment.mConfigInfoList.Add(
               new ConfigInfo
               {
                   mItemIndex = "seq2er1s",
                   mItemInfo = "sl00aaqde22"
               });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 3";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_111", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_211", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 4";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_114s1", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_214s1", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            myList.Add(lAirCraftEquipmentConfig1);

            lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM2";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 1";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1222", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2222", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 2";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1333", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2333", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "Equipment_ID: 3";
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_1444", "1", "1", "1", "1"));
            lSubEquipment.mHWPartList.Add(new HWPart("HW_PART_2444", "2", "2", "2", "2"));
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig1",
                    mSWLocationID = "2",
                    mSWLocationDescription = "d",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig2",
                    mSWLocationID = "23",
                    mSWLocationDescription = "d11",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lSubEquipment.mSWConfigList.Add(
                new SWConfig
                {
                    mSWConfigIndex = "SWconfig4",
                    mSWLocationID = "244",
                    mSWLocationDescription = "d421cdd",
                    mSWPartList = { new SWPart("SWPart1", "22", "33"), new SWPart("SWPart2", "22", "33") }
                });
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            myList.Add(lAirCraftEquipmentConfig1);

            return myList;
        }
    }
}
