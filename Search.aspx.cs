using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace Licenta
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Recipe> reciepes = new List<Recipe>(GetReceipes());
            Repeater2.DataSource = reciepes;
            Repeater2.DataBind();
        }

        string baseUrl = "https://api.spoonacular.com/";
        public List<Recipe> GetReceipes()
        {
            Root receipes = new Root();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("recipes/complexSearch?apiKey=4bf4ec1927934264b57d31947fcbb48e&query=pasta&number=3").Result;

                if (response.IsSuccessStatusCode)
                {
                    var receipeResponse = response.Content.ReadAsStringAsync().Result;
                    receipes = JsonConvert.DeserializeObject<Root>(receipeResponse);

                }
                client.Dispose();
                List<Recipe> result = new List<Recipe>(receipes.Results);

                return result;
            }
        }
    }
}