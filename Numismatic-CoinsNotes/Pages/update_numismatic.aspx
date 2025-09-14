<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="update_numismatic.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.update_numismatic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Update Numismatic!</h3>
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
                        <label for="title">Title</label>
                        <asp:TextBox ID="tb_title" runat="server" placeholder="Title..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="description">Description</label>
                        <asp:TextBox ID="tb_description" runat="server" placeholder="Description..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="type">Type  </label>
                        <asp:DropDownList ID="ddl_type" runat="server" DataSourceID="SqlDataSource2" DataTextField="type" DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT * FROM [Cashtype]"></asp:SqlDataSource>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="condition">Condition  </label>
                        <asp:DropDownList ID="ddl_condition" runat="server" DataSourceID="SqlDataSource1" DataTextField="condition" DataValueField="id"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT * FROM [Conditions]"></asp:SqlDataSource>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="imprintValue">Imprint Value</label>
                        <asp:TextBox ID="tb_imprintValue" runat="server" TextMode="Number" Attributes-Add="step:any" placeholder="Imprint Value..."></asp:TextBox>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label for="currentValue">Current Value</label>
                        <asp:TextBox ID="tb_currentValue" runat="server" TextMode="Number" Attributes-Add="step:any" placeholder="Current Value..."></asp:TextBox>
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
                        <label for="active">Active  </label>
                        <asp:DropDownList ID="ddl_active" runat="server">
                            <asp:ListItem Text="Active" Value="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="False"></asp:ListItem>
                        </asp:DropDownList>
                    </fieldset>
                </div>
                 <div class="">
                     <fieldset>
                         <asp:Button ID="btn_updateNumismatic" class="orange-button" runat="server" Text="Update Numismatic" OnClick="btn_updateNumismatic_Click" />
                         <asp:Button ID="btn_gotoBack" class="orange-button" runat="server" Text="Go Back" OnClick="btn_gotoBack_Click" />
                     </fieldset>
                 </div>
                 <span class="category"><asp:Label ID="lbl_infos" runat="server"></asp:Label></span>
            </div>            
        </div>
    </div>
</asp:Content>
