using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Uploads_File_Upload : System.Web.UI.Page
{
    string FolderID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FolderID = Request.Params["FolderID"];
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string FNText = FileHelper.GetFileName();
        string[] array = FNText.Split(',');
        int k = array.Length;
        int s = k - 1;
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            for (int num = 0; num < k; num++)
            {
                string sql = "update Files set FolderID=@FolderID where FileName=@FileName";
                cmd = new SqlCommand(sql.ToString(), conn);
                cmd.Parameters.AddWithValue("@FolderID", FolderID);
                cmd.Parameters.AddWithValue("@FileName", array[num]);
                cmd.ExecuteNonQuery();
            }
            FileHelper.CleanFileName();
            conn.Close();
        }
        Response.Redirect("File_Man.aspx");
    }
}