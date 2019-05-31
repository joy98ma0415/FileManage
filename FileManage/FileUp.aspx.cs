using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace FileManage
{
    public partial class FileUp : System.Web.UI.Page
    {
        private CommonClass CC = new CommonClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SaveFUC();
            }
        }

        private void SaveFUC()
        {
            //創建動態增加陣列
            ArrayList AL = new ArrayList();
            foreach (Control C in tabFU.Controls)
            {
                if (C.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlTableRow")
                {
                    HtmlTableCell HTC = (HtmlTableCell)C.Controls[0];
                    foreach (Control FUC in HTC.Controls)
                    {
                        //判斷該控制項是否為上傳控制項（FileUpLoad），如果是，則添加到ArrayList中
                        if (FUC.GetType().ToString() == "System.Web.UI.WebControls.FileUpload")
                        {
                            FileUpload FU = (FileUpload)FUC;
                            AL.Add(FU);
                        }
                    }
                }
            }
            //將保存在陣列ArrayList中的所有上傳控制項（FileUpLoad），添加到緩存中，命名為“FilesControls”
            Session.Add("FilesControls", AL);
        }

        protected void InsertFUC()
        {
            ArrayList AL = new ArrayList();
            //清空表格tabFU中原有的上傳控制項
            this.tabFU.Rows.Clear();
            //調用GetFUCInfo方法，將存放在緩存中的上傳控制項添加到表格tabFU中
            GetFUCInfo();
            //在表格tabFU中添加一個上傳控制項
            HtmlTableRow HTR = new HtmlTableRow();
            HtmlTableCell HTC = new HtmlTableCell();
            HTC.Controls.Add(new FileUpload());
            HTR.Controls.Add(HTC);
            tabFU.Rows.Add(HTR);
            //調用SaveFUC方法，將添加的上傳控制項保存在緩存中
            SaveFUC();
        }

        /// <summary>
        /// 用於讀取緩存中存儲的上傳檔控制項集
        /// </summary>
        protected void GetFUCInfo()
        {
            ArrayList AL = new ArrayList();
            //判斷緩存中是否已存在上傳控制項
            if (Session["FilesControls"] != null)
            {
                //將緩存中的上傳控制項集存放在資料集ArrayList中
                AL = (System.Collections.ArrayList)Session["FilesControls"];
                for (int i = 0; i < AL.Count; i++)
                {
                    HtmlTableRow HTR = new HtmlTableRow();
                    HtmlTableCell HTC = new HtmlTableCell();
                    HTC.Controls.Add((System.Web.UI.WebControls.FileUpload)AL[i]);
                    HTR.Controls.Add(HTC);
                    //將上傳控制項添加到名為tabFU表格中
                    tabFU.Rows.Add(HTR);
                }
            }
        }

        /// <summary>
        /// 用於執行檔上傳操作
        /// </summary>
        //檔是否上傳（1：上傳成功，0：文件未被上傳）
        public static int IntIsUF = 0;

        private void UpFile()
        {
            //獲取檔保存的路徑
            string FilePath = Server.MapPath(".//") + "Files";
            //獲取由用戶端上載檔的控制項集合
            HttpFileCollection HFC = Request.Files;
            for (int i = 0; i < HFC.Count; i++)
            {
                //對用戶端已上載的單獨檔的訪問
                HttpPostedFile UserHPF = HFC[i];
                try
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //調用GetAutoID方法獲取上傳檔自動編號
                        int IntFieldID = CC.GetAutoID("fileID", "files");
                        //檔的真實名（格式：[檔編號]上傳檔案名）
                        //用於實現上傳多個相同檔時，原有檔不被覆蓋
                        DateTime dt = DateTime.Now;
                        string dtstr = dt.ToString("yyyy年MM月dd日");
                        string strFileTName = "[" + IntFieldID + "]" + System.IO.Path.GetFileName(UserHPF.FileName);
                        //定義插入字串，將上傳檔資訊保存在資料庫中
                        string sqlStr = "insert into files(fileID,fileName,fileLoad,fileUpDate,fileTrueName)";
                        sqlStr += "values('" + IntFieldID + "'";
                        sqlStr += ",'" + System.IO.Path.GetFileName(UserHPF.FileName) + "'";
                        sqlStr += ",'" + FilePath + "'";
                        sqlStr += ",'" + dtstr + "'";
                        sqlStr += ",'" + strFileTName + "')";
                        //打開與資料庫的連接
                        SqlConnection myConn = CC.GetConnection();
                        myConn.Open();
                        SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
                        myCmd.ExecuteNonQuery();
                        myCmd.Dispose();
                        myConn.Dispose();
                        //將上傳的文件存放在指定的資料夾中
                        UserHPF.SaveAs(FilePath + "//" + strFileTName);
                        IntIsUF = 1;
                    }
                }
                catch
                {
                    //檔上傳失敗，清空緩存中的上傳控制項集，重新載入上傳頁面
                    if (Session["FilesControls"] != null)
                    {
                        Session.Remove("FilesControls");
                    }
                    Response.Write(CC.MessageBox("處理出錯！", "FileUp.aspx"));
                    return;
                }
            }
            //當檔上傳成功或者沒有上傳檔，都需要清空緩存中的上傳控制項集，重新載入上傳頁面
            if (Session["FilesControls"] != null)
            {
                Session.Remove("FilesControls");
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
                Response.Write(CC.MessageBox("上傳成功！", "FileUp.aspx"));
            }
            else
            {
                Response.Write(CC.MessageBox("請選擇上傳檔！", "FileUp.aspx"));
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            //執行上傳檔
            UpFile();
        }

        protected void btnAddFU_Click(object sender, EventArgs e)
        {
            //執行添加上傳控制項方法
            InsertFUC();
        }
    }
}