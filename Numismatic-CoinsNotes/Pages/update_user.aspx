<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="update_user.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.update_user" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Update User!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="" style="text-align: center">
                    <fieldset>
                        <asp:Image ID="Image1" runat="server" CssClass="img-fluid" Width="230" Height="200"/>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="email">Email Address</label><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMes Text="*" sage="RegularExpressionValidator" ErrorMessage="Enter a valid email address!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tb_email"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="tb_email" runat="server" placeholder="Email..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="verified">Verified  </label>
                        <asp:DropDownList ID="ddl_verified" runat="server">
                            <asp:ListItem Value="0">False</asp:ListItem>
                            <asp:ListItem Value="1">True</asp:ListItem>
                        </asp:DropDownList>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="active">Active  </label>
                        <asp:DropDownList ID="ddl_active" runat="server">
                            <asp:ListItem Value="0">False</asp:ListItem>
                            <asp:ListItem Value="1">True</asp:ListItem>
                        </asp:DropDownList>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="type">Type</label>
                        <asp:DropDownList ID="ddl_type" runat="server" DataSourceID="SqlDataSource1" DataTextField="type" DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT * FROM [Usertypes]"></asp:SqlDataSource>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="image">Image  </label>
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" />
                    </fieldset>
                </div>
                 <div class="">
                     <fieldset>
                         <asp:Button ID="btn_updateUser" class="orange-button" runat="server" Text="Update User" OnClick="btn_updateUser_Click" />
                         <asp:Button ID="btn_gotoBack" class="orange-button" runat="server" Text="Go Back" OnClick="btn_gotoBack_Click" />
                     </fieldset>
                 </div>
                 <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>            
        </div>
    </div>
</asp:Content>
