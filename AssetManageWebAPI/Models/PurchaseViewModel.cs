using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManageWebAPI.Models
{
    public class PurchaseViewModel
    {
        public int pd_id { get; set; }
        public string pd_order_no { get; set; }
        public Nullable<decimal> pd_ad_id { get; set; }
        public Nullable<decimal> pd_type_id { get; set; }
        public Nullable<decimal> pd_vendor_id { get; set; }
        public string assetname { get; set; }
        public string assettype { get; set; }
        public string vendorname { get; set; }
        public int pd_qty { get; set; }
        public string pd_odateStr { get; set; }
        public string pd_ddateStr { get; set; }
        public string pd_status { get; set; }


    }
}