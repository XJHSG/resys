<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="ActivePage.aspx.cs" Inherits="Product_ActivePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
     <div class="form-signin">
         <h1 style="text-align:center"> <small >按确认键激活您的账号</small></h1>
         <br />
         <asp:TextBox ID="ActiveCode" runat="server" class="form-control" ReadOnly="True"></asp:TextBox>
         <br />
         <div style="text-align:center">
          <asp:Label ID="ErrorLable" runat="server" Text="" ForeColor="Red" Font-Bold="true" ></asp:Label>
             </div>
           <asp:Button ID="SureActive" runat="server" Text="激活" class="btn btn-primary btn-block" type="submit" OnClick="SureActiving_Click" />
     </div>
    </div>
</asp:Content>

