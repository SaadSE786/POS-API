using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Level3
    {
        public int intLevel3Id { get; set; }
        public string varLevel3Name { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public int? intLevel2Id { get; set; }
        public string varLevel2Name { get; set; }
    }
}