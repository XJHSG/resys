using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class WebUC_CategoryName : System.Web.UI.UserControl
{
    public string CategoryID { set; get; }  

    protected void Page_Load(object sender, EventArgs e)
    {
        int CID = Convert.ToInt16(CategoryID);
        if (!IsPostBack)
        {
            using (SqlConnection conn = new DB().GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from WebCategories where ID = @ID";
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", CategoryID);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read()) 
                {
                    Label1.Text = rd["CategoryName"].ToString();
                }
                rd.Close();
            }
        }

    }
}