using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Product_User_Edit : System.Web.UI.Page
{
    public static int i;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        string userid = Convert.ToString(Session["UserID"]);
        using (SqlConnection conn = new DB().GetConnection())
        {

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from [Users] where [ID] = @UserID";
            cmd.Parameters.AddWithValue("@UserID", userid);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Avatar_Image.ImageUrl = rd["Avatar"].ToString();
                User_Name.Text = rd["UserName"].ToString();
                Tel.Text = rd["Phone"].ToString();
                Signature.Text = rd["Signature"].ToString();
                Birth_Date.Text = rd["Birthdate"].ToString();
                Avatar_SImg.ImageUrl = rd["Avatar"].ToString();
                UserName.Text = rd["UserName"].ToString();
                Email.Text = rd["Email"].ToString();
            }
            rd.Close();
        }
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            Password1.TextMode = TextBoxMode.SingleLine;
            Password2.TextMode = TextBoxMode.SingleLine;
            Password3.TextMode = TextBoxMode.SingleLine;
        }
        else
        {
            Password1.TextMode = TextBoxMode.Password;
            Password2.TextMode = TextBoxMode.Password;
            Password3.TextMode = TextBoxMode.Password;
        }
    }

    protected void Save_Info_Click(object sender, EventArgs e)
    {
        int userid = 1;
        using (SqlConnection conn = new DB().GetConnection())
        {
           
            StringBuilder sb = new StringBuilder("Update [Users] set UserName=@UserName,Phone=@Phone,Signature=@Signature,Birthdate=@Birthdate");
            sb.Append(" where ID=@UserID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Birthdate", Birth_Date.Text.Trim());
            cmd.Parameters.AddWithValue("@UserName", User_Name.Text.Trim());
            cmd.Parameters.AddWithValue("@UserID", userid);
            cmd.Parameters.AddWithValue("@Phone", Tel.Text.Trim());
            cmd.Parameters.AddWithValue("@Signature", Signature.Text.Trim());
            conn.Open();
            i=cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    //public static int getSometing()
    //{
    //    return i;
    //}    

    protected void Psd_Upd_Click(object sender, EventArgs e)
    {
        string userid = Convert.ToString(Session["UserID"]);
        string a = Hidden2.Value.ToString();
        string[] s = new string[3];
        s[0] = "密码更新失败，请与管理员联系！";
        s[1] = "修改成功";
        s[2] = "旧密码错误！";
        if(a.Equals("1"))
        {
            i = DoUpdate();
        }
        ErrorLabel.Text = s[i];
    }

    protected int DoUpdate()
    {
        int i = 0;
        string oldPassword = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select Password from [Users] where ID = @UserID";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                oldPassword = rd["Password"].ToString();
            }
            rd.Close();

            if (oldPassword.Equals(Util.GetHash(Password1.Text.Trim())))
            {
                cmd.CommandText = "Update [Users] set Password = @Password where ID = @UserID2";
                cmd.Parameters.AddWithValue("@Password", Util.GetHash(Password2.Text.Trim()));
                cmd.Parameters.AddWithValue("@UserID2", Session["UserID"].ToString());
                i = cmd.ExecuteNonQuery();

            }
            else
            {
                i = 2;//第二种情况，旧密码错误。
            }
            conn.Close();
        }
        return i;
    }
}