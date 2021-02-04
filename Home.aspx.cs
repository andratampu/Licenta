using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Identity;

namespace Licenta
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (LicentaEntities entities = new LicentaEntities())
            {
                var ret = entities.Users.FirstOrDefault(x => x.Username == HttpContext.Current.Request.LogonUserIdentity.Name);
                if (ret == null)
                {
                    entities.Users.Add(new User { Username = HttpContext.Current.Request.LogonUserIdentity.Name });
                    entities.SaveChanges();
                }
            }
        }
    }
}