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
    public partial class DetailsPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IngredientsAndInstructions ingredientsAndInstructions = GetReceipeById();

            DataList1.DataSource = ingredientsAndInstructions.ingredients;
            DataList1.DataBind();

            DataList2.DataSource = ingredientsAndInstructions.instructions;
            DataList2.DataBind();
        }

        static string baseUrl = "https://api.spoonacular.com/";

        public IngredientsAndInstructions GetReceipeById()
        {
            int id = Int32.Parse(Session["RecipeID"].ToString());

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

                foreach (Step step in instructions.steps)
                {
                    result.instructions.Add(step.step);
                }

                foreach (Ingredient ingredient in ingredients.ingredients)
                {
                    result.ingredients.Add(ingredient.amount.metric.value + ingredient.amount.metric.unit + " of " + ingredient.name);
                }

                return result;
            }
        }
    }
}