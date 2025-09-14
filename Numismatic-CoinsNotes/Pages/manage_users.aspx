<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="manage_users.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.manage_users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Manage Users!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="properties section">
        <div class="row">
            <div class="filters" style="margin-left: 25px">
                <div class="mt-3" style="justify-items: center">
                    <asp:Button ID="btn_new_user" runat="server" Text="New User" class="orange-button" OnClick="btn_new_user_Click" />
                </div>
            </div>

        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                <div class="col-lg-3 col-md-6">
                    <!-- TODO: LEITURA IMAGEM SEM CRASHAR OS BOTÕES -->
                    <div class="item">
                        <span class="category"><%# Eval("Type") %></span>
                        <h4>
                            <a href="#"><%# Eval("Name") %></a>
                        </h4>
                        <ul>
                            <li><b>Email:</b> <%# Eval("Email") %></li>
                            <li><b>Verified:</b> <%# Eval("Verified") %></li>
                            <li><b>Active:</b> <%# Eval("Active") %></li>
                        </ul>
                        <div>
                            <asp:LinkButton ID="btnUpdate" runat="server"
                                Text="Update"
                                CommandName="UpdateItem"
                                CommandArgument='<%# Eval("id") %>'
                                CssClass="btn btn-warning mt-2" />
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Inactive"
                                CommandName="DeleteItem"
                                CommandArgument='<%# Eval("id") %>'
                                CssClass="btn btn-danger mt-2" 
                                OnClientClick="return confirm('Are you sure?');"
                                Visible='<%# Convert.ToBoolean(Eval("Active")) %>' />
                            <asp:LinkButton ID="btn_active" runat="server" Text="Active"
                                CommandName="ActiveItem"
                                CommandArgument='<%# Eval("id") %>'
                                CssClass="btn btn-success mt-2" 
                                OnClientClick='return confirm("Are you sure?");'
                                Visible='<%# !Convert.ToBoolean(Eval("Active")) %>' />
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
