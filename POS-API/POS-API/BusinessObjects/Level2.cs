using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POS_API.BusinessObjects
{
    public class Level2
    {
        public int intLevel2Id { get; set; }
        public string varLevel2Name { get; set; }
        public DateTime? dtCreationDate { get; set; }
        public DateTime? dtUpdationDate { get; set; }
        public int? intCreatedBy { get; set; }
        public int? intUpdatedBy { get; set; }
        public int? intLevel1Id { get; set; }
        public string varLevel1Name { get; set; }
    }
}