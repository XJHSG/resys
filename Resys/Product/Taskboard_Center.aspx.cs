using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

public partial class Taskboard_Center : System.Web.UI.Page
{
    public string projectID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
                Response.End();
            }
            else
            {
                ProjectID.Text = Request.QueryString["ID"];
                projectID = ProjectID.Text;
                MyDataBind();
                ExecutorDataBind();
            }
        }

    }

    public void MyDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            int count = 0;
            string sql = "select * from TaskLists where ProjectID=@ProjectID order by Orders";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProjectID", ProjectID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Task_Repeater.DataSource = rd;
            Task_Repeater.DataBind();
            rd.Close();

            cmd.CommandText = "select * from Projects where ID=@ProjectID";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                TaskListNums.Text = Convert.ToString(rd["TaskListNums"]);
                ProjectName.Text = rd["ProjectName"].ToString();
            }
            
            rd.Close();

            cmd.CommandText = "select UserName from Users where ID=@UserID";
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                UserName.Text = rd["UserName"].ToString();
            }

            rd.Close();

            cmd.CommandText = "select count(*) as maxrow from [Members] where ProjectID=@ProjectID";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                count = Convert.ToInt16(rd["maxrow"]);
            }
            rd.Close();


            string s = "";
            string[] UserID = new string[count];
            cmd.CommandText = "select UserID from [Members] where ProjectID=@ProjectID";
            rd = cmd.ExecuteReader();
            for (int j = 0; j < count; j++)
            {
                if (rd.Read())
                {
                    UserID[j] = rd["UserID"].ToString();
                    for (int k = 0; k < count; k++)
                    {
                        s = string.Join(",", UserID);
                        IDs.Text = s;
                    }
                }
            }
            rd.Close();

            //Util.ShowMessage(ProjectID.Text, "");
            cmd.CommandText = "select top " + count + " UserName,Email,Avatar from Users where ID in  (" + IDs.Text + ") order by ID desc";
            rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
            conn.Close();

        }
    }

    private void ExecutorDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from Members";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            ExecutorDDL.DataSource = rd;
            ExecutorDDL.DataTextField = "MemberName";
            ExecutorDDL.DataValueField = "UserID";
            ExecutorDDL.DataBind();
            rd.Close();
            string UserID = Session["UserID"].ToString();
            if (ExecutorDDL.Items.FindByText(UserID) != null)
            {
                ExecutorDDL.ClearSelection();
                ExecutorDDL.Items.FindByText(UserID).Selected = true;
            }
            conn.Close();

        }
    }
    private void SubExecutorDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Select * from Members";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            n_subitemE.DataSource = rd;
            n_subitemE.DataTextField = "MemberName";
            n_subitemE.DataValueField = "UserID";
            n_subitemE.DataBind();
            rd.Close();
            string UserID = Session["UserID"].ToString();
            if (n_subitemE.Items.FindByText(UserID) != null)
            {
                n_subitemE.ClearSelection();
                n_subitemE.Items.FindByText(UserID).Selected = true;
            }
            conn.Close();

        }
    }

    protected void AddMember_Repeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

    }


    protected void Task_Repeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hf = (HiddenField)e.Item.FindControl("hfid");
            Repeater rpchild = (Repeater)e.Item.FindControl("TaskItem_Repeater");
            if (hf != null || hf.ToString() != "")
            {
                int id = Convert.ToInt32(hf.Value);
                using (SqlConnection conn = new DB().GetConnection())
                {
                    string sql = "select * from TaskItems where TaskListID=@TaskListID order by Orders";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TaskListID", id);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    rpchild.DataSource = rd;
                    rpchild.DataBind();
                    rd.Close();
                    conn.Close();
                }
            }
        }
    }

    protected void TaskItem_Repeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        string finished = "";
        string executorID = "";
        string avatarUrl = "";
        string strsubItemNums = "";
        string strlinkNums = "";
        string strupNums = "";
        string strcommentNums = "";
        string m_priority = "";
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hfitemid = (HiddenField)e.Item.FindControl("hfitemid");//获取任务的ID
            HtmlGenericControl taskItem = (HtmlGenericControl)e.Item.FindControl("taskItem");//获取需要修改样式的html标签<li>的ID
            HtmlGenericControl ok = (HtmlGenericControl)e.Item.FindControl("ok");//获取需要修改样式的html标签<span>的ID
            HtmlGenericControl avatar = (HtmlGenericControl)e.Item.FindControl("avatar");//获取需要修改样式的html标签<div>的ID
            HtmlGenericControl subItemNums = (HtmlGenericControl)e.Item.FindControl("subItemNums");
            HtmlGenericControl linkNums = (HtmlGenericControl)e.Item.FindControl("linkNums");
            HtmlGenericControl upNums = (HtmlGenericControl)e.Item.FindControl("upNums");
            HtmlGenericControl commentNums = (HtmlGenericControl)e.Item.FindControl("commentNums");
            HtmlGenericControl priority = (HtmlGenericControl)e.Item.FindControl("priority");
            if (hfitemid != null || hfitemid.ToString() != "")
            {
                int id = Convert.ToInt32(hfitemid.Value);
                using (SqlConnection conn = new DB().GetConnection())
                {
                    string sql = "select * from TaskItems where ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        finished = rd["IsFinished"].ToString();
                        executorID = rd["ExecutorID"].ToString();
                        strsubItemNums = rd["SubItemNums"].ToString();
                        strlinkNums = rd["LinkNums"].ToString();
                        strupNums = rd["UpNums"].ToString();
                        strcommentNums = rd["CommentNums"].ToString();
                        m_priority = rd["Priority"].ToString();
                    }
                    if (finished == "True")
                    {
                        //任务更新IsFinished
                        taskItem.Attributes.Add("class", "task list-disabled done");
                        ok.Attributes.Add("class", "glyphicon glyphicon-ok mvisible");
                    }
                    if (strsubItemNums == "")
                    {
                        subItemNums.Style["display"] = "none";
                    }
                    if (strlinkNums == "")
                    {
                        linkNums.Style["display"] = "none";
                    }
                    if (strupNums == "")
                    {
                        upNums.Style["display"] = "none";
                    }
                    if (strcommentNums == "")
                    {
                        commentNums.Style["display"] = "none";
                    }
                    switch (m_priority)
                    {
                        case "0":
                            priority.Style["background-color"] = "#A6A6A6";
                            break;
                        case "1":
                            priority.Style["background-color"] = "#FFAF38";
                            break;
                        case "2":
                            priority.Style["background-color"] = "#FF4F3E";
                            break;
                        default:
                            priority.Style["opacity"] = "0";
                            break;
                    }
                    rd.Close();
                    //获取头像
                    sql = "select * from Users where ID=@ExecutorID";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ExecutorID", executorID);
                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        avatarUrl = rd["Avatar"].ToString();
                        avatar.Style["background-image"] = "url(" + avatarUrl + ")";
                    }
                    conn.Close();
                }
            }
        }
    }

    protected void TaskCreatorBtn_Click(object sender, EventArgs e)
    {
        if (ItemText.Text != "")
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                StringBuilder sb = new StringBuilder("insert into TaskItems(ItemText,TaskListID,ExecutorID,Priority,Orders,CreatorID)");
                sb.Append(" values ( @ItemText,@TaskListID,@ExecutorID,@Priority,@Orders,@CreatorID) ");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@ItemText", ItemText.Text);
                cmd.Parameters.AddWithValue("@TaskListID", TaskListID.Value.ToString());
                cmd.Parameters.AddWithValue("@ExecutorID", ExecutorDDL.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Priority", PriorityDDL.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Orders", ItemOrders.Value.ToString());
                cmd.Parameters.AddWithValue("@CreatorID", Session["UserID"].ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Update TaskLists set TotalItemNums=TotalItemNums+1 where ID=@TaskListID";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

                Response.Redirect(Request.Url.ToString()); //刷新页面
            }
        }
        else
        {
            Response.Write("<script>alert('任务名不能为空！');</script>");
        }
    }

    protected void TaskListCreatorBtn_Click(object sender, EventArgs e)
    {
        if (TaskListText.Text != "")
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                StringBuilder sb = new StringBuilder("insert into TaskLists(TaskListText,ProjectID,TotalItemNums,FinishedItemNums,Orders,IsTemplate)");
                sb.Append(" values ( @TaskListText,@ProjectID,@TotalItemNums,@FinishedItemNums,@Orders,@IsTemplate) ");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@TaskListText", TaskListText.Text);
                cmd.Parameters.AddWithValue("@ProjectID", ProjectID.Text);
                cmd.Parameters.AddWithValue("@TotalItemNums", 0);
                cmd.Parameters.AddWithValue("@FinishedItemNums", 0);
                cmd.Parameters.AddWithValue("@Orders", Convert.ToInt16(TaskListNums.Text) + 1);
                cmd.Parameters.AddWithValue("@IsTemplate", 0);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Update Projects set TaskListNums=TaskListNums+1 where ID=@ProjectID";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

                Response.Redirect(Request.Url.ToString());//刷新页面
            }
        }

    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        SubExecutorDataBind();
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "SELECT   Participators.*, Users.ID AS Expr1, Users.* FROM Participators INNER JOIN Users ON Participators.UserID = Users.ID where Participators.TaskItemID=@TaskItemID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TaskItemID", ita_hidf.Value);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            //Repeater1.DataSource = rd;
            //Repeater1.DataBind();
            rd.Close();
            //cmd.CommandText = "SELECT TaskItems.*, Users.* FROM  Users INNER JOIN TaskItems ON Users.ID = TaskItems.ExecutorID where TaskItems.ID=@TaskItemID";
            //rd = cmd.ExecuteReader();
            //if (rd.Read())
            //{
            //    Image1.ImageUrl = rd["Avatar"].ToString();
            //}
            //rd.Close();
            cmd.CommandText = "SELECT * FROM TaskItems where ID=@TaskItemID";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Label5.Text = rd["ItemText"].ToString();
            }
            rd.Close();
            cmd.CommandText = "SELECT Links.*, Files.*, Links.ID AS Expr1, Files.ID AS Expr2  FROM  Links INNER JOIN  Files ON Links.FileID = Files.ID where Links.RefID=@TaskItemID and Links.RefTable='TaskItem'";       
            rd = cmd.ExecuteReader();
            Repeater3.DataSource = rd;
            Repeater3.DataBind();
            rd.Close();
            cmd.CommandText = "SELECT   Users.*, TaskSubItems.*, TaskSubItems.ID AS Expr1 FROM    TaskSubItems INNER JOIN  Users ON TaskSubItems.ExecutorID = Users.ID where TaskSubItems.ParentID=@TaskItemID order by Expr1";
            rd = cmd.ExecuteReader();
            Repeater4.DataSource = rd;
            Repeater4.DataBind();
            rd.Close();
            conn.Close();
        }
    }

}