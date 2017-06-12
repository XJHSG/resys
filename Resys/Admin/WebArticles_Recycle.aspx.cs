using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;


public partial class Admin_WebArticles_Recycle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            

                    MyInit();
                    MyDataBind();
                }
            
        
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from WebCategories where Level=2 order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            SubsDDL.DataSource = rd;
            SubsDDL.DataValueField = "ID";
            SubsDDL.DataTextField = "CategoryName";
            SubsDDL.DataBind();
            rd.Close();
            SubsDDL.Items.Insert(0, new ListItem("分类查询", "-1"));



            // RoleID=1,2,3,4，分别对应Administrator,Editor,Contributor,Author
            // int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
            AuthorDDL.Items.Clear();
            cmd.CommandText = "select * from Users order by ID desc";
            rd = cmd.ExecuteReader();
            AuthorDDL.DataSource = rd;
            AuthorDDL.DataValueField = "ID";
            AuthorDDL.DataTextField = "UserName";
            AuthorDDL.DataBind();
            rd.Close();
            AuthorDDL.Items.Insert(0, new ListItem("所有用户", "-1"));

        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    private void MyDataBind()
    {
        AspNetPager1.PageSize = Convert.ToInt16(PageSizeDDL.SelectedValue);
        string param = SearchTB.Text;
        StringBuilder whereStr = new StringBuilder(" where IsDeleted=1 ");
        if (!String.IsNullOrEmpty(param))
        {
            whereStr.Append(" and [Title] like '%").Append(Server.HtmlEncode(param.Trim().Replace("'", ""))).Append("%' ");
        }

        if (Convert.ToInt16(SubsDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and CategoryID = ").Append(SubsDDL.SelectedValue);
        }
        if (Convert.ToInt16(AuthorDDL.SelectedValue) > 0)
        {
            whereStr.Append(" and UserID = ").Append(AuthorDDL.SelectedValue);
        }


        string sql = "select count(ID) as total from WebArticles " + whereStr.ToString();

        using (SqlConnection conn = (SqlConnection)new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                AspNetPager1.RecordCount = Convert.ToInt16(rd[0]);
            }
            else
            {
                AspNetPager1.RecordCount = 0;
            }
            rd.Close();

            Label1.Text = AspNetPager1.RecordCount + "";//总记录数
            Label2.Text = (AspNetPager1.RecordCount / AspNetPager1.PageSize) + 1 + "";//总页数            

            if (AspNetPager1.CurrentPageIndex == 1)
            {
                sql = "Select top " + AspNetPager1.PageSize + " * from WebArticles " + whereStr.ToString() + " " + OrderDDL.SelectedValue;
            }
            else
            {
                // Select Top 页容量 * from 表 where 条件 and id not in	(Select Top 页容量*（当前页数-1） id 	from 表 where 条件 order by 排序条件) order by 排序条件
                sql = "Select top " + AspNetPager1.PageSize + " * from WebArticles " + whereStr.ToString() + " and id not in ( select top " + AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1) + " id  from WebArticles " + whereStr.ToString() + " " + OrderDDL.SelectedValue + " ) " + OrderDDL.SelectedValue;
                //sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER ( " + orderStr + ") AS MyRank,* FROM Article " + whereStr +" ) AS Rank " + whereStr + " and MyRank BETWEEN " +AspNetPager1.StartRecordIndex+" AND "+ (AspNetPager1.StartRecordIndex+AspNetPager1.PageSize-1) +orderStr;
            }
            //TestLabel.Text = sql;
            cmd.CommandText = sql;
            rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
        }



        using (SqlConnection conn = new DB().GetConnection())
        {
            string Subname = "";
            string Subid = "";
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();

            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                // cmd.CommandText = "select * from  WebCategories where ID = " + GridView1.Rows[i].Cells[4].Text.ToString();
                cmd.CommandText = "select * from  WebArticles where ID = " + GridView1.DataKeys[i].Value;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Subid = rd["CategoryID"].ToString();
                }
                rd.Close();

                cmd.CommandText = "select * from  WebCategories where ID = " + Subid;
                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Subname = rd["CategoryName"].ToString();
                    Label label = (Label)GridView1.Rows[i].FindControl("SubName");
                    label.Text = Subname.ToString();
                }
                rd.Close();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }



    }


    protected void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (SelectAllCheckBox.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }

    protected void PageSizeDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }


    protected void SubsDDL_SelectedIndexChanged1(object sender, EventArgs e)
    {
        MyDataBind();
    }
   

    protected void AuthorDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }


    protected void OrderDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        MyDataBind();
    }

    protected void DelBtn_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }


        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);

            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Delete from WebArticles where ID in (" + ids + ") ";
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                //SqlCommand cmd1 = conn.CreateCommand();
                //cmd1.CommandText = "Delete from Articles_ArticleTags where ArticleID in (" + ArticleIDS.Text + ") ";
                //cmd1.ExecuteNonQuery();
                //cmd1.Dispose();

                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandText = "Delete from WebAttachments where ArticleID in (" + ids + ") ";
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();


                //cmd.CommandText = "select * from Articles where ID in (" + ids + ") order by ID desc";
                //SqlDataReader rd = cmd.ExecuteReader();
                //GridView1.DataSource = rd;
                //GridView1.DataBind();
               // rd.Close();
               
                conn.Close();

            }  
            MyDataBind();


            //Response.Redirect(Server.HtmlEncode("WebArticle_DelTrue.aspx?IDS=" + ids));
        }

      
    }

    protected void recovery_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            CheckBox checkBox = (CheckBox)GridView1.Rows[i].FindControl("ChechBox1");
            if (checkBox.Checked == true)
            {
                ids += "," + GridView1.DataKeys[i].Value;
            }
        }
        if (!String.IsNullOrEmpty(ids))
        {
            ids = ids.Substring(1);
            IDS.Text = ids;
            string[] array = ids.Split(',');
            int k = array.Length;

            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "Update WebArticles set IsDeleted = 0 where ID in (" + IDS.Text + ")";
                //cmd.Parameters.AddWithValue("@ID", IDS.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();



                MyDataBind();


                conn.Close();

            }


        }
       
    }
    

   



   
}
