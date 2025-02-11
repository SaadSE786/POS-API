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
    
    public partial class tblTransporter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTransporter()
        {
            this.tblStockMains = new HashSet<tblStockMain>();
        }
    
        public int intTransporterId { get; set; }
        public string varTransporterName { get; set; }
        public string varContactNo { get; set; }
        public string varAddress { get; set; }
        public string varEmail { get; set; }
        public Nullable<System.DateTime> dtCreationDate { get; set; }
        public Nullable<System.DateTime> dtUpdationDate { get; set; }
        public Nullable<int> intCreatedBy { get; set; }
        public Nullable<int> intUpdatedBy { get; set; }
        public Nullable<int> intCompanyId { get; set; }
    
        public virtual tblCompany tblCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStockMain> tblStockMains { get; set; }
    }
}
