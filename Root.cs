using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Licenta
{
    public class Root
    {
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "number")]
        public int Number { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Recipe> Results { get; set; }

        [JsonProperty(PropertyName = "totalResults")]
        public int TotalResults { get; set; }
    }
}