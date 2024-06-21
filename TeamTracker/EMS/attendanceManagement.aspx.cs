using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeamTracker.EMS
{
    public partial class attendanceManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        { 

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            getName();
        }
        //Fetch
        protected void Button2_Click(object sender, EventArgs e)
        {
            attendance();
        }
        //reset
        protected void Button4_Click(object sender, EventArgs e)
        {
            getName();
        }

        //user defined functions
        void getName()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT full_name FROM employee_master_tbl WHERE user_id = '" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["full_name"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Wrong Employee ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void attendance()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO attendanceManagement_tbl (user_id,full_name,present,leave,overtime,working_days,date,time,status,updated_time,overtime_amount) VALUES (@user_id,@full_name,@present,@leave,@overtime,@working_days,@date,@time,@status,@updated_time,@overtime_amount)", con);
                cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@full_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@present", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@leave", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@overtime", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@working_days", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@date",TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@time",DateTime.Now );
                cmd.Parameters.AddWithValue("@status", "Present");
                cmd.Parameters.AddWithValue("@updated_time",DateTime.Now);
                cmd.Parameters.AddWithValue("@overtime_amount","67777");
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Employees Details Fetch Successfully');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        
    }
}