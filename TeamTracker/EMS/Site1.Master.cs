using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTracker.EMS
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("homepage.aspx");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userLogin.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
          
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
           
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminLogin.aspx");
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminUserManagement.aspx");
        }
    }
}