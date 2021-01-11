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
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Reciepe> reciepes = new List<Reciepe>(GetReceipes());
            Repeater1.DataSource = reciepes;
            Repeater1.DataBind();
        }

        string Baseurl = "https://api.spoonacular.com/";
        public List<Reciepe> GetReceipes()
        {
            Root receipes = new Root();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("recipes/complexSearch?apiKey=4bf4ec1927934264b57d31947fcbb48e&query=pasta&number=3").Result;

                if (response.IsSuccessStatusCode)
                {
                    var receipeResponse = response.Content.ReadAsStringAsync().Result;
                    receipes = JsonConvert.DeserializeObject<Root>(receipeResponse);

                }
                client.Dispose();
                List<Reciepe> result = new List<Reciepe>(receipes.Results);

                return result;
            }
        }


    }
}