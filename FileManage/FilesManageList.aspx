<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilesManageList.aspx.cs" Inherits="FileManage.FilesManageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table width="100%" align="center" background="images/文件上传.jpg" style="width: 1003px; height: 658px">
            <tr>
                <td style="height: 105px">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" style="height: 45px" valign="top">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp;&nbsp; &nbsp;<asp:HyperLink ID="hpLinkDefault" runat="server" NavigateUrl="~/Default.aspx" ImageUrl="~/images/文件上传1.jpg">首页</asp:HyperLink>
                    &nbsp; &nbsp;
                  <asp:HyperLink ID="hpLinkUpF" runat="server" NavigateUrl="~/FileUp.aspx" ImageUrl="~/images/文件上传2.jpg">上传文件</asp:HyperLink></td>
            </tr>
            <tr>
                <td align="center" style="height: 35px" valign="top">文件管理</td>
            </tr>
            <tr>
                <td valign="top" style="height: 78px">
                    <table width="100%" style="font-size: 9pt" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td align="right" style="width: 455px">文件名稱關鍵字：</td>
                            <td align="left">
                                <asp:TextBox ID="txtFilesName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 455px;">創建時間：</td>
                            <td align="left">
                                <asp:DropDownList ID="ddlUD" runat="server" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 455px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 455px;"></td>
                            <td align="left">
                                <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" align="center" style="font-size: 9pt; height: 395px;" valign="top">
                    <asp:GridView ID="gvFiles" runat="server" Width="700px" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" OnPageIndexChanging="gvFiles_PageIndexChanging" OnRowDataBound="gvFiles_RowDataBound" DataKeyNames="filesID" Height="155px">
                        <Columns>
                            <asp:BoundField DataField="fileID" HeaderText="文件編號">
                                <ControlStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fileName" HeaderText="檔案名稱">
                                <ControlStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fileUpDate" HeaderText="創建時間">
                                <ControlStyle Font-Size="Small" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="永久刪除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/images/删除.gif" OnClick="imgbtnDelete_Click" />
                                </ItemTemplate>
                                <ControlStyle Font-Size="Small" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="文件下載">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDF" runat="server" ImageUrl="~/images/文件下载.gif" OnClick="imgbtnDF_Click" />
                                </ItemTemplate>
                                <ControlStyle Font-Size="Small" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>