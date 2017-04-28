using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PopularNames.Models
{
    public class Entry
    {
        public Entry()
        {
            this.Girls = new List<string>();
            this.Boys = new List<string>();
        }

        public string Year { get; set; }
        public List<string> Girls { get; set; }
        public List<string> Boys { get; set; }
    }
}