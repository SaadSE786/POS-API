//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblItem()
        {
            this.tblStockDetails = new HashSet<tblStockDetail>();
        }
    
        public int intItemId { get; set; }
        public Nullable<int> intCompanyId { get; set; }
        public string varItemName { get; set; }
        public Nullable<decimal> dcOpenStock { get; set; }
        public Nullable<decimal> dcMinLevel { get; set; }
        public Nullable<decimal> dcMaxLevel { get; set; }
        public Nullable<decimal> dcOrderLevel { get; set; }
        public Nullable<System.DateTime> dtOpenDate { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<decimal> dcSellRate { get; set; }
        public Nullable<decimal> dcPurRate { get; set; }
        public Nullable<decimal> dcRetailSaleRate { get; set; }
        public Nullable<decimal> dcDistributorSaleRate { get; set; }
        public Nullable<decimal> dcDiscount { get; set; }
        public Nullable<bool> isTaxable { get; set; }
        public Nullable<bool> isExpirable { get; set; }
        public string varUom { get; set; }
        public Nullable<System.DateTime> dtExpiryDate { get; set; }
        public Nullable<System.DateTime> dtCreationDate { get; set; }
        public Nullable<System.DateTime> dtUpdationDate { get; set; }
    
        public virtual tblCompany tblCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStockDetail> tblStockDetails { get; set; }
    }
}
