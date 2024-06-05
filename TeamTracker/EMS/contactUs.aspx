<%@ Page Title="" Language="C#" MasterPageFile="~/EMS/Site1.Master" AutoEventWireup="true" CodeBehind="contactUs.aspx.cs" Inherits="TeamTracker.EMS.contactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12 ">
                <center>
                    <h2>Contact Us</h2>
                    <hr />
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col-6 text-center align-content-center">
                <img width="344px" src="../Assets/Images/contact3.jpg" class="img-fluid" />
            </div>
            <div class="col-md-6">
                <form style="width: 26rem;">
                    <div data-mdb-input-init class="form-outline mb-4">
                        <input type="text" id="form4Example1" class="form-control" placeholder="Name" />
                    </div>
                    <div data-mdb-input-init class="form-outline mb-4">
                        <input type="email" id="form4Example2" class="form-control" placeholder="Email address" />
                    </div>
                    <div data-mdb-input-init class="form-outline mb-4">
                        <textarea class="form-control" id="form4Example3" rows="4" placeholder="Message"></textarea>
                    </div>
                    <button data-mdb-ripple-init type="button" class="btn btn-primary btn-block mb-4">Send</button>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
