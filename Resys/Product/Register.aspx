<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="css/drag.css" rel="stylesheet" />
    <style type="text/css">
        .tips_false {
            color: red;
        }

        .tips {
            color: rgba(0, 0, 0, 0.5);
            padding-left: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">

        <asp:PlaceHolder ID="RegisterPlaceHolder" runat="server">
            <div class="form-signin">
                <label for="Email" class="sr-only">邮箱</label>
                <asp:TextBox ID="Email" type="email" class="form-control" placeholder="您的邮箱" required="required" autofocus="autofocus" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Registing" runat="server" Text="即刻开始" class="btn btn-primary btn-block" type="submit" OnClick="Registing_Click" />
                <br />
                <p class="text-center">
                    <span class="text-muted">已有账号？ <a href="/Product/Login.aspx">登陆</a></span>
                </p>
            </div>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="SurePlaceHolder" Visible="false" runat="server">
            <div class="form-signin">
                <label for="Email2" class="sr-only">邮箱</label>
                <asp:TextBox ID="Email2" type="text" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                <label for="UserName" class="sr-only">用户名</label>
                <asp:TextBox ID="UserName" type="text" class="form-control" placeholder="您的用户名" required="required" autofocus="autofocus" runat="server"></asp:TextBox>
                <label for="Password" class="sr-only">密码</label>
                <p>
                    <asp:TextBox ID="Password" type="password" onblur="checkpsd1()" class="form-control" placeholder="（密码）字母，数字，至少六位" required="required" runat="server"></asp:TextBox>
                    <span class="text-center tips" id="divpassword1"></span>
                </p>
                <div id="drag"></div>
                <asp:Label ID="ErrorLable" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                <input id="Hidden1" type="hidden" runat="server" />
                <br />
                <asp:Button ID="SureRegisting" runat="server" Text="注册" class="btn btn-primary btn-block" type="submit" OnClick="SureRegisting_Click" />
            </div>
        </asp:PlaceHolder>


    </div>

    <script>window.jQuery || document.write('<script src="js/jquery-2.1.4.min.js"><\/script>')</script>
   <script src="js/drag.js"></script>
   <script type="text/javascript">
       $('#drag').drag();
       function checkpsd1() {
           psd1 = $("#ContentPlaceHolder1_Password").val();
           var flagZM = false;

           var flagSZ = false;

           var flagQT = false;

           if (psd1.length < 6 || psd1.length > 30) {

               divpassword1.innerHTML = '<font class="tips_false">长度错误</font>';
               $("#ContentPlaceHolder1_SureRegisting").attr("disabled", true);

           } else {
               for (i = 0; i < psd1.length; i++) {
                   if ((psd1.charAt(i) >= 'A' && psd1.charAt(i) <= 'Z') || (psd1.charAt(i) >= 'a' && psd1.charAt(i) <= 'z')) {
                       flagZM = true;
                   }
                   else if (psd1.charAt(i) >= '0' && psd1.charAt(i) <= '9') {
                       flagSZ = true;
                   }
                   else {
                       flagQT = true;
                   }
               }
               if (!flagZM || !flagSZ || flagQT) {

                   divpassword1.innerHTML = '<font class="tips_false">密码必须是字母数字的组合</font>';
                   $("#ContentPlaceHolder1_SureRegisting").attr("disabled", true);
               } else {
                   divpassword1.innerHTML = '<font class="tips_true">输入正确</font>';
              
               }
           }
           //alert("hello");
       }
	</script>
</asp:Content>


