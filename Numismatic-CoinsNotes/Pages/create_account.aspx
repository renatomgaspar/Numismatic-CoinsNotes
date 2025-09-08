<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="create_account.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.create_account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Create Your Account!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <label for="name">Name</label>
                        <asp:TextBox ID="tb_name" runat="server" placeholder="Your Name..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="email">Email Address</label><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMes Text="*" sage="RegularExpressionValidator" ErrorMessage="Enter a valid email address!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tb_email"></asp:RegularExpressionValidator>
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
                         <asp:Button ID="btn_createAccount" class="orange-button" runat="server" Text="Create Account" OnClick="btn_createAccount_Click" />
                     </fieldset>
                 </div>
                 <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>            
        </div>
    </div>
</asp:Content>
