using Microsoft.Ajax.Utilities;
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
    public partial class userManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        //go button 
        protected void Button1_Click(object sender, EventArgs e)
        {
            getUserByID();
        }

        //add button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfuserExists())
            {
                Response.Write("<script>alert('Employee Id already Exists');</script>");
            }
            else
            {
                addNewUser();
            }
        }

        //update button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfuserExists())
            {
                updateUser();
            }
            else
            {
                Response.Write("<script>alert('Employee Id doesn't Exists');</script>");
            }
        }

        //delete button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfuserExists())
            {
                deleteUser();
            }
            else
            {
                Response.Write("<script>alert('Employee Id doesn't Exists');</script>");
            }
        }

        //user defined function        
        void getUserByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM user_login_tbl WHERE user_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][2].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid user ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void deleteUser()
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
        void updateUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("UPDATE user_login_tbl SET user_name=@user_name,password=@password WHERE user_id='" + TextBox1.Text.Trim() + "'", con);
                cmd.Parameters.AddWithValue("@user_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@password",TextBox2.Text.Trim().ToUpper().Substring(0,1) + TextBox2.Text.Trim().ToLower().Substring(1, 1) + "@" + TextBox1.Text.Trim().Substring(2,3));
                
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('User updated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void addNewUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO user_login_tbl (user_id,password,user_name) VALUES (@user_id,@password,@user_name)", con);
                cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@user_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim().ToUpper().Substring(0,1) + TextBox2.Text.Trim().ToLower().Substring(1, 1) + "@" + TextBox1.Text.Trim().Substring(2,3));

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('User added Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        bool checkIfuserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM user_login_tbl WHERE user_id = '" + TextBox1.Text.Trim() + "';", con);
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
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

    }
}