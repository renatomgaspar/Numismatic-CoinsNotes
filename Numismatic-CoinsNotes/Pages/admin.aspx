<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/base_template.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Numismatic_CoinsNotes.Pages.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-heading header-text">
        <div class="container">
          <div class="row">
            <div class="col-lg-12">
              <h3>Admin Page</h3>
            </div>
          </div>
        </div>
      </div>

    <div class="section properties">
    <div class="container">
      <div class="row properties-box">
        <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
          <div class="item">
            <img src="../Assets/images/homeWpp.png" alt="" height="192" width="200">
            
            <h4><a href="manage_numismatics.aspx">Manage Numismatics</a></h4>
              <hr />
            <div class="main-button">
              <a href="manage_numismatics.aspx">Go to Page</a>
            </div>
          </div>
        </div>
        
          <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
            <div class="item">
              <img src="../Assets/images/homeWpp.png" alt="" height="192" width="200">
      
              <h4><a href="manage_cashtype.aspx">Manage Cash Types</a></h4>
                <hr />
              <div class="main-button">
                <a href="manage_cashtype.aspx">Go to Page</a>
              </div>
            </div>
          </div>

          <div class="row properties-box">
              <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
                <div class="item">
                  <img src="../Assets/images/homeWpp.png" alt="" height="192" width="200">
      
                  <h4><a href="manage_conditions.aspx">Manage Conditions</a></h4>
                    <hr />
                  <div class="main-button">
                    <a href="manage_conditions.aspx">Go to Page</a>
                  </div>
                </div>
              </div>
          </div>
        </div>

        <div class="row properties-box">
          <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
            <div class="item">
              <img src="../Assets/images/wallpaperusers.jpg" alt="" height="192" width="200">
      
              <h4><a href="manage_users.aspx">Manage Users</a></h4>
                <hr />
              <div class="main-button">
                <a href="manage_users.aspx">Go to Page</a>
              </div>
            </div>
          </div>

              <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
                <div class="item">
                  <img src="../Assets/images/wallpaperusers.jpg" alt="" height="192" width="200">
      
                  <h4><a href="manage_usertypes.aspx">Manage Users Types</a></h4>
                    <hr />
                  <div class="main-button">
                    <a href="manage_usertypes.aspx">Go to Page</a>
                  </div>
                </div>
              </div>
              <div class="col-lg-4 col-md-6 align-self-center mb-30 properties-items col-md-6 adv">
                <div class="item">
                  <img src="../Assets/images/wallpaperstatistics.jpg" alt="" height="192" width="200">
      
                  <h4><a href="statistics.aspx">Statistics</a></h4>
                    <hr />
                  <div class="main-button">
                    <a href="statistics.aspx">Go to Page</a>
                  </div>
                </div>
              </div>
            </div>
        </div>             
  </div>
</asp:Content>
