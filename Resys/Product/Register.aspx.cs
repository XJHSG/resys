using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;


public partial class Register : System.Web.UI.Page
{
    public static string table = "Users";

    protected void Page_Load(object sender, EventArgs e)
    {

        SureRegisting.Attributes.Add("onclick", this.GetPostBackEventReference(SureRegisting) + ";this.disabled=true;");


    }

    protected void Registing_Click(object sender, EventArgs e)
    {
        string EmailStr = Email.Text;
        string column = "Email";
        if (Util.AreadyExistd(table, column, EmailStr.Trim()))
        {
            Util.ShowMessage("邮箱已存在！", "Login.aspx");
        }
        else
        {
            RegisterPlaceHolder.Visible = false;
            SurePlaceHolder.Visible = true;
            Email2.Text = Email.Text;
        }
    }

    protected void SureRegisting_Click(object sender, EventArgs e)
    {
        string s = Hidden1.Value;
        string column = "UserName";
        string UserNameStr = UserName.Text;
        string EmailStr = Email.Text.Trim();
        string passwordStr = Password.Text;
        if (Util.AreadyExistd(table, column, UserNameStr.Trim()))
        {
            Util.ShowMessage("用户名已存在！", "Login.aspx");
        }
        else
        {
            if (s == "1")
            {
                string guid = Guid.NewGuid().ToString().Substring(0, 8);
                int j = DoRegist(UserNameStr.Trim(), passwordStr.Trim(), guid);
                if (j == 1)
                {
                    string activecode = Guid.NewGuid().ToString().Substring(0, 8);//生成激活码 
                    string href = "http://" + HttpContext.Current.Request.Url.Authority + "/Product/ActivePage.aspx?Email=" + EmailStr + "&activecode=" + activecode + "";
                    string href2 = "http://" + HttpContext.Current.Request.Url.Authority + "/Product/ActivePage.aspx?Email=" + EmailStr + "&activecode=" + activecode + "";
                    string emailSubject = "Resys -- 激活账号邮件";
                    string emailContent = "欢迎使用Resys邮箱激活账号服务，如果此操作并不是由您发起的，请忽略此邮件。";
                    if (Util.SendMail(Email.Text, UserNameStr.Trim(), emailSubject, href, href2, emailContent))
                    {
                        ErrorLable.Text = "邮件发送成功！";
                        ErrorLable.Visible = true;
                    }
                   // Util.ShowMessage("用户注册成功", "User_Edit.aspx");
                }
            }
            else
            {
                ErrorLable.Text = "滑动验证出错";
            }
        }
    }

 

    private int DoRegist(string u, string p, string guid)
    {
        int i = 0;
        //int r = new Random().Next(10);
        //string avatar = @"images/avatars/" + r + ".png";
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("insert into [Users]( UserName,Password,Email,UserRoleID,RegisterDT,IsActive )");
            sb.Append(" values ( @UserName,@Password,@Email,@UserRoleID,@RegisterDT,@IsActive  ) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@UserName", u);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(p));//密码加密
            cmd.Parameters.AddWithValue("@Email", Email.Text);
            cmd.Parameters.AddWithValue("@UserRoleID", 4);
            cmd.Parameters.AddWithValue("@RegisterDT", DateTime.Now);
            cmd.Parameters.AddWithValue("@IsActive", 0);
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        return i;
    }
}