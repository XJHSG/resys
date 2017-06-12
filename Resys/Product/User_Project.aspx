<%@ Page Title="" Language="C#" MasterPageFile="~/RESYS.master" AutoEventWireup="true" CodeFile="User_Project.aspx.cs" Inherits="User_Project" %>

<%@ Register Src="~/Product/FriendSlider.ascx" TagPrefix="uc1" TagName="FriendSlider" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
     <div class="row">
           <div class="col-md-2" >
           <uc1:FriendSlider runat="server" ID="FriendSlider" />
               </div>
     <div class="col-md-3" role="complementary">
         <div class="panel panel-default">
      <div class="panel-body">
        
    <div class="input-group">
      <input type="text" class="form-control" placeholder="Search for..."/>
      <span class="input-group-btn">
        <button class="btn btn-default" type="button">Go!</button>
      </span>
    </div><!-- /input-group -->

    <br />
          <button class="btn btn-default" type="submit">添加新分组</button>
          <p />
    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
  <div class="panel panel-default">
    <div class="panel-heading" role="tab" id="headingOne">
      <h4 class="panel-title">
        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          分组一
        </a>
      </h4>
    </div>
    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
      <div class="panel-body">
        <div class="media">
  <div class="media-left">
    <a href="#">
      <img class="media-object" src="Images/1.png" alt="..."/>
    </a>
  </div>
  <div class="media-body">
    <h4 class="media-heading">鸣人</h4>
    <span>...</span>
  </div>
</div>
          <div class="media">
  <div class="media-left">
    <a href="#">
      <img class="media-object" src="Images/1.png" alt="..."/>
    </a>
  </div>
  <div class="media-body">
    <h4 class="media-heading">鸣人</h4>
    <span>...</span>
  </div>
</div>
      </div>
    </div>
  </div>
  
  
</div>
  
</div><!-- /.row -->
      </div>
            
         </div>
         </div>
</asp:Content>

