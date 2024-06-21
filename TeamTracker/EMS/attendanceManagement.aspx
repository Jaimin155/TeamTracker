<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="attendanceManagement.aspx.cs" Inherits="TeamTracker.EMS.attendanceManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Employees Attendance Detail</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img width="100px" src="../Assets/Images/avatar1.jpg" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr color="black">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Employee Id</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Employee Id"></asp:TextBox>
                                        <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <label>Employee Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Employee Name" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Date</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Total Present</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Total Present"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Total Leave</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Total Leave"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Over Time</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Over Time"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Total Working Days</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Total Working Days"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="Button2" runat="server" Text="Fetch" CssClass="btn btn-mx btn-block btn-primary" OnClick="Button2_Click" />
                            </div>
                            <div class="col-6">
                                <asp:Button ID="Button4" runat="server" Text="Reset" CssClass="btn btn-mx btn-block btn-danger" OnClick="Button4_Click" />
                            </div>

                        </div>
                    </div>
                </div>
                <a href="../../EMS/homepage.aspx"><< Back to Home</a><br>
                <br>
            </div>
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>All Employees</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr color="black">
                            </div>
                        </div>
                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:emsDBConnectionString %>' SelectCommand="SELECT * FROM [attendanceManagement_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered dt-responsive" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="user_id" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="user_id" HeaderText="Employee ID" ReadOnly="True" SortExpression="user_id"></asp:BoundField>
                                        <asp:BoundField DataField="full_name" HeaderText="Name" SortExpression="full_name"></asp:BoundField>
                                        <asp:BoundField DataField="present" HeaderText="Present" SortExpression="present"></asp:BoundField>
                                        <asp:BoundField DataField="leave" HeaderText="Leave" SortExpression="leave"></asp:BoundField>
                                        <asp:BoundField DataField="overtime" HeaderText="OverTime" SortExpression="overtime"></asp:BoundField>
                                        <asp:BoundField DataField="working_days" HeaderText="Working Days" SortExpression="working_days"></asp:BoundField>
                                        <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date"></asp:BoundField>
                                        <asp:BoundField DataField="time" HeaderText="Time" SortExpression="time"></asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status"></asp:BoundField>
                                        <asp:BoundField DataField="updated_time" HeaderText="Updated Time" SortExpression="updated_time"></asp:BoundField>
                                        <asp:BoundField DataField="overtime_amount" HeaderText="OverTime Amount" SortExpression="overtime_amount"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
