using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

public partial class Admin_WebArticles_Add : System.Web.UI.Page
{

    //上传封面图后更新数据表Resources用到的数据
    string ResourceName = "";
    string extension = "";
    string physicalName = "";
    string fileName = "";
    static int variable = 2;
    static string files = "";
    static string filesize = "";
    static string fileName2 = "";

    public string RandomIDCD = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        if (!IsPostBack)
        {
           
           
            string UserID = Session["UserID"].ToString();
            string UserDict = "~/Users/" + UserID;
            if (!Directory.Exists(Server.MapPath(UserDict)))
            {
                Directory.CreateDirectory(UserDict);
            }

            if (Request.QueryString["ID"] != null && !String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {
                // 修改文章
                string ArticleID = Request.QueryString["ID"];
                IDLabel.Text = Request.QueryString["ID"].ToString();
                if ((Convert.ToInt16(Session["RoleID"]) < 3))
                {
                    //选择哪个文章的ID   
                    MyInitForUpdate();//调用函数
                    files = "";
                    filesize = "";
                    fileName2 = "";
                    File_Return();
                }
                else
                {

                    Response.Write("<script language='javascript'> alert('你无法访问该页面' );window.location ='.../Product/User_Center.aspx';</script>");

                }
            }
            else
            {
                // 新增文章 
           
                files = "";
                filesize = "";
                fileName2 = "";
              
                MyInitForAdd();
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from Users where ID=@UserID";
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read()) {
                        UserName.Text = rd["UserName"].ToString();
                    }
                }
                RandomID.Text = Guid.NewGuid().ToString();
                RandomIDCD = RandomID.Text;

                //RandomID.Text = "";//第一次加载页面，为空
            }




        }
        coverphotoshow();
        fileshow();
    }


    //检验过程
    private string Check()
    {
        int i = 0;
        string[] s = new string[6];
        s[0] = "";
        s[1] = "操作失败！ 文章标题不能为空！";
        s[2] = "操作失败！ 文章内容不能为空！";
        string TitleStr = TitleTB.Text;
        string Content = Editor1.Text;
        if ((!String.IsNullOrEmpty(TitleStr)) && (!String.IsNullOrEmpty(Content)))
        {

            DataBind();

        }
        if (String.IsNullOrEmpty(TitleStr))
        {
            i = 1;//第二种情况，用户名密码为空
        }
        if (String.IsNullOrEmpty(Content))
        {
            i = 2;
        }
        return s[i];
    }

    //新建时读取基本数据
    private void MyInitForAdd()
    {
        CDT_TextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from WebCategories where Level=1 order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Cats.DataSource = rd;
            Cats.DataValueField = "ID";
            Cats.DataTextField = "CategoryName";
            Cats.DataBind();
            rd.Close();


            cmd.CommandText = "select max(Orders) as orders from WebArticles";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MaxOrders.Text = rd[0].ToString();
            }
            rd.Close();



            cmd.CommandText = "select * from WebCategories where ParentID = " + Cats.SelectedValue + " order by Orders desc";
            rd = cmd.ExecuteReader();
            Subs.DataSource = rd;
            Subs.DataTextField = "CategoryName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();


        }

    }

    protected void Cats_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CatID = Convert.ToInt16(Cats.SelectedValue);
        int Count = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT count(*)as Num from WebCategories where ParentID = " + Cats.SelectedValue;
            conn.Open();
            SqlDataReader rd = null;
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Count = Convert.ToInt16(rd["Num"].ToString());
            }
            rd.Close();

            if (Count > 0)
            {
                Subs.Visible = true;
            }
            else
            {
                Subs.Visible = false;
            }


            cmd.CommandText = "select * from WebCategories where ParentID = " + Cats.SelectedValue + " order by Orders asc";
            rd = cmd.ExecuteReader();
            Subs.DataSource = rd;
            Subs.DataTextField = "CategoryName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();

        }


    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        Button6.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        Button7.BackColor = System.Drawing.ColorTranslator.FromHtml("#f0f0f0");
        Button5.BackColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    }

    //发表文章
    protected void Button3_Click(object sender, EventArgs e)
    {
        ResultLabel.Text = Check();
        ResultLabel.ForeColor = System.Drawing.Color.Red;
        if (ResultLabel.Text == "")
        {
            Article_Add1();
            Response.Redirect("WebArticles_Add.aspx?ID=" + IDLabel.Text);
        }
    }

    // 保存草稿
    protected void Button1_Click(object sender, EventArgs e)
    {
        ResultLabel.Text = Check();
        ResultLabel.ForeColor = System.Drawing.Color.Red;
        if (ResultLabel.Text == "")
        {
            int i = 0;
            if (String.IsNullOrEmpty(IDLabel.Text))
            {
                //RandomID.Text = Guid.NewGuid().ToString();
                i = DoInsert(0);
            }
            else
            {
                 i = DoUpdate(0);
            }

            if (i == 1)
            {
                ResultLabel.Text = "保存草稿成功！";
                ResultLabel.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("WebArticles_Add.aspx?ID=" + IDLabel.Text);
            }
            else
            {
                ResultLabel.Text = "保存草稿失败，请重试！";
                ResultLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    // 发表新文章
    protected void Button11_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebArticles_Add.aspx");
    }
    

    //提交按钮调用函数
    private void Article_Add1()
    {
        int i = 0;
        if (String.IsNullOrEmpty(IDLabel.Text))
        {
            i = DoInsert(1);
        }
        else
        {
            i = DoUpdate(1);

        }

        if (i == 1)
        {
            ResultLabel.Text = "保存文章成功！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
          
        }
        else
        {
            ResultLabel.Text = "保存文章失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }

    }


    //新建文章插入数据
    private int DoInsert(int finished)
    {
       // string[] str1 = files.Split(',');
        string[] str1 = fileName2.Split(',');
        int k = str1.Length;
        string[] strfileid = new string[100];
        int i = 0;

        string cpimgurl;
        if (variable == 2)
        {
            cpimgurl = CoverPhoto1.ImageUrl;
        }
        else cpimgurl = CoverPhoto.ImageUrl;

        using (SqlConnection conn = new DB().GetConnection())
        {

            StringBuilder sb = new StringBuilder("Insert into WebArticles ( Title,CategoryID,Picture,Abs,Content,Orders,UserID,UserName,CDT,Views,Ups,IsDeleted,GUID,IsFinished) ");
            sb.Append(" values ( @Title,@CategoryID,@Picture,@Abs,@Content,@Orders,@UserID,@UserName,@CDT,@Views,@Ups,@IsDeleted,@GUID,@IsFinished) ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@CategoryID", Subs.SelectedValue);
            cmd.Parameters.AddWithValue("@Content", Editor1.Text);

            // cmd.Parameters.AddWithValue("@CatName", Cats.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@UserName", UserName.Text);
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
           // cmd.Parameters.AddWithValue("@Picture", "未写");
            cmd.Parameters.AddWithValue("@IsDeleted", 0);
            cmd.Parameters.AddWithValue("@Views", 0);
            cmd.Parameters.AddWithValue("@Ups", 0);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@GUID", RandomID.Text);
            cmd.Parameters.AddWithValue("@IsFinished", finished);

            if (cpimgurl == "")
            {
                cpimgurl = "Admin/Upload/CoverPhoto/school.jpg";
                cmd.Parameters.AddWithValue("@Picture", cpimgurl);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Picture", cpimgurl);
            }

            if (Summary.Text == "")
            {
                string str = "";
                string contenttext = Editor1.Text.Substring(0, Editor1.Text.LastIndexOf("</"));
                str = contenttext;
                str = Regex.Replace(str, "<br />", "  ");
                string regexstr = @"<[^>]*>";
                str = Regex.Replace(str, regexstr, string.Empty, RegexOptions.IgnoreCase);
                Regex reg = new Regex(@"(?i)(&nbsp;)+");
                str = reg.Replace(str, " ");
                str = Regex.Replace(str, "&gt;", ">");
                str = Regex.Replace(str, "&lt;", "<");
                str = Regex.Replace(str, "&frasl;", "/");
                str = Regex.Replace(str, "&sim;", "~");
                str = Regex.Replace(str, "&laquo;", "《");
                str = Regex.Replace(str, "&lsquo;", "'");
                str = Regex.Replace(str, "&ldquo;", "''");
                str = Regex.Replace(str, "&hellip;", "…");
                str = Regex.Replace(str, "#39;", "'");
                str = Regex.Replace(str, "&amp;", "&");
                str = Regex.Replace(str, "&mdash;", "—");
                str = Regex.Replace(str, "&quot;", "''");
                if (str.Length < 30)
                {
                    //Summary.Text = Editor1.Text.Substring(0, Editor1.Text.LastIndexOf(">"));

                    Summary.Text = str;

                }

                else
                {
                    Summary.Text = str.Substring(0, 30);
                }

                cmd.Parameters.AddWithValue("@Abs", Summary.Text);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Abs", Summary.Text);

            }

            conn.Open();
            i = cmd.ExecuteNonQuery();
        }


        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from WebArticles where GUID = @RandomID";
            cmd.Parameters.AddWithValue("@RandomID", RandomID.Text);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                IDLabel.Text = rd["ID"].ToString();
            }
            rd.Close();
            conn.Close();
        }

     
        using (SqlConnection conn = new DB().GetConnection())
        {

            for (int j = 0; j < k-1; j++)
            {
                if (str1[j] == null || str1[j] == "") break;
                else
                {
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = "select * from WebFiles where FileName = '" + str1[j] + "' order by id desc";
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        strfileid[j] = rd["ID"].ToString();

                    }
                    rd.Close();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }

            }



            for (int j = 0; j < k-1; j++)
            {
                if (strfileid[j] == null || strfileid[j] == "") break;
                else
                {

                    SqlCommand cmd2 = conn.CreateCommand();
                    conn.Open();
                    cmd2.CommandText = "Insert into WebAttachments (FileID,ArticleID)values (@FileID,@ArticleID)";
                    cmd2.Parameters.AddWithValue("@FileID", strfileid[j]);
                    cmd2.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    conn.Close();
                }
            }
        }
     
        return i;
    }



    // 修改文章函数
    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from WebCategories where Level=1 order by Orders desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Cats.DataSource = rd;
            Cats.DataValueField = "ID";
            Cats.DataTextField = "CategoryName";
            Cats.DataBind();
            rd.Close();


            cmd.CommandText = "select max(Orders) as orders from WebArticles";
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                MaxOrders.Text = rd[0].ToString();
            }
            rd.Close();


            string CategoryID = "0";
            cmd.CommandText = "select * from WebArticles where ID =" + IDLabel.Text;
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                TitleTB.Text = rd["Title"].ToString();
                Summary.Text = rd["Abs"].ToString();
                CategoryID = rd["CategoryID"].ToString();
                CDT_TextBox.Text = String.Format("{0:yyyy-MM-dd}", rd["CDT"]);
                Orders.Text = rd["Orders"].ToString();
                Editor1.Text = rd["Content"].ToString();
                CoverPhoto1.ImageUrl = rd["Picture"].ToString();
                RandomID.Text = rd["GUID"].ToString();
                RandomIDCD = RandomID.Text;


            }
            rd.Close();

            string ParentID = "0";
            cmd.CommandText = "select * from WebCategories where ID=@CategoryID";
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                ParentID = rd["ParentID"].ToString();
            }
            rd.Close();

            if (Cats.Items.FindByValue(ParentID) != null)
            {
                Cats.ClearSelection();
                Cats.Items.FindByValue(ParentID).Selected = true;
            }

            cmd.CommandText = "select * from WebCategories where ParentID = " + Cats.SelectedValue + " order by Orders desc";
            rd = cmd.ExecuteReader();
            Subs.DataSource = rd;
            Subs.DataTextField = "CategoryName";
            Subs.DataValueField = "ID";
            Subs.DataBind();
            rd.Close();

            if (Subs.Items.FindByValue(CategoryID) != null)
            {
                Subs.ClearSelection();
                Subs.Items.FindByValue(CategoryID).Selected = true;
            }

        }

    }

    // 更新操作
    private int DoUpdate(int finished)
    {
        string[] str1 = fileName2.Split(',');
        int k = str1.Length;
        string[] strfileid = new string[100];
        int i = 0;

        string cpimgurl;
        if (variable == 2)
        {
            cpimgurl = CoverPhoto1.ImageUrl;

        }
        else cpimgurl = CoverPhoto.ImageUrl;
        
        //更新文章列表的数据
        using (SqlConnection conn = new DB().GetConnection())
        {


            StringBuilder sb = new StringBuilder("Update WebArticles set Title = @Title,CategoryID=@CategoryID,Picture=@Picture,Abs=@Abs,Content=@Content,Orders=@Orders,UserID=@UserID,UserName=@UserName,CDT=@CDT,IsFinished=@IsFinished where ID = @ID  ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Title", TitleTB.Text);
            cmd.Parameters.AddWithValue("@Content", Editor1.Text);
            cmd.Parameters.AddWithValue("@Abs", Summary.Text);
            cmd.Parameters.AddWithValue("@CategoryID", Subs.SelectedValue);

            cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@UserName", UserName.Text);
            cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
            //cmd.Parameters.AddWithValue("@Picture", "未写");
            cmd.Parameters.AddWithValue("@Picture", cpimgurl);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@IsFinished", finished);

            cmd.Parameters.AddWithValue("@ID", Request.QueryString["ID"].ToString());
            conn.Open();
            i = cmd.ExecuteNonQuery();
         
        }

        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Delete from WebAttachments where ArticleID ='" + IDLabel.Text + "'";
            conn.Open();
            cmd.ExecuteNonQuery();
        }


        using (SqlConnection conn = new DB().GetConnection())
        {

            for (int j = 0; j < k - 1; j++)
            {
                if (str1[j] == null || str1[j] == "") break;
                else
                {
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = "select * from WebFiles where FileName = '" + str1[j] + "' order by id desc";
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        strfileid[j] = rd["ID"].ToString();

                    }
                    rd.Close();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                }

            }



            for (int j = 0; j < k - 1; j++)
            {
                if (strfileid[j] == null || strfileid[j] == "") break;
                else
                {

                    SqlCommand cmd2 = conn.CreateCommand();
                    conn.Open();
                    cmd2.CommandText = "Insert into WebAttachments (FileID,ArticleID)values (@FileID,@ArticleID)";
                    cmd2.Parameters.AddWithValue("@FileID", strfileid[j]);
                    cmd2.Parameters.AddWithValue("@ArticleID", IDLabel.Text);
                    cmd2.ExecuteNonQuery();
                    cmd2.Dispose();
                    conn.Close();
                }
            }
        }

        return i;
    }


    //附件操作
    protected void file_add_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "block";
    }

    protected void File_Close_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "none";
   
    }

    protected void file_sure_Click(object sender, EventArgs e)
    {
        files_add.Style["display"] = "none";
        
        string FNText = FileHelper.GetFileName();
        //return_file1 = Util.GetFileName();
        string[] array = FNText.Split(',');
        int k = array.Length;
        int s = k - 1;
        int i = 0;

        string sql = "";
        using (SqlConnection conn = new DB().GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            for (int num = 0; num < k; num++)
            {
                sql = "Update [WebFiles] set FolderID=@FolderID,FolderName=@FolderName,IsDeleted=@IsDeleted,CreatorID=@CreatorID where FileName = @FileName";
                cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@FileName", array[num]);
                cmd.Parameters.AddWithValue("@FolderID", "3");
                cmd.Parameters.AddWithValue("@FolderName", "附件");
                cmd.Parameters.AddWithValue("@IsDeleted", "0");
                cmd.Parameters.AddWithValue("@CreatorID", Session["UserID"].ToString());
                cmd.ExecuteNonQuery();

                sql = "select * from [WebFiles] where FileName = @FileName";
                cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@FileName", array[num]);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    files += rd["ShowName"].ToString() + ",";
                    fileName2 += rd["FileName"].ToString() + ",";
                    filesize += rd["FileSizeInKB"].ToString() + ",";
                }
                rd.Close();
                cmd.ExecuteNonQuery();

            }
            FileHelper.CleanFileName();
            conn.Close();
            conn.Open();
            cmd.CommandText = "Update WebFolders set Counts = Counts - " + s.ToString() + " where ID = 1;Update WebFolders set Counts = Counts + " + s.ToString() + " where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", "3");
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
      
     
        File_Box.Controls.Clear();
        fileshow();
        
    }
    protected void File_Close(object sender, EventArgs e)
    {

        string DeleteText = "";
        string sql = "";
        LinkButton b = (LinkButton)sender;
        foreach (Control cont in b.Parent.Controls)
        {
            if (cont is LinkButton)
            {
                if (((LinkButton)cont).Text != "删除")
                {
                    string[] strfiles = files.Split(',');
                    string[] strfilenames = fileName2.Split(',');
                    string[] strfilesize = filesize.Split(',');
                    int k = strfilenames.Length;
       
                    files = "";
                    filesize = "";
                    fileName2 = "";
                    using (SqlConnection conn = new DB().GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        sql = "Update [WebFiles] set IsDeleted=@IsDeleted where FileName = @FileName";
                        cmd = new SqlCommand(sql.ToString(), conn);
                        cmd.Parameters.AddWithValue("@IsDeleted", "1");
                        cmd.Parameters.AddWithValue("@FileName", ((LinkButton)cont).Text.ToString());
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        DeleteText = ((LinkButton)cont).Text.ToString();

                        for (int i = 0; i <= k-1; i++)
                        {
                            if (strfilenames[i] != DeleteText)
                            {
                                files += strfiles[i] + ",";
                                fileName2 += strfilenames[i] + ",";
                                filesize += strfilesize[i] + ",";

                            }
                        }

                    }
                  
                }
            }
        }
       File_Box.Controls.Clear();
     
        fileshow();
    }

   

    private void fileshow()
    {
        int j = 1;
        string[] str = files.Split(',');
        string[] strname = fileName2.Split(',');
        string[] strsize = filesize.Split(',');
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == "" || str[i] == null) break;
            Panel p1 = new Panel();
            p1.ID = "file_panel" + i.ToString();
            p1.Style["padding"] = "1px 10px";
            // p1.Style["background-color"] = "#8aac82";
            Label im1 = new Label();
            im1.CssClass = "menu-icon glyphicon glyphicon-paperclip";
            im1.ID = "fileaddimg" + i.ToString();
            Label l2 = new Label();
            l2.Text = "&nbsp;&nbsp;&nbsp;附件" + j.ToString() + "：";
            LinkButton file1 = new LinkButton();
            file1.ID = "file" + i.ToString();
            file1.Text = str[i];
            Label l1 = new Label();
            l1.Text = " ( " + strsize[i].ToString() + "KB ) ";
            l1.Style["color"] = "#080808";
            LinkButton l3 = new LinkButton();
            l3.Text = strname[i].ToString();
            l3.Style["display"] = "none";
            LinkButton file_close = new LinkButton();
            //file_close.Style["float"] = "right";
            file_close.ID = "file_close" + i.ToString();
            file_close.Text = "删除";
            //file_close.Style["float"] = "right";
            file_close.Click += File_Close;
            p1.Controls.Add(im1);
            p1.Controls.Add(l2);
            p1.Controls.Add(file1);
            p1.Controls.Add(l1);
            p1.Controls.Add(l3);
            p1.Controls.Add(file_close);
            File_Box.Controls.Add(p1);
            j++;
        }
     
    }

   

       //修改时附件加载
       protected void File_Return()
       {
           files = "";
           fileName2 = "";
          // filesize = "";
           string filesid = "";
           //IDLabel.Text = Request.QueryString["ID"].ToString();
           using (SqlConnection conn = new DB().GetConnection())
           {
               SqlCommand cmd = conn.CreateCommand();
               conn.Open();
               cmd.CommandText = "select * from  [WebAttachments] where ArticleID=@ArticleID";
               cmd.Parameters.AddWithValue("@ArticleID", Request.QueryString["ID"].ToString());
               SqlDataReader rd = cmd.ExecuteReader();
               while (rd.Read())
               {
                   filesid += rd["FileID"].ToString()+ ","  ;
 
               }
               rd.Close();
               cmd.ExecuteNonQuery();
               cmd.Dispose();
           

               string[] strfilesid = filesid.Split(',');
               int k = strfilesid.Length-1;
 
               for (int j = 0; j < k ; j++)
               {
                   SqlCommand cmd1 = conn.CreateCommand();
                   cmd1.CommandText = "select * from  [WebFiles] where ID = @FileID";
                   cmd1.Parameters.AddWithValue("@FileID", strfilesid[j]);
                   rd = cmd1.ExecuteReader();
                   while (rd.Read())
                   {
                       files += rd["ShowName"].ToString() + ",";            
                       fileName2 += rd["FileName"].ToString() + ","; 
                       filesize += rd["FileSizeInKB"].ToString() + ",";
                       filesid += rd["ID"].ToString() + ",";
                     
                   }
                   rd.Close();
                  
                  
               }
               //string[] fileid1 = filesid.Split(',');
               //using (SqlConnection conn1 = new DB().GetConnection())
               //{
               //    SqlCommand cmd2 = conn.CreateCommand();
               //    for (int i = 0; i < fileid1.Length; i++)
               //    {
               //        cmd2.CommandText = "update WebFiles set IsDeleted =0 where ID ='" + fileid1[i] + "'";
               //        cmd2.ExecuteNonQuery();
               //        cmd2.Dispose();
               //    }
               //}
               conn.Close();
           }

       }

    //封面图操作
       protected void Button10_Click(object sender, EventArgs e)
       {
           Cover.Style["display"] = "block";
       }
       protected void UploadButton_Click(object sender, EventArgs e)
       {
           CoverPhoto1.Visible = false;
           CoverPhoto.Visible = true;
           if (FileUpload1.HasFile)
           {
               try
               {
                   ResourceName = FileUpload1.FileName;
                   extension = System.IO.Path.GetExtension(ResourceName).ToLower();
                   string allowExtension = ConfigurationManager.AppSettings["PhotoExtension"].ToString().ToLower();
                   string[] ss = allowExtension.Split(',');
                   bool success = false;
                   foreach (string s in ss)
                   {
                       if (extension.Equals(s.Trim()))
                       {
                           success = true;
                           break;
                       }
                   }

                   if (success && Session["UserID"] != null)
                   {
                       string dir = "Admin" + "/" + "Upload" +"/" + "CoverPhoto" + "/" + DateTime.Now.ToString("yyyyMM");

                       // 目录不存在,则创建目录
                       if (!Directory.Exists(Server.MapPath(dir)))
                       {
                           Directory.CreateDirectory(Server.MapPath(dir));
                       }

                       string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                       string number = String.Format("{0:0000}", new Random().Next(1000));//生产****四位数的字符串
                       fileName = Session["UserID"].ToString() + "_" + now + "_" + number + extension;
                       physicalName = dir + "/" + Session["UserID"].ToString() + "_" + now + "_" + number + extension;

                    //  int  fileSizeInMB = FileUpload1.PostedFile.ContentLength / 1024;
                       //if (fileSizeInMB == 0) fileSizeInMB = 1;         
                    //  PhotoFS.Text = fileSizeInMB.ToString();
                       // 保存图片到服务器
                       FileUpload1.SaveAs(Server.MapPath(physicalName));

                       //在数据表Resources插入一条记录操作
                       InsertDataBase();

                  
                       if (!String.IsNullOrEmpty(CoverPhoto.ImageUrl))
                       {
                           int a = 0;
                           File.Delete(Server.MapPath(CoverPhoto.ImageUrl));
                           using (SqlConnection conn = new DB().GetConnection())
                           {
                               SqlCommand cmd = conn.CreateCommand();

                               conn.Open();
                               cmd.CommandText = "select * from WebFiles order by ID desc";
                               SqlDataReader rd = cmd.ExecuteReader();
                               if (rd.Read())
                               {
                                   a = int.Parse(rd["ID"].ToString());
                                   a -= 1;
                                   //cmd1.CommandText = "delete from Resources where ID=@ID";
                                   //cmd1.Parameters.AddWithValue("@ID", a);
                                   //cmd1.ExecuteNonQuery();

                               }
                               rd.Close();
                               //已在后面加多一条更新“封面图”文件夹下的资源数的操作
                               cmd.CommandText = "delete from WebFiles where id in(select top 1 id from WebFiles where id in( select top 2 id from WebFiles where CreatorID=@UserID order by id desc)order by id);Update WebFolders set Counts = Counts-1 where ID = 2";
                               cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                               cmd.ExecuteNonQuery();
                               rd.Close();

                               conn.Close();
                           }

                       }

                       CoverPhoto.ImageUrl = physicalName;
                       variable = 1;


                       //MyDataBind();
                   }
                   else
                   {
                       ResultLabel.Text = "上传图片格式错误！";
                       ResultLabel.Visible = true;
                       ResultLabel.ForeColor = System.Drawing.Color.Red;
                   }
               }
               catch (Exception exc)
               {
                   ResultLabel.Text = "上传图片失败。请重试！或者与管理员联系！<br>" + exc.ToString();
                   ResultLabel.Visible = true;
                   ResultLabel.ForeColor = System.Drawing.Color.Red;
               }
           }
           else
           {
               ResultLabel.Text = "请选择封面图片";
               ResultLabel.Visible = true;
           }

       }

       //在数据表Resources插入一条记录操作
       protected bool InsertDataBase()
       {
           bool result = false;

           using (SqlConnection conn = new DB().GetConnection())
           {
               //向resources插入一条记录操作
               StringBuilder sb = new StringBuilder("Insert into WebFiles (ShowName,FileName,FilePath,FileSizeInKB,FileType,Extentsion,FolderID,FolderName,CreatorID,CDT,IsDeleted)");
               sb.Append(" values(@ShowName,@FileName,@FilePath,@FileSize,@FileType,@Extentsion,@FolderID,@FolderName,@CreatorID,@CDT,@IsDeleted)");
               SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
               cmd.Parameters.AddWithValue("@ShowName", ResourceName);
               cmd.Parameters.AddWithValue("@FileName", fileName);
               cmd.Parameters.AddWithValue("@FilePath", "/" + physicalName);
               cmd.Parameters.AddWithValue("@FileSizeInKB", PhotoFS.Text);
               cmd.Parameters.AddWithValue("@FileType", "图片");
               cmd.Parameters.AddWithValue("@Extentsion", extension);
               cmd.Parameters.AddWithValue("@FolderID", 2);
               cmd.Parameters.AddWithValue("@FolderName", "封面图");
               cmd.Parameters.AddWithValue("@CreatorID", Session["UserID"].ToString());
               cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
               cmd.Parameters.AddWithValue("@IsDeleted", 0);
               conn.Open();
               cmd.ExecuteNonQuery();
               conn.Close();
               conn.Open();
               cmd.CommandText = "SELECT count(*) from WebFiles where FolderID =2";
               int count = int.Parse(Convert.ToString(cmd.ExecuteScalar()));
               cmd.CommandText = "Update WebFolders set Counts = " + count.ToString() + " where ID = 2";
               cmd.ExecuteNonQuery();
               cmd.Dispose();
               //插入成功
               result = true;
           }
           return result;

       }
       private void coverphotoshow()
       {
           int a = 0;
           using (SqlConnection conn = new DB().GetConnection())
           {
               SqlCommand cmd = conn.CreateCommand();
               conn.Open();
               cmd.CommandText = "select * from  [WebFiles] where FolderName='封面图' order by ID desc  ";
               SqlDataReader rd = cmd.ExecuteReader();
               while (rd.Read())
               {
                   a++;
                   ImageButton img1 = new ImageButton();
                   img1.BorderStyle = BorderStyle.None;
                   img1.Width = Unit.Percentage(25);
                   img1.Height = Unit.Percentage(25);
                   img1.Style["padding"] = "3px";
                   img1.ImageUrl = rd["FilePath"].ToString();
                   img1.ID = "im" + a.ToString();
                   img1.Click += new ImageClickEventHandler(coverphoto_return);
                   coverphotos.Controls.Add(img1);
               }
               rd.Close();
           }

       }

       protected void coverphoto_return(object sender, EventArgs e)
       {
           ImageButton ib = (ImageButton)sender;
           //Response.Write("<script>alert('"+ib.ImageUrl.ToString()+"');</script>");
           Cover.Style["display"] = "none";
           CoverPhoto1.ImageUrl = ib.ImageUrl;
           variable = 2;
           CoverPhoto1.Visible = true;
           CoverPhoto.Visible = false;
       }

       protected void EscCoverShow_Click(object sender, EventArgs e)
       {
           Cover.Style["display"] = "none";

       }




}