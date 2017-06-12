using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class RESYS : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserRoleID"] == null || Session["UserID"] == null)
            {
                Literal1.Text = "<a href='/Product/Login.aspx'>登录</a>";
                Avatar.Visible = false;
            }
            else
            {
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from [Users] where [ID] = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", 1);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        Avatar_SImg.ImageUrl = rd["Avatar"].ToString();
                    }
                }
            }
        }
    }
}
