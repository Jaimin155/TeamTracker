<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="userManagement.aspx.cs" Inherits="TeamTracker.EMS.userManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //$('.table').DataTable();
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
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
                                    <h4>Employees Detail</h4>
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
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Employee Name"></asp:TextBox>
                                </div>
                            

                            </div>      
                            </div>
                        <div class="row">
                            <div class="col-4">
                                <asp:Button ID="Button2" runat="server" Text="Add" CssClass="btn btn-mx btn-block btn-success" OnClick="Button2_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="Button3" runat="server" Text="Update" CssClass="btn btn-mx btn-block btn-warning" OnClick="Button3_Click" />
                            </div>
                            <div class="col-4">
                                <asp:Button ID="Button4" runat="server" Text="Delete" CssClass="btn btn-mx btn-block btn-danger" OnClick="Button4_Click" />
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
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:emsDBConnectionString %>" ProviderName="<%$ ConnectionStrings:emsDBConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [user_login_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered dt-responsive" ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="user_id">
                                    <Columns>
                                        <asp:BoundField DataField="user_id" HeaderText="Employee ID" ReadOnly="True" SortExpression="user_id"></asp:BoundField>
                                        <asp:BoundField DataField="user_name" HeaderText="Employee Name" SortExpression="user_name"></asp:BoundField>
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
