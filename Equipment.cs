using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta
{
    public class Equipment
    {
        public int id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public Temperature temperature { get; set; }
    }
}