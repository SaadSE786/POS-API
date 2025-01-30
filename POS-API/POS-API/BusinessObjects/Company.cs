using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Company
    {
        public int intCompanyId { get; set; }
        public string varCompanyName {  get; set; }
        public string varCompanyAddress { get; set; }
        public string varContactEmail { get; set; }
        public string varContactNo { get; set; }
        public byte[] varCompanyLogo { get; set; }
        public DateTime dtCreationDate { get; set; }
        public DateTime dtUpdationDate { get; set; }

    }
}