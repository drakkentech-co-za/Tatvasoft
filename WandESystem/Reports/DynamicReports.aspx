<%@ Page Title="W & E - Dynamic Reports" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="DynamicReports.aspx.cs" Inherits="DynamicReports" %>

    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Dynamic Reports
    </h1>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMessage"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form_field top_pad">
                    <div class="titlebg">
                        <h2>
                            Details
                        </h2>
                    </div>
                    <div class="content_block">
                        <table border="0" cellspacing="2" width="100%" cellpadding="2">
                            <tr>
                                <td align="left">
                                    Server Path :
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblServerPath"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Folder Path :
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblFolderPath"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="10%">
                                    Reports :
                                </td>
                                <td align="left" width="50%">
                                    <asp:DropDownList runat="server" ID="ddlReports" CssClass="width200">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnGenerate" Text="Generate" 
                                        onclick="btnGenerate_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" Height="500px">
                                                        </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
