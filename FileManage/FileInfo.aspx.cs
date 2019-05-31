using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace FileManage
{
    public partial class FileInfo : System.Web.UI.Page
    {
        private CommonClass CC = new CommonClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //打開資料庫
                SqlConnection myConn = CC.GetConnection();
                myConn.Open();
                //從資料庫中獲取指定檔的資料資訊
                string sqlStr = "select fileName,fileUpDate,fileLoad from files where fileID=" + Convert.ToInt32(Request["id"].ToString());
                SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
                DataSet ds = new DataSet();
                dapt.Fill(ds, "files");
                if (ds.Tables["files"].Rows.Count > 0)
                {
                    //顯示檔的資料資訊
                    Response.Write("檔案的相關資訊");
                    Response.Write("<hr>");
                    Response.Write("檔案所在位置：" + ds.Tables["files"].Rows[0][2].ToString() + "<br>");
                    Response.Write("檔案名：" + ds.Tables["files"].Rows[0][0].ToString() + "<br>");
                    Response.Write("創建時間：" + ds.Tables["files"].Rows[0][1].ToString() + "<br>");
                    Response.Write("<hr>");
                }
                myConn.Close();
            }
        }
    }
}