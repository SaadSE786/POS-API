using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class StockDetail
    {
        public int intStockDetailId { get; set; }
        public int? intStid { get; set; }
        public int? intItemId { get; set; }
        public int? intWarehouseId { get; set; }
        public int? intQuantity { get; set; }
        public decimal? dcRate { get; set; }
        public decimal? dcAmount { get; set; }
        public decimal? dcDisc { get; set; }
        public decimal? dcDiscAmount { get; set; }
        public decimal? dcExclTaxAmount { get; set; }
        public decimal? dcTax { get; set; }
        public decimal? dcTaxAmount { get; set; }
        public decimal? dcInclTaxAmount { get; set; }
        public string varType { get; set; }
        public decimal? dcPurRate { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
    }
}