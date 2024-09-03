<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdhocTokenDetail.aspx.cs" Inherits="AdhocTokenDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <link href="ChoosenJS/jquery-ui.css" rel="stylesheet" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
        });
        $(function () {
            $("#<%= txtIssueDate.ClientID %>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Adhoc Token Issue
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="0" width="100%" cellpadding="10">
                    <tr>
                        <td align="left" style="width: 15%">Account No:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList ID="ddlAccountNo" runat="server" Width="300" data-placeholder="Select Account Number"
                                class="chzn-select" DataTextField="AccountNo" DataValueField="HouseId"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblAccountNo" Visible="false" />
                            &nbsp;
                            <asp:Label ID="lblAddress" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvAccountNo" runat="server" ControlToValidate="ddlAccountNo"
                                ErrorMessage="Enter Account Number" Text="*" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Employee:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlEmployeeId" runat="server" Width="300" data-placeholder="Select Employee"
                                class="chzn-select" DataTextField="EmployeeNo" DataValueField="EmployeeId" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeId_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Label ID="lblEmployeeName" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ControlToValidate="ddlEmployeeId"
                                ErrorMessage="Enter Employee ID" Text="*" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Month-Year:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px" data-placeholder="Select Month-Year"
                                class="chzn-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPeriodId" runat="server" ControlToValidate="ddlPeriodId"
                                ErrorMessage="Enter Employee ID" Text="*" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Token Units:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTokenUnits" runat="server" CssClass="width200">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTokenUnits" runat="server" ControlToValidate="txtTokenUnits"
                                ErrorMessage="Enter Token Units" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Token Number:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTokenNumber" runat="server" CssClass="width200">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvERFSize" runat="server" ControlToValidate="txtTokenNumber"
                                ErrorMessage="Enter Token Number" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Meter No:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMeterNo" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMeterNo" runat="server" ControlToValidate="txtMeterNo"
                                ErrorMessage="Enter Meter No" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">Issue Date:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="width130" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIssueDate" runat="server" ControlToValidate="txtIssueDate"
                                ErrorMessage="Enter Issue Date" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnImport" runat="server" Text="Issue" CausesValidation="true"
                                OnClick="btnImport_Click" OnClientClick='return confirm("Are you sure you want to issue this token to selected account?");'></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
