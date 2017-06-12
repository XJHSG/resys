//任务更新IsFinished
$(".check-box").click(function () {
    $(this).parent().toggleClass("done");//对被选元素进行添加/删除类
    $(this).children("span").toggleClass("mvisible");
    $.ajax({
        type: "post",
        url: "Taskboard_WebService.asmx/UpdateFinished", //服务端处理程序   
        data: { id: $(this).parent().data("id") },
        success: function (msg) {
            console.log('更新IsFinished成功');
        }
    });
});

//编辑任务列表 Modal 完全对用户隐藏时触发
$('#TaskListMore').on('hidden.bs.modal', function () {
    $("#DelBtn").css("display", "inline-block");//按钮显示
    $("#ConfirmBtn").css("display", "inline-block");//按钮显示
    $("#modal-body1").css("display", "block");//modal-body1显示
    $("#modal-body2").css("display", "none");//modal-body2隐藏
    $("#TaskListDelBtn").css("display", "none");//确认删除按钮显示
});

//sortable拖动
$(function () {
    $(".m_title").bind('mouseover', function () {
        $(this).css("cursor", "move")
    });
    var list = $("#module_list");
    var TaskItem_list = $("#TaskItem_list");
    var old_order = [];
    //获取原先的顺序列表  
    list.children(".modules").each(function () {
        if (this.getAttribute("data-order") != null) {
            old_order.push(this.getAttribute("data-order"));
        }
    });
    list.sortable({
        opacity: 1, //设置拖动时候的透明度   
        revert: true, //缓冲效果   
        cursor: 'move', //拖动的时候鼠标样式   
        handle: '.m_title',  //可以拖动的部位，模块的标题部分
        placeholder: "modules-highlight",
        axis: "x",
        items: "li:not(.list-disabled)",//指定元素内的哪一个项目应是 sortable
        update: function () {
            var new_id = [];
            list.children(".modules").each(function () {
                new_id.push(this.getAttribute("data-id"));
            });
            var newid = new_id.join(',');
            var oldid = old_order.join(',');
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/UpdateListOrder", //服务端处理程序   
                data: { id: newid, order: oldid },   //id:新的排列对应的ID,order：原排列顺序   
                success: function (msg) {
                    console.log('排序成功');
                }
            });
        }
    });
    list.disableSelection();
    $(".connectedSortable").sortable({
        connectWith: ".connectedSortable",
        delay: 150,//设置延时，以毫秒计，防止点击时不必要的拖拽
        opacity: 1, //设置拖动时候的透明度   
        revert: true, //缓冲效果   
        cursor: 'move', //拖动的时候鼠标样式
        placeholder: "sortable-highlight",
        update: function () {
            var new_id = [];
            var listid = this.id;
            var task = $("#" + this.id + "");
            task.children(".task").each(function () {
                new_id.push(this.getAttribute("data-id"));
            });
            var newid = new_id.join(',');
            var oldid = old_order.join(',');
            $.ajax({
                type: "post",
                url: "Taskboard_WebService.asmx/UpdateItemOrder", //服务端处理程序   
                data: { id: newid, listid: listid },   //id:新的排列对应的ID
                success: function (msg) {
                    console.log('排序成功');
                    console.log(msg.getElementsByTagName("string")[0].childNodes[0].nodeValue);//解析XML
                    window.location.reload();//页面刷新
                }
            });
        }
    }).disableSelection();
});