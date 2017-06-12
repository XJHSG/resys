using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;

/// <summary>
///DB 的摘要说明
/// </summary>
public class DB
{
	public DB()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public SqlConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        return conn;
    }
    
}
