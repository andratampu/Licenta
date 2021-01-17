using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta
{
    public class Autocomplete
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Id { get; set; }
        public string Aisle { get; set; }
        public List<string> PossibleUnits { get; set; }

    }
}