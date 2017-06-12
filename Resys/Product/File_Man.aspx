<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="File_Man.aspx.cs" Inherits="Uploads_File_Man" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style2 {
            width: 62px;
        }
        .auto-style3 {
            width: 248px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span><a>文件库</a> /
                        <asp:LinkButton ID="LB_Folder1" runat="server">一级文件夹</asp:LinkButton><asp:HiddenField ID="hf_FolderID1" runat="server" />/
                        <asp:LinkButton ID="LB_Folder2" runat="server">二级文件夹</asp:LinkButton><asp:HiddenField ID="hf_FolderID2" runat="server" />/
                        <asp:LinkButton ID="LB_Folder3" runat="server">三级文件夹</asp:LinkButton><asp:HiddenField ID="hf_FolderID3" runat="server" />/
                        <asp:LinkButton ID="LB_Folder4" runat="server">四级文件夹</asp:LinkButton><asp:HiddenField ID="hf_FolderID4" runat="server" />
                    </span>
                    <span style="float:right">
                        <asp:LinkButton ID="LbUpload" runat="server" OnClick="LbUpload_Click"><i class="glyphicon glyphicon-arrow-up"></i>上传文件</asp:LinkButton></span>
                    <%--<span style="float: right"><a href="File_Upload.aspx"><i class="glyphicon glyphicon-arrow-up"></i>上传文件</a></span>--%>
                    <span style="float: right"><a href="#" data-toggle="modal" data-target="#myModal"><i class="glyphicon glyphicon-plus"></i>新建文件夹</a>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                </div>
                <div class="panel-body">
                    <div class="table-responsive" runat="server" id="panel1">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="auto-style2">    
                                        <input type="checkbox" id="chkAll" name="chkAll" value="checkbox" onclick="checkAll  ('chkAll',this);" /></th>
                                    <th>
                                        <div id="allWork" style="display:none">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="Lb_EditName" runat="server"><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="Lb_Del" runat="server" OnClientClick="return confirm('确定要删除所选内容？')"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                        </div>
                                    </th>
                                    <th class="auto-style3">大小</th>
                                    <th>共享来源</th>
                                    <th><%# String.Format("{0:yyyy年MM月dd日 HH时}",Eval("CDT") ) %></th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="Repeater_Folders" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox" runat="server"  name="chkSelect" onclick="checkNumber();"/>
                                            &nbsp;&nbsp;<i class="glyphicon glyphicon-folder-open text-primary"></i>
                                            <asp:Label ID="LabelID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LabelParentID" runat="server" Text='<%#Eval("ParentID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LabelLevel" runat="server" Text='<%#Eval("level") %>' Visible="false"></asp:Label>                                        
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LB_Name" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="refreshTable"><%#Eval("FolderName") %></asp:LinkButton>
                                            <asp:TextBox ID="TextBox_Name" runat="server" Visible="false" placeholder="请输入新名字"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:LinkButton ID="LB_AlterName1" runat="server" Visible="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="AlterName"><i class="glyphicon glyphicon-plus"></i></asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="LB_Cancel1" runat="server" Visible="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="cancel"><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                        </td>
                                        <td><%#Eval("FolderSizeInKB") %></td>
                                        <td>共享来源</td>
                                        <td><%# String.Format("{0:yyyy年MM月dd日 HH时}",Eval("CDT") ) %></td>
                                        <td>
                                            <asp:LinkButton ID="Lb_EditName" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="ChangeName"><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="Lb_Del" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="DEL" OnClientClick="return confirm('确定要删除？')"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="Lb_Move" runat="server" data-toggle="modal" data-target="#MoveModal" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="MOVE"><i class="glyphicon glyphicon-retweet"></i></asp:LinkButton>
                                        </td>
                                        <td><i class="glyphicon glyphicon-chevron-up"></i></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater ID="Repeater_File" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox" runat="server" /> &nbsp;&nbsp;
                                            <asp:Label ID="Lbtubiao" runat="server"></asp:Label>
                                            <asp:Label ID="LabelID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="LbExtension" runat="server" Visible="false" Text='<%#Eval("Extension") %>'></asp:Label>
                                        </td>
                                       
                                        <td>
                                            <asp:LinkButton ID="LB_Name" runat="server"><a target="_blank" href='../<%# Eval("FilePath") %>'><%#Eval("ShowName") %></a></asp:LinkButton>
                                            <asp:TextBox ID="TextBox_Name" runat="server" Visible="false" placeholder="请输入新名字"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:LinkButton ID="LB_AlterName1" runat="server" Visible="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="AlterName"><i class="glyphicon glyphicon-plus"></i></asp:LinkButton>&nbsp;&nbsp;
                                            <asp:LinkButton ID="LB_Cancel1" runat="server" Visible="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="cancel" ><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                        </td>
                                        <td><%#Eval("FileSizeInKB") %></td>
                                        <td>共享来源</td>
                                        <td><%# String.Format("{0:yyyy年MM月dd日 HH时}",Eval("CDT") ) %></td>
                                        <td><asp:LinkButton ID="Lb_EditName" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="ChangeName"><i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="Lb_Del" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="DEL" OnClientClick="return confirm('确定要删除？')"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="Lb_Move" runat="server" data-toggle="modal" data-target="#MoveModal" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="MOVE"><i class="glyphicon glyphicon-retweet"></i></asp:LinkButton>
                                        </td>
                                        <td><i class="glyphicon glyphicon-chevron-up"></i></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                    <div class="col-md-12" id="ErrorPanel" runat="server" visible="false" style="text-align: center;">
                        <h1>你尚未有文件夹，请创建文件夹</h1>
                        <button class="btn btn-primary btn-lg" type="button" data-toggle="modal" data-target="#myModal">新建文件夹</button>
                    </div>

                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel">
                                        <asp:Label ID="LabelModalTitle" runat="server" Text="Label">新建文件夹</asp:Label>
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <h4>请输入文件名</h4>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-lg"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        取消
                                    </button>
                                    <asp:Button ID="btn_addFolder" runat="server" Text="确认提交" CssClass="btn btn-primary" OnClick="btn_addFolder_Click" />
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal -->
                    </div>

                    <div class="modal fade" id="MoveModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                &times;
                                            </button>
                                            <h4 class="modal-title">移动到</h4>
                                            <asp:HiddenField ID="Hf_arrFolders" runat="server" Value="0" />
                                            <asp:HiddenField ID="moveHfLevel" runat="server" Value="1"/>
                                            <asp:HiddenField ID="moveHfFolderID" runat="server" value="0"/>
                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="modal-body">
                                            <asp:LinkButton ID="Lb_Back" runat="server" Visible="false" OnClick="Lb_Back_Click"><i class="glyphicon glyphicon-arrow-left"></i>后退</asp:LinkButton>
                                            <ul class="list-group">
                                                <asp:Repeater ID="rptFolder" runat="server" OnItemCommand="rptFolder_ItemCommand">
                                                    <ItemTemplate>
                                                        <li class="list-group-item">
                                                            <%--<asp:RadioButton ID="economic" runat="server" />--%>
                                                            <input type="radio" id="economic" name="economic" value='<%# Eval("Id") %>' runat="server" onclick="return selectSingleRadio(this, 'economic');" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:HiddenField ID="hfid" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:LinkButton ID="LB_Name" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="refreshTable"><%#Eval("FolderName") %></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                取消
                                            </button>
                                            <asp:Button ID="Btn_move" runat="server" Text="确认提交" CssClass="btn btn-primary" OnClick="Btn_move_Click"/>
                                        </div>
                                    
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal -->
                    </div>

                    <asp:Label ID="LabelProjectID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LabelLevel" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LabelParentID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LabelFolderID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LabelFileID" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="LabelFolderName" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:HiddenField ID="moveType" runat="server" />
                    <asp:HiddenField ID="moveID" runat="server" />
                    
                </div>
            </div>
        </div>
    </div>

        
        <script type="text/javascript">  
            function selectSingleRadio(rbtn1, GroupName) {  
                $("input[type=radio]").each(function (i) {  
                    if (this.name.substring(this.name.length - GroupName.length) == GroupName) {  
                        this.checked = false;  
                    }  
                })  
                rbtn1.checked = true;  
            }

            function checkAll(chkAllID, thisObj) {
                var chkAll = document.getElementById(chkAllID);
                var chks = document.getElementsByTagName("input");
                var chkNo = 0;
                var selectNo = 0;
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i].type == "checkbox") {
                        //全选触发事件  
                        if (chkAll == thisObj) {
                            chks[i].checked = thisObj.checked;
                        }
                            //非全选触发 
                        else {
                            if (chks[i].checked && chks[i].id != chkAllID)
                                selectNo++;
                        }
                        if (chks[i].id != chkAllID) {
                            chkNo++;
                        }
                    }
                }
                if (chkAll != thisObj) {
                    chkAll.checked = chkNo == selectNo;
                }
                checkNumber();
            }
            //checkSelectNo 函数是用来获取 所有checkbox 选中的个数 这个在用来判断 是否有勾选时非常有用。
            function checkSelectNo(chkAllID) {
                var chks = document.getElementsByTagName("input");
                var selectNo = 0;
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i].type == "checkbox") {
                        if (chks[i].id != chkAllID && chks[i].checked) {
                            selectNo++;
                        }
                    }
                }
                return selectNo;
            }
            
            function checkNumber()
            {
                if (checkSelectNo(checkAll) > 1) {
                    document.getElementById("allWork").style.display = "";//显示
                } else {
                    document.getElementById("allWork").style.display = "none";//隐藏
                }
            }

            
        </script>  

</asp:Content>


