using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TeamTracker.EMS
{
    public partial class userProfile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userLogin.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        getUserData();
                    }
                }
            }
            catch (Exception ex) 
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userLogin.aspx");
            }
        }
        //update buttion click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userLogin.aspx");
            }
            else
            {
                updateUserData();
            }
        }

        //user defined function
        void getUserData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM employee_master_tbl WHERE user_id='" + Session["username"].ToString() + "';",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                TextBox2.Text = dt.Rows[0]["dob"].ToString();
                TextBox8.Text = dt.Rows[0]["user_id"].ToString();
                DropDownList2.SelectedValue = dt.Rows[0]["gender"].ToString().Trim();
                DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox4.Text = dt.Rows[0]["email"].ToString();
                TextBox6.Text = dt.Rows[0]["city"].ToString();
                TextBox5.Text = dt.Rows[0]["full_address"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();
                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void updateUserData()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd2 = new SqlCommand("UPDATE user_login_tbl SET user_name=@user_name,password=@password WHERE user_id='" + Session["username"].ToString().Trim() + "'", con);
                cmd2.Parameters.AddWithValue("@user_name", TextBox1.Text.Trim());
                cmd2.Parameters.AddWithValue("@password", password);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("UPDATE employee_master_tbl SET full_name=@full_name,dob=@dob,gender=@gender,contact_no=@contact_no,email=@email,state=@state,city=@city,pincode=@pincode,full_address=@full_address,account_status=@account_status,password=@password WHERE user_id='" + Session["username"].ToString().Trim() + "'", con);
                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@gender", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox5.Text.Trim());                
                cmd.Parameters.AddWithValue("@password", password);                
                cmd.Parameters.AddWithValue("@account_status", "Pending");
                int result= cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {
                    Response.Write("<script>alert('Your Details updated Successfully');</script>");
                    getUserData();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Entry!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}