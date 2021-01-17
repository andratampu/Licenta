using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Licenta
{
    public class Ingredient
    {
        [JsonProperty(PropertyName = "amount")]
        public Amount amount { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
    }
}