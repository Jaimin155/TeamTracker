<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="employeeSalary.aspx.cs" Inherits="TeamTracker.EMS.employeeSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Employees Salary Detail</h4>
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
                                        <asp:Button ID="Button1" runat="server" Text="Go" CssClass="btn btn-outline-primary" OnClick="true" />
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
                                <label>Working Days</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Working Days" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Salary</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Salary" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-5 ">
                                <asp:Button ID="Button2" runat="server" Text="Paid" CssClass="btn btn-mx btn-block btn-outline-warning" />
                            </div>
                            <div class="col-5">
                                <asp:Button ID="Button3" runat="server" Text="Add Bonus" CssClass="btn btn-mx btn-block btn-outline-primary" />
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
                                    <h4>All Employees List</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr color="black">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered dt-responsive" ID="GridView1" runat="server"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
