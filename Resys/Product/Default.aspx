﻿<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Product_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/css/bootstrap-combined.min.css" rel="stylesheet"/>  
    <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>  
    <script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tree well">
        <ul id="ul_tree">
        </ul>
    </div>
    <script type="text/javascript">  
        var testdata = [{  
            id: '1',  
            text: '系统设置',  
            nodes: [{  
                id: '11',  
                text: '编码管理',  
                nodes: [{  
                    id: '111',  
                    text: '自动管理',  
                    nodes: [{  
                        id: '1111',  
                        text: '手动管理',  
                        nodes: [{  
                            id: '11111',  
                            text: '底层管理',  
                        }]  
                    }]  
                }]  
            }]  
        }, {  
            id: '2',  
            text: '基础数据',  
            nodes: [{  
                id: '21',  
                text: '基础特征'  
            }, {  
                id: '22',  
                text: '特征管理'  
            }]  
        }];  
  
  
        (function ($) {  
            //使用js的严格模式  
            'use strict';  
  
            $.fn.jqtree = function (options) {  
                //合并默认参数和用户传过来的参数  
                options = $.extend({}, $.fn.jqtree.defaults, options || {});  
  
                var that = $(this);  
                var strHtml = "";  
                //如果用户传了data的值，则直接使用data，否则发送ajax请求去取data  
                if (options.data) {  
                    strHtml = initTree(options.data);  
                    that.html(strHtml);  
                    initClickNode();  
                }  
                else {  
                    //在发送请求之前执行事件  
                    options.onBeforeLoad.call(that, options.param);  
                    if (!options.url)  
                        return;  
                    //发送远程请求获得data  
                    $.getJSON(options.url, options.param, function (data) {  
                        strHtml = initTree(data);  
                        that.html(strHtml);  
                        initClickNode();  
  
                        //请求完成之后执行事件  
                        options.onLoadSuccess.call(that, data);  
                    });  
                }  
  
                //注册节点的点击事件  
                function initClickNode() {  
                    $('.tree li').addClass('parent_li').find(' > span').attr('title', '收起');  
                      
                    //设置节点默认关闭状态  
                    $('.tree li.parent_li > span').parent('li.parent_li').find(' > ul > li').hide('fast');  
                    $('.tree li.parent_li > span').on('click', function (e) {  
                        var children = $(this).parent('li.parent_li').find(' > ul > li');  
                        if (children.is(":visible")) {  
                            children.hide('fast');  
                            $(this).attr('title', '展开').find(' > i').addClass('icon-plus-sign').removeClass('icon-minus-sign');  
                        } else {  
                            children.show('fast');  
                            $(this).attr('title', '收起').find(' > i').addClass('icon-minus-sign').removeClass('icon-plus-sign');  
                        }  
  
                        $('.tree li[class="parent_li"]').find("span").css("background-color", "transparent");  
                        $(this).css("background-color", "#428bca");  
  
                        options.onClickNode.call($(this), $(this));  
                    });  
                };  
  
                //递归拼接html构造树形子节点  
                function initTree(data) {  
                    var strHtml = "";  
                    for (var i = 0; i < data.length; i++) {  
                        var arrChild = data[i].nodes;  
                        var strHtmlUL = "";  
                        var strIconStyle = "icon-leaf";  
                        if (arrChild && arrChild.length > 0) {  
                            strHtmlUL = "<ul>";  
                            strHtmlUL += initTree(arrChild) + "</ul>";  
                            strIconStyle = "icon-minus-sign";  
                        }  
  
                        strHtml += "<li style='list-style-type:none;' id=\"li_" + data[i].id + "\"><span id=\"span_" + data[i].id + "\"><i class=\"" + strIconStyle + "\"></i>" + data[i].text + "</span>" + strHtmlUL + "</li>";  
  
                    }  
                    return strHtml;  
                };  
            };  
  
            //默认参数  
            $.fn.jqtree.defaults = {  
                url: null,  
                param: null,  
                data: null,  
                onBeforeLoad: function (param) { },  
                onLoadSuccess: function (data) { },  
                onClickNode: function (selector) { }  
            };  
  
        })(jQuery);  
        $(function () {  
            $("#ul_tree").jqtree({  
                data: testdata,  
                param: {},  
                onBeforeLoad: function (param) {  
                },  
                onLoadSuccess: function (data) {  
                },  
                onClickNode: function (selector) {  
                }  
            });  
        });  
    </script>
</asp:Content>

