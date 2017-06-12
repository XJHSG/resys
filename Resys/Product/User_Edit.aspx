<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="User_Edit.aspx.cs" Inherits="Product_User_Edit" %>

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
      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    	<script src="js/moment-with-locales.js"></script>
   <script src="js/bootstrap-datetimepicker.js"></script>
      <script src="js/cropper.js"></script>
		<script src="js/sitelogo.js"></script>
    <link href="css/cropper.min.css" rel="stylesheet"/>
		<link href="css/sitelogo.css" rel="stylesheet"/>
     <link href="css/bootstrap-datetimepicker.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css"/>
    <div class="container">
    <div class="row">
         <div class="col-lg-3 col-sm-3 col-xs-12">
    <div class="panel panel-default">
  <div class="panel-body">
  <div class="media">
  <div class="media-left">
      
    <a href="#">
        <asp:Image ID="Avatar_SImg" class="img-circle" runat="server" width="60"/>
  
    </a>
  </div>
  <div class="media-body">
      <h4><asp:Label ID="UserName" runat="server" class="media-heading"></asp:Label></h4>
      <small><asp:Label ID="Email" runat="server"  ></asp:Label></small>
  </div>
</div>
  </div>
       </div>

      <div class="panel panel-default">
  <div class="panel-body">
   <nav class="bs-docs-sidebar hidden-print hidden-xs hidden-sm">
            <ul class="nav bs-docs-sidenav">
                <li>
                    <a id="a1" href="#">个人信息</a>

                </li>
                 <li>
                    <a id="a2" href="#">账号密码</a>

                </li>
                </ul>
              </nav>
  </div>
       </div>
       </div>
       

<div class="col-lg-9 col-sm-9 col-xs-12">
    <div class="panel panel-default userinfo">
              
  <div class="panel-body">
  <div class="panel-heading">个人信息</div>
    <div class="row">
  <div class="col-sm-2  text-muted">
     头像
  </div>

      <div class="col-sm-6">
          <asp:Image ID="Avatar_Image" class="img-circle" runat="server" width="90"/>
          
    <button type="button" class="btn btn-primary"  data-toggle="modal" data-target="#avatar-modal" style="margin-left:20px">上传新头像</button>
<div class="user_pic" style="margin: 10px;">
			
		</div>
          <div class="modal fade" id="avatar-modal" aria-hidden="true" aria-labelledby="avatar-modal-label" role="dialog" tabindex="-1">
			<div class="modal-dialog modal-lg">
				<div class="modal-content">
					<!--<form class="avatar-form" action="upload-logo.php" enctype="multipart/form-data" method="post">-->
					<div class="avatar-form">
						<div class="modal-header">
							<button class="close" data-dismiss="modal" type="button">&times;</button>
							<h4 class="modal-title" id="avatar-modal-label">上传图片</h4>
						</div>
						<div class="modal-body">
							<div class="avatar-body">
								<div class="avatar-upload">
									<input class="avatar-src" name="avatar_src" type="hidden"/>
									<input class="avatar-data" name="avatar_data" type="hidden"/>
									<label for="avatarInput" style="line-height: 35px;">图片上传</label>
									<button class="btn btn-danger"  type="button" style="height: 35px;" onclick="$('input[id=avatarInput]').click();">请选择图片</button>
									<span id="avatar-name"></span>
									<input class="avatar-input hide" id="avatarInput" name="avatar_file" type="file"/></div>
								<div class="row">
									<div class="col-md-9">
										<div class="avatar-wrapper"></div>
									</div>
									<div class="col-md-3">
										<div class="avatar-preview preview-lg" id="imageHead"></div>
										<!--<div class="avatar-preview preview-md"></div>
								<div class="avatar-preview preview-sm"></div>-->
									</div>
								</div>
								<div class="row avatar-btns">
									<div class="col-md-4">
										<div class="btn-group">
											<button class="btn btn-danger fa fa-undo" data-method="rotate" data-option="-90" type="button" title="Rotate -90 degrees"> 向左旋转</button>
										</div>
										<div class="btn-group">
											<button class="btn  btn-danger fa fa-repeat" data-method="rotate" data-option="90" type="button" title="Rotate 90 degrees"> 向右旋转</button>
										</div>
									</div>
									<div class="col-md-5" style="text-align: right;">								
										<button class="btn btn-danger fa fa-arrows" data-method="setDragMode" data-option="move" type="button" title="移动">
							            <span class="docs-tooltip" data-toggle="tooltip" title="" data-original-title="$().cropper(&quot;setDragMode&quot;, &quot;move&quot;)">
							            </span>
							          </button>
							          <button type="button" class="btn btn-danger fa fa-search-plus" data-method="zoom" data-option="0.1" title="放大图片">
							            <span class="docs-tooltip" data-toggle="tooltip" title="" data-original-title="$().cropper(&quot;zoom&quot;, 0.1)">
							              <!--<span class="fa fa-search-plus"></span>-->
							            </span>
							          </button>
							          <button type="button" class="btn btn-danger fa fa-search-minus" data-method="zoom" data-option="-0.1" title="缩小图片">
							            <span class="docs-tooltip" data-toggle="tooltip" title="" data-original-title="$().cropper(&quot;zoom&quot;, -0.1)">
							              <!--<span class="fa fa-search-minus"></span>-->
							            </span>
							          </button>
							          <button type="button" class="btn btn-danger fa fa-refresh" data-method="reset" title="重置图片">
								            <span class="docs-tooltip" data-toggle="tooltip" title="" data-original-title="$().cropper(&quot;reset&quot;)" aria-describedby="tooltip866214"/>
								       </button>
							        </div>
									<div class="col-md-3">
										
                                        <asp:Button ID="Save_Image" runat="server" class="btn btn-danger btn-block avatar-save fa fa-save" type="button" data-dismiss="modal" Text="保存修改"  />
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
      </div>
    </div>   
<br />
       <div class="row">
       <div class="col-sm-2  text-muted">
     用户名
  </div>
            <div class="col-sm-6">
          
         <asp:Label  ID="User_Name" runat="server" class="form-control"></asp:Label>
                </div>
          
   </div>
      <br />
       <div class="row">
       <div class="col-sm-2  text-muted">
     联系电话
  </div>
            <div class="col-sm-6">
            <asp:TextBox ID="Tel" type="text" class="form-control" runat="server"></asp:TextBox>
                </div>
           </div>
         <br />
       <div class="row">
       <div class="col-sm-2  text-muted">
     备注
  </div>
            <div class="col-sm-6">
           <asp:TextBox ID="Signature" type="text" class="form-control" runat="server"></asp:TextBox>
                </div>
           </div>
        <br />
       <div class="row">
       <div class="col-sm-2  text-muted">
     生日
  </div>
          <div class='col-sm-6'>
            <div class="form-group">
                <div class='input-group date' id='datetimepicker1'>
                    <asp:TextBox ID="Birth_Date" type='text' class="form-control" runat="server"></asp:TextBox>
                  
                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>
        </div>
            <script type="text/javascript">
                $(function () {
                    $('#datetimepicker1').datetimepicker();
                });
        </script>
           </div>

       <div class="row">
            <div class='col-sm-6' style="float:right">
      <asp:Button ID="Save_Info" runat="server" Text="保存信息" type="button" class="btn btn-info"  OnClick="Save_Info_Click"   />
                   
                </div>

        </div>

     

  </div>
         
     
