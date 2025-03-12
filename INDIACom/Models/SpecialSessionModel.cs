using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDIACom.Models
{
    public class SpecialSessionModel
    {

        public string SSName { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }
        public string Topic { get; set; }
        public DateTime Request_Date { get; set; }
    }
}