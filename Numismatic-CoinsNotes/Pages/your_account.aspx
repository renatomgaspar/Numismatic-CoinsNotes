<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="your_account.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.your_account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Your Information</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <asp:Button ID="btn_adminPage" class="orange-button" runat="server" Text="Admin Page" Visible="False" />
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <asp:Image ID="userImage" runat="server" Height="200" Width="200" />
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <b><label>Name: </label></b>
                        <asp:Label ID="lbl_name" runat="server"></asp:Label>
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <b><label>Email: </label></b>
                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <asp:Button ID="btn_changePassword" class="orange-button" runat="server" Text="Change Password" OnClick="btn_changePassword_Click" />
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <asp:Button ID="btn_logout" class="red-button" runat="server" Text="Logout" OnClick="btn_logout_Click" />
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
