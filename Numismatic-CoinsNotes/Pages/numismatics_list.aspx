<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="numismatics_list.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.numismatics_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>All of Numismatics!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="properties section">
        <div class="row">
            <div class="filters" style="margin-left: 25px">
                Price:  
                <asp:DropDownList ID="ddl_price" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_price_SelectedIndexChanged">
                    <asp:ListItem Value="">None</asp:ListItem>
                    <asp:ListItem Value="ASC">Ascending</asp:ListItem>
                    <asp:ListItem Value="DESC">Descending</asp:ListItem>
                </asp:DropDownList>

                Cash Type:  
                <asp:DropDownList 
                    ID="ddl_cashtype" 
                    runat="server" 
                    DataSourceID="SqlDataSource1" 
                    DataTextField="type" 
                    DataValueField="type"
                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddl_cashtype_SelectedIndexChanged">
                    <asp:ListItem Text="All" Value="" />
                </asp:DropDownList>
                <asp:SqlDataSource 
                    ID="SqlDataSource1" 
                    runat="server" 
                    ConnectionString="<%$ ConnectionStrings:connectionString %>" 
                    SelectCommand="SELECT [id], [type] FROM [Cashtype]">
                </asp:SqlDataSource>
                <asp:Button ID="btn_clearfilters" runat="server" Text="Clear Filters" class="orange-button" OnClick="btn_clearfilters_Click" />

            </div>
        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                <div class="col-lg-3 col-md-6">
                    <div class="item">
                        <a href="#">
                            <asp:Image ID="Image1" runat="server" CssClass="img-fluid" />
                        </a>
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
                        <div>
                            <asp:LinkButton ID="btn_saveToFavourite" runat="server" Text="Add to Favourite"
                                CommandName="SaveToFavourite"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn btn-success mt-2" />
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
