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
    
    public partial class tblPledger
    {
        public int intPlid { get; set; }
        public Nullable<int> intCompanyId { get; set; }
        public Nullable<int> intPartyId { get; set; }
        public Nullable<int> intDcno { get; set; }
        public Nullable<System.DateTime> dtVrDate { get; set; }
        public string varDescription { get; set; }
        public string varVrType { get; set; }
        public Nullable<decimal> dcDebit { get; set; }
        public Nullable<decimal> dcCredit { get; set; }
        public Nullable<System.DateTime> dtCreationDate { get; set; }
        public Nullable<System.DateTime> dtUpdationDate { get; set; }
        public Nullable<int> intCreatedBy { get; set; }
        public Nullable<int> intUpdatedBy { get; set; }
    
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblParty tblParty { get; set; }
    }
}
