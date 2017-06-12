using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

public partial class Product_FindPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }

    public static int i;
    protected void ResetPsd_Click(object sender, EventArgs e)
    {
        string a = Hidden2.Value.ToString();
        string[] s = new string[3];
        s[0] = "密码更新失败，请与管理员联系！";
        s[1] = "修改成功";
        s[2] = "旧密码错误！";
        if (a.Equals("1"))
        {
            i = DoUpdate();
        }
        EmailError.Text = s[i];
       
    }

    protected int DoUpdate()
    {
        int i = 0;
        string userNameStr = Request["user"].ToString();
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "Update [Users] set Password = @Password where UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", userNameStr);
            cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password2.Text.Trim()));
            conn.Open();
            i = cmd.ExecuteNonQuery();
            conn.Close();

        }

        if (i == 1)
        {
            name.InnerText = userNameStr;
            PlaceHolder1.Visible = false;
            PlaceHolder2.Visible = true;
        }

        return i;
    }
}