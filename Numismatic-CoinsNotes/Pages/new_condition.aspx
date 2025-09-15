<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="new_condition.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.new_condition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="page-heading header-text">
            <div class="container">
              <div class="row">
                <div class="col-lg-12">
                  <h3>New Condition!</h3>
                </div>
              </div>
            </div>
        </div>

        <div class="contact-page section">
            <div class="container">
                <div id="contact-form">
                    <div class="">
                        <fieldset>
                            <label for="email">Condition:</label>
                            <asp:TextBox ID="tb_condition" runat="server" placeholder="Condition..."></asp:TextBox>
                        </fieldset>
                    </div>
                     <div class="">
                         <fieldset>
                            <asp:Button ID="btn_net_condition" class="orange-button" runat="server" Text="Create Condition" OnClick="btn_create_condition_Click" />
                             <asp:Button ID="btn_goBack" class="orange-button" runat="server" Text="Go Back" OnClick="btn_gotoBack_Click" />
                         </fieldset>
                     </div>
                    <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
                </div>   
            </div>
        </div>
</asp:Content>
