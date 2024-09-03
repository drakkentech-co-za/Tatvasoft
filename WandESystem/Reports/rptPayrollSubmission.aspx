<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="rptPayrollSubmission.aspx.cs" Inherits="Reports_rptPayrollSubmission" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.2.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                    <tr>
                        <td>
                            <div class="form_field top_pad">
                                <div class="titlebg">
                                    <h2>
                                        Report Filters
                                    </h2>
                                </div>
                                <div class="content_block">
                                    <table border="0" cellspacing="2" width="100%" cellpadding="2">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="left">
                                                            Month-Year:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            Employee Type:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList runat="server" ID="ddlEmpType" CssClass="width130">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button runat="server" ID="btnShowReport" Text="Show Report" CausesValidation="false"
                                                                OnClick="btnShowReport_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form_field top_pad">
                                <div class="titlebg">
                                    <h2>
                                        Payroll submission
                                    </h2>
                                </div>
                                <div class="content_block" style="padding:0">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td align="center" valign="top">
                                                <asp:UpdatePanel ID="udp" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnShowReport" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%">
                                                        </rsweb:ReportViewer>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="UpdatePanelloading" id="divProgress" style="width: 100%; height: 100%;">
                <table id="ProgressTable" cellpadding="0" cellspacing="0" border="0" style="width: 100%;
                    height: 100%;">
                    <tr align="center" valign="middle">
                        <td class="LoadingText" align="center" valign="middle">
                           Please Wait..
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
