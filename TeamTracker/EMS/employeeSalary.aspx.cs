using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TeamTracker.EMS
{
    public partial class employeeSalary : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        const decimal monthlySalary = 90000;
        const decimal overtimeRate = 200; // per 30 minutes
        const decimal dailySalary = monthlySalary / 30;//excluding 8 days of weekends

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            getName();
            calculateSalary();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            previousRecords();
            removeAttendanceRecords();
        }

        // Get employee name based on Employee ID
        void getName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
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
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        // Calculate salary based on attendance records
        void calculateSalary()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT date, status, overtime 
                          FROM attendanceManagement_tbl 
                          WHERE user_id = @user_id 
                          AND date BETWEEN @fromDate AND @toDate", con);
                    cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@fromDate", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@toDate", TextBox6.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    int totalWorkedDays = 0;
                    int totalAbsents = 0;
                    TimeSpan totalOvertime = TimeSpan.Zero;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["status"].ToString() == "Present" || row["status"].ToString()=="Overtime")
                        {
                            totalWorkedDays++;
                            if (TimeSpan.TryParse(row["Overtime"].ToString(), out TimeSpan overtime))
                            {
                                totalOvertime += overtime;
                            }
                        }
                        else if (row["status"].ToString() == "Absent")
                        {
                            totalAbsents++;
                        }                        
                    }

                    TextBox3.Text = totalWorkedDays.ToString();
                    TextBox5.Text = $"{totalOvertime.Hours} hours {totalOvertime.Minutes} minutes";

                    decimal salary = monthlySalary;
                    int excessAbsents = totalAbsents > 3 ? totalAbsents - 3 : 0;
                    salary -= excessAbsents * dailySalary;
                    salary += ((decimal)totalOvertime.TotalMinutes / 30) * overtimeRate;

                    TextBox4.Text = salary.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void previousRecords()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO previous_record (user_id,fullname,fromDate,toDate,total_worked,salary) VALUES (@user_id,@fullname,@fromDate,@toDate,@total_worked,@salary)", con);
                cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@fullname", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@fromDate", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@toDate", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@total_worked", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@salary", TextBox4.Text.Trim());
                cmd.ExecuteNonQuery();           
                con.Close();
                Response.Write("<script>alert('User record added Successfully');</script>");
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        // Remove attendance records within the specified date range
        void removeAttendanceRecords()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(
                        @"DELETE FROM attendanceManagement_tbl 
                          WHERE user_id = @user_id 
                          AND date BETWEEN @fromDate AND @toDate", con);
                    cmd.Parameters.AddWithValue("@user_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@fromDate", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@toDate", TextBox6.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Response.Write("<script>alert('Records deleted successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('No records found to delete');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}