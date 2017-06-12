<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="User_Center.aspx.cs" Inherits="User_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/Product/css/style.css" />
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 75%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .glyphicon-star {
            color: #eb7019;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <div style="padding-left: 50px; padding-right: 50px; padding-bottom: 150px">
        <div>
            <div style="float: left;">
                <button type="button" class="btn btn-primary btn-arrow-right">首页</button>
                <button type="button" class="btn btn-info btn-arrow-right">项目</button>
            </div>
            <div style="float: right">
                <ol class="breadcrumb" id="Project_son">
                    <li><a href="#">成员</a></li>
                    <li class="active">任务</li>
                    <li><a href="#">日志</a></li>
                    <li><a href="#">文件</a></li>
                    <li><a href="#">群聊</a></li>
                    <li><a href="#">更多</a></li>
                </ol>
            </div>
        </div>
        <div style="clear: both;"></div>
        <%--我创建的项目--%>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row" style="padding-left: 50px; padding-right: 50px;">
                    <div class="Underline0">我创建的项目</div>
                    <div class="row Underline2 ">
                        <%--一行项目开始--%>
                        <%--<div class="target"></div>拖动释放区--%>

                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="col-xs-12 Underline3" draggable="true">
                                    <div class="col-xs-1">
                                        <a>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="/Product/images/u65.jpg" Width="60px" Height="60px" /></a>
                                    </div>
                                    <div class="col-xs-6 Underlin4"><a href="/Product/Taskboard_Center.aspx?ID=<%# Eval("pid") %>"><%# Eval("ProjectName") %>（<%# Eval("FinishedItemNums") %>/<%# Eval("TotalItemNums") %>）</a></div>
                                    <div class="col-xs-5 Underline5">
                                        <div class="Underline6">
                                            <div class="Underline7">
                                                <div class="col-xs-9"><%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %></div>
                                                <div class="col-xs-3">
                                                    <image id="Image2" src="<%# Eval("Avatar") %>" width="48px" height="48px" class="OwnerImage" />
                                                </div>
                                            </div>
                                            <div class="Underline8">
                                                <asp:LinkButton ID="Image3" runat="server" Class="col-xs-3 icon-user-plus" CommandArgument='<%#Eval("pid")%>' OnCommand="PassByValue" Style="font-size: 28px; line-height: 60px; margin-top: 10px;"></asp:LinkButton>

                                                <asp:LinkButton ID="Image4" runat="server" Class="col-xs-3 icon-lock" CommandArgument='<%#Eval("pid")%>' OnCommand="Archied" Style="font-size: 28px; line-height: 60px; margin-top: 10px;" />

                                                <asp:LinkButton ID="Image5" runat="server" Class="col-xs-3 icon-grid" data-toggle="modal" data-target="#myModal1" CommandArgument='<%#Eval("pid")%>' OnCommand="PassByValue" Style="font-size: 28px; line-height: 60px; margin-top: 10px;" />

                                                <div class="col-xs-3" style="margin-top: 15px;">
                                                    <asp:LinkButton ID="Label1" runat="server" Style="font-size: 28px; line-height: 60px;" CommandArgument='<%#Eval("mid")%>' OnCommand="Start"><i  class="<%# "glyphicon glyphicon-star"+(Eval("IsStarProject").ToString()=="True"?"":"-empty")%>"></i></asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--一行项目结束--%>
                        <div class="col-xs-12 Underline3" draggable="true" style="height: 58px; margin: 0; padding: 0;">
                            <div class="col-xs-12 Underline9" data-toggle="modal" data-target="#myModal" style="line-height: 58px; margin: 0; padding: 0; float: left;"><span class="icon-plus2" style="height: 58px; font-size: 32px; line-height: 58px; width: auto;"></span><span style="height: 58px; font-size: 24px; line-height: 58px; font-weight: bolder; position: relative; top: -5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;添加新项目</span></div>
                        </div>
                    </div>
                </div>
                <%--我创建的项目完成--%>

                <%--我参与的项目--%>
                <div class="row" style="padding-left: 50px; padding-right: 50px;">
                    <div class="Underline0">我参与的项目</div>

                    <div class="row Underline2 ">
                        <div class="col-xs-12 Underline3">
                            <div class="col-xs-1">
                                <a>
                                    <asp:Image ID="Image15" runat="server" ImageUrl="/Product/images/u65.jpg" Width="60px" Height="60px" /></a>
                            </div>
                            <div class="col-xs-6 Underlin4"><a>项目名称1（12/24）</a></div>
                            <a class="col-xs-5  Underlin4">
                                <div class="col-xs-9">2017/3/19</div>
                                <div class="col-xs-3 ">
                                    <asp:Image ID="Image21" runat="server" ImageUrl="/Product/images/u84.png" Width="48px" Height="48px" CssClass="OwnerImage" />
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <%--我参与的项目完成--%>

                <%--归档项目--%>
                <div class="row" style="padding-left: 50px; padding-right: 50px;">
                    <div class="Underline0">归档项目</div>

                    <div class="row Underline2 guidang">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div class="col-xs-12 Underline3">
                                    <div class="col-xs-1">
                                        <a>
                                            <asp:Image ID="Image16" runat="server" ImageUrl="/Product/images/u65.jpg" Width="60px" Height="60px" /></a>
                                    </div>
                                    <div class="col-xs-6 Underlin4"><a><%# Eval("ProjectName") %>（<%# Eval("FinishedItemNums") %>/<%# Eval("TotalItemNums") %>）</a></div>
                                    <div class="col-xs-5  Underlin4">
                                        <div class="col-xs-9"><%# String.Format("{0:yyyy-MM-dd}",Eval("CDT") ) %></div>
                                        <asp:LinkButton ID="ImageButton6" runat="server" Class="col-xs-3 " Style="font-size: 28px; line-height: 60px;" Width="48px" Height="60px" CommandArgument='<%#Eval("ID")%>' OnCommand="OpenProject"><i class="icon-no_encryption" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>



                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--归档项目完成--%>
    </div>

    <!-- 模态框（Modal） -->
    <%--新建项目的--%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin: 15%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">新建项目</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" Text=" 请输入项目名称" onFocus="if(value==defaultValue){value='';this.style.color='#000'}" onBlur="if(!value){value=defaultValue;this.style.color='#999'}" Style="color: #999999"></asp:TextBox>
                    <p></p>
                    <asp:TextBox ID="TextBox2" CssClass="form-control" TextMode="MultiLine" runat="server" Rows="3" Text=" 请输入项目简介" onFocus="if(value==defaultValue){value='';this.style.color='#000'}" onBlur="if(!value){value=defaultValue;this.style.color='#999'}" Style="color: #999999; resize: none;"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="选择模板创建项目" data-dismiss="modal" data-toggle="modal" data-target="#myModal2" />
                    <asp:Button ID="Button2" class="btn btn-success" runat="server" Text="确定并创建" OnClick="Button2_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%-- 项目属性的--%>

    <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 15%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel1">项目设置</h4>
                </div>

                <div class="modal-body">
                    <div style="padding: 5px 10px;">
                        <strong>封面图</strong><p></p>

                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Product/images/u105.jpg" Width="250px" Height="100px" Style="margin-right: 15px; float: left;" />
                        <div style="width: 50px; float: left;">
                            <p></p>
                            <button type="button" class="btn btn-info" style="padding: 5px 50px;">本地上传</button>
                            <p></p>
                            <button type="button" class="btn btn-default" style="padding: 5px 50px;">选择图片</button>
                        </div>

                    </div>
                    <div style="clear: both;"></div>
                    <div style="padding: 5px 10px;">
                        <strong>项目名称</strong><p></p>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:Label ID="NowID1" runat="server" Text="Label" Style="display: none;"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="padding: 5px 10px;">
                        <strong>项目简介</strong><p></p>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="TextBox5" TextMode="MultiLine" CssClass="form-control" runat="server" Rows="3" Style="resize: none;"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="padding: 5px 10px;">
                        <strong>项目拥有者</strong><p></p>
                        <asp:Image ID="Image8" runat="server" ImageUrl="/product/images/1.png" Style="margin: 0 10px;" /><span id="span1" runat="server" style="margin: 0 10px;">测试用户</span>
                        <asp:Button ID="Exchange" CssClass="btn btn-default" runat="server" Style="padding: 2px 15px;" Text="转让" data-dismiss="modal" data-toggle="modal" data-target="#myModal3" OnClick="Exchange_Click"></asp:Button>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button3" class="btn btn-danger" runat="server" Text="删除" OnClick="Button3_Click" />
                    <asp:Button ID="Button5" class="btn btn-info" runat="server" Text="归档" OnClick="Button5_Click" />
                    <asp:Button ID="Button4" class="btn btn-success" runat="server" Text="保存" OnClick="Button4_Click" />
                </div>
            </div>
        </div>

    </div>

    <%-- 项目属性的end--%>
    <%-- 选择转让人的--%>
    <div class="modal fade" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel3">转让项目</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePane9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Repeater ID="Repeater4" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <asp:Image ID="Image55" runat="server" ImageUrl="/product/images/1.png" Style="margin: 0 10px;" />
                                        <span><%# Eval("MemberName") %></span>
                                        <asp:Button ID="Button53" class="btn btn-danger" runat="server" Text="转让" Style="float: right; margin-right: 20px;" />
                                    </div>
                                    <hr />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button8" class="btn btn-success" runat="server" data-dismiss="modal" data-toggle="modal" data-target="#myModal1" Text="返回" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%-- 选择转让人的--%>

    <%--模板选择1--%>
    <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel2">模板选择</h4>
                </div>

                <div class="modal-body" id="Template" runat="server">
                    <ul id="myTab" class="nav nav-tabs">
                        <li class="active"><a href="#home" data-toggle="tab">公共模板 </a></li>
                        <li><a href="#ios" data-toggle="tab">VIP模板</a></li>
                        <li><a href="#jmeter" data-toggle="tab">私人模板</a></li>
                    </ul>
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade in active" id="home">
                            <div class="row">
                            <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                    <a style="width:120px;" data-id="<%#Eval("ID")%>" data-projectname="<%# Eval("ProjectName")%>" data-description="<%#Eval("Description")%>" data-name="<%#Eval("UserName")%>" data-cdt="<%#Eval("CDT")%>" data-tasklistnums="<%#Eval("TaskListNums")%>" class="pop1" data-dismiss="modal" data-toggle="modal" data-target="#Template1">
                                        <image src="<%# Eval("Picture") %>" width="100" height="100" style="margin: 10px;" />
                                        <span style="display: block; margin-top: -5px; margin-left: 20px;"><%# Eval("ProjectName").ToString().Length<=8?Eval("ProjectName"):(Eval("ProjectName").ToString().Substring(0,7)+"...") %></span>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="返回" data-dismiss="modal" data-toggle="modal" data-target="#myModal" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%--模板选择1--%>

    <%--模板选择2--%>
    <div class="modal fade" id="Template1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel5">模板信息——修改</h4>
                </div>
                <div class="modal-body" id="Div1" runat="server">
                    <div class="tab-content">
                        <div class="tab-pane fade in active">
                            <input type="hidden" id="hidden1" runat="server" />
                            <input type="hidden" id="hidden2" runat="server" />
                            <input type="hidden" id="hidden3" runat="server" />
                            <div class="row">
                                <div class="col-xs-12">项目名称:&nbsp;&nbsp;<asp:TextBox ID="TextBox4" runat="server" style="width:60%;margin-bottom:10px;"></asp:TextBox></div>
                                <div class="col-xs-12">项目描述:&nbsp;&nbsp;<asp:TextBox ID="TextBox6" runat="server" style="width:60%;margin-bottom:10px;"></asp:TextBox></div>
                                <div class="col-xs-12" id="name" style="margin-bottom:10px;"></div>
                                <div class="col-xs-12" id="cdt" style="margin-bottom:10px;"></div>
                                <div class="col-xs-12" id="tasklistnums" style="margin-bottom:10px;"></div>
                                <div class="col-xs-12" style="margin-bottom:10px;">项目列表名：</div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="Button12" runat="server" Text="Button" OnClick="Button12_Click" Style="display: none;" />
                                    <div class="row" style="margin-left:60px;">
                                        <asp:Repeater ID="Repeater5" runat="server">
                                            <ItemTemplate>
                                                <div class="col-xs-2" style="text-align:center;"><%#Eval("TaskListText") %></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button11" class="btn btn-success" runat="server" Text="确认" OnClick="Button11_Click" OnClientClick="return CreateTemplates(this);"  />
                    <asp:Button ID="Button9" class="btn btn-primary" runat="server" Text="返回" data-dismiss="modal" data-toggle="modal" data-target="#myModal2" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%--模板选择2--%>
    <div class="modal fade" id="myModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel4">上传图片</h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button7" class="btn btn-primary" runat="server" Text="返回" data-dismiss="modal" data-toggle="modal" data-target="#myModal" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(".pop1").mousemove(function () {
            $(this).css("cursor", "pointer");
        })

        $(".pop1").click(function () {
            document.getElementById("<%=hidden1.ClientID%>").value = $(this).data("id");
            document.getElementById("<%=TextBox4.ClientID%>").value = $(this).data("projectname");
            document.getElementById("<%=TextBox6.ClientID%>").value = $(this).data("description");
            $("#name").html("模板创建者：" + $(this).data("name"));
            $("#cdt").html("模板创建时间：" + $(this).data("cdt"));
            $("#tasklistnums").html("项目列表数：" + $(this).data("tasklistnums"));
            $("#ContentPlaceHolder1_Button12").click();
        })
        function CreateTemplates(obj) {
            document.getElementById("<%=hidden2.ClientID%>").value = document.getElementById("<%=TextBox4.ClientID%>").value
            document.getElementById("<%=hidden3.ClientID%>").value = document.getElementById("<%=TextBox6.ClientID%>").value
            
        }
    </script>
    
</asp:Content>

