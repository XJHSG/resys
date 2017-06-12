using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Uploads_File_Man : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            LabelLevel.Text = "1";
            LabelProjectID.Text = "1";
            LabelParentID.Text = "0";
            CreateCache();
            InitTable(int.Parse(LabelParentID.Text),int.Parse(LabelLevel.Text));
        }
        
    }

    //创建缓存
    private void CreateCache()
    {
        List<Folder> FolderList = new List<Folder>();
        List<File> FileList = new List<File>();
        int countFolder = 0;
        int countFile = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            string sql = "select * from Folders where ProjectID=@ProjectID1 and IsAchived=@IsAchived1 and IsDeleted=@IsDel1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProjectID1", LabelProjectID.Text);
            cmd.Parameters.AddWithValue("@IsAchived1", 0);
            cmd.Parameters.AddWithValue("@IsDel1", 0);
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                FolderList.Add(new Folder
                {
                    ID = (Int64)rd["ID"],
                    CreatorID=(Int64)rd["creatorid"],
                    ProjectID = (Int64)rd["ProjectID"],
                    ParentID=(Int64)rd["parentid"],
                    FolderSizeinKB=(int)rd["FolderSizeinKB"],
                    SubFolderNums = (int)rd["SubFolderNums"],
                    FileNums=(int)rd["FileNums"],
                    Level=(int)rd["level"],
                    FolderName = rd["FolderName"].ToString(),
                    CDT = (DateTime)rd["CDT"]
                });
                countFolder++;
            }
            
            rd.Close();

            cmd.CommandText = "select * from Files where IsDeleted=@IsDel2 order by Extension";
            cmd.Parameters.AddWithValue("@IsDel2", 0);
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                FileList.Add(new File
                {
                    ID = (Int64)rd["ID"],
                    FolderID = (Int64)rd["FolderID"],
                    CreatorID=(Int64)rd["CreatorID"],
                    FileSizeInKB=(int)rd["FileSizeInKB"],
                    DownNums=(int)rd["DownNums"],
                    LinkNums=(int)rd["LinkNums"],
                    FileName = rd["FileName"].ToString(),
                    FilePath=rd["FilePath"].ToString(),
                    ShowName=rd["ShowName"].ToString(),
                    Extension = rd["Extension"].ToString(),
                    CDT = (DateTime)rd["CDT"]
                });
                countFile++;
            }
            rd.Close();
            conn.Close();

            if (countFile > 0 || countFolder > 0)
            {
                panel1.Visible = true;
                ErrorPanel.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                ErrorPanel.Visible = true;
            }

            Cache.Insert("Folders", FolderList);
            Cache.Insert("Files", FileList);
            InitTable(int.Parse(LabelParentID.Text), int.Parse(LabelLevel.Text));
        }
    }

    
    private void InitTable(int parentID,int level)
    {
        if (Cache["Folders"] == null || Cache["Files"] == null)
        {
            CreateCache();
        }
        NewFolders(parentID,level);
        NewFiles(parentID);

        Repeater_File.DataSource = NewFiles(parentID);
        Repeater_File.DataBind();

        Repeater_Folders.DataSource = NewFolders(parentID, level);
        Repeater_Folders.DataBind();

        rptFolder.DataSource = Move(int.Parse(moveHfLevel.Value), int.Parse(moveHfFolderID.Value));
        rptFolder.DataBind();
        InitTubiao();
    }
    
    private List<File> NewFiles(int parentID)
    {
        List<File> mFiles = new List<File>();
        List<File> AllFiles = (List<File>)Cache["Files"];
        foreach (var File in AllFiles)
        {
            if (File.FolderID == parentID)
            {
                mFiles.Add(File);
            }
        }
        return mFiles;
    }

    private List<Folder> NewFolders(int parentID,int level)
    {
        List<Folder> mFolders = new List<Folder>();
        List<Folder> AllFolders = (List<Folder>)Cache["Folders"];
        foreach (var Folder in AllFolders) {
            if (Folder.ParentID == parentID && Folder.Level == level)
            {
                mFolders.Add(Folder);
            }
        }
        return mFolders;
    }
    //更新图标样式
    private void InitTubiao()
    {
        for (int i = 0; i < Repeater_File.Items.Count; i++)
        {
            Label Lbtubiao = Repeater_File.Items[i].FindControl("Lbtubiao") as Label;
            Lbtubiao.Attributes["class"] = "glyphicon glyphicon-folder-open text-primary";

            Label LbExtension = Repeater_File.Items[i].FindControl("LbExtension") as Label;

            int Type = FileHelper.TypeOfFile(LbExtension.Text);

            if (Type==3)
            {
                Lbtubiao.Attributes["class"] = "glyphicon glyphicon-music text-primary";
            }
            else if (Type==2)
            {
                Lbtubiao.Attributes["class"] = "glyphicon glyphicon-film text-primary";
            }
            else if (Type==4)
            {
                Lbtubiao.Attributes["class"] = "glyphicon glyphicon-list-alt text-primary";
            }
            else if (Type==1)
            {
                Lbtubiao.Attributes["class"] = "glyphicon glyphicon-picture text-primary";
            }

        }
    }

    protected void btn_addFolder_Click(object sender, EventArgs e)
    {
        addFolder();
        InitTable(int.Parse(LabelParentID.Text),int.Parse(LabelLevel.Text));
    }

    private void addFolder()
    {
        if (IsRightful())
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                StringBuilder sb = new StringBuilder("Insert into Folders (ProjectID,creatorID,FolderName,IsAchived,CDT,ParentID,FolderSizeInKB,SubFolderNums,FileNums,Level,IsDeleted)");
                sb.Append(" values(@ProjectID,@creatorID,@FolderName,@IsAchived,@CDT,@ParentID,@FolderSizeInKB,@SubFolderNum,@FileNums,@mLevel,@IsDeleted)");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.AddWithValue("@ProjectID", 1);
                cmd.Parameters.AddWithValue("@creatorID", LabelProjectID.Text);
                cmd.Parameters.AddWithValue("@FolderName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@IsAchived", 0);
                cmd.Parameters.AddWithValue("@CDT", DateTime.Now);
                cmd.Parameters.AddWithValue("@ParentID", LabelParentID.Text);
                cmd.Parameters.AddWithValue("@FolderSizeInKB", 0);
                cmd.Parameters.AddWithValue("@SubFolderNum", 0);
                cmd.Parameters.AddWithValue("@FileNums", 0);
                cmd.Parameters.AddWithValue("@mLevel", LabelLevel.Text);
                cmd.Parameters.AddWithValue("@IsDeleted", 0);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();        
            }
            Cache.Remove("Folders");
            //Response.Write("缓存清除成功");
        }
        else
        {
            Response.Write("<script>alert('请输入文件名!')</script>");
        }
        TextBox1.Text = "";
    }



    private bool IsRightful() {
        bool result = true;
        if (TextBox1.Text == "") {
            result = false;
        }
        return result;
    }

    protected void Repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        TextBox tb = e.Item.FindControl("TextBox_Name") as TextBox;
        LinkButton lb1 = e.Item.FindControl("LB_Name") as LinkButton;
        LinkButton lb2 = e.Item.FindControl("LB_AlterName1") as LinkButton;
        LinkButton lb3 = e.Item.FindControl("LB_Cancel1") as LinkButton;
        Label lbid = e.Item.FindControl("LabelID") as Label;
        Label lbParentID = e.Item.FindControl("LabelParentID") as Label;
        Label lbLevel = e.Item.FindControl("LabelLevel") as Label;

        if (e.CommandName == "ChangeName")
        {
            tb.Visible = true;
            //tb.Attributes.Add("onkeyup", "onkeyup1("+source+","+e+")");
            lb1.Visible = false;
            lb2.Visible = true;
            lb3.Visible = true;

            if (e.Item.Parent.ID == "Repeater_File")
            {
                LabelFileID.Text = lbid.Text.ToString();
            }
            if (e.Item.Parent.ID == "Repeater_Folders")
            {
                LabelFolderID.Text = lbid.Text.ToString();
            }


        }
        else if (e.CommandName == "AlterName")
        {

            tb.Visible = false;
            lb1.Visible = true;
            lb2.Visible = false;
            lb3.Visible = false;
            lb1.Text = tb.Text;

            using (SqlConnection conn = new DB().GetConnection())
            {
                conn.Open();
                if (e.Item.Parent.ID == "Repeater_File")
                {
                    StringBuilder sb = new StringBuilder("update Files set ShowName=@ShowNameU where id=@idU1");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@ShowNameU", tb.Text);
                    cmd.Parameters.AddWithValue("@idU1", LabelFileID.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (e.Item.Parent.ID == "Repeater_Folders")
                {
                    StringBuilder sb = new StringBuilder("update Folders set FolderName=@FolderNameU where id=@idU2");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@FolderNameU", tb.Text);
                    cmd.Parameters.AddWithValue("@idU2", LabelFolderID.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        else if (e.CommandName == "cancel")
        {
            tb.Visible = false;
            lb1.Visible = true;
            lb2.Visible = false;
            lb3.Visible = false;
        }
        else if (e.CommandName == "DEL")
        {
            if (e.Item.Parent.ID == "Repeater_File")
            {
                LabelFileID.Text = lbid.Text.ToString();
            }
            if (e.Item.Parent.ID == "Repeater_Folders")
            {
                LabelFolderID.Text = lbid.Text.ToString();
            }
            using (SqlConnection conn = new DB().GetConnection())
            {
                conn.Open();
                if (e.Item.Parent.ID == "Repeater_File")
                {
                    StringBuilder sb = new StringBuilder("update Files set IsDeleted=@IsDelU1 where id=@idU1");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@IsDelU1", 1);
                    cmd.Parameters.AddWithValue("@idU1", LabelFileID.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (e.Item.Parent.ID == "Repeater_Folders")
                {
                    StringBuilder sb = new StringBuilder("update Folders set IsDeleted=@IsDelU2 where id=@idU2");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("@IsDelU2", 1);
                    cmd.Parameters.AddWithValue("@idU2", LabelFolderID.Text);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            CreateCache();
        }
        else if (e.CommandName == "refreshTable")
        {
            LabelLevel.Text = (int.Parse(lbLevel.Text) + 1).ToString();
            LabelParentID.Text = lbid.Text;
            InitTable(int.Parse(LabelParentID.Text), int.Parse(LabelLevel.Text));
            //Response.Write(int.Parse(lbLevel.Text) + "," + LabelParentID.Text);
        }
        else if (e.CommandName == "MOVE")
        {
            if (e.Item.Parent.ID == "Repeater_File")
            {
                moveType.Value = "File";
                moveID.Value = lbid.Text;
            }
            else if (e.Item.Parent.ID == "Repeater_Folders")
            {
                moveType.Value = "Folder";
                moveID.Value = lbid.Text;
                rptFolder.DataSource = moveFolder(1, 0, int.Parse(moveID.Value));
                rptFolder.DataBind();
            }
        }
    }

    //protected void CheckBoxALL_CheckedChanged(object sender, EventArgs e)
    //{      
    //    if (CheckBoxALL.Checked)
    //    {
    //        for (int i = 0; i < Repeater_Folders.Items.Count; i++)
    //        {
    //            CheckBox cb = Repeater_Folders.Items[i].FindControl("CheckBox") as CheckBox;
    //            cb.Checked = true;
    //        }
    //        for (int i = 0; i < Repeater_File.Items.Count; i++)
    //        {
    //            CheckBox cb = Repeater_File.Items[i].FindControl("CheckBox") as CheckBox;
    //            cb.Checked = true;
    //        }
    //        Panel2.Visible = true;
    //    }
    //    else if (CheckBoxALL.Checked == false)
    //    {
    //        for (int i = 0; i < Repeater_Folders.Items.Count; i++)
    //        {
    //            CheckBox cb = Repeater_Folders.Items[i].FindControl("CheckBox") as CheckBox;
    //            cb.Checked = false;
    //        }
    //        for (int i = 0; i < Repeater_File.Items.Count; i++)
    //        {
    //            CheckBox cb = Repeater_File.Items[i].FindControl("CheckBox") as CheckBox;
    //            cb.Checked = false;
    //        }
    //        Panel2.Visible = false;
    //    }

    //}

    public class Folder
    {
        public Int64 ID { get; set; }
        public Int64 ProjectID { get; set; }
        public Int64 CreatorID { get; set; }
        public Int64 ParentID { get; set; }
        public int FolderSizeinKB { get; set; }
        public int SubFolderNums { get; set; }
        public int FileNums { get; set; }
        public int Level { get; set; }
        public string FolderName { get; set; }
        public bool IsAchived { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CDT { get; set; }
    }

    public class File
    {
        public Int64 ID { get; set; }
        public Int64 FolderID { get; set; }
        public Int64 CreatorID { get; set; }
        public int FileSizeInKB { get; set; }
        public int LinkNums { get; set; }
        public int DownNums { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ShowName { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CDT { get; set; }
    }


    protected void LbUpload_Click(object sender, EventArgs e)
    {
        string s = LabelParentID.Text;
        Response.Redirect("File_Upload.aspx?FolderID="+s);
    }

    protected List<Folder> Move(int level,int FolderID)
    {
        List<Folder> mFolders = new List<Folder>();
        List<Folder> AllFolders = (List<Folder>)Cache["Folders"];
        foreach (var Folder in AllFolders)
        {
            if (Folder.ParentID == FolderID && Folder.Level == level)
            {
                mFolders.Add(Folder);
            }
        }
        return mFolders;
    }

    protected void rptFolder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lb1 = e.Item.FindControl("LB_Name") as LinkButton;
        HiddenField hf1 = e.Item.FindControl("hfid") as HiddenField;

        if (e.CommandName == "refreshTable")
        {
            moveHfLevel.Value = (int.Parse(moveHfLevel.Value) + 1).ToString();
            moveHfFolderID.Value = (int.Parse(hf1.Value)).ToString();
            Label1.Text = moveHfLevel.Value;
            
            if (int.Parse(moveHfLevel.Value) > 1)
            {
                Lb_Back.Visible = true;
            }
            else
            {
                Lb_Back.Visible = false;
            }
            Hf_arrFolders.Value += "," + hf1.Value;
            Label2.Text = Hf_arrFolders.Value;
            if (moveType.Value == "Folder")
            {
                rptFolder.DataSource = moveFolder(int.Parse(moveHfLevel.Value), int.Parse(hf1.Value),-1);
                rptFolder.DataBind();
            }
            else if (moveType.Value == "File")
            {
                rptFolder.DataSource = moveFolder(int.Parse(moveHfLevel.Value), int.Parse(hf1.Value),int.Parse(moveID.Value));
                rptFolder.DataBind();
            }
            
        }

        Label3.Text = moveHfFolderID.Value;
    }

    private List<Folder> moveFolder(int level, int parentID, int moveID)
    {
        if (Cache["Folders"] == null || Cache["Files"] == null)
        {
            CreateCache();
        }
        List<Folder> mFolders = new List<Folder>();
        List<Folder> AllFolders = (List<Folder>)Cache["Folders"];
        foreach (var Folder in AllFolders)
        {
            if (Folder.ParentID == parentID && Folder.Level == level && Folder.ID != moveID)
            {
                mFolders.Add(Folder);
            }
        }
        return mFolders;
    }

    protected void Lb_Back_Click(object sender, EventArgs e)
    {
        moveHfLevel.Value = (int.Parse(moveHfLevel.Value) - 1).ToString();
        int mLevel = int.Parse(moveHfLevel.Value);
        string[] arr1 = Hf_arrFolders.Value.Split(',');
        Hf_arrFolders.Value = "0";

        if (mLevel > 1)
        {
            for (int i = 1; i < arr1.Length - 1; i++)
            {
                Hf_arrFolders.Value += ","+arr1[i];
            }

        }
        moveHfFolderID.Value = arr1[mLevel - 1];

        Label2.Text = Hf_arrFolders.Value;

        Label1.Text = mLevel.ToString();
        

        if (mLevel > 1)
        {
            Lb_Back.Visible = true;
        }
        else
        {
            Lb_Back.Visible = false;
        }

        rptFolder.DataSource= moveFolder(mLevel,int.Parse(arr1[mLevel-1]),-1);
        rptFolder.DataBind();
        Label3.Text = moveHfFolderID.Value;
    }

    protected void Btn_move_Click(object sender, EventArgs e)
    {
        int id = 0;
        for (int i = 0; i < rptFolder.Items.Count; i++)
        {
            HtmlInputRadioButton radio1 = rptFolder.Items[i].FindControl("economic") as HtmlInputRadioButton;
            if (radio1.Checked == true)
            {
                id = int.Parse(radio1.Value);
                break;
            }
            else
            {
                id = int.Parse(moveHfFolderID.Value);
                Response.Write("222");
            }
        }


        Response.Write(Label3.Text = moveHfFolderID.Value);
    }
}