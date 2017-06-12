using System;
using System.Collections.Generic;
using System.Linq;
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
/// Users_WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class Users_WebService : System.Web.Services.WebService {

    public Users_WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string SaveLogs(string userid,string module,string action,string page,string userip)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into [Logs]( UserID,Module,Action,Page,CDT,UserIP )");
            sb.Append(" values ( @UserID,@Module,@Action,@Page,@CDT,@UserIP  ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@Module", module);
            cmd.Parameters.AddWithValue("@Action", action);
            cmd.Parameters.AddWithValue("@Page", page);
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@UserIP", userip);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        if (i == 1)
        {
            return "Sucess";
        }
        else 
        {
            return "Failure";
        }
    }

    [WebMethod]
    public String changeExector(string TaskItemID, string Email)
    {

        int i = 0;

        using (SqlConnection conn = new DB().GetConnection())
        {
            string id = "";
            string sql = "select ID from Users where UserName=@Email";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", Email.Trim());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                id = rd["ID"].ToString();
            }
            rd.Close();

            cmd.CommandText = "update TaskItems set ExecutorID=@ExectorID  where ID=@ID";
            cmd.Parameters.AddWithValue("@ID", TaskItemID);
            cmd.Parameters.AddWithValue("@ExectorID", id);
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        if (i == 1)
        {
            return "Sucess";
        }
        else
        {
            return "Failure";
        }
    }

    [WebMethod]
    public String sendEmail(string Email,string UserName,string EmailSubject,string Href) 
    {

        Util.SendMail(Email, UserName, EmailSubject, Href, Href, EmailSubject);
        return "Sucess";
    }

   
    
}
