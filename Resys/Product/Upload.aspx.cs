using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
//using CuteWebUI;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Upload : System.Web.UI.Page
{
    string UserID = "0";
    //存储允许上传的资源的后缀名，key=Extension，value=TypeName
    Dictionary<string, string> ResourceTypes = new Dictionary<string, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserID = Session["UserID"].ToString();
        UserID = 123.ToString();
        string _targDir =DateTime.Now.ToString("yyyyMM");
        string basePath = Server.MapPath("~/Uploads/" + _targDir);
        string FileNames = "";
        HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
        
        //如果目录不存在，则创建目录 
        if (files != null) {
            if (!Directory.Exists(basePath)) {
                //创建文件夹
                Directory.CreateDirectory(basePath);
            }
            
            for (int i = 0; i < files.Count; i++) {
                //string ResourceName = "1a2b3c4d5e6f7d8e9f10g11h12i13j14k15l16m17n18o19p20q";
                //是否为公开资源
                //string IsPublic = Session["IsPublic"].ToString();
                //虚拟存放目录ID
                //string UntrueFoldersID = Session["UntrueFoldersID"].ToString();
                //文件原名称
                string OldFileName = files[i].FileName;
                //文件大小（字节为单位）
                int Size = files[i].ContentLength;
                string size;
                if (Size > 1024)
                {
                    Size /= 1024;
                    size = Size.ToString();
                }
                else {
                    size = "0";
                }
                string fileSize=size;
                //文件后缀名
                string Extentsion = Path.GetExtension(files[i].FileName).ToLower();
                //自动命名文件名称
                string fileName = UserID+ "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + new Random().Next(1000, 10000).ToString()  + Extentsion;
                FileNames += "," + fileName;
                string FT = Extentsion.Substring(1);
                //文件上传保存物理路径
                string filePath = "Uploads" + "/" + _targDir + "/" + fileName;
                //文件类型  FileType
                
                using (SqlConnection conn = new DB().GetConnection())
                {
                   
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandText = "select * from ResourceTypes where Extension=@Extension";
                    cmd.Parameters.AddWithValue("@Extension", FT);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                       Label1.Text= rd["TypeName"].ToString();
                    }
                    
                    rd.Close();
                    conn.Close();
                }
                String fileType = Label1.Text;


                //写入文件
                files[i].SaveAs(basePath +"/"+fileName);
                using (SqlConnection conn = new DB().GetConnection())
                {
                    StringBuilder sb = new StringBuilder("Insert into files(FileName,ShowName,FilePath,FileSizeInKB,FileType,Extension,FolderID,CreatorID,CDT,LinkNums,DownNums,IsDeleted)");
                    sb.Append(" values(@FileName,@ShowName,@FilePath,@FileSizeInKB,@FileType,@Extension,@FolderID,@CreatorID,@CDT,@LinkNums,@DownNums,@IsDeleted)");
                    SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                    cmd.Parameters.AddWithValue("FileName", fileName);
                    cmd.Parameters.AddWithValue("@ShowName", OldFileName);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@FileSizeInKB", fileSize);
                    cmd.Parameters.AddWithValue("@FileType", fileType);
                    cmd.Parameters.AddWithValue("@Extension", Extentsion);
                    cmd.Parameters.AddWithValue("@FolderID",1 );
                    cmd.Parameters.AddWithValue("@CreatorID",UserID );
                    cmd.Parameters.AddWithValue("@CDT",DateTime.Now );
                    cmd.Parameters.AddWithValue("@LinkNums",0 );
                    cmd.Parameters.AddWithValue("@DownNums", 0);
                    cmd.Parameters.AddWithValue("@IsDeleted", 0);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                    //插入成功

                }

            }
            FileHelper.SetFileName(FileNames);

        }
    }
}