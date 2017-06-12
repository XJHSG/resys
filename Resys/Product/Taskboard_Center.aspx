<%@ Page Title="Taskboard_Center" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="Taskboard_Center.aspx.cs" Inherits="Taskboard_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/style.css" />
    <script type="text/javascript" src="../Admin/ckeditor201507/ckeditor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="js/jquery.qrcode.js"></script>
    <script src="js/ff-range.js"></script>
    <script src="js/scripts.js"></script>
    <script src="js/emailAutoComplete.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="ProjectID" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="TaskListNums" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="IDs" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="hrefText" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="hrefLink" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="UserName" runat="server" Text="0" Visible="true"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="ita_hidf" runat="server" Value="1" />
            <asp:Button ID="aBt" runat="server" Text="Button" OnClick="Button12_Click" Visible="True" Style="display: none;" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <nav class="project-navigation">
        <div style="float: left;">
            <a href="../Index.aspx" class="btn btn-primary btn-arrow-right">首页</a>
            <a href="User_Center.aspx" class="btn btn-info btn-arrow-right">项目</a>
            <button type="button" class="btn btn-info btn-arrow-right">
                <asp:Label ID="ProjectName" runat="server" Text=""></asp:Label></button>
        </div>
        <div style="float: right">
            <ol class="breadcrumb" id="Project_son">
                <li><a href="#">成员</a></li>
                <li class="active">任务</li>
                <li><a href="#">日志</a></li>
                <li><a href="File_Man.aspx">文件</a></li>
                <li><a href="#">群聊</a></li>
                <li><a href="#">更多</a></li>
            </ol>
        </div>
    </nav>
    <div class="boardview">
        <div class="taskboard">
            <ul id="module_list">
                <asp:Repeater ID="Task_Repeater" runat="server" OnItemDataBound="Task_Repeater_ItemDataBound">
                    <ItemTemplate>
                        <li id='ListID_<%#Eval("ID") %>' class="modules" data-id='<%#Eval("ID") %>' data-order='<%#Eval("Orders") %>'>
                            <asp:HiddenField ID="hfid" runat="server" Value=' <%#Eval("ID")%>' />
                            <header class="m_title">
                                <div class="m_title-name">
                                    <span id='TaskListText_<%#Eval("ID") %>'><%#Eval("TaskListText") %></span> （<%#Eval("FinishedItemNums") %>/ <%#Eval("TotalItemNums") %>）
                               
                               
                                </div>
                                <a class="listmore glyphicon glyphicon-option-horizontal" data-listid='<%#Eval("ID") %>' data-totalitemnums='<%#Eval("TotalItemNums") %>' data-toggle="modal" data-target="#TaskListMore"></a>
                            </header>
                            <div class="modules-wrap">
                                <section class="modules-content">
                                    <ul id='<%#Eval("ID") %>' class="connectedSortable">
                                        <asp:Repeater ID="TaskItem_Repeater" runat="server" OnItemDataBound="TaskItem_Repeater_ItemDataBound">
                                            <ItemTemplate>
                                                <li id="taskItem" runat="server" class="task list-disabled" data-id='<%#Eval("ID") %>' data-listid='<%#Eval("TaskListID") %>' data-itemtext='<%#Eval("ItemText") %>'>
                                                    <asp:HiddenField ID="hfitemid" runat="server" Value=' <%#Eval("ID")%>' />
                                                    <div id="priority" runat="server" class="task-priority"></div>
                                                    <a class="check-box"><span id="ok" runat="server" class="glyphicon glyphicon-ok"></span></a>
                                                    <div class="task-content-set" data-toggle="modal" data-target="#myModal">
                                                        <div class="task-content-wrapper">
                                                            <div class="task-content"><%#Eval("ItemText")%></div>
                                                            <div id="avatar" runat="server" class="task-avatar"></div>
                                                        </div>
                                                        <div class="task-info-wrapper clearfix">
                                                            <div class="task-infos">
                                                                <%--<span class="tag">
                                                                    <span class="tag-label"></span>
                                                                    <span class="tag-name">xx</span>
                                                                </span>--%>
                                                                <span id="subItemNums" runat="server" class="icon-wrapper with-text">
                                                                    <span class="fa fa-list-ul" aria-hidden="true" style="margin-right: 4px;"></span>
                                                                    <%#Eval("SubItemNums")%>
                                                                </span>
                                                                <span id="linkNums" runat="server" class="icon-wrapper with-text">
                                                                    <span class="fa fa-link" aria-hidden="true" style="margin-right: 4px;"></span>
                                                                    <%#Eval("LinkNums")%>
                                                                </span>
                                                                <span id="upNums" runat="server" class="icon-wrapper with-text">
                                                                    <span class="fa fa-thumbs-up" aria-hidden="true" style="margin-right: 4px;"></span>
                                                                    <%#Eval("UpNums")%>
                                                                </span>
                                                                <span id="commentNums" runat="server" class="icon-wrapper with-text">
                                                                    <span class="fa fa-comment" aria-hidden="true" style="margin-right: 4px;"></span>
                                                                    <%#Eval("CommentNums")%>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>

                                    <div class="task-creator-handler-wrap">
                                        <a class="task-creator-handler link-add-handler" data-listid='<%#Eval("ID") %>' data-totalnum='<%#Eval("TotalItemNums") %>' data-toggle="modal" data-target="#TaskCreator">
                                            <span class="glyphicon glyphicon-plus"></span>新建任务</a>
                                    </div>
                                </section>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>

                <li class="modules modules-add-view list-disabled">
                    <div class="handler-wrap">
                        <a class="expand-creator-handler" data-toggle="modal" data-target="#TaskListCreator">
                            <span class="glyphicon glyphicon-plus"></span>新建任务列表</a>
                    </div>
                </li>
            </ul>
        </div>
    </div>

    <!-- TaskItems Modal -->
    <div class="modal fade" id="TaskCreator" tabindex="-1" role="dialog" aria-labelledby="TaskModalTitle">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="TaskModalTitle">新建任务</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="ItemText" class="control-label">任务：</label>
                        <asp:TextBox ID="ItemText" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ExecutorDDL" class="control-label">执行者：</label>
                        <asp:DropDownList ID="ExecutorDDL" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="PriorityDDL" class="control-label">优先级：</label>
                        <asp:DropDownList ID="PriorityDDL" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Text="普通" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="紧急"></asp:ListItem>
                            <asp:ListItem Value="2" Text="非常紧急"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <%--<div class="form-group">
                        <label for="EndDT" class="control-label">截止时间：</label>
                        <input id="EndDT" type="datetime-local" class="form-control"/>
                    </div>--%>
                    <input type="hidden" id="TaskListID" runat="server" class="form-control" />
                    <input type="hidden" id="ItemOrders" runat="server" class="form-control" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关 闭</button>
                    <asp:Button ID="TaskCreatorBtn" runat="server" Text="确 定" CssClass="btn btn-primary" OnClick="TaskCreatorBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- TaskItems Modal -->

    <!-- TaskList Modal -->
    <div class="modal fade" id="TaskListCreator" tabindex="-1" role="dialog" aria-labelledby="TaskListModalTitle">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="TaskListModalTitle">新建任务列表</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="TaskListText" class="control-label">任务列表：</label>
                        <asp:TextBox ID="TaskListText" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关 闭</button>
                    <asp:Button ID="TaskListCreatorBtn" runat="server" Text="确 定" CssClass="btn btn-primary" OnClick="TaskListCreatorBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <!-- TaskList Modal -->

    <!-- TaskListMore Modal -->
    <div class="modal fade" id="TaskListMore" tabindex="-1" role="dialog" aria-labelledby="TaskListMoreTitle">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="margin-top: 25%;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="TaskListMoreTitle">编辑任务列表</h4>
                </div>
                <div id="modal-body1" class="modal-body">
                    <div class="form-group">
                        <label for="TaskListTextNew" class="control-label">任务列表：</label>
                        <input id="TaskListTextNew" type="text" class="form-control" />
                    </div>
                    <input type="hidden" id="TaskListEditID" runat="server" class="form-control" />
                    <input type="hidden" id="TotalItemNums" runat="server" class="form-control" />
                </div>
                <div id="modal-body2" class="modal-body" style="display: none;">
                    <div class="form-group">
                        <p id="WarningLabel" style="font-weight: bold; color: red;"></p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关 闭</button>
                    <a onclick="TaskListDel()" id="DelBtn" class="btn btn-danger">删 除</a>
                    <button id="TaskListDelBtn" type="button" class="btn btn-danger" data-dismiss="modal" style="display: none;">确定删除</button>
                    <button id="ConfirmBtn" type="button" class="btn btn-primary" data-dismiss="modal">确 定</button>
                </div>
            </div>
        </div>
    </div>

    <!-- TaskListMore Modal -->

    <!-- TaskItemsMore Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document" style="width: 65%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="myModalLabel">任务属性 -- [<strong>
                                <asp:Label ID="Label5" runat="server"></asp:Label>

                            </strong>]
                    </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-body">
                    <div class='row'>
                        <div class="col-lg-2">
                            <ul id="myTab" class="nav nav-pills nav-stacked">
                                <li class="active"><a href="#attribute3" data-toggle="tab">基本信息</a></li>
                                <li><a href="#attribute1" data-toggle="tab">任务成员</a></li>
                                <li><a href="#attribute2" data-toggle="tab">时间/优先级</a></li>
                                <li><a href="#attribute4" data-toggle="tab">链接</a></li>
                                <li><a href="#attribute5" data-toggle="tab">子任务</a></li>
                                <li><a href="#attribute6" data-toggle="tab">操作日志</a></li>
                                <li><a href="#attribute7" data-toggle="tab">评论</a></li>
                            </ul>
                        </div>
                        <div class="col-lg-10">
                            <div id="myTabContent" class="tab-content">
                                <%--选项卡1：任务成员，包括执行者和参与者--%>
                                <div class="tab-pane fade" id="attribute1">
                                    <div id="Pro_Item_Executor" style="padding-left: 1%; padding-right: 1%; float: left;">执行者：</div>
                                    <div style="width: 85%; float: left;">
                                        <div class="dropdown">

                                            <a id="dLabel" data-target="#" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="/Product/images/u65.jpg" Style="border-radius: 50%; width: 35px; height: 35px; vertical-align: middle;" />
                                                某钿NK     
           </a>

                                            <div id="fade">
                                                <input id="customerId" type="text" oninput="searchList(this.value)" placeholder="查找成员" />

                                                <ul class="dropdown-menu" aria-labelledby="dLabel">
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <ItemTemplate>

                                                            <li value="<%# Eval("Email") %>">

                                                                <img src='<%# Eval("Avatar") %>' alt="头像" style="width: 25px;" />

                                                                <span style="margin-left: 20px; line-height: 25px;"><%# Eval("UserName") %></span>

                                                            </li>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <a href="#" id="show2">
                                                        <span class="glyphicon glyphicon-plus" style="color: white; border-radius: 50%; background-color: #31b0d5; width: 25px; line-height: 25px; text-align: center; margin-top: 10px"></span>
                                                        <span style="margin-left: 20px; line-height: 25px;">邀请新成员</span>
                                                    </a>
                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="clear: both"></div>
                                    <div style="margin-top: 38%">
                                        <div id="Pro_Item_Participant" style="padding-left: 1%; padding-right: 1%; float: left;">参与者：</div>
                                        <div style="width: 85%; float: left;">

                                            <asp:Image ID="Image2" runat="server" ImageUrl="/Product/images/u65.jpg" Style="border-radius: 50%; width: 35px; height: 35px; vertical-align: middle;" />
                                            <asp:LinkButton ID="LinkButton11" runat="server">某钿NK</asp:LinkButton>
                                            <a href="#" class="btn btn-info" style="border-radius: 50%;" data-dismiss="modal" data-toggle="modal" data-target="#myModal2">+</a>

                                        </div>
                                    </div>
                                </div>
                                <%--选项卡2：起止时间--%>
                                <div class="tab-pane fade" id="attribute2">
                                    <div style="width: 85%; float: left;">
                                        创建时间：
                                        <asp:Label ID="Label1" runat="server"></asp:Label><p></p>
                                        开始时间：
                                        <input id="starttime1" type="text" /><input id="Button3" class="btn btn-default" type="button" value="修改" style="margin-left: 2%; padding: 5px 15px;" /><p></p>
                                        截止时间：
                                        <input id="endtime1" type="text" /><input id="Button4" class="btn btn-default" type="button" value="修改" style="margin-left: 2%; padding: 5px 15px;" /><p></p>
                                        优先级：
                                        <input id="Button2" class="btn btn-default" type="button" style="margin-left: 1%; padding-top: 2px; padding-bottom: 2px;" value="普通" onclick="SetUrgency(this.id)" />
                                        <input id="Button5" class="btn btn-default" type="button" style="margin-left: 1%; padding-top: 2px; padding-bottom: 2px;" value="紧急" onclick="SetUrgency(this.id)" />
                                        <input id="Button6" class="btn btn-default" type="button" style="margin-left: 1%; padding-top: 2px; padding-bottom: 2px;" value="非常紧急" onclick="SetUrgency(this.id)" />

                                    </div>
                                </div>
                                <%--选项卡3：基本信息--%>
                                <div class="tab-pane fade in active" id="attribute3">
                                    任务名：<p></p>
                                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Style="color: #999999; resize: none; margin-bottom: 10px;"></asp:TextBox>
                                    备注：<p></p>
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" Style="color: #999999; resize: none; margin-bottom: 10px;"></asp:TextBox>
                                    <input id="Button15" class="btn btn-info" type="button" value="保存" style="float: right;" />
                                </div>
                                <%--选项卡4：链接--%>
                                <div class="tab-pane fade" id="attribute4">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:Repeater ID="Repeater3" runat="server">
                                                <ItemTemplate>
                                                    <div data-id="<%# Eval("Expr1") %>">
                                                        <div style="width: 70%; display: inline-block;">
                                                            <asp:LinkButton ID="LinkButton2" runat="server"><%# Eval("FilePath") %></asp:LinkButton>
                                                        </div>
                                                        <div style="display: inline-block">
                                                            <a href="">取消关联</a>
                                                        </div>
                                                    </div>
                                                    <p style="clear: both;"></p>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--选项卡5：子任务--%>
                                <div class="tab-pane fade" id="attribute5">
                                    <div style="padding-left: 1%; padding-right: 1%;">子任务：</div>
                                    <div class="panel panel-default" style="max-height: 250px; overflow-y: scroll;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="Repeater4" runat="server">
                                                    <ItemTemplate>
                                                        <div class="panel-body">
                                                            <div data-id="<%# Eval("Expr1") %>">
                                                                <div style="width: 50%; float: left;">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" /><%# Eval("ItemText") %>
                                                                </div>
                                                                <div style="float: left" data-id="<%# Eval("Expr1") %>">
                                                                    <img alt="" src="<%# Eval("Avatar") %>" style="border-radius: 50%; height: 25px; width: 25px; vertical-align: middle;" />
                                                                    <strong style="padding: 10px;"><%# Eval("CDT").ToString().Substring(0,10) %></strong>
                                                                    <a href="">删除</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div>
                                        <input type="button" class="btn btn-default" value="添加子任务" style="width: 100%; margin-bottom: 10px; margin-top: -10px;" data-dismiss="modal" data-toggle="modal" data-target="#myModal4" /></div>
                                </div>
                                <div class="tab-pane fade" id="attribute6">
                                    <p>
                                        第6个
                                   
                               
                                    </p>
                                </div>
                                <div class="tab-pane fade" id="attribute7">

                                    <%--评论功能--%>
                                    <div style="clear: both"></div>
                                    <div style="border: 0px solid; padding-top: 15px;">
                                        <div id="Pro_Item_Comment" style="padding-left: 1%; padding-right: 1%;">点赞与评论：</div>
                                        <div>
                                            <div class="col-md-12">
                                                <asp:Button class="btn btn-default" ID="Button11" runat="server" Text="点赞" />
                                                <input type="button" class="btn btn-info" value="编写留言" onclick="document.getElementById('CommentWrite').style.display == 'none' ? document.getElementById('CommentWrite').style.display = 'block' : document.getElementById('CommentWrite').style.display = 'none'" style="width: 100%;" />
                                            </div>
                                            <div id="Write" runat="server" visible="true">

                                                <div id="CommentWrite" style="display: none;">
                                                    <div class="col-lg-12">
                                                        <div style="height: 100px">
                                                            <asp:TextBox ID="Editor1" runat="server" TextMode="MultiLine" />
                                                            <script type="text/javascript">
                                                                var editor = CKEDITOR.replace('<%= Editor1.ClientID %>', { height: "100px" });
                                                            </script>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both"></div>
                                                    <div class="col-lg-12">
                                                        <div style="float: right; margin-top: 45px;">
                                                            <asp:Button ID="Publish" runat="server" Text="发布" />
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="feedBack">
                                                    <div id="CommentShow">
                                                        <div class="col-md-12">
                                                            <div style="padding-top: 10px; border-top: 1px solid #808080; margin-top: 10px;">
                                                                <div class="col-md-2" style="float: left;">
                                                                    <div style="display: inline-block; float: left;">
                                                                        <asp:Image ID="ImageAvatar" ImageUrl="#" runat="server" Height="40px" Width="40px" /><br />
                                                                        <span style="font-size: 16px; color: #1277c1">某钿NK</span>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <p><span style="color: #808080;">[<strong>2017-02-08</strong>]</span></p>
                                                                    <asp:Label ID="LabelContent" runat="server" Text='这是第一条评论，或者说是留言这是第二条评论或者说是留言这是第二条评论，或者说是留言这是第二条评论，或者说是留言这是第二条评论，或者说是留言这是第二条评论，或者说是留言'></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div style="clear: both"></div>
                                                            <div style="padding-top: 10px; border-top: 1px solid #808080; margin-top: 10px;">
                                                                <div class="col-md-2" style="float: left;">
                                                                    <div style="display: inline-block; float: left;">
                                                                        <asp:Image ID="Image10" ImageUrl="#" runat="server" Height="40px" Width="40px" /><br />
                                                                        <span style="font-size: 16px; color: #1277c1">这货是谁</span>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <p><span style="color: #808080;">[<strong>2017-02-08</strong>]</span></p>
                                                                    <asp:Label ID="Label8" runat="server" Text='这是第二条评论，或者说是留言'></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <%--评论功能--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--第二层属性模态框--%>
    <%--邀请新成员--%>
    <div class="modal fade in" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title text-center" id="myModalLabel2">邀请新成员</h4>
                </div>
                <div class="modal-body ">
                    <span>账号邀请</span> <a href="#" style="float: right" id="show3"><i class="glyphicon glyphicon-qrcode"></i>&nbsp;&nbsp;<span>通过链接邀请</span></a>
                    <p></p>
                    <div class="parentCls">
                        <input type="email" id="emailtext" class="form-control inputElem" placeholder="请输入邮箱查找,回车确认" />
                    </div>
                    <br />


                    <p>合作过的成员</p>

                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="AddMember_Repeater_ItemDataBound">
                        <ItemTemplate>
                            <div>
                                <img src='<%# Eval("Avatar") %>' alt="头像" style="margin: 0 10px;" />
                                <span><%# Eval("UserName") %></span>
                                <asp:Button ID="Button7" class="btn btn-danger" runat="server" Text="移除" Style="float: right; margin-right: 20px;" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button1" class="btn btn-info" runat="server" data-dismiss="modal" data-toggle="modal" data-target="#myModal" Text="返回" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%--二维码邀请--%>
    <div class="modal fade in" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h2 class="modal-title text-center" id="myModalLabel3">邀请成员</h2>
                </div>
                <div class="modal-body">
                    <h5 class="text-center">扫描二维码加入项目或分享项目链接邀请成员</h5>
                    <div id="twodimensioncode" class="text-center">
                    </div>
                    <div style="display: none">
                        <select id="render">
                            <option value="canvas" selected="selected">Canvas</option>
                        </select>
                        <input id="size" type="range" value="150" min="100" max="1000" step="50" />
                        <input id="color" type="color" value="#333333" />
                        <input id="bg-color" type="color" value="#ffffff" />
                        <textarea id="text">http://resys.cn/</textarea>
                        <input id="minversion" type="range" value="6" min="1" max="10" step="1" />
                        <select id="eclevel">
                            <option value="L">L - Low (7%)</option>
                            <option value="M">M - Medium (15%)</option>
                            <option value="Q">Q - Quartile (25%)</option>
                            <option value="H" selected="selected">H - High (30%)</option>
                        </select>
                        <input id="quiet" type="range" value="1" min="0" max="4" step="1" />
                        <input id="radius" type="range" value="50" min="0" max="50" step="10" />
                        <select id="mode">
                            <option value="0">0 - Normal</option>
                            <option value="1">1 - Label-Strip</option>
                            <option value="2" selected="selected">2 - Label-Box</option>
                            <option value="3">3 - Image-Strip</option>
                            <option value="4">4 - Image-Box</option>
                        </select>
                        <input id="label" type="text" value="Resys" />
                        <input id="fontsize" type="range" value="20" min="1" max="30" step="1" />
                        <input id="font" type="text" value="Impact" />
                        <input id="fontcolor" type="color" value="#777777" />
                        <input id="image" type="file" />
                        <input id="imagesize" type="range" value="13" min="1" max="30" step="1" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <%--新增子任务--%>
    <div class="modal fade in" id="myModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" data-toggle="modal" data-target="#myModal"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title text-center" id="myModalLabel4">新增子任务</h4>
                </div>
                <div class="modal-body ">
                    <div class="form-group">
                        <label for="n_subitemname" class="control-label">子任务：</label>
                        <asp:TextBox ID="n_subitemname" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="n_subitemE" class="control-label">执行者：</label>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="n_subitemE" runat="server" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <input type="hidden" id="this_item_id" runat="server" class="form-control" />
            </div>
                <div class="modal-footer">
                    <button id="New_SubItem" class="btn btn-info" data-dismiss="modal" data-toggle="modal" data-target="#myModal">确认添加</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" data-toggle="modal" data-target="#myModal">返回</button>
        </div>
    </div>
        </div>
    </div>
    <!-- TaskItemsMore Modal -->
    <script type="text/javascript">
        $(document).on('show.bs.modal', '.modal', function(event) {
            $(this).appendTo($('body'));
        }).on('shown.bs.modal', '.modal.in', function(event) {
            setModalsAndBackdropsOrder();
        }).on('hidden.bs.modal', '.modal', function(event) {
            setModalsAndBackdropsOrder();
        });

        $(document).ready(    
           
             function() {    
                 $("#myModal2").keydown(function(event) {    
                     if (event.keyCode == 13) { 
                         var email = $('#emailtext').val().trim();
                         var username =  document.getElementById('<%=UserName.ClientID %>').innerText;

                         var joint = username + "(" + email + ")";
                   
                         
                         //获取当前网址，如： http://localhost:8080/Tmall/index.jsp
                         var curWwwPath=window.document.location.href;

                         //获取主机地址之后的目录如：/Tmall/index.jsp
                         var pathName=window.document.location.pathname;
                         var pos=curWwwPath.indexOf(pathName);

                         //获取主机地址，如： http://localhost:8080
                         var localhostPaht=curWwwPath.substring(0,pos) + "/Product/InviteToProject.aspx?Email=" + email; 

                         
                         //alert(localhostPaht); 
                         
                         $.ajax({
                             type: "post",
                             url: "Users_WebService.asmx/sendEmail", //服务端处理程序   
                             data:{ Email: email, UserName: username, Href: localhostPaht},
                             success: function (res) {
                                 //alert("1");
                                 console.log('发送邮件成功');
                             }
                         });
                     }    
                 })    
             }    
             

         );  

        function setModalsAndBackdropsOrder() {  
            var modalZIndex = 1040;
            $('.modal.in').each(function(index) {
                var $modal = $(this);
                modalZIndex++;
                $modal.css('zIndex', modalZIndex);
                $modal.next('.modal-backdrop.in').addClass('hidden').css('zIndex', modalZIndex - 1);
            });
            $('.modal.in:visible:last').focus().next('.modal-backdrop.in').removeClass('hidden');
        }

      
        
        $('#myModal').on("show.bs.modal", function (e) {
            
            var btn = $(e.relatedTarget),
               itemtext = btn.parent().data("itemtext"),
               iid = btn.parent().data("id");

          
            $("#show2").click(function(){  
                         
                $("#myModal2").modal("show");
            
            });  

            $("#show3").click(function(){  
                         
                $("#myModal3").modal("show");
            
            });  

            
           
     

            //$(".nice-select").click(function(e){
            //    $(this).find("ul").show();
            //    e.stopPropagation();
            //});
            //$('#fade').addClass("fade");
            $('#dLabel').click(function(e){
                $('#fade').toggleClass("fade");
            });
			
            $(".dropdown ul li").hover(function(e){
                $(this).toggleClass("on");
                e.stopPropagation();
            });
			
            var Uarry=$(".dropdown ul li");//获取所有的li元素  
            $(".dropdown ul li").click(function(){//点击事件       
                     
                var count=$(this).index();//获取li的下标  
                // var email=Uarry.eq(count).val();
                var email=Uarry.eq(count).text();
                //alert(email);
                $.ajax({
                    type: "post",
                    url: "Users_WebService.asmx/changeExector", //服务端处理程序   
                    data:{ TaskItemID: iid, Email: email },
                    success: function (res) {
                        window.location.reload();
                        console.log('修改执行者成功');
                    }
                });
              
                e.stopPropagation();
            });

           
            $(document).click(function(){
                $(".dropdown").find("ul").show();
                $('#fade').show();
                $("#customerId").show();
            });
        });

        function searchList(strValue) {
            var count = 0;
            if (strValue != "") {
                $(".dropdown ul li").each(function(i) {
                    var contentValue = $(this).text();
                    if (contentValue.toLowerCase().indexOf(strValue.toLowerCase()) < 0) {
                        $(this).hide();
                        count++;
                    } else {
                        $(this).show();
                    }
                    if (count == (i + 1)) {
                        $(".dropdown").find("ul").hide();
                    } else {
                        $(".dropdown").find("ul").show();
                    }
                });
            } else {
                $(".dropdown ul li").each(function(i) {
                    $(this).show();
                });
            }
        };

         
		
	</script>

    <script type="text/javascript">
        //ITEM属性ID传值
        $(".task-content-set").click(function () {
            document.getElementById('<%=ita_hidf.ClientID %>').value = $(this).parent().data("id");
            document.getElementById('<%=aBt.ClientID %>').click();
        });
        //任务属性 Modal 传参数
        var sign1=0;//0时关闭模态框刷新；
        $('#myModal').on("show.bs.modal", function (e) {
            sign1=1;
            var btn = $(e.relatedTarget),
                itemtext = btn.parent().data("itemtext"),
                iid = btn.parent().data("id");
            // 根据ID传参数
            //document.getElementById('<%=Label5.ClientID %>').innerText = itemtext;
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/SelectItemAttribute", //服务端处理程序   
                data: { id: btn.parent().data("id") },
                success: function (res) {
                    document.getElementById('<%=TextBox1.ClientID %>').value = $(res).find("ItemNote").text();
                    document.getElementById('<%=TextBox2.ClientID %>').value = $(res).find("ItemText").text();
                    document.getElementById('<%=Label1.ClientID %>').innerText = $(res).find("CDT").text().substr(0,10);
                    document.getElementById('starttime1').value= $(res).find("BeginDT").text().substr(0, 10);
                    document.getElementById('endtime1').value= $(res).find("EndDT").text().substr(0, 10);
                    if ($(res).find("Priority").text() == 0) document.getElementById('Button2').setAttribute("class", "btn btn-tiny");
                    if ($(res).find("Priority").text() == 1) document.getElementById('Button5').setAttribute("class", "btn btn-warning");
                    if ($(res).find("Priority").text() == 2) document.getElementById('Button6').setAttribute("class", "btn btn-danger");
                    console.log('查找item成功');
                },
                error: function () {
                    console.log('查找item失败');
                }
            });
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/SelectItemMember", //服务端处理程序   
                data: { id: btn.parent().data("id") },
                success: function (res) {
                    console.log('查找itemmember成功');
                },
                error: function () {
                    console.log('查找item失败');
                }
            });
        })
        //模态框关闭时刷新
        $('#myModal').on('hide.bs.modal', function () {
           // window.location.reload(); 
        })
        $('#myModal4').on('show.bs.modal', function () {
            sign1=0;
        })
        $('#myModal4').on('hide.bs.modal', function () {
            sign1=1;
        })
        
        //新建任务 Modal 传参数
        var modal = $("#TaskCreator");
        modal.on("show.bs.modal", function (e) {
            // 这里的btn就是触发元素，即你点击的新建按钮
            var btn = $(e.relatedTarget),
                listid = btn.data("listid"),
                totalnum = btn.data("totalnum");
            // 根据ID传参数
            //document.getElementById('<%=TaskListID.ClientID %>').value = listid;
            $("#<%=TaskListID.ClientID %>").attr("value", listid);
            $("#<%=ItemOrders.ClientID %>").attr("value", totalnum);//ItemOrders 从0计算
        });

        //编辑任务列表 Modal 传参数
        var listmoremodal = $("#TaskListMore");
        listmoremodal.on("show.bs.modal", function (e) {
            // 这里的btn就是触发元素，即你点击的编辑按钮
            var btn = $(e.relatedTarget),
                listid = btn.data("listid"),
                totalitemnums = btn.data("totalitemnums");
            // 根据ID传参数
            //document.getElementById('<%=TaskListEditID.ClientID %>').value = listid;
            $("#<%=TaskListEditID.ClientID %>").attr("value", listid);
            $("#TaskListTextNew").val($("#TaskListText_" + listid + "").text());
            $("#<%=TotalItemNums.ClientID %>").attr("value", totalitemnums);
        });

        //TaskListDel删除按钮判断与隐藏
        function TaskListDel() {
            if ($("#<%=TotalItemNums.ClientID %>").attr("value") == 0) {
                $("#WarningLabel").html("您确定要永远删除这个列表吗?");//改变提示信息的文字内容
                $("#TaskListDelBtn").css("display", "inline-block");//确认删除按钮显示
            }
            else {
                $("#WarningLabel").html("删除该任务列表前，请清空列表中所有的任务！");//改变提示信息的文字内容
            }
            $("#DelBtn").css("display", "none");//按钮隐藏
            $("#ConfirmBtn").css("display", "none");//按钮隐藏
            $("#modal-body1").css("display", "none");//modal-body1隐藏
            $("#modal-body2").css("display", "block");//modal-body2显示
        }

        //TaskListConfirm确认按钮
        $("#ConfirmBtn").click(function () {
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/TaskListUpdateName", //服务端处理程序   
                data: { listid: $("#<%=TaskListEditID.ClientID %>").attr("value"), newname: $("#TaskListTextNew").val() },
                success: function (msg) {
                    if (msg.getElementsByTagName("string")[0].childNodes[0].nodeValue!=" ")
                    {
                        $("#TaskListText_" + $("#<%=TaskListEditID.ClientID %>").attr("value") + "").text(msg.getElementsByTagName("string")[0].childNodes[0].nodeValue);
                    }
                    else {
                        alert("任务列表名不能为空！");
                    }
                    
                }
            });
        });

        //TaskListDelBtn确认删除按钮
        $("#TaskListDelBtn").click(function () {
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/TaskListDel", //服务端处理程序   
                data: { listid: $("#<%=TaskListEditID.ClientID %>").attr("value"), projectid: <%= projectID %> },
                success: function (msg) {
                    console.log(msg);
                    if (msg.getElementsByTagName("int")[0].childNodes[0].nodeValue == "1")
                    {
                        $("#ListID_" + $("#<%=TaskListEditID.ClientID %>").attr("value") + "").remove();
                    }
                    else {
                        alert("任务列表删除失败！");
                    }
                }
            });
        });
        //修改Item基本属性
        $("#Button15").click(function () {
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/TaskItemUpdateName", //服务端处理程序   
                data: { itemid: document.getElementById('<%=ita_hidf.ClientID %>').value, itemname: $("#<%=TextBox2.ClientID %>").val(),itemnote:$("#<%=TextBox1.ClientID %>").val() },
                success: function (msg) {
                    console.log(document.getElementById('<%=ita_hidf.ClientID %>').value);
                    document.getElementById('<%=Label5.ClientID %>').innerText=$("#<%=TextBox2.ClientID %>").val();
                }
            });
        });
        //设置时间按钮
        $("#Button3").click(function(){
            var a = /^(\d{4})-(\d{2})-(\d{2})$/;
            if (!a.test(document.getElementById("starttime1").value)) { 
                alert("日期格式不正确!") }
            else{
                var stime=new Date(document.getElementById("starttime1").value);
                var etime=new Date(document.getElementById("endtime1").value);
                if(stime>etime){alert("开始时间不能大于结束时间！")}
                else{
                    $.ajax({
                        type: "post",
                        url: "Taskboard_WebService.asmx/UpdateStartTime", //服务端处理程序   
                        data: { id: document.getElementById('<%=ita_hidf.ClientID %>').value, time:document.getElementById("starttime1").value },
                    success: function (msg) {
                        alert("修改开始时间成功!") 
                    },
                    error:function(msg){
                        alert("请输入正确的日期!") 
                    }
                });}
        }
        });
        //设置时间按钮
        $("#Button4").click(function(){
            var a = /^(\d{4})-(\d{2})-(\d{2})$/
            if (!a.test(document.getElementById("endtime1").value)) { 
                alert("日期格式不正确!") }
            else{
                var stime=new Date(document.getElementById("starttime1").value);
                var etime=new Date(document.getElementById("endtime1").value);
                if(stime>etime){alert("结束时间不能小于开始时间！")}
                else{
                    $.ajax({
                        type: "post",
                        url: "Taskboard_WebService.asmx/UpdateEndTime", //服务端处理程序   
                        data: { id: document.getElementById('<%=ita_hidf.ClientID %>').value, time:document.getElementById("endtime1").value },
                    success: function (msg) {
                        alert("修改结束时间成功!") 
                    },
                    error:function(msg){
                        alert("请输入正确的日期!") 
                    }
                });}
        }
    });
    //设置时间按钮
        function SetUrgency(u_id){
            var a="";
            var c="";
            if(u_id=="Button2"){a="0";c="btn btn-tiny"}
            else if(u_id=="Button5"){a="1";c="btn btn-warning"}
            else if(u_id=="Button6"){a="2";c="btn btn-danger"}
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/UpdateUrgency", //服务端处理程序   
                data: { id: document.getElementById('<%=ita_hidf.ClientID %>').value, u:a },
                success: function (msg) {
                    document.getElementById("Button2").setAttribute("class","btn btn-default");
                    document.getElementById("Button5").setAttribute("class","btn btn-default");
                    document.getElementById("Button6").setAttribute("class","btn btn-default");
                    document.getElementById(u_id).setAttribute("class", c);
                },
                error:function(msg){
                    alert("修改失败!") ;
                }
            });
    }
        //新增子任务
        $("#New_SubItem").click(function(){
            if(document.getElementById("<%= n_subitemname.ClientID %>").value == ""){alert("子任务名不能为空！")}
            else{
                var NsubitemE=document.getElementById('<%=n_subitemE.ClientID %>');
                var NsubitemEValue = NsubitemE.options[NsubitemE.selectedIndex].value;
                    $.ajax({
                        type: "post",
                        url: "Taskboard_WebService.asmx/InsertSubItem", //服务端处理程序   
                        data: { id: document.getElementById('<%=ita_hidf.ClientID %>').value, subitemname:document.getElementById("<%= n_subitemname.ClientID %>").value, subitemE:NsubitemEValue },
                        success: function (msg) {
                            document.getElementById('<%=aBt.ClientID %>').click();
                            alert("添加子任务成功!") ;
                        },
                        error:function(msg){
                            alert("添加子任务失败!") ;
                        }
                    });
                }
            
        });
    </script>
</asp:Content>
