<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="manage_conditions.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.manage_conditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Manage Conditions</h3>
            </div>
          </div>
        </div>
      </div>
    <div class="mt-2" style="justify-items: center">
        <div class="main-button mt-2 mb-2">
            <a href="new_condition.aspx">New</a>
        </div>
    </div>
    <div class="properties section">
        <div class="row">
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6">
                        <div class="item">
                            <h4>Id - <%#Eval("id") %></h4>
                            <ul>
                                <li><b>Type:</b> <%#Eval("ConditionName") %></li>
                            </ul>
                            <div>
                                <asp:LinkButton ID="btnUpdate" runat="server"
                                    Text="Update"
                                    CommandName="UpdateItem"
                                    CommandArgument='<%# Eval("id") %>'
                                    CssClass="btn btn-warning mt-2" />
                            </div>
                        </div>
                    </div>
                    <br />
        
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
