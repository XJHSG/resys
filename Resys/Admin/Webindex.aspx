<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/WebBackstage.master" AutoEventWireup="true" CodeFile="Webindex.aspx.cs" Inherits="Admin_Webindex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
         当前位置：<a href="#">首页</a>
         <div class="col-md-12">
             <div class="profile-container">
                 <div class="profile-header row">
                     <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-top:30px">

                        <div class="databox radius-bordered databox-shadowed databox-graded" >
                            <div class="databox-left bg-themeprimary">
                                <div class="databox-piechart">
                                    <div data-toggle="easypiechart" class="easyPieChart" data-barcolor="#fff" data-linecap="butt" data-percent="15" data-animate="500" data-linewidth="3" data-size="47" data-trackcolor="rgba(255,255,255,0.2)"><span class="white font-90">15%</span></div>
                                </div>
                            </div>
                            <div class="databox-right">
                                <span class="databox-number themethirdcolor">3</span>
                                <div class="databox-text darkgray">新增用户</div>
                                <div class="databox-stat themethirdcolor radius-bordered">
                                    <i class="stat-icon  icon-lg fa fa-envelope-o"></i>
                                </div>
                            </div>
                        </div>
                     </div>
                 </div>
             </div>
         </div>
     </div>
</asp:Content>