</div>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
    <div class="panel panel-default  passwordinfo ">
        <div class="panel-body" >
  <div class="panel-heading">修改密码</div>
           <div class="row">
            <div class="col-sm-6">
                <asp:TextBox ID="Password1"  TextMode="Password" class="form-control" runat="server" placeholder="旧密码"></asp:TextBox>
             
             </div>
                  </div>
            <br />
             <div class="row">
             <div class="col-sm-6">
                  <asp:TextBox ID="Password2"  TextMode="Password" onblur="checkpsd1()" class="form-control" runat="server" required="required" placeholder="新密码"></asp:TextBox>
               <span class="text-center tips" id="divpassword1"></span>
             </div>
                 </div>
    
       <br />
             <div class="row">
             <div class="col-sm-6">
                 <asp:TextBox ID="Password3"  TextMode="Password" onblur="checkpsd2()" class="form-control" runat="server"  required="required" placeholder="确认新密码"></asp:TextBox>
                   <span  id="divpassword2"></span>
             </div>
          </div>

            <div class="row">
               <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                      <br />
                 <div class="col-sm-6">
               <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
             Text="查看密码" Visible="true"   oncheckedchanged="CheckBox1_CheckedChanged"/>  
                 <input id="Hidden2" type="hidden" runat="server" />
              <asp:Button ID="Psd_Upd" runat="server" class="btn btn-info pull-right" Text="确认修改" OnClick="Psd_Upd_Click" />
                 </div>
            </div>
        
            </div>
      </div>
       </div>

                                          

                                               </ContentTemplate>
     </asp:UpdatePanel>
    </div>
        </div>

    <script src="js/html2canvas.min.js" type="text/javascript" charset="utf-8"></script>
		<script type="text/javascript">
		    $(document).ready(function () {
		        $('.passwordinfo').css("display", "none");
		    })

		    $('#a1').click(function () {
		        $('.userinfo').css("display", "inherit");
		        $('.passwordinfo').css("display", "none");
		    })

		    $('#a2').click(function () {
		        $('.userinfo').css("display", "none");
		        $('.passwordinfo').css("display", "inherit");
		        
		    
		   
		    })
		//做个下简易的验证  大小 格式 
			$('#avatarInput').on('change', function(e) {
				var filemaxsize = 1024 * 3;//5M
				var target = $(e.target);
				var Size = target[0].files[0].size / 1024;
				if(Size > filemaxsize) {
					alert('图片过大，请重新选择!');
					$(".avatar-wrapper").childre().remove;
					return false;
				}
				if(!this.files[0].type.match(/image.*/)) {
					alert('请选择正确的图片!')
				} else {
					var filename = document.querySelector("#avatar-name");
					var texts = document.querySelector("#avatarInput").value;
					var teststr = texts; //你这里的路径写错了
					testend = teststr.match(/[^\\]+\.[^\(]+/i); //直接完整文件名的
					filename.innerHTML = testend;
				}
			
			});
			
			<%--$(".btn-info").on("click", function () {

			    var something = <%=getSometing()%>; 
			    if(something == 1){
			        myModal.innerHTML = '<font class="tips_false">长度错误</font>';
			     
			    }
			})--%>


			$(".avatar-save").on("click", function () {
			   
				var img_lg = document.getElementById('imageHead');
				// 截图小的显示框内的内容
				html2canvas(img_lg, {
					allowTaint: true,
					taintTest: false,
					onrendered: function(canvas) {
						canvas.id = "mycanvas";
						//生成base64图片数据
						var dataUrl = canvas.toDataURL("image/jpeg");
						var newImg = document.createElement("img");
						newImg.src = dataUrl; 
						imagesAjax(dataUrl);
					}
				});
			})
			
			function imagesAjax(src) {
				var data = {};
				data.img = src;
				data.jid = $('#jid').val();
         	$.ajax({
				    type: 'post',
				    url: 'Avatar_Upload.ashx',
         	    //data: data,
				    data: { img:src, id:$('#jid').val() },
                    success: function (ex) {
					    console.log('修改成功');
					    window.location.reload();
					    
					}
				});
			}

			function checkpsd1() {
			    psd1 = document.getElementById("ContentPlaceHolder1_Password2").value;
			    var flagZM = false;

			    var flagSZ = false;

			    var flagQT = false;

			    if (psd1.length < 6 || psd1.length > 12) {

			        divpassword1.innerHTML = '<font class="tips_false">长度错误</font>';

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
			        } else {
			            divpassword1.innerHTML = '<font class="tips_true">输入正确</font>';
			        }
			    }
			    //alert("hello");
			}

			function checkpsd2() {
			    psd1 = document.getElementById("ContentPlaceHolder1_Password2").value;
			    psd2 = document.getElementById("ContentPlaceHolder1_Password3").value;
			    if (psd1 == psd2) {
			        document.getElementById("ContentPlaceHolder1_Hidden2").value = 1;
			        
			    }
			    else
			    {
			        divpassword2.innerHTML = '<font class="tips_false">两次密码输入不一致</font>';
			    }
			}
		</script>
      </div>
</asp:Content>

