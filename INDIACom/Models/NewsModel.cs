using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDIACom.Models
{
    public class NewsModel
    {
        public int NewsId { get; set; }
        public string Headline { get; set; }
        public string Details { get; set; }
        public DateTime NewsDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Event { get; set; }
        public string FilePath { get; set; }
    }
}