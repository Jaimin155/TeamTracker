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
                CheckAndAddMissedPunchOuts();
                MarkAbsentForMissedPunchIns();
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
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO attendanceManagement_tbl (user_id, full_name, status, date, punchIn_time, note, image) VALUES (@user_id, @full_name, @status, @date, @punchIn_time, @note, @image)", con))
                        {
                            cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                            cmd.Parameters.AddWithValue("@full_name", Session["fullname"]);
                            cmd.Parameters.AddWithValue("@status", "Present");
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@punchIn_time", DateTime.Now.ToString("HH:mm:ss"));
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
                    using (SqlCommand cmd = new SqlCommand("UPDATE attendanceManagement_tbl SET punchOut_time=@punchOut_time WHERE user_id=@user_id AND date=@date", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", Session["username"]);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@punchOut_time", DateTime.Now.ToString("HH:mm:ss"));
                        int rowsAffected = cmd.ExecuteNonQuery();
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

        void CheckAndAddMissedPunchOuts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("UPDATE attendanceManagement_tbl SET punchOut_time = @defaultPunchOutTime WHERE punchOut_time IS NULL AND date < @currentDate", con))
                    {
                        cmd.Parameters.AddWithValue("@defaultPunchOutTime", "23:59:59");
                        cmd.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void MarkAbsentForMissedPunchIns()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    // Identify the users who have not punched in today
                    using (SqlCommand cmd = new SqlCommand("SELECT user_id FROM user_login_tbl WHERE user_id NOT IN (SELECT user_id FROM attendanceManagement_tbl WHERE date = @currentDate)", con))
                    {
                        cmd.Parameters.AddWithValue("@currentDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string userId = reader["user_id"].ToString();
                                MarkUserAbsent(userId);
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

        void MarkUserAbsent(string userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO attendanceManagement_tbl (user_id, full_name, status, date, punchIn_time, punchOut_time) VALUES (@user_id, (SELECT user_name FROM user_login_tbl WHERE user_id = @user_id), @status, @date, NULL, NULL)", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", userId);
                        cmd.Parameters.AddWithValue("@status", "Absent");
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
    }
}
