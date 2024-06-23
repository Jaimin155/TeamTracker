<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="attendancePage.aspx.cs" Inherits="TeamTracker.EMS.attendancePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" id="punchInDiv" runat="server">
        <div class="row d-flex justify-content-center">
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Attendance Page (Punch In)</h4>
                                    <hr color="black">
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Upload Your Photo:</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Note:</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Write Note Here..." TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-6 d-flex">
                                <asp:Button CssClass="btn btn-mx btn-block btn-primary" ID="Button1" runat="server" Text="Punch In" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a><br><br>
            </div>
        </div>
    </div>
    <div class="container-fluid" id="punchOutDiv" runat="server">
        <div class="row d-flex justify-content-center">
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Attendance Page (Punch Out)</h4>
                                    <hr color="black">
                                </center>
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <div class="col-6 d-flex">
                                <asp:Button CssClass="btn btn-mx btn-block btn-primary" ID="Button2" runat="server" Text="Punch Out" OnClick="Button2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a><br><br>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function toggleDivs() {
            var punchInDiv = document.getElementById('<%= punchInDiv.ClientID %>');
            var punchOutDiv = document.getElementById('<%= punchOutDiv.ClientID %>');
            if (punchInDiv.style.display === 'none') {
                punchInDiv.style.display = 'block';
                punchOutDiv.style.display = 'none';
            } else {
                punchInDiv.style.display = 'none';
                punchOutDiv.style.display = 'block';
            }
        }
    </script>
</asp:Content>
