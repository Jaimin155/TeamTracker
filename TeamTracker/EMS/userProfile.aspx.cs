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

        }
        //update buttion click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                { 
                    con.Open(); 
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO employee_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,account_status) VALUES (@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@account_status)", con);
                cmd.Parameters.AddWithValue("@full_name",TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob",TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no",TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@email",TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@state",DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city",TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode",TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address",TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "Office");

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Successfully Updated Profile');</script>");
            }
            catch (Exception ex) 
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}