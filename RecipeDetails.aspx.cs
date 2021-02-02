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

            favorite = model.Favorites.Where(x => x.RecipeId == recipe.ID && x.UserId == Page.User.Identity.Name).FirstOrDefault();

            Image1.ImageUrl = recipe.Image;
            Label2.Text = recipe.ingredients;
            Label4.Text = recipe.instructions;

            if(favorite == null)
            {
                Button1.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                Button1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Recipe recipe = (Recipe)Session["Recipe"];
            Favorite favorite = new Favorite();

            if (Button1.ForeColor == System.Drawing.Color.Red)
            {
                Button1.ForeColor = System.Drawing.Color.Black;

                favorite = model.Favorites.Where(x => x.RecipeId == recipe.ID && x.UserId == Page.User.Identity.Name).FirstOrDefault();

                model.Entry(favorite).State = EntityState.Deleted;
                model.SaveChanges();
            }
            else
            {
                Button1.ForeColor = System.Drawing.Color.Red;

                favorite.RecipeId = recipe.ID;
                favorite.UserId = Page.User.Identity.Name;

                model.Favorites.Add(favorite);
                model.SaveChanges();

            }
        }
    }
}