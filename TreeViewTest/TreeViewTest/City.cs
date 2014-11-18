using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeViewTest
{
    /// <summary>
    /// WPF的TreeView数据绑定基础
    /// Copyright (C) 遗昕 | weisim3.com  03.11.2012
    /// </summary>
    class City
    {
        public int cityId { get; set; }
        public string cityName { get; set; }
        public List<City> ChildItem { get; set; }
    }
}
