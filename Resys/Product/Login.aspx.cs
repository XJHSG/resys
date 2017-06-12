using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    string valid = "";
    public static string userid = "1";
  
    protected void Page_Load(object sender, EventArgs e)
    {
        //Password.Attributes.Add("onkeydown", "SubmitKeyClick( );");

        string IP = Request.UserHostAddress;//IP地址
        Label1.Text = IP;
        if (!IsPostBack) 
        {
           
            Session["UserID"] = null;
            Session["UserName"] = null;   
        }

    }
    protected void Loging_Click(object sender, EventArgs e)
    {
        
        //执行用户登录
        int roleid = Util.DoLogin(Email.Text.Trim(), Password.Text.Trim());

        if (roleid == -1)
        {
            ErrorLabel.Text = "邮箱或密码错误！";

        }
        
        else
        {
            using (SqlConnection conn = new DB().GetConnection()) {

                string sql = "select ID,IsActive from [Users] where Email = @Email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", Email.Text);
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    valid = rd["IsActive"].ToString();
                    userid = rd["ID"].ToString();
                }
                if (valid == "True")
                {
                    if (roleid == 1)
                    {
                        Util.ShowMessage("登录成功！", "/Admin/Webindex.aspx");
                    }
                    else
                    {
                        Util.ShowMessage("登录成功！", "User_Center.aspx");
                    }
                }
                else 
                {
                    ErrorLabel.Text = "您的账号还没有激活，请查看您的邮件激活账号！";
                }
            }
        }
    }


    public static string getUserID()
    {
        return userid;
    }


}