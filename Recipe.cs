using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Licenta
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "calories")]
        public int Calories { get; set; }
        [JsonProperty(PropertyName = "carbs")]
        public string Carbs { get; set; }
        [JsonProperty(PropertyName = "fat")]
        public string Fat { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "imageType")]
        public string ImageType { get; set; }
        [JsonProperty(PropertyName = "protein")]
        public string Protein { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //public List<ReturnedIngredient> returnedIngredients { get; set; }

        public string ingredients { get; set; }

        //public List<ReturnedInstructions> returnedInstructions { get; set; }

        public string instructions { get; set; }
    }
}