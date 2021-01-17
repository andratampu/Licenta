using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta
{
    public class Step
    {
        public List<Equipment> equipment { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public int number { get; set; }
        public string step { get; set; }
        public Length length { get; set; }
    }
}