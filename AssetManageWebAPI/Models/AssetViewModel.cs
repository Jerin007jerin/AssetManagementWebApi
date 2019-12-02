using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManageWebAPI.Models
{
    public class AssetViewModel
    {
        public string ad_name { get; set; }
        public decimal ad_id { get; set; }
        public string ad_type_id { get; set; }
        public string ad_class { get; set; }
    }
}