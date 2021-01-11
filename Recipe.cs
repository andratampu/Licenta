using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta
{
    public class Recipe
    {
        public int ID { get; set; }
        public int Calories { get; set; }
        public string Carbs { get; set; }
        public string Fat { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public string Protein { get; set; }
        public string Title { get; set; }
    }
}