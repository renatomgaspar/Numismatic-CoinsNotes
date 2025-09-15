<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="update_usertype.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.update_usertype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Update User Type!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <label for="email">Type:</label>
                        <asp:TextBox ID="tb_type" runat="server" placeholder="Type..."></asp:TextBox>
                    </fieldset>
                </div>
                 <div class="">
                     <fieldset>
                        <asp:Button ID="btn_update_cashtype" class="orange-button" runat="server" Text="Update" OnClick="btn_update_cashtype_Click" />
                         <asp:Button ID="btn_goBack" class="orange-button" runat="server" Text="Go Back" OnClick="btn_gotoCreate_Click" />
                     </fieldset>
                 </div>
                <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>   
        </div>
    </div>
</asp:Content>
