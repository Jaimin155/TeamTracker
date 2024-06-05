<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="TeamTracker.EMS.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <img src="../Assets/Images/home-bg.png" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <!--row can be divided into 12 parts-->
                <div class="col-12">
                    <center>
                        <h2>Our Features</h2>
                        <p><b>Our 3 Primary Features</b></p>
                    </center>
                </div>
            </div>
            <div class="row">
                <!--row can be divided into 12 parts we need 3 part so col-4-->
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/office.jpg" />
                        <h4>Digital Employee Track</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/attendance.jpg" />
                        <h4>Attendance Track</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/employee.jpg" />
                        <h4>Manage Employee Details</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
            </div>
        </div>
    </section>

    <section>
        <img src="../Assets/Images/home-bg2.png" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <!--row can be divided into 12 parts-->
                <div class="col-12">
                    <center>
                        <h2>Our Process</h2>
                        <p><b>We have 3 Primary Process</b></p>
                    </center>
                </div>
            </div>
            <div class="row">
                <!--row can be divided into 12 parts we need 3 part so col-4-->
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/signin.jpg" />
                        <h4>Sign In</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/manage.jpg" />
                        <h4>Track Employee</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="120px" src="../Assets/Images/report.jpg" />
                        <h4>Analyse Report</h4>
                        <p class="text-justify">Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor. reprehenderit</p>
                    </center>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
