<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.change_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Change your Password</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <label>Current Password</label>
                        <asp:TextBox ID="tb_currentPassword" runat="server" placeholder="Password..." TextMode="Password"></asp:TextBox>
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <label>New Password</label>
                        <asp:TextBox ID="tb_newPassword" runat="server" placeholder="New Password..." TextMode="Password"></asp:TextBox>
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <label>Repeat New Password</label>
                        <asp:TextBox ID="tb_newPasswordRepeat" runat="server" placeholder="New Password..." TextMode="Password"></asp:TextBox>
                    </fieldset>
                 </div>
                 <div class="">
                     <fieldset>
                         <asp:Button ID="btn_changePassword" class="orange-button" runat="server" Text="Change Password" OnClick="btn_changePassword_Click" />
                         <asp:Button ID="btn_back" class="orange-button" runat="server" Text="Go Back" OnClick="btn_back_Click" />
                     </fieldset>
                 </div>
                <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>   
        </div>
    </div>
</asp:Content>
