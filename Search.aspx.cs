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
using System.Text.RegularExpressions;


namespace Licenta
{
    public partial class Search : System.Web.UI.Page
    {
        static List<Recipe> global_recipes;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label2.Text = "Search what you would like to eat.";

        }

        static string baseUrl = "https://api.spoonacular.com/";
        public List<Recipe> GetReceipes( string search, string ingredientsTxt)
        {
            string ingredients = Regex.Replace(ingredientsTxt, @"\s+", ",");
            Root receipes = new Root();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string req = $"recipes/complexSearch?apiKey=4bf4ec1927934264b57d31947fcbb48e&query={search}&number=1";

                if(!string.IsNullOrEmpty(ingredients) && !string.IsNullOrWhiteSpace(ingredients))
                {
                    req += $"&includeIngredients={ingredients}";
                }

                HttpResponseMessage response = client.GetAsync(req).Result;

                if (response.IsSuccessStatusCode)
                {
                    var receipeResponse = response.Content.ReadAsStringAsync().Result;
                    receipes = JsonConvert.DeserializeObject<Root>(receipeResponse);

                }
                client.Dispose();
                List<Recipe> result = new List<Recipe>(receipes.Results);

                foreach(Recipe recipe in result)
                {
                    recipe.ingredients = GetReceipeIngredients(recipe.ID);
                    recipe.instructions = GetReceipeInstructions(recipe.ID);
                }

                return result;
            }
        }

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

        public IngredientsAndInstructions GetReceipeById()
        {
            int id = Convert.ToInt32(Request.QueryString["recipeID"]);

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

        public string GetReceipeIngredients(int id)
        {
            
            IngredientsRoot ingredients = new IngredientsRoot();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage responseIngredients = client.GetAsync($"recipes/{id}//ingredientWidget.json?apiKey=4bf4ec1927934264b57d31947fcbb48e").Result;

                if (responseIngredients.IsSuccessStatusCode)
                {
                    var response = responseIngredients.Content.ReadAsStringAsync().Result;
                    ingredients = JsonConvert.DeserializeObject<IngredientsRoot>(response);

                }

                client.Dispose();

                string result = "";

                foreach (Ingredient ingredient in ingredients.ingredients)
                {
                    result += ingredient.amount.metric.value + " " + ingredient.amount.metric.unit + " of "  + ingredient.name + "<br />";
                }
                
                return result;
            }

        }

        public string GetReceipeInstructions(int id)
        {
            List<InstructionsRoot> instructions = new List<InstructionsRoot>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseInstructions = client.GetAsync($"recipes/{id}/analyzedInstructions?apiKey=4bf4ec1927934264b57d31947fcbb48e").Result;

                if (responseInstructions.IsSuccessStatusCode)
                {
                    var response = responseInstructions.Content.ReadAsStringAsync().Result;
                    instructions = JsonConvert.DeserializeObject<List<InstructionsRoot>>(response);
                }

                client.Dispose();

                string result = "";

                foreach (Step step in instructions[0].steps)
                {
                    result += step.step + "<br />";
                }

                return result;
            }

        }

        public static Recipe GetRecipeObject(string id)
        {
            var ret = (from l in global_recipes
                            where l.ID.ToString() == id
                            select l).Distinct().ToList();
            Recipe res = (Recipe)ret.FirstOrDefault();

            return res;
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Item")
            {
                Label label1 = (Label)DataList1.Items[e.Item.ItemIndex].FindControl("Label3");
                string labelValue = label1.Text;

                Page.ClientScript.RegisterStartupScript( this.GetType(), "OpenWindow", "window.open('RecipeDetails.aspx','_newtab');", true);

                Session["Recipe"] = GetRecipeObject(labelValue);
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string searchString = searchTxt.Text;
            string searchStringI = TextBox1.Text;
            List <Recipe> reciepes = new List<Recipe>(GetReceipes(searchString, searchStringI));
            global_recipes = reciepes;
            DataList1.DataSource = reciepes;
            DataList1.DataBind();
        }
    }
}