using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;


public partial class User_Center : System.Web.UI.Page
{
    public string random_id = "";
    public string project_id = "";
    public string UserID = "7";
    public string NowProject_ID = "";
    public string UserName = "";
    public string UserAvatar = "";
    public string UserRoleID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Users();
        MyInit();
    }
    protected void Users() 
    {
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * From Users where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", UserID);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                UserName = rd["UserName"].ToString();
                UserAvatar = rd["Avatar"].ToString();
                UserRoleID = rd["UserRoleID"].ToString();
            }
        }
    }
    protected void MyInit() 
    {
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Projects.ProjectName, Projects.CDT,Projects.ID As pid,Members.ID As mid, Projects.OwnedDT,Projects.TotalItemNums,Projects.FinishedItemNums, Users.UserName, Users.Avatar, Members.IsStarProject, Members.IsArchied, Members.UserID FROM Projects LEFT OUTER JOIN Users ON Projects.OwnerID = Users.ID LEFT JOIN Members ON Projects.ID = Members.ProjectID where Projects.IsArchied = 0 and Projects.IsTemplate=0 and Projects.CreatorID=@UserID and Projects.IsDeleted=0";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater1.DataSource = rd;
            Repeater1.DataBind();
            rd.Close();
            cmd.CommandText = "select * from Projects where IsArchied = 1 and IsTemplate=0 Order By ID Desc";
            SqlDataReader rd1 = cmd.ExecuteReader();
            Repeater2.DataSource = rd1;
            Repeater2.DataBind();
            rd1.Close();
            cmd.CommandText = "SELECT Projects.ID,Projects.ProjectName,Projects.Description,Projects.Picture, Users.UserName,Projects.TaskListNums,Projects.CDT FROM Projects INNER JOIN Users ON Projects.CreatorID =Users.ID where Projects.IsTemplate=1 Order By ID Desc";
            SqlDataReader rd2 = cmd.ExecuteReader();
            Repeater3.DataSource = rd2;
            Repeater3.DataBind();
            rd2.Close();
        }
    }
    protected void Members() 
    {
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * From Members where ProjectID=@ProjectID";
            cmd.Parameters.AddWithValue("@ProjectID", NowProject_ID);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater4.DataSource = rd;
            Repeater4.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.baidu.com");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        CreatProject();
        MyInit();
        //Response.Redirect("http://www.baidu.com");
    }

    protected void CreatProject()
    {
        random_id = Guid.NewGuid().ToString();
        using (SqlConnection conn1 = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into Projects(ProjectName,ProjectType,Description,CreatorID,OwnerID,IsArchied,IsFinished,TaskListNums,TotalItemNums,FinishedItemNums,MemberNums,CDT,OwnedDT,GUID,IsTemplate,UnassignedTaskNums,IsLocked,IsDeleted)");
            sb.Append(" values ( @ProjectName,@ProjectType,@Description,@CreatorID,@OwnerID,@IsArchied,@IsFinished,@TaskListNums,@TotalItemNums,@FinishedItemNums,@MemberNums,@CDT,@OwnedDT,@GUID,@IsTemplate,@UnassignedTaskNums,@IsLocked,@IsDeleted) ");
            SqlCommand cmd1 = new SqlCommand(sb.ToString(), conn1);
            cmd1.Parameters.AddWithValue("@ProjectName", TextBox1.Text);
            cmd1.Parameters.AddWithValue("@ProjectType", "private");
            cmd1.Parameters.AddWithValue("@Description", TextBox2.Text.Length>50?TextBox2.Text.Substring(0,50):TextBox2.Text);
            cmd1.Parameters.AddWithValue("@CreatorID", UserID);
            cmd1.Parameters.AddWithValue("@OwnerID", UserID);
            cmd1.Parameters.AddWithValue("@IsArchied", 0);
            cmd1.Parameters.AddWithValue("@IsFinished", 0);
            cmd1.Parameters.AddWithValue("@TaskListNums", 3);
            cmd1.Parameters.AddWithValue("@TotalItemNums", 0);
            cmd1.Parameters.AddWithValue("@FinishedItemNums", 0);
            cmd1.Parameters.AddWithValue("@MemberNums", 1);
            cmd1.Parameters.AddWithValue("@Validation", "需要申请");
            cmd1.Parameters.AddWithValue("@CDT",DateTime.Now.ToString("yyyy-MM-dd"));
            cmd1.Parameters.AddWithValue("@OwnedDT", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd1.Parameters.AddWithValue("@IsEqualProject", 0);
            cmd1.Parameters.AddWithValue("@Jurisdiction", 0);
            cmd1.Parameters.AddWithValue("@GUID", random_id);
            cmd1.Parameters.AddWithValue("@IsTemplate", 0);
            cmd1.Parameters.AddWithValue("@UnassignedTaskNums", 0);
            cmd1.Parameters.AddWithValue("@IsLocked", 0);
            cmd1.Parameters.AddWithValue("@IsDeleted", 0);
            conn1.Open();
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            cmd1.CommandText = "select * from projects where GUID=@GUID";
            SqlDataReader rd = cmd1.ExecuteReader();
            while (rd.Read()) 
            {
                project_id = rd["ID"].ToString();
            }
            cmd1.Dispose();
            rd.Close();
            cmd1.CommandText = "insert into TaskLists(IsTemplate,TaskListText,Orders,TotalItemNums,FinishedItemNums,ProjectID)values(0,'未开始',1,0,0,@ID)";
            cmd1.Parameters.AddWithValue("@ID", project_id);
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            cmd1.CommandText = "insert into TaskLists(IsTemplate,TaskListText,Orders,TotalItemNums,FinishedItemNums,ProjectID)values(0,'进行中',2,0,0,@ID)";
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            cmd1.CommandText = "insert into TaskLists(IsTemplate,TaskListText,Orders,TotalItemNums,FinishedItemNums,ProjectID)values(0,'已完成',3,0,0,@ID)";
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            cmd1.CommandText = "insert into Members(ProjectRoleID,UserID,MemberName,ProjectID,IsStarProject,IsArchied,CDT)values('1',@OwnerID,@MemberName,@ID,0,0,@CDT)";
            cmd1.Parameters.AddWithValue("@MemberName", UserName);
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            conn1.Close();

        }

    }
    protected void PassByValue(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        NowID1.Text = id;
        NowProject_ID = id;
        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Projects where ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while(rd.Read())
            {
                TextBox3.Text = rd["ProjectName"].ToString();
                TextBox5.Text = rd["Description"].ToString();
            }
            rd.Close();
        }
        Members();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Projects set ProjectName=@ProjectName,Description=@Description where ID=@ID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@ProjectName", TextBox3.Text.ToString());
            cmd1.Parameters.AddWithValue("@Description", TextBox5.Text.ToString());
            cmd1.Parameters.AddWithValue("@ID", NowID1.Text.ToString());
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        MyInit();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Projects set IsDeleted=@IsDeleted where ID=@ID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@IsDeleted", 1);
            cmd1.Parameters.AddWithValue("@ID", NowID1.Text.ToString());
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        MyInit();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Projects set IsArchied=@IsArchied where ID=@ID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@IsArchied", 1);
            cmd1.Parameters.AddWithValue("@ID", NowID1.Text.ToString());
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        MyInit();
    }
    protected void Archied(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Projects set IsArchied=@IsArchied where ID=@ID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@IsArchied", 1);
            cmd1.Parameters.AddWithValue("@ID", id);
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        MyInit();
    }
    protected void OpenProject(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb1 = new StringBuilder("Update Projects set IsArchied=@IsArchied where ID=@ID ");
            SqlCommand cmd1 = new SqlCommand(sb1.ToString(), conn);
            cmd1.Parameters.AddWithValue("@IsArchied", 0);
            cmd1.Parameters.AddWithValue("@ID", id);
            conn.Open();
            cmd1.ExecuteNonQuery();
        }
        MyInit();
    }
    protected void AddMember(object sender, CommandEventArgs e) 
    {
        string id = e.CommandArgument.ToString();

    }
    protected void Start(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string is_start = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Members where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())is_start = rd["IsStarProject"].ToString();
            rd.Close();
            cmd.CommandText = "Update Members set IsStarProject=@IsStarProject where ID=@ID";
            cmd.Parameters.AddWithValue("@IsStarProject",is_start=="True"?"0":"1");
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        MyInit();
    }
   
    protected void Exchange_Click(object sender, EventArgs e)
    {
        Members();
    }

    protected void Button12_Click(object sender,EventArgs e)
    {
        string values = this.hidden1.Value;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from TaskLists where ProjectID=@ID order by Orders desc";
            cmd.Parameters.AddWithValue("@ID", values);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Repeater5.DataSource = rd;
            Repeater5.DataBind();
            rd.Close();
        }
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        string id = this.hidden1.Value;
        string ProjectName = this.hidden2.Value;
        string Description = this.hidden3.Value;
        random_id = Guid.NewGuid().ToString();
        string ProjectType = "", CreatorID = "", OwnerID = "", IsArchied = "", IsFinished = "", TaskListNums = "", TotalItemNums = "", FinishedItemNums = "", MemberNums = "", CDT = "", OwnedDT = "", GUID = "", IsTemplate = "", UnassignedTaskNums = "", IsLocked = "", IsDeleted = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Projects where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", id);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) { ProjectType = "prative"; CreatorID = UserID; OwnerID = UserID; IsArchied = "0"; IsFinished = "0"; TaskListNums = rd["TaskListNums"].ToString(); TotalItemNums = rd["TaskListNums"].ToString(); FinishedItemNums = rd["TaskListNums"].ToString(); MemberNums = "1"; UnassignedTaskNums = "0"; }
            rd.Close();
            cmd.CommandText = "insert into Projects(ProjectName,ProjectType,Description,CreatorID,OwnerID,IsArchied,IsFinished,TaskListNums,TotalItemNums,FinishedItemNums,MemberNums,CDT,OwnedDT,GUID,IsTemplate,UnassignedTaskNums,IsLocked,IsDeleted)values ( @ProjectName,@ProjectType,@Description,@CreatorID,@OwnerID,@IsArchied,@IsFinished,@TaskListNums,@TotalItemNums,@FinishedItemNums,@MemberNums,@CDT,@OwnedDT,@GUID,@IsTemplate,@UnassignedTaskNums,@IsLocked,@IsDeleted)";
            cmd.Parameters.AddWithValue("@ProjectName", ProjectName);
            cmd.Parameters.AddWithValue("@ProjectType", "private");
            cmd.Parameters.AddWithValue("@Description", Description.Length > 50 ? Description.Substring(0, 50) : Description);
            cmd.Parameters.AddWithValue("@CreatorID", UserID);
            cmd.Parameters.AddWithValue("@OwnerID", UserID);
            cmd.Parameters.AddWithValue("@IsArchied", 0);
            cmd.Parameters.AddWithValue("@IsFinished", 0);
            cmd.Parameters.AddWithValue("@TaskListNums", TaskListNums);
            cmd.Parameters.AddWithValue("@TotalItemNums", 0);
            cmd.Parameters.AddWithValue("@FinishedItemNums", 0);
            cmd.Parameters.AddWithValue("@MemberNums", 1);
            cmd.Parameters.AddWithValue("@Validation", "需要申请");
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@OwnedDT", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@IsEqualProject", 0);
            cmd.Parameters.AddWithValue("@Jurisdiction", 0);
            cmd.Parameters.AddWithValue("@GUID", random_id);
            cmd.Parameters.AddWithValue("@IsTemplate", 0);
            cmd.Parameters.AddWithValue("@UnassignedTaskNums", 0);
            cmd.Parameters.AddWithValue("@IsLocked", 0);
            cmd.Parameters.AddWithValue("@IsDeleted", 0);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd.CommandText = "select * from projects where GUID=@GUID";
            SqlDataReader rd1 = cmd.ExecuteReader();
            while (rd1.Read())
            {
                project_id = rd1["ID"].ToString();
            }
            cmd.Dispose();
            rd1.Close();
            cmd.CommandText = "insert into TaskLists(IsTemplate,TaskListText,Orders,TotalItemNums,FinishedItemNums,ProjectID)values(0,'列表1',1,0,0,@ppID)";
            cmd.Parameters.AddWithValue("@ppID", project_id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.CommandText = "insert into TaskLists(IsTemplate,TaskListText,Orders,TotalItemNums,FinishedItemNums,ProjectID)values(0,'列表2',2,0,0,@ppID)";
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd.CommandText = "insert into Members(ProjectRoleID,UserID,MemberName,ProjectID,IsStarProject,IsArchied,CDT)values('1',@OwnerID,@MemberName,@ID,0,0,@CDT)";
            cmd.Parameters.AddWithValue("@MemberName", UserName);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }
        Response.Write("<Script language='JavaScript'>winodw.opener.location.reload();</Script>");
    }
}