<%@ WebHandler Language="C#" Class="Avatar_Upload" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;

public class Avatar_Upload : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
   
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        string s = context.Request["img"];
        var tmpArr = s.Split(',');
        string updir = context.Server.MapPath(@"~\Product/Users/Avatar") + "\\";
       
        byte[] bytes = Convert.FromBase64String(tmpArr[1]);
        string now = DateTime.Now.ToString("yyyyMMddHHmmss");
        string number = new Random().Next(1000).ToString();

        // 头像的路径统一为根目录下的Users/Avatars目录下，这个文件夹，已经存在，不需要检查是否存在
        string avatar = "Users/Avatar/"+ now + "-" + number + ".jpg";
        var sp=avatar.Split('/');
        //StringBuilder avatar2 = new StringBuilder("Product/Users/Avatar");
        //avatar2.Append("2");
        //System.IO.File.WriteAllBytes(@"c:\test.jpg",bytes); 
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            ms.Write(bytes, 0, bytes.Length);
            var img = Image.FromStream(ms, true);
            img.Save(string.Format("{0}{1}", updir, sp[2]));
             
        }
        //这里写数据库
        int userid = 1;
        using (SqlConnection conn = new DB().GetConnection())
        {
           
            StringBuilder sb = new StringBuilder("Update [Users] set Avatar=@Avatar");
            sb.Append(" where ID=@UserID");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.AddWithValue("@Avatar", avatar);
            cmd.Parameters.AddWithValue("@UserID", userid);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
       
      // HttpPostedFile uploadFile = context.Request.InputStream;
        //HttpFileCollection files = context.Request.Files;
        //if (files != null)
        //{
        //    for (int i = 0; i < files.Count; i++)
        //    {
               
        //        //写入文件
        //        files[i].SaveAs(updir + "/" + "123");

              

        //    }

            

            
        //}

              
        }

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}