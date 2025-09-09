<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="404error.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages._404error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Page Could Not be Found!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                 <div class="">
                     <fieldset>
                         <asp:Button ID="btn_backHome" class="orange-button" runat="server" Text="Go Home" OnClick="btn_backHome_Click" />
                     </fieldset>
                 </div>
            </div>   
        </div>
    </div>
</asp:Content>
