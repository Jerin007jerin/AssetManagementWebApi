//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManageWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AssetMaster
    {
        public int am_id { get; set; }
        public Nullable<decimal> am_atype_id { get; set; }
        public Nullable<decimal> am_make_id { get; set; }
        public Nullable<decimal> am_ad_id { get; set; }
        public string am_model { get; set; }
        public string am_snumber { get; set; }
        public Nullable<System.DateTime> am_myear { get; set; }
        public Nullable<System.DateTime> am_pdate { get; set; }
        public string am_warranty { get; set; }
        public Nullable<System.DateTime> am_from { get; set; }
        public Nullable<System.DateTime> am_to { get; set; }
    
        public virtual AssetDef AssetDef { get; set; }
        public virtual Assettype Assettype { get; set; }
        public virtual VendorCreation VendorCreation { get; set; }
    }
}
