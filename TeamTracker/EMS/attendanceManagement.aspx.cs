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
            GridView1.DataBind();
        }

        // Fetch
        protected void Button2_Click(object sender, EventArgs e)
        {
            getUserDetails();
            getAttendanceDetails();
        }

        // Clear
        protected void Button4_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
        }

        // user-defined functions
        void getUserDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);                
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("SELECT full_name FROM employee_master_tbl WHERE user_id = @user_id", con);
                    cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
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

        void getAttendanceDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);                
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand(
                        @"SELECT full_name, date, punchIn_time, punchOut_time, status, overtime 
                          FROM attendanceManagement_tbl 
                          WHERE user_id = @user_id 
                          AND date BETWEEN @fromDate AND @toDate", con);
                    cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@fromDate", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@toDate", TextBox6.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Calculate Total Leave, Total Present, and Overtime
                        int totalLeave = 0;
                        int totalPresent = 0;
                        TimeSpan totalOvertime = TimeSpan.Zero;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["status"].ToString() == "Present")
                            {
                                totalPresent++;
                                if (TimeSpan.TryParse(row["Overtime"].ToString(), out TimeSpan overtime))
                                {
                                    totalOvertime += overtime;
                                }
                            }
                            else if (row["status"].ToString() == "Absent")
                            {
                                totalLeave++;
                            }
                        }

                        TextBox3.Text = totalPresent.ToString();
                        TextBox4.Text = totalLeave.ToString();
                        TextBox5.Text = $"{totalOvertime.Hours} hours {totalOvertime.Minutes} minutes";
                    }
                    else
                    {
                        Response.Write("<script>alert('No records found for the specified date range.');</script>");
                    }                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
