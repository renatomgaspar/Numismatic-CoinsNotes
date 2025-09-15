<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Welcome! Make your own Collection!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="properties section">
        <div class="container">
            <div class="row">
                <div class="col-lg-4 offset-lg-4">
                    <div class="section-heading text-center">
                        <h6>| Numismatics</h6>
                        <h2>Some of Our Numismatics</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6">
                        <div class="item">
                            <div style="text-align: center">
                                <asp:Image ID="Image1" runat="server" CssClass="img-fluid" ImageUrl='<%# "../Helpers/ImageHandler.ashx?id=" + Eval("Id")  + "&page=1"%>' Width="230" Height="200"/>
                            </div>
                            <span class="category"><%# Eval("Type") %></span>
                            <span class="category"><%# Eval("Condition") %></span>
                            <h6>€<%# Eval("Currentvalue", "{0:N2}") %></h6>
                            <h4>
                                <a href="#"><%# Eval("Title") %></a>
                            </h4>
                            <ul>
                                <li><b>Description:</b> <%# Eval("Description") %></li>
                                <li><b>Value:</b> <%# Eval("Imprintvalue", "{0:N2}") %></li>
                                <li><b>Current Value:</b> <%# Eval("Currentvalue", "{0:N2}") %></li>
                            </ul>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>
