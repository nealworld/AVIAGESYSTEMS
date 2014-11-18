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
using System.Collections;
using ElectronicLogbookDataLib.AirCraftEquipment;
namespace TreeViewTest
{
    /// <summary>
    /// WPF的TreeView数据绑定基础
    /// Copyright (C) 遗昕 | weisim3.com  03.11.2012
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            label1.Content = "^…";

            //treeView.ItemsSource = GetList();
            treeView1.ItemsSource = GetList();
            treeView2.ItemsSource = GetTable().Keys;


            treeView5.ItemsSource = GetList();
        }

        /// <summary>
        /// GetTable() ->构建Hashtable数据
        /// </summary>
        /// <returns></returns>
        private Hashtable GetTable()
        {
            Hashtable City = new Hashtable();
            City.Add("北京", "1");
            City.Add("上海", "2");
            City.Add("广州", "3");
            City.Add("Hashtable", "4");
            return City;
        }

        /// <summary>
        /// GetList() -> 构建ArrayList数据
        /// </summary>
        /// <returns></returns>
        private List<AirCraftEquipmentConfig> GetList()
        {
            List<AirCraftEquipmentConfig> myList = new List<AirCraftEquipmentConfig>();

            AirCraftEquipmentConfig lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM1";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            SubEquipment lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "1";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "2";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "3";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            myList.Add(lAirCraftEquipmentConfig1);

            lAirCraftEquipmentConfig1 = new AirCraftEquipmentConfig();
            lAirCraftEquipmentConfig1.mConfigName = "GPM2";
            lAirCraftEquipmentConfig1.mSubEquipmentList = new List<SubEquipment>();
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "1";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "2";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            lSubEquipment = new SubEquipment();
            lSubEquipment.mEquipmentID = "3";
            lAirCraftEquipmentConfig1.mSubEquipmentList.Add(lSubEquipment);
            myList.Add(lAirCraftEquipmentConfig1);

            return myList;
        }

        /// <summary>
        /// GetCity() -> 构建泛型List数据
        /// </summary>
        /// <returns></returns>
        private List<City> GetCity()
        {
            List<City> myItem = new List<City>();
            myItem.Add(new City()
            {
                cityId = 1,
                cityName = "北京/Beijing",
                ChildItem = new List<City>() { 
                new City{ cityName="朝阳", ChildItem=new List<City>(){
                new City { cityName="建国门"}
                }
                
                }, 
                new City { cityName="海淀"},
                new City{ cityName="丰台"},
                new City {cityName="宣武", ChildItem=new List<City>(){
                new City { cityName="右安门"}
                }},
                new City {cityName="崇文"}}

            });

            myItem.Add(new City()
            {
                cityId = 2,
                cityName = "台北/Taipei",
                ChildItem = new List<City>() { 
                new City{ cityName="中正区", ChildItem=new List<City>(){
                new City { cityName="东门"}
                }}, 
                new City { cityName="三重区"},
                new City{ cityName="中山区"},
                new City {cityName="大安区"},
                new City {cityName="内湖区"}}

            });

            myItem.Add(new City()
            {
                cityId = 3,
                cityName = "上海/Shanghai",
                ChildItem = new List<City>() { 
                new City{ cityName="浦东"}, 
                new City { cityName="黄埔"},
                new City{ cityName="徐汇"},
                new City {cityName="普陀"},
                new City {cityName="长宁"}}

            });

            myItem.Add(new City()
            {
                cityId = 4,
                cityName = "香港/Hongkong",
                ChildItem = new List<City>() { 
                new City{ cityName="湾仔区"}, 
                new City { cityName="九龙城区"},
                new City{ cityName="黄大仙区"},
                new City {cityName="中西区", ChildItem=new List<City>(){
                new City { cityName="半山区"}
                }},
                new City {cityName="东区"}}

            });

            return myItem;
        }

        //private List<City> GetCity()
        //{
        //    List<City> myItem = new List<City>();
        //    myItem.Add(new City()
        //    {
        //        cityId = 1,
        //        cityName = "北京/Beijing",
        //        //子级
        //        ChildItem = new List<City>() { 
        //        new City{ cityName="朝阳" //子级        
        //        }, 
        //        new City { cityName="海淀"},
        //        new City{ cityName="丰台"},
        //        new City {cityName="宣武" //子级
        //        },
        //        new City {cityName="崇文"}}

        //    });

        //    myItem.Add(new City()
        //    {
        //        cityId = 3,
        //        cityName = "上海/Shanghai",
        //        //子级
        //        ChildItem = new List<City>() { 
        //        new City{ cityName="浦东"}, 
        //        new City { cityName="黄埔"},
        //        new City{ cityName="徐汇"},
        //        new City {cityName="普陀"},
        //        new City {cityName="长宁"}}

        //    });

        //    return myItem;
        //}

        private void treeView_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            //label1.Content = ((City)treeView.SelectedItem).cityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (treeView5.SelectedItem == null)
            {
                MessageBox.Show("treeView5.SelectedItem null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                MessageBox.Show(treeView5.SelectedItem.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }
     


    }
}
