<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="new_cashtype.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.new_cashtype" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
    <div class="container">
      <div class="row">
        <div class="col-lg-12">
          <h3>New Cash Type!</h3>
        </div>
      </div>
    </div>
</div>

<div class="contact-page section">
    <div class="container">
        <div id="contact-form">
            <div class="">
                <fieldset>
                    <label for="type">Type</label>
                    <asp:TextBox ID="tb_type" runat="server" placeholder="Cash Type..."></asp:TextBox>
                </fieldset>
            </div>
             <div class="">
                 <fieldset>
                     <asp:Button ID="btn_createType" class="orange-button" runat="server" Text="Create Type" OnClick="btn_createType_Click" />
                     <asp:Button ID="btn_gotoBack" class="orange-button" runat="server" Text="Go Back" OnClick="btn_gotoBack_Click" />
                 </fieldset>
             </div>
             <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
        </div>            
    </div>
</div>
</asp:Content>
