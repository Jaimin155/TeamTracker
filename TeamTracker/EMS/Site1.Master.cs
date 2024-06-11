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
            try
            {
                if (Session["role"].Equals(""))
                {
                    LinkButton1.Visible = false; //user PRofile
                    LinkButton2.Visible = true; //user login
                    LinkButton3.Visible = false; //logout login
                    LinkButton4.Visible = false; //hello user login
                    LinkButton5.Visible = true; //adimin login
                    LinkButton6.Visible = false; //user management
                    LinkButton7.Visible = false; //user profile
                }
                else if (Session["role"].Equals("user"))
                {
                    LinkButton1.Visible = true; //user PRofile
                    LinkButton2.Visible = false; //user login
                    LinkButton3.Visible = true; //logout login
                    LinkButton4.Visible = true; //hello user login
                    LinkButton4.Text = "Hello "+Session["username"].ToString();
                    LinkButton5.Visible = true; //adimin login
                    LinkButton6.Visible = false; //user management
                    LinkButton7.Visible = false; //user profile
                }
                else if (Session["role"].Equals("admin"))
                {
                    LinkButton1.Visible = false; //user PRofile
                    LinkButton2.Visible = false; //user login
                    LinkButton3.Visible = true; //logout login
                    LinkButton4.Visible = true; //hello user login
                    LinkButton4.Text = "Hello Admin";
                    LinkButton5.Visible = false; //adimin login
                    LinkButton6.Visible = true; //user management
                    LinkButton7.Visible = true; //user profile
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("userProfile.aspx");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("userLogin.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            //Session["fullname"]=dr.GetValue(8).ToString();
            Session["role"] = "";
            LinkButton1.Visible = false; //user PRofile
            LinkButton2.Visible = true; //user login
            LinkButton3.Visible = false; //logout login
            LinkButton4.Visible = false; //hello user login
            LinkButton5.Visible = true; //adimin login
            LinkButton6.Visible = false; //user management
            LinkButton7.Visible = false; //user profile
            Response.Redirect("homepage.aspx");
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
            Response.Redirect("userManagement.aspx");
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminUserManagement.aspx");
        }
    }
}