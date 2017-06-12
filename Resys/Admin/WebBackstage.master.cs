using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_WebBackstage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            if (Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "/product/Login.aspx");
                Response.End();
            }
            else
            {
                
                
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from Users where ID=@UserID";
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        UserName1.Text = rd["UserName"].ToString();
                        UserName2.Text = UserName1.Text;
                        Email.Text = rd["Email"].ToString();
                        Image1.ImageUrl = rd["Avatar"].ToString();
                        Image2.ImageUrl = Image1.ImageUrl;
                    }
                    rd.Close();
                    conn.Close();
                }
                
            }
        }
    }
}
