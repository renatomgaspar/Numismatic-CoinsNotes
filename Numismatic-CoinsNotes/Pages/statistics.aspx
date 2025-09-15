<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="statistics.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Statistics!</h3>
            </div>
          </div>
        </div>
    </div>

    <div class="contact-page section">
        <div class="container">
            <div id="contact-form">
                <div class="">
                    <fieldset>
                        <label><b>Total Users:</b></label>
                        <asp:Label ID="lbl_totalUsers" runat="server" Text="Label"></asp:Label>
                    </fieldset>
                </div>
                <div class="">
                    <fieldset>
                        <label><b>Total Users With Collections: </b></label>
                        <asp:Label ID="lbl_totalUserWithCollections" runat="server" Text="Label"></asp:Label>
                    </fieldset>
                 </div>
                 <div class="">
                     <fieldset>
                         <label><b>User with more Coins: </b></label>
                         <asp:Label ID="lbl_user" runat="server" Text="Label"></asp:Label>
                         <label><b>Quantity: </b></label>
                         <asp:Label ID="lbl_quantity" runat="server" Text="Label"></asp:Label>
                     </fieldset>
                  </div>
                <div class="">
                    <fieldset>
                        <label><b>Top 10 Most Valuable Coins: </b></label>
                        <div class="properties section mt-0">
                            <div class="row">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-lg-6 col-md-8">
                                            <div class="item">
                                                <div style="text-align: center">
                                                    <asp:Image ID="Image1" runat="server" CssClass="img-fluid" ImageUrl='<%# Bind("Image") %>' Width="230" Height="200"/>
                                                </div>
                                                <span class="category"><%# Eval("Type") %></span>
                                                <span class="category"><%# Eval("Condition") %></span>
                                                <h6>€<%# Eval("Currentvalue", "{0:N2}") %></h6>
                                                <h4>
                                                    <%# Eval("Title") %>
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
                    </fieldset>
                 </div>
                <div class="">
                    <fieldset>
                        <label><b>Coin Average Value per User: </b></label>
                        <div class="properties section mt-0">
                            <div class="row">
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-lg-4">
                                            <div class="item" style="display: flex; flex-direction: column; align-items: center; text-align: center; max-width: 250px; word-wrap: break-word;">
                                                <asp:Image ID="Image2" runat="server" CssClass="img-fluid" ImageUrl='<%# Bind("Image") %>' Width="230" Height="200" />

                                                <h6 style="margin: 10px 0 5px;">€<%# Eval("Average") %></h6>

                                                <h4 style="margin: 0; word-break: break-word;"><%# Eval("Email") %></h4>
                                            </div>
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                    <FooterTemplate></FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </fieldset>
                 </div>
            </div>   
        </div>
    </div>
</asp:Content>
