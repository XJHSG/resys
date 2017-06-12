using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Text;


/// <summary>
/// Taskboard_WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class Taskboard_WebService : System.Web.Services.WebService {

    public Taskboard_WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    /// <summary>  
    /// 更新IsFinished
    /// </summary>
    /// <param name="id"></param>  
    [WebMethod]
    public string UpdateFinished(string id)
    {
        int flag = 0;
        string finished = "";
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
            }
            rd.Close();
            if (finished == "True")
            {
                sql = "update TaskItems set IsFinished=0 where ID=@ID";
            }
            else
            {
                sql = "update TaskItems set IsFinished=1 where ID=@ID";
            }
            flag = 1;
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }
        if (flag == 1)
        {
            return "Success!";
        }
        else
        {
            return "Failure!";
        }

    }

    /// <summary>  
    /// 重新更新顺序  
    /// </summary>  
    /// <param name="id"></param>  
    /// <param name="order"></param>  
    [WebMethod]
    public string UpdateListOrder(string id, string order)
    {

        string[] deptIds = id.Split(',');
        string[] orders = order.Split(',');
        int flag = 0;
        string sql = "";
        for (int i = 0; i < deptIds.Length; i++)
        {
            for (int j = 0; j < orders.Length; j++)
            {
                if (i == j)
                {
                    sql += "update TaskLists set Orders=" + orders[j] + " where ID='" + deptIds[i] + "';";
                    flag = 1;
                }
            }
        }
        if (flag == 1)
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            return "Success!";
        }
        else
        {
            return "Failure!";
        }

    }

    /// <summary>  
    /// 重新更新顺序  
    /// </summary>  
    /// <param name="id"></param>  
    /// <param name="listid"></param>  
    [WebMethod]
    public string UpdateItemOrder(string id, string listid)
    {
        string[] deptIds = id.Split(',');
        int flag = 0;
        string sql = "";
        string result = "";
        for (int i = 0; i < deptIds.Length; i++)
        {
            sql += "update TaskItems set TaskListID=" + listid + " ,Orders=" + i + " where ID='" + deptIds[i] + "';";
            flag = 1;
        }
        if (flag == 1)
        {
            if (id.Length == 0)
            {
                sql += "update TaskLists set TotalItemNums=" + id.Length + " where ID='" + listid + "';";
                result = id.Length.ToString();
            }
            else
            {
                sql += "update TaskLists set TotalItemNums=" + deptIds.Length + " where ID='" + listid + "';";
                result = deptIds.Length.ToString();
            }
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            return result;
        }
        else
        {
            return "Failure!";
        }

    }

    /// <summary>  
    /// 删除TaskList
    /// </summary>  
    /// <param name="listid"></param>  
    /// <param name="listid"></param>  
    [WebMethod]
    public int TaskListDel(string listid, string projectid)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Delete from TaskLists where ID=@TaskListEditID;Update Projects set TaskListNums=TaskListNums-1 where ID=@ProjectID;");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@TaskListEditID", listid);
            cmd.Parameters.AddWithValue("@ProjectID", projectid);
            conn.Open();
            i=cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 2)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>  
    /// 更改TaskList名称  
    /// </summary>  
    /// <param name="listid"></param>  
    /// <param name="listid"></param>  
    [WebMethod]
    public string TaskListUpdateName(string listid, string newname)
    {
        int i=0;
        if (newname != " " && newname != "")
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                StringBuilder sb = new StringBuilder("Update TaskLists set TaskListText=@TaskListTextNew where ID=@TaskListEditID");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@TaskListTextNew", newname);
                cmd.Parameters.AddWithValue("@TaskListEditID", listid);
                conn.Open();
                i=cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

            } 
        }
        if (i == 1)
        {
            return newname;
        }
        else
        {
            return " ";
        }
    }


    /// <summary>  
    ///  查找item属性
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public DataSet SelectItemAttribute(string id)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("select * from TaskItems where ID="+id);
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        
    }
    private string Dtb2Json(DataTable dtb)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        System.Collections.ArrayList dic = new System.Collections.ArrayList();
        foreach (DataRow dr in dtb.Rows)
        {
            System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
            foreach (DataColumn dc in dtb.Columns)
            {
                drow.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            dic.Add(drow);
        }
        return jss.Serialize(dic);
    }
    /// <summary>  
    ///  查找item属性--成员
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public DataSet SelectItemMember(string id)
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("SELECT Users.*, Participators.*FROM Users INNER JOIN Participators ON Users.ID = Participators.UserID where Participators.TaskItemID=" + id);
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd.CommandText, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }

    }
    /// <summary>  
    ///  修改item属性--基本属性
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public string TaskItemUpdateName(string itemid,string itemname, string itemnote)
    {
        int i = 0;
        if (itemname != " " && itemname != "")
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                StringBuilder sb = new StringBuilder("Update TaskItems set ItemText=@ItemText,ItemNote=@ItemNote where ID=@TaskListEditID");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@ItemText", itemname);
                cmd.Parameters.AddWithValue("@ItemNote", itemnote);
                cmd.Parameters.AddWithValue("@TaskListEditID", itemid);
                conn.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();

            }
        }
        if (i == 1)
        {
            return itemname;
        }
        else
        {
            return " ";
        }
    }
     /// <summary>  
    ///  修改item属性--startTime
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public string UpdateStartTime(string id, string time)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update TaskItems set BeginDT=@BeginDT where ID=@TaskItemID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@BeginDT", time);
            cmd.Parameters.AddWithValue("@TaskItemID", id);
            conn.Open();
            i=cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 1) return time;
        else return "";
    }
    /// <summary>  
    ///  修改item属性--startTime
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public string UpdateEndTime(string id, string time)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update TaskItems set EndDT=@EndDT where ID=@TaskItemID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@EndDT", time);
            cmd.Parameters.AddWithValue("@TaskItemID", id);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 1) return time;
        else return "";
    }
    /// <summary>  
    ///  修改item属性--Urgency紧急程度
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public string UpdateUrgency(string id, string u)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update TaskItems set Priority=@Priority where ID=@TaskItemID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Priority", u);
            cmd.Parameters.AddWithValue("@TaskItemID", id);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 1) return "1";
        else return "";
    }
    /// <summary>  
    ///  添加子任务SubItem
    /// </summary>  
    /// <param name="id"></param>  
    [WebMethod]
    public string InsertSubItem(string id, string subitemname, string subitemE)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("INSERT INTO TaskSubItems (ItemText,ParentID,ExecutorID,CDT,IsFinished,IsDeleted) VALUES (@ItemText,@ParentID,@ExecutorID,@CDT,@IsFinished,@IsDeleted)");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@ItemText", subitemname);
            cmd.Parameters.AddWithValue("@ParentID", id);
            cmd.Parameters.AddWithValue("@ExecutorID", subitemE);
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@IsFinished", 0);
            cmd.Parameters.AddWithValue("@IsDeleted", 0);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 1) return "1";
        else return "";
    }


}
