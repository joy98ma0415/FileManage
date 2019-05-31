<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FileUp.aspx.cs" Inherits="FileManage.FileUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table background="images/文件上传.jpg" style="width: 1003px; height: 658px">
        <tr>
            <td align="center" valign="bottom" style="height: 133px">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;
                  <asp:HyperLink ID="hpLinkDefault" runat="server" NavigateUrl="~/Default.aspx" ImageUrl="~/images/文件上传1.jpg">首頁</asp:HyperLink>
                &nbsp; &nbsp; &nbsp;&nbsp;<asp:HyperLink ID="hpLinkFM" runat="server" NavigateUrl="~/FilesManageList.aspx" ImageUrl="~/images/文件管理3.jpg">文件管理</asp:HyperLink></td>
        </tr>
        <tr>
            <td align="center" valign="bottom" style="height: 42px">上傳文件</td>
        </tr>
        <tr>
            <td align="center" valign="top" rowspan="2" style="height: 483px">
                <table id="tabFU" runat="server" enableviewstate="true" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnUp" runat="server" Text="上傳所有檔" OnClick="btnUp_Click" Width="94px" />
                <asp:Button ID="btnAddFU" runat="server" Text="增加上載檔" OnClick="btnAddFU_Click" Width="96px" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
</asp:Content>