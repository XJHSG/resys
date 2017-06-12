<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
  <%--    <asp:TextBox ID="userip" runat="server" Visible="false"></asp:TextBox>--%>
     <p style="display:none"><asp:Label ID="Label1" runat="server" ></asp:Label></p>
      <div class="form-signin">
        <label for="Email" class="sr-only">邮箱</label>
        <asp:TextBox ID="Email" type="email" class="form-control" placeholder="邮箱" required="required" autofocus="autofocus" runat="server" ></asp:TextBox>

        <label for="Password" class="sr-only">密码</label>
        <asp:TextBox ID="Password" type="password"  onkeypress="searchPress();"  class="form-control aa" placeholder="密码" required="required" runat="server" ></asp:TextBox>

        <asp:Button id="Loging" runat="server" Text="登 录" class="btn btn-primary btn-block" type="submit" onclick="Loging_Click"/>
        <p class="text-center">
            <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
        </p>
        <br />
        <p class="text-center">
            <span class="text-muted"> <a href="/Product/Register.aspx">注册账号&nbsp;&nbsp;</a>|&nbsp;&nbsp;<a href="/Product/EmailToFind.aspx">找回密码</a></span>
        </p>
      </div>

    </div>

<script>

    function searchPress(){
      
        var theEvent = window.event || arguments.callee.caller.arguments[0]; //谷歌能识别event，火狐识别不了，所以增加了这一句，chrome浏览器可以直接支持event.keyCode
        var code = theEvent.keyCode;
        if (code == 13) {
            $('#ContentPlaceHolder1_Loging').click();
            var isChrome = navigator.userAgent.toLowerCase().match(/chrome/) != null;//判断是否是谷歌浏览器
            if(isChrome){
                event.keyCode=9;
                event.returnValue = false;//屏蔽其默认的返回值
                var txtpwd=<%=Loging.ClientID%>;
                document.getElementById(txtpwd).click();
            }
        }
    }
   


    $(".btn-primary").click(function () {
        var userid = <%=getUserID()%>; 
        var module = "用户模块";
        var action = "登陆成功";
        var page = "Login.aspx";
        //var userip = document.getElementById("ContentPlaceHolder1_userip").value;
        var userip = document.getElementById('<%=Label1.ClientID %>').innerText;
      
     
        $.ajax({
        
            type: "post",
            url: "Users_WebService.asmx/SaveLogs", //服务端处理程序   
            data:{ UserID: userid, Module: module, Action: action, Page: page, UserIP: userip },
            success: function (msg) {
                console.log('插入历史记录成功');
            }
        });

        //alert(userip);
       
    })
</script>
</asp:Content>

