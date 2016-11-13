<%@ Page Title="" Language="C#" MasterPageFile="~/MasterFrontPage.master" AutoEventWireup="true" CodeFile="activePage.aspx.cs" Inherits="actigePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <div style="margin-top:30px;"></div>
    <div class="row">
    <div class="col-lg-5 col-md-5 col-sm-12"></div>
    <div class="col-lg-7 col-md-7 col-sm-12">
         <div><p style="color:red;font-size:16px;font-weight:bold;margin-left:3%;">请输入激活账号的激活码</p></div>
              <br />
             <span> 激活码：<asp:TextBox ID="ActiveCode" runat="server" Width="155px" ReadOnly="True"></asp:TextBox></span>
            <p style="margin-left:13%;">
            <asp:Button ID="btnNext" runat="server"  Text="确定" OnClick="btnNext_Click"  />
            </p>
          <div style="margin-left:65px;"><asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label></div>
        <div style="margin-left:10px;"><asp:Label ID="Label3" runat="server" ForeColor="Red" Text=""></asp:Label></div>
        </div>
           
    </div>

     <div style="margin-top:30px;"></div>

</asp:Content>

