using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Transporter
    {
        public int intTransporterId { get; set; }
        public string varTransporterName { get; set; }
        public string varContactNo { get; set; }
        public string varAddress { get; set; }
        public string varEmail { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public int? intCompanyId { get; set; }
    }
}