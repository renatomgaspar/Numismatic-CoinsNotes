<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="password_recovery.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.password_recovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Password Recovery</h3>
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
                         <asp:Button ID="btn_password_recovery" class="orange-button" runat="server" Text="Password Recovery" OnClick="btn_password_recovery_Click" />
                        <asp:Button ID="btn_back" class="orange-button" runat="server" Text="Go Back" OnClick="btn_back_Click" />
                     </fieldset>
                 </div>
                <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>   
        </div>
    </div>
</asp:Content>
