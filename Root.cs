using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta
{
    public class Root
    {
        public int Offset { get; set; }
        public int Number { get; set; }
        public List<Recipe> Results { get; set; }
        public int TotalResults { get; set; }
    }
}