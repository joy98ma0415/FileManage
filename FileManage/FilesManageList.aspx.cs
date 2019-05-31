using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FileManage
{
    public partial class FilesManageList : System.Web.UI.Page
    {
        private CommonClass CC = new CommonClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLBind();//綁定檔創建時間
                AllGVBind();//綁定所檔資訊
                this.ddlUD.Items.Insert(0, "請選擇...");
            }
        }

        private void AllGVBind()
        {
            //湖羲迵杅擂踱腔蟀諉
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            //脤戙趼睫揹
            SqlDataAdapter dapt = new SqlDataAdapter("select * from files", myConn);
            DataSet ds = new DataSet();
            //沓喃杅擂摩
            dapt.Fill(ds, "files");
            //堂隅杅擂諷璃
            this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
            this.gvFiles.DataKeyNames = new string[] { "fileID" };
            this.DataBind();
            //庋溫梩蚚腔訧埭
            ds.Dispose();
            dapt.Dispose();
            myConn.Close();
        }

        private void DDLBind()
        {
            //湖羲迵杅擂踱腔蟀諉
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            //脤戙恅璃斐膘奀潔
            SqlDataAdapter dapt = new SqlDataAdapter("select distinct fileUpDate from files", myConn);
            DataSet ds = new DataSet();
            //沓喃杅擂摩
            dapt.Fill(ds, "files");
            //堂隅狟嶺粕等
            this.ddlUD.DataSource = ds.Tables["files"].DefaultView;
            this.ddlUD.DataTextField = ds.Tables["files"].Columns[0].ToString();
            this.ddlUD.DataBind();
            //庋溫梩蚚腔訧埭
            ds.Dispose();
            dapt.Dispose();
            myConn.Close();
        }

        //堂隅刲坰善腔恅璃陓洘
        protected void PartGVBind()
        {
            //湖羲迵杅擂踱腔蟀諉
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            //脤戙睫磁刲坰沭璃腔趼睫揹
            string sqlStr = "select * from files";
            if (this.txtFilesName.Text.Trim() != "" || ddlUD.SelectedIndex != 0)
            {
                sqlStr += " where ";
                if (this.txtFilesName.Text.Trim() != "" && ddlUD.SelectedIndex == 0)
                {
                    sqlStr += "fileName like'%" + this.txtFilesName.Text.Trim() + "%'";
                }
                else if (this.txtFilesName.Text.Trim() == "" && ddlUD.SelectedIndex != 0)
                {
                    sqlStr += "fileUpDate= '" + this.ddlUD.SelectedValue.ToString() + "'";
                }
                else
                {
                    sqlStr += "fileUpDate='" + this.ddlUD.SelectedValue.ToString() + "'";
                    sqlStr += "  and fileName like'%" + this.txtFilesName.Text.Trim() + "%'";
                }
            }
            SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
            DataSet ds = new DataSet();
            //沓喃杅擂摩
            dapt.Fill(ds, "files");
            //堂隅杅擂諷璃
            this.gvFiles.DataSource = ds.Tables["files"].DefaultView;
            this.gvFiles.DataKeyNames = new string[] { "fileID" };
            this.DataBind();
            //庋溫梩蚚腔訧埭
            ds.Dispose();
            dapt.Dispose();
            myConn.Close();
        }

        public static int IntIsSearch;//瓚剿岆瘁眒萸僻賸刲坰偌聽

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PartGVBind();
            IntIsSearch = 1;
        }

        protected void gvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFiles.PageIndex = e.NewPageIndex;
            if (IntIsSearch == 1)
            {
                PartGVBind();
            }
            else
            {
                AllGVBind();
            }
        }

        protected void gvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //扷梓痄雄善GridView諷璃腔砩俴奀ㄛ蜆俴赻雄曹傖硌隅晇伎
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#BEC9F6';this.style.color='buttontext';this.style.cursor='default';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';this.style.color=''");
                //邧僻俴湖羲陔珜
                e.Row.Attributes.Add("ondblclick", "window.open('FileInfo.aspx?id=" + e.Row.Cells[0].Text + "')");
            }
        }

        protected void DeleteTFN(string sqlStr)
        {
            //湖羲杅擂踱
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
            DataSet ds = new DataSet();
            dapt.Fill(ds, "files");
            //鳳硌隅恅璃腔繚噤
            string strFilePath = Server.MapPath("Files/") + ds.Tables["files"].Rows[0][0].ToString();
            //覃蚚File濬腔Delete源楊ㄛ刉壺硌隅恅璃
            File.Delete(strFilePath);
            ds.Dispose();
            myConn.Close();
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //鳳imgbtnDelete腔ImageButton勤砓
            ImageButton imgbtn = (ImageButton)sender;
            //竘蚚imgbtnDelete諷璃腔虜諷璃奻珨撰諷璃
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            //鳳恅璃淩妗俷靡
            string sqlStr = "select fileTrueName from files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //婓恅璃標Files狟ㄛ刉壺蜆恅璃
            DeleteTFN(sqlStr);
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            //植杅擂踱笢刉壺蜆恅璃陓洘
            string sqlDelStr = "delete from files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            SqlCommand myCmd = new SqlCommand(sqlDelStr, myConn);
            myCmd.ExecuteNonQuery();
            myCmd.Dispose();
            myConn.Close();
            //笭陔堂隅
            if (IntIsSearch == 1)
            {
                PartGVBind();
            }
            else
            {
                AllGVBind();
            }
        }

        protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
        {
            //鳳imgbtnDelete腔ImageButton勤砓
            ImageButton imgbtn = (ImageButton)sender;
            //竘蚚imgbtnDelete諷璃腔虜諷璃奻珨撰諷璃
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            //鳳恅璃淩妗俷靡
            string sqlStr = "select fileTrueName from files where fileID='" + gvFiles.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //湖羲杅擂踱
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
            DataSet ds = new DataSet();
            dapt.Fill(ds, "files");
            //鳳恅璃繚噤
            string strFilePath = Server.MapPath("Files//" + ds.Tables["files"].Rows[0][0].ToString());
            ds.Dispose();
            myConn.Close();
            ////狟婥硌隅腔恅璃
            //if (File.Exists(strFilePath))
            //{
            //    Response.Clear();
            //    Response.ClearHeaders();
            //    Response.Buffer = false   ;
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFilePath, System.Text.Encoding.UTF8));
            //    Response.AppendHeader("Content-Length", strFilePath.Length.ToString());
            //    Response.WriteFile(strFilePath);
            //    Response.Flush();
            //    Response.End();
            //}
            //狟婥硌隅腔恅璃
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
        }
    }
}