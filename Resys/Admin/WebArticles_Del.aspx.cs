using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class WebArticles_Del : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ( Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else
            {
             
                    if (Request.QueryString["IDS"] != null)
                    {
                        IDSLabel.Text = Request.QueryString["IDS"].ToString();

                        MyInit();

                    }
                
            }
        }
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from WebArticles where ID in (" + IDSLabel.Text + ") and IsFinished = 1 order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();

            //计算要删除的已完成的文章共多少
            cmd.CommandText = "select count(*) as maxrow from WebArticles where ID in (" + IDSLabel.Text + ") and IsFinished = 1 ";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Count.Text = rd["maxrow"].ToString();
            }
            rd.Close();


            conn.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Update WebArticles set IsDeleted=1  where ID in (" + IDSLabel.Text + ") ";
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();




            cmd.CommandText = "select * from WebArticles where IsDeleted=0 and ID in (" + IDSLabel.Text + ") order by ID desc";
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();

        }
        if (i > 0)
        {
            ResultLabel.Text = "成功删除" + i + "篇文章！     可在回收站中恢复！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
            Label1.Visible = true;

        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Admin/WebArticles_Man.aspx");
    }
}
