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

                string req = $"recipes/complexSearch?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff&query={search}&number=15";

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

                HttpResponseMessage responseInstructions = client.GetAsync($"recipes/{id}/analyzedInstructions?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;
                HttpResponseMessage responseIngredients = client.GetAsync($"recipes/{id}//ingredientWidget.json?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;
                
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

                HttpResponseMessage responseInstructions = client.GetAsync($"recipes/{id}/analyzedInstructions?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;
                HttpResponseMessage responseIngredients = client.GetAsync($"recipes/{id}//ingredientWidget.json?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;

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

                if(instructions!=null && instructions.steps!=null)
                    foreach (Step step in instructions.steps)
                    {
                        result.instructions.Add(step.step);
                    }

                if (ingredients != null && ingredients.ingredients != null)
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
                
                HttpResponseMessage responseIngredients = client.GetAsync($"recipes/{id}//ingredientWidget.json?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;

                if (responseIngredients.IsSuccessStatusCode)
                {
                    var response = responseIngredients.Content.ReadAsStringAsync().Result;
                    ingredients = JsonConvert.DeserializeObject<IngredientsRoot>(response);

                }

                client.Dispose();

                string result = "";

                if(ingredients != null && ingredients.ingredients !=null)
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

                HttpResponseMessage responseInstructions = client.GetAsync($"recipes/{id}/analyzedInstructions?apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;

                if (responseInstructions.IsSuccessStatusCode)
                {
                    var response = responseInstructions.Content.ReadAsStringAsync().Result;
                    instructions = JsonConvert.DeserializeObject<List<InstructionsRoot>>(response);
                }

                client.Dispose();

                string result = "";
                if(instructions !=null && instructions.Count>0)
                    if(instructions[0].steps!=null)
                        foreach (Step step in instructions[0].steps)
                        {
                            result += step.step + "<br />";
                        }

                return result;
            }

        }

        public Recipe GetReceipeInfo(int id)
        {
            Recipe recipe = new Recipe();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseRecipe = client.GetAsync($"recipes/{id}/information?includeNutrition=false&apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;
                if (responseRecipe.IsSuccessStatusCode)
                {
                    var response = responseRecipe.Content.ReadAsStringAsync().Result;
                    if(response!=null)
                        recipe = JsonConvert.DeserializeObject<Recipe>(response);
                }

                client.Dispose();

                return recipe;
            }
        }

        public List<Recipe> GetRandomRecipes()
        {
            Root receipes = new Root();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.GetAsync($"recipes/random?number=15&apiKey=f0bc1cb5a0da48e1b8282cba70ca05ff").Result;

                if (response.IsSuccessStatusCode)
                {
                    var receipeResponse = response.Content.ReadAsStringAsync().Result;
                    receipes = JsonConvert.DeserializeObject<Root>(receipeResponse);

                }
                client.Dispose();
                List<Recipe> result = new List<Recipe>(receipes.Results);

                foreach (Recipe recipe in result)
                {
                    recipe.ingredients = GetReceipeIngredients(recipe.ID);
                    recipe.instructions = GetReceipeInstructions(recipe.ID);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            LicentaEntities model = new LicentaEntities();

            Recommendation recommendation = model.Recommendations.FirstOrDefault(x => x.UserId == HttpContext.Current.Request.LogonUserIdentity.Name);

            if(recommendation == null)
            {
                List<Recipe> recipes = new List<Recipe>(GetRandomRecipes());
                global_recipes = recipes;
                DataList1.DataSource = recipes;
                DataList1.DataBind();
            }
            else
            {
                List<Recipe> recipes = new List<Recipe>();
                int count = 0;
                foreach(string id in recommendation.Recommendations.Split(','))
                {
                    if (count >= 15)
                        break;

                    Recipe recipe = GetReceipeInfo(Int32.Parse(id));
                    if(!string.IsNullOrEmpty(recipe.Title) || !string.IsNullOrWhiteSpace(recipe.Title))
                    {
                        recipe.ingredients = GetReceipeIngredients(recipe.ID);
                        recipe.instructions = GetReceipeInstructions(recipe.ID);
                        count++;
                        recipes.Add(recipe);

                    }
                }

                global_recipes = recipes;
                DataList1.DataSource = recipes;
                DataList1.DataBind();
            }

        }
    }
}