using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class StockMain
    {
        public int intStid { get; set; }
        public int? intCompanyId { get; set; }
        public int? intPartyId { get; set; }
        public int? intVrno { get; set; }
        public int? intVrnoA { get; set; }
        public DateTime? dtVrDate { get; set; }
        public string varRemarks { get; set; }
        public int? intTransporterId { get; set; }
        public string varVrType { get; set; }
        public decimal? dcDiscount { get; set; }
        public decimal? dcDiscountAmount { get; set; }
        public decimal? dcExpense { get; set; }
        public decimal? dcAdditionalCharges { get; set; }
        public decimal? dcNetAmount { get; set; }
        public decimal? dcTotalAmount { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public List<StockDetail> StockDetails { get; set; }
    }
}