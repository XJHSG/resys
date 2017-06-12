<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="File_Upload.aspx.cs" Inherits="Uploads_File_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="webuploader/style2.css" />
    <link rel="stylesheet" type="text/css" href="webuploader/webuploader.css" />

    <script type="text/javascript" src="webuploader/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="webuploader/webuploader.js"></script>
    <script type="text/javascript" src="webuploader/ImagUpload.js"></script>
    <script type="text/javascript">
        $().ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="clearfix"></div>
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div id="wrapper">
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <div>                                
                    <asp:Label ID="FET" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FS" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FN" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="FP" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="ResourceTypeLabel" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div id="container">
                    <!--头部，相册选择和格式选择-->
                    <div id="uploader">
                        <div class="queueList">
                            <p>1、选择要上传的文件。合法的文件包括：音频、视频、图片、文档、压缩、Flash等。</p>
                            <div id="dndArea" class="placeholder">
                                
                                <div id="filePicker"></div>
                                <p>或将文件拖到这里，单次最多可选50个文件</p>
                                <p>&nbsp;</p>
                            </div>
                        </div>
                        <div class="statusBar" style="display: none;">
                            <div class="progress">
                                <span class="text">0%</span>
                                <span class="percentage"></span>
                            </div>
                            <div class="info"></div>
                            <div class="btns">
                                <div id="filePicker2"></div>
                                <div class="uploadBtn">开始上传</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <p>&nbsp;</p>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="确定上传" CssClass="btn btn-default"/>&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="返回" CssClass="btn btn-default" OnClick="Button2_Click"/></div>
        </div>
    </div>
</asp:Content>

