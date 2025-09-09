<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Login to see Your Collection!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <label for="email">Email Address</label><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter a valid email address!" Text="*" ControlToValidate="tb_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="tb_email" runat="server" placeholder="Email..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label>Password</label>
                        <asp:TextBox ID="tb_password" runat="server" placeholder="Password..." TextMode="Password"></asp:TextBox>
                    </fieldset>
                 </div>
                 <div class="">
                     <fieldset>
                         <asp:Button ID="btn_login" class="orange-button" runat="server" Text="Login" OnClick="btn_login_Click" />
                         <asp:Button ID="btn_gotoCreate" class="orange-button" runat="server" Text="Create Account Page" OnClick="btn_gotoCreate_Click" />
                     </fieldset>
                 </div>
                <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>   
        </div>
    </div>
    
</asp:Content>
