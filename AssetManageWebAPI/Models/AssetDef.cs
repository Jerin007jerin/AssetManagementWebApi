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
    
    public partial class AssetDef
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetDef()
        {
            this.AssetMasters = new HashSet<AssetMaster>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public decimal ad_id { get; set; }
        public string ad_name { get; set; }
        public Nullable<decimal> ad_type_id { get; set; }
        public string ad_class { get; set; }
    
        public virtual Assettype Assettype { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetMaster> AssetMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
