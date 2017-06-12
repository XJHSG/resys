using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Product_ActivePage : System.Web.UI.Page
{
    DateTime RegisterDateTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string activeCode = Request["activecode"].ToString();
            ActiveCode.Text = activeCode;

        }
    }

    protected void SureActiving_Click(object sender, EventArgs e) 
    {
        //取出参数id 
        string username = Request["user"].ToString();

        DateTime dtTime = DateTime.Now;
        //2判断id为id的记录是否存在 
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select RegisterDT from [Users] where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", username);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                RegisterDateTime = Convert.ToDateTime(rd["RegisterDT"]);

            }
            rd.Close();

            if (dtTime.AddDays(-30) > RegisterDateTime)
            {
                ErrorLable.Text = "该账号已经超过三十天没有激活，已经失效！";
                
            }
            else
            {
                ErrorLable.Text = "您的账号已经激活了";
                cmd.CommandText = "Update [Users] set IsActive = @IsActive,ActiveDT = @ActiveDT  where  UserName = @UserName1 ";
                cmd.Parameters.AddWithValue("@UserName1", username);
                cmd.Parameters.AddWithValue("@IsActive", "True");
                cmd.Parameters.AddWithValue("@ActiveDT", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

        }
        
    }
}