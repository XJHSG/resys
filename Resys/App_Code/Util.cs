using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;

/// <summary>
///Util 的摘要说明
/// </summary>
public class Util
{

    public static string GetHash(string password)
    {
        byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
        byte[] b2 = new SHA1Managed().ComputeHash(b);
        return Convert.ToBase64String(b2, 0, b2.Length);
    }

    public static void ShowMessage(string words, string location)
    {
        System.Web.HttpContext.Current.Response.Write("<script>alert('" + words + "');</script>");
        System.Web.HttpContext.Current.Response.Write("<script>location.href='" + location + "';</script>");

    }

  

    // 用户登录，失败返回-1，成功返回RoleID
    public static int  DoLogin(string email, string password)
    {
        int RoleID = -1;
        string UserID = "1";
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from [Users] where [Email] = @Email and [Password] = @Password";
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(password));
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                RoleID = Convert.ToInt16(rd["UserRoleID"]);
                System.Web.HttpContext.Current.Session["UserRoleID"] = RoleID;
                System.Web.HttpContext.Current.Session["UserRoleID"] = rd["UserRoleID"].ToString();
                System.Web.HttpContext.Current.Session["Email"] = rd["Email"].ToString();
                UserID = rd["ID"].ToString();
                System.Web.HttpContext.Current.Session["UserID"] = UserID;
            }
            cmd.Dispose();
            rd.Close();

        }
        return RoleID;
    }

    public static bool AreadyExistd(string table, string column, string value)
    {
        bool result = false;
        using (IDbConnection conn = new DB().GetConnection())
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count(*) from " + table + " where " + column + " = '" + value + "'";
            conn.Open();
            if ((int)cmd.ExecuteScalar() > 0)
            {
                result = true;
            }
        }
        return result;
    }


    //替换邮箱文本
    public static string ReplaceText(String userName, string name, string href, string href2, string content1, string content2)
    {

        string path = string.Empty;

        path = HttpContext.Current.Server.MapPath(@"~\Product/EmailTemplate.html");

        if (path == string.Empty)
        {
            return string.Empty;
            //Util.ShowMessage("1","Login.aspx");
        }
        System.IO.StreamReader sr = new System.IO.StreamReader(path);
        string str = string.Empty;
        str = sr.ReadToEnd();
        str = str.Replace("$USER_NAME$", userName);
        str = str.Replace("$NAME$", name);
        str = str.Replace("$HREF$", href);
        str = str.Replace("$HREF2$", href2);
        str = str.Replace("$content1$", content1);
        str = str.Replace("$content2$", content2);

        return str;
    }

    //发送邮件
    public static bool SendMail(string email, string username,string emailSubject,string hrefText,string hrefLink,string content)
    {
        string error = "";
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.qq.com";
        client.Port = 25;
        //超时时间  
        client.EnableSsl = false;
        client.Timeout = 10000;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        //身份验证
        client.Credentials = new NetworkCredential("875190712@qq.com", "lfswlrayvbgebgaa");
        client.EnableSsl = true;
        MailMessage Message = new MailMessage();
        Message.From = new MailAddress("875190712@qq.com", "Resys");
        Message.To.Add(email);
        Message.Subject = emailSubject;
        string href = hrefText;
        string href2 = hrefLink;
        string content1 = content;
        string content2 = "请点击以下链接进行验证";
        string strbody = Util.ReplaceText(username, username, href, href2, content1, content2);

        Message.Body = strbody;
        Message.SubjectEncoding = System.Text.Encoding.UTF8;
        Message.BodyEncoding = System.Text.Encoding.UTF8;
        Message.IsBodyHtml = true;
        Message.BodyEncoding = System.Text.Encoding.UTF8;
        Message.Priority = System.Net.Mail.MailPriority.High;
        bool ret = true;
        try
        {
            client.Send(Message);
        }
        catch (SmtpException ex)
        {
            error = ex.Message;
            ret = false;
        }
        catch (Exception ex2)
        {
            error = ex2.Message;
            ret = false;
        }
        return ret;
    }
}
