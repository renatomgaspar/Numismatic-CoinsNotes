<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="manage_cashtype.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.manage_cash_type" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Manage Cash Type</h3>
            </div>
          </div>
        </div>
      </div>
    <div class="mt-2" style="justify-items: center">
        <div class="main-button mt-2 mb-2">
            <a href="new_cashtype.aspx">New</a>
        </div>
    </div>
    
    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
        <HeaderTemplate></HeaderTemplate>
        <ItemTemplate>
            <div class="container" style="justify-items: center">
                <div class="col-lg-5">
                    <div class="accordion" id="accordionExample">
                        <div class="accordion-item">
                            <div class="section-heading">
                                <h2>Id - <%#Eval("id") %></h2>
                            </div>
                            <h2 class="accordion-header" id="headingOne">
                                <b>Type:</b> <%#Eval("type") %>
                            </h2>
                        </div>
                    </div>

                    <asp:LinkButton ID="btnUpdate" runat="server"
                        Text="Update"
                        CommandName="UpdateItem"
                        CommandArgument='<%# Eval("id") %>'
                        CssClass="btn btn-warning mt-2" />
                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                        CommandName="DeleteItem"
                        CommandArgument='<%# Eval("id") %>'
                        CssClass="btn btn-danger mt-2" 
                        OnClientClick="return confirm('Are you sure?');" />
                </div>
            </div>

            <br />
            
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </asp:Repeater>
</asp:Content>
