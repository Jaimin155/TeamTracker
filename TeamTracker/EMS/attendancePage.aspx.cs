using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace TeamTracker.EMS
{
    public partial class attendancePage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckPunchStatus();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CheckIfAlreadyPunchedIn())
            {
                Response.Write("<script>alert('You have already punched in today.');</script>");
                return;
            }

            if (PunchIn())
            {
                Session["PunchedIn"] = true;
                punchInDiv.Visible = false;
                punchOutDiv.Visible = true;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!CheckIfAlreadyPunchedIn())
            {
                Response.Write("<script>alert('You need to punch in first.');</script>");
                return;
            }

            if (CheckIfAlreadyPunchedOut())
            {
                Response.Write("<script>alert('You have already punched out today.');</script>");
                return;
            }

            PunchOut();
            Session["PunchedIn"] = false;
            punchInDiv.Visible = true;
            punchOutDiv.Visible = false;
        }

        bool PunchIn()
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    byte[] imageData = null;
                    using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                    {
                        imageData = br.ReadBytes((int)FileUpload1.PostedFile.ContentLength);
                    }

                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        DateTime punchInTime = DateTime.Now;
                        string status = GetPunchInStatus(punchInTime);

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO attendanceManagement_tbl (user_id, full_name, status, date, punchIn_time, note, image) VALUES (@user_id, @full_name, @status, @date, @punchIn_time, @note, @image)", con))
                        {
                            cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                            cmd.Parameters.AddWithValue("@full_name", Session["fullname"]);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@date", punchInTime.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@punchIn_time", punchInTime.ToString("HH:mm:ss"));
                            cmd.Parameters.AddWithValue("@note", TextBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@image", imageData);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Response.Write("<script>alert('Punch In successfully.');</script>");
                                return true;
                            }
                            else
                            {
                                Response.Write("<script>alert('Failed to mark attendance.');</script>");
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please select an image before submitting.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return false;
        }

        void PunchOut()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    DateTime punchOutTime = DateTime.Now;

                    using (SqlCommand cmd = new SqlCommand("SELECT punchIn_time FROM attendanceManagement_tbl WHERE user_id=@user_id AND date=@date", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                        cmd.Parameters.AddWithValue("@date", punchOutTime.ToString("yyyy-MM-dd"));
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            DateTime punchInTime;
                            if (DateTime.TryParse(result.ToString(), out punchInTime))
                            {
                                string status = GetPunchOutStatus(punchInTime, punchOutTime);

                                // Calculate overtime
                                TimeSpan overtime = TimeSpan.Zero;
                                if (punchOutTime.TimeOfDay > TimeSpan.FromHours(16))
                                {
                                    overtime = punchOutTime.TimeOfDay - TimeSpan.FromHours(16);
                                }

                                using (SqlCommand updateCmd = new SqlCommand("UPDATE attendanceManagement_tbl SET punchOut_time=@punchOut_time, status=@status, overtime=@overtime WHERE user_id=@user_id AND date=@date", con))
                                {
                                    updateCmd.Parameters.AddWithValue("@punchOut_time", punchOutTime.ToString("HH:mm:ss"));
                                    updateCmd.Parameters.AddWithValue("@status", status);
                                    updateCmd.Parameters.AddWithValue("@overtime", overtime.ToString(@"hh\:mm\:ss"));
                                    updateCmd.Parameters.AddWithValue("@user_id", Session["username"]);
                                    updateCmd.Parameters.AddWithValue("@date", punchOutTime.ToString("yyyy-MM-dd"));

                                    int rowsAffected = updateCmd.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        Response.Write("<script>alert('Punch Out Successfully');</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Something went wrong in Punching Out!');</script>");
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Punch In Time format.');</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        bool CheckIfAlreadyPunchedIn()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM attendanceManagement_tbl WHERE user_id = @user_id AND date = @date AND punchIn_time IS NOT NULL", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return false;
        }

        bool CheckIfAlreadyPunchedOut()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM attendanceManagement_tbl WHERE user_id = @user_id AND date = @date AND punchOut_time IS NOT NULL", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            return false;
        }

        void CheckPunchStatus()
        {
            if (CheckIfAlreadyPunchedIn() && !CheckIfAlreadyPunchedOut())
            {
                punchInDiv.Visible = false;
                punchOutDiv.Visible = true;
                Session["PunchedIn"] = true;
            }
            else
            {
                punchInDiv.Visible = true;
                punchOutDiv.Visible = false;
                Session["PunchedIn"] = false;
            }
        }

        string GetPunchInStatus(DateTime punchInTime)
        {
            // Check if the current day is a weekend
            if (punchInTime.DayOfWeek == DayOfWeek.Saturday || punchInTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return "Weekend";
            }
            else
            {
                // If it's a weekday, check if the punch in time is within the working hours
                return (punchInTime.TimeOfDay >= TimeSpan.FromHours(9) && punchInTime.TimeOfDay <= TimeSpan.FromHours(16)) ? "Present" : "Absent";
            }
        }

        string GetPunchOutStatus(DateTime punchInTime, DateTime punchOutTime)
        {
            if (punchInTime.DayOfWeek == DayOfWeek.Saturday || punchInTime.DayOfWeek == DayOfWeek.Sunday)
            {
                // Calculate overtime if the punch out time is after 16:00:00 on a weekend
                TimeSpan punchOutTimeOfDay = punchOutTime.TimeOfDay;
                if (punchOutTimeOfDay > TimeSpan.FromHours(16))
                {
                    return "Overtime";
                }
                else
                {
                    return "Present";
                }
            }
            else
            {
                // If it's a weekday, calculate overtime if the punch out time is after 16:00:00
                TimeSpan punchOutTimeOfDay = punchOutTime.TimeOfDay;
                if (punchOutTimeOfDay > TimeSpan.FromHours(16))
                {
                    return "Overtime";
                }
                else
                {
                    return "Present";
                }
            }
        }

        public void MarkAbsentForMissedPunchIns()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Fetch users who have not punched in by 16:00:00
                    string query = "SELECT user_id, full_name FROM attendanceManagement_tbl WHERE punchIn_time IS NULL AND date = @date AND status <> 'Weekend'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string userId = reader["user_id"].ToString();
                                string fullName = reader["full_name"].ToString();
                                MarkUserAbsent(userId, fullName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void MarkUserAbsent(string userId, string fullName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE attendanceManagement_tbl SET status = 'Absent' WHERE user_id = @user_id AND date = @date", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
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
