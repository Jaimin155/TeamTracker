using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTracker.EMS
{
    public partial class adminUserManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //go button
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getUserById();
        }
        //fromHome
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateUserStatusById("WorkFromHome");
        }
        //hibrid
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
             updateUserStatusById("Pending");
        }
        //onoffice
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
             updateUserStatusById("onOffice");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteUserById();
        }

        //user defined function
        bool checkIfuserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM employee_master_tbl WHERE user_id = '" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void getUserById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM employee_master_tbl WHERE user_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox2.Text= dr.GetValue(0).ToString();
                        TextBox7.Text= dr.GetValue(10).ToString();
                        TextBox8.Text= dr.GetValue(1).ToString();
                        TextBox3.Text= dr.GetValue(2).ToString();
                        TextBox4.Text= dr.GetValue(3).ToString();
                        TextBox9.Text= dr.GetValue(4).ToString();
                        TextBox10.Text= dr.GetValue(5).ToString();
                        TextBox11.Text= dr.GetValue(6).ToString();
                        TextBox6.Text= dr.GetValue(7).ToString();                        
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials');</script>)");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void updateUserStatusById(string status)
        {
            if (checkIfuserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE employee_master_tbl SET account_status='" + status + "' WHERE user_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Employee Status Updated Successfully');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Employee Id');</script>");
            }
        }
        void deleteUserById()
        {
            if (checkIfuserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("DELETE FROM user_login_tbl WHERE user_id='" + TextBox1.Text.Trim() + "' ", con);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("DELETE FROM employee_master_tbl WHERE user_id='" + TextBox1.Text.Trim() + "' ", con);
                    cmd2.ExecuteNonQuery();

                    con.Close();
                    Response.Write("<script>alert('User deleted Successfully');</script>");
                    clearForm();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }                
            }
            else
            {
                Response.Write("<script>alert('Invalid Employee Id');</script>");
            }
        }
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox6.Text = "";
        }
    }
}