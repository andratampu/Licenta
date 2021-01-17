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

using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;


namespace Licenta
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Recipe> reciepes = new List<Recipe>(GetReceipes());
            DataList1.DataSource = reciepes;
            DataList1.DataBind();
        }

        static string baseUrl = "https://api.spoonacular.com/";
        public List<Recipe> GetReceipes()
        {
            Root receipes = new Root();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("recipes/complexSearch?apiKey=4bf4ec1927934264b57d31947fcbb48e&query=pasta&number=17").Result;

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

        //[WebMethod]
        //public static List<string> GetAutocomplete(string searchTxt)
        //{
        //    List<Autocomplete> autocomplete = new List<Autocomplete>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(baseUrl);

        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage response = client.GetAsync($"food/ingredients/autocomplete?apiKey=4bf4ec1927934264b57d31947fcbb48e&query={searchTxt}&number=6").Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var autocompletes = response.Content.ReadAsStringAsync().Result;
        //            autocomplete = JsonConvert.DeserializeObject<List<Autocomplete>>(autocompletes);

        //        }
        //        client.Dispose();

        //        List<string> result = new List<string>();

        //        foreach (Autocomplete auto in autocomplete)
        //        {
        //            result.Add(auto.Name);
        //        }

        //        return result;


        //    }
        //}

        public IngredientsAndInstructions GetReceipeById(int id)
        {
            InstructionsRoot instructions = new InstructionsRoot();
            IngredientsRoot ingredients = new IngredientsRoot();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseInstructions = client.GetAsync($"recipes/{id}/analyzedInstructions?apiKey=4bf4ec1927934264b57d31947fcbb48e").Result;
                HttpResponseMessage responseIngredients = client.GetAsync($"recipes/{id}//ingredientWidget.json?apiKey=4bf4ec1927934264b57d31947fcbb48e").Result;
                
                if (responseInstructions.IsSuccessStatusCode)
                {
                    var response = responseInstructions.Content.ReadAsStringAsync().Result;
                    instructions = JsonConvert.DeserializeObject<InstructionsRoot>(response);

                }

                if (responseIngredients.IsSuccessStatusCode)
                {
                    var response = responseIngredients.Content.ReadAsStringAsync().Result;
                    ingredients = JsonConvert.DeserializeObject<IngredientsRoot>(response);

                }

                client.Dispose();

                IngredientsAndInstructions result = new IngredientsAndInstructions();

                foreach(Step step in instructions.steps)
                {
                    result.instructions.Add(step.step);
                }

                foreach(Ingredient ingredient in ingredients.ingredients)
                {
                    result.ingredients.Add(ingredient.amount.metric.value + ingredient.amount.metric.unit + " of " + ingredient.name);
                }

                return result;
            }
        }

        protected void Button1_Click(object sender, ImageClickEventArgs e)
        {
            Session["RecipeId"] = Label1.Text;
            Response.Redirect("DetailsPopup.aspx");
        }

        //protected void btnOk_Click(object sender, ImageClickEventArgs e)
        //{
        //    //Do Work

        //    mpePopUp.Hide();
        //}

        //protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        //{
        //    //Do Work

        //    mpePopUp.Hide();
        //}

    }
}