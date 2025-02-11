using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Item
    {
        public int intItemId { get; set; }
        public int? intCompanyId { get; set; }
        public string varItemName { get; set; }
        public decimal? dcOpenStock { get; set; }
        public decimal? dcMinLevel { get; set; }
        public decimal? dcMaxLevel { get; set; }
        public decimal? dcOrderLevel { get; set; }
        public DateTime? dtOpenDate { get; set; }
        public bool? isActive { get; set; }
        public decimal? dcSellRate { get; set; }
        public decimal? dcPurRate { get; set; }
        public decimal? dcRetailSaleRate { get; set; }
        public decimal? dcDistributorSaleRate { get; set; }
        public decimal? dcDiscount { get; set; }
        public bool? isTaxable { get; set; }
        public bool? isExpirable { get; set; }
        public string varUom { get; set; }
        public DateTime? dtExpiryDate { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
    }
}