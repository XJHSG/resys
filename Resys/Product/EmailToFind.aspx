<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="EmailToFind.aspx.cs" Inherits="Product_EmailToFind" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container">
             <div class="form-signin">
                 <h1 class="text-center" style="font-family: 'Impact Normal', 'Impact'; font-size: 42px;">Resys</h1>

                 <h3 class="text-center"><small>忘记密码</small></h3>
              <br />
                  <label for="Email" class="sr-only">邮箱</label>
        <asp:TextBox ID="Email" type="email" class="form-control" placeholder="请输入您的注册邮箱" required="required" autofocus="autofocus" runat="server" ></asp:TextBox>
               <br />
        <p class="text-center"><asp:Label ID="EmailError" runat="server" ForeColor="Red" Text=""  ></asp:Label></p>
          <asp:Button ID="Loging" runat="server" Text="重置密码" class="btn btn-primary btn-block" type="submit"  onclick="emailToFind_Click"/>
            </div>
       </div>
</asp:Content>

