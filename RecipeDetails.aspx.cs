using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Licenta
{
    public partial class RecipeDetails : System.Web.UI.Page
    {
        LicentaEntities model = new LicentaEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            Recipe recipe = new Recipe();
            Favorite favorite = new Favorite();

            recipe = (Recipe)Session["Recipe"];

            favorite = model.Favorites.Where(x => x.RecipeId == recipe.ID && x.UserId == HttpContext.Current.Request.LogonUserIdentity.Name).FirstOrDefault();

            Image1.ImageUrl = recipe.Image;
            Label2.Text = recipe.ingredients;
            Label4.Text = recipe.instructions;

            DropDownList1.SelectedValue = favorite.Rating.ToString();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recipe recipe = (Recipe)Session["Recipe"];
            Favorite favorite = new Favorite();

            favorite.RecipeId = recipe.ID;
            favorite.UserId = HttpContext.Current.Request.LogonUserIdentity.Name;
            favorite.Rating = Int32.Parse(DropDownList1.SelectedValue);

            model.Favorites.Add(favorite);
            model.SaveChanges();
        }
    }
}