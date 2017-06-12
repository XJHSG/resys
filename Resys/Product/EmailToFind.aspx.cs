using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

public partial class Product_EmailToFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
    }


    public static string table = "Users";
    public static string username = "";
    protected void emailToFind_Click(object sender, EventArgs e)
    {
        //string EmailStr = Email.Text;
        EmailError.Text = CheckEmail();

    }

    private string CheckEmail()
    {
        string EmailStr = Email.Text;
        string column = "Email";
        int i = 0;
        string[] s = new string[3];
        s[0] = "邮箱输入错误，请重新输入！";
        s[1] = "邮箱号不能为空";
        s[2] = "";
        if (!String.IsNullOrEmpty(EmailStr))
        {
            if (Util.AreadyExistd(table, column, EmailStr.Trim()))
            {
                using (SqlConnection conn = new DB().GetConnection()) 
                {
                    string sql = "select UserName from [Users] where Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", EmailStr.Trim());
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        username = rd["UserName"].ToString();
                    }
                    rd.Close();
                }
                string emailSubject = "Resys -- 找回密码邮件";
                string href = "http://" + HttpContext.Current.Request.Url.Authority + "/Product/FindPassword.aspx?user=" + username + "";
                string href2 = "http://" + HttpContext.Current.Request.Url.Authority + "/Product/FindPassword.aspx?user=" + username + "";
                string emailContent = "欢迎使用Resys邮箱找回您的密码，如果此操作并不是由您发起的，请忽略此邮件。";
                if (Util.SendMail(EmailStr.Trim(), username, emailSubject, href, href2, emailContent))
                {
                    EmailError.Text = "邮件发送成功！";
                    EmailError.Visible = true;
                    i = 2;
                }
              

            }
        }
        else
        {
            i = 1;//第二种情况，用户名为空
        }

        return s[i];
    }

}