<%@ Page Title="W & E - Employees Report" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="EmployeesReport.aspx.cs" Inherits="EmployeesReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h1>Employee Detail Report</h1>
    <rsweb:ReportViewer ID="rptEmployees" runat="server" SizeToReportContent="true" Width="100%">
    </rsweb:ReportViewer>
</asp:Content>
