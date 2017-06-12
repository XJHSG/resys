<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="Info_Center.aspx.cs" Inherits="Product_Info_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/style.css" />
    <style type="text/css">
        .accordion-toggle{
            display:block;
        }
        @media (min-width: 768px)
        {
            .bs{
                margin-right: 0;
                margin-left: 0;
                background-color: #fff;
                border-color: #ddd;
                border-width: 1px;
                border-radius: 4px 4px 0 0;
                -webkit-box-shadow: none;
                box-shadow: none;
            }
        }
        .bs {
            height:100%;
            position: relative;
            padding: 25px 15px 15px;
            margin: 10px 0px;
            border-color: #e5e5e5 #eee #eee;
            border-style: solid;
            border-width: 1px;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
            box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
        }
        .accordion-title{
            margin-top: 0;
            margin-bottom: 15px;
            text-align:center;
        }
    </style>
    <script src="http://cdn.jsdelivr.net/vue/2.1.3/vue.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <nav class="project-navigation">
        <div style="float: left;">
            <a href="../Index.aspx" class="btn btn-primary btn-arrow-right">首页</a>
            <a href="User_Center.aspx" class="btn btn-info btn-arrow-right">项目</a>
            <button type="button" class="btn btn-info btn-arrow-right">项目一</button>
        </div>
        <div style="float: right">
            <ol class="breadcrumb" id="Project_son">
                <li><a href="#">课程信息</a></li>
                <li class="active">学习任务</li>
                <li><a href="File_Man.aspx">教学资源</a></li>
                <li><a href="#">群聊</a></li>
                <li><a href="#">更多</a></li>
            </ol>
        </div>
    </nav>

    <div class="container">
      <div class="row">
        <div class="col-md-3">
            <div class="bs" id="app-1">
            <h3 class="accordion-title">课程名称</h3>
            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
              <div class="panel panel-default" v-for="list in lists">
                <div class="panel-heading" role="tab" :id="list.headingid">
                  <h4 class="panel-title">
                    <a class="accordion-toggle" role="button" data-toggle="collapse" data-parent="#accordion" :href="list.href" aria-expanded="true" :aria-controls="list.collapseid">
                      {{ list.text }}
                    </a>
                  </h4>
                </div>
                <div :id="list.collapseid" class="panel-collapse collapse" role="tabpanel" :aria-labelledby="list.headingid">
                  <div class="panel-body">
                    Anim pariatur cliche reprehenderit
                  </div>
                </div>
              </div>
              <%--<div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingTwo">
                  <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                      单元二
                    </a>
                  </h4>
                </div>
                <div id="collapseTwo" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                  <div class="panel-body">
                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                  </div>
                </div>
              </div>
              <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingThree">
                  <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                      单元三
                    </a>
                  </h4>
                </div>
                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                  <div class="panel-body">
                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                  </div>
                </div>
              </div>

                <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingFour">
                  <h4 class="panel-title">
                    <a class="accordion-toggle collapsed" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                      单元四
                    </a>
                  </h4>
                </div>
                <div id="collapseFour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                  <div class="panel-body">
                    Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                  </div>
                </div>
              </div>--%>
            </div>
            </div>
        </div>
        <div class="col-md-9">
            <div class="bs" id="app-2">
                <div class="table-responsive">
                  <table class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>任务标题</th>
                            <th>截止时间</th>
                            <th>评分结果</th>
                            <th>完成状态</th>
                            <th>创建人</th>
                            <th>评论数</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="list in lists">
                        <th scope="row">{{ list.text }}</th>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        </tr>
                        <%--<tr>
                        <th scope="row">2</th>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        </tr>
                        <tr>
                        <th scope="row">3</th>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        <td>Table cell</td>
                        </tr>--%>
                    </tbody>
                  </table>
                </div>
            </div>
        </div>
      </div>
    </div>

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
                    <asp:Button ID="TaskListCreatorBtn" runat="server" Text="确 定" CssClass="btn btn-primary"  />
                </div>
            </div>
        </div>
    </div>
    <!-- TaskList Modal -->
<%--OnClick="TaskListCreatorBtn_Click"--%>
    <script type="text/javascript">
        var app1 = new Vue({
            el: '#app-1',
            data: {
                lists: [
                  { text: '任务列表一', headingid: 'h1', collapseid: 'c1', href: '#c1' },
                  { text: '任务列表二', headingid: 'h2', collapseid: 'c2', href: '#c2' },
                  { text: '任务列表三', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '任务列表四', headingid: 'h4', collapseid: 'c4', href: '#c4' },
                  { text: '任务列表五', headingid: 'h5', collapseid: 'c5', href: '#c5' },
                ]
            }
        });

        var app2 = new Vue({
            el: '#app-2',
            data: {
                lists: [
                  { text: '子任务1', headingid: 'h1', collapseid: 'c1', href: '#c1' },
                  { text: '子任务2', headingid: 'h2', collapseid: 'c2', href: '#c2' },
                  { text: '子任务3', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务4', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务5', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务6', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务7', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务8', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务9', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务10', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务11', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务12', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务13', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务14', headingid: 'h3', collapseid: 'c3', href: '#c3' },
                  { text: '子任务15', headingid: 'h3', collapseid: 'c3', href: '#c3' }
                ]
            }
        })
    </script>

</asp:Content>

