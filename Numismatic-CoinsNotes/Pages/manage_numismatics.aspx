<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="manage_numismatics.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.manage_numismatics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Manage Numismatics!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="properties section">
        <div class="row">
            
            <div class="filters" style="margin-left: 25px">
                <div class="mt-3" style="justify-items: center">
                    <asp:Button ID="btn_new_numismatic" runat="server" Text="New Numismatic" class="orange-button" OnClick="btn_new_numismatic_Click" />
                </div>
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
                <asp:Button ID="btn_clearfilters" runat="server" Text="Clear Filters" class="orange-button mb-3" OnClick="btn_clearfilters_Click" />

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
                </div>
                <br />
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </asp:Repeater>
    </div>
    </div>
</asp:Content>
