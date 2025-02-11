using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Warehouse
    {
        public int intWarehouseId { get; set; }
        public string varWarehouseName { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public int? intCompanyId { get; set; }
    }
}