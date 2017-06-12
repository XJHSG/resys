<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="FindPassword.aspx.cs" Inherits="Product_FindPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="container">
           <asp:PlaceHolder ID="PlaceHolder1" runat="server">
             <div class="form-signin">
                 <h1 class="text-center" style="font-family: 'Impact Normal', 'Impact'; font-size: 42px;">Resys</h1>

                 <h3 class="text-center"><small>Resys重置密码</small></h3>
              <br />
                  <label for="Password" class="sr-only">密码</label>
             <asp:TextBox ID="Password1" type="password" onblur="checkpsd1()"  class="form-control" placeholder="密码" required="required" runat="server" ></asp:TextBox>
              <span class="text-center tips" id="divpassword1"></span>

         <label for="Password" class="sr-only">密码</label>
        <asp:TextBox ID="Password2" type="password" onblur="checkpsd2()"  class="form-control" placeholder="密码" required="required" runat="server" ></asp:TextBox>
       <span  id="divpassword2"></span>
               <br />

        <p class="text-center"><asp:Label ID="EmailError" runat="server" ForeColor="Red" Text=""  ></asp:Label></p>
          <asp:Button ID="ResetPsd" runat="server" Text="重置密码" class="btn btn-primary btn-block" type="submit" OnClick="ResetPsd_Click"/>
          <input id="Hidden2" type="hidden" runat="server" />
            </div>
               </asp:PlaceHolder>

            <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">
                 <div class="form-signin">
                      <h1 class="text-center" style="font-family: 'Impact Normal', 'Impact'; font-size: 42px;">Resys</h1>
                     <h3 class="text-center"><small><label id="name"  runat="server"></label></small></h3> 
                      <h3 class="text-center"><small>密码重置成功，请前往登陆</small></h3>
                     <br />
                     <a href="Login.aspx"  class="btn btn-primary btn-block">登陆</a>
                </div>

            </asp:PlaceHolder>
       </div>
   <script>
       function checkpsd1() {
           psd1 = $("ContentPlaceHolder1_Password1").val();
           var flagZM = false;

           var flagSZ = false;

           var flagQT = false;

           if (psd1.length < 6 || psd1.length > 12) {

               divpassword1.innerHTML = '<font class="tips_false">长度错误</font>';
               $("#ContentPlaceHolder1_ResetPsd").attr("disabled", true);

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
                   $("#ContentPlaceHolder1_ResetPsd").attr("disabled", true);
               } else {
                   divpassword1.innerHTML = '<font class="tips_true">输入正确</font>';
               }
           }
           //alert("hello");
       }

       function checkpsd2() {
           psd1 = $("ContentPlaceHolder1_Password1").val();
           psd2 = $("ContentPlaceHolder1_Password2").val();
           if (psd1 == psd2) {
               $("#ContentPlaceHolder1_Hidden2").val = 1;

           }
           else {
               divpassword2.innerHTML = '<font class="tips_false">两次密码输入不一致</font>';
               $("#ContentPlaceHolder1_ResetPsd").attr("disabled", true);
           }
       }
    </script>
</asp:Content>

