<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeEdit.aspx.cs" Inherits="EmployeeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chzn-select").chosen();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h1> Employee Details</h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 15%">
                            Employee No:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList ID="ddlEmpNo" runat="server" Width="300" data-placeholder="Select Employee Number"
                                class="chzn-select" DataTextField ="IDENTIFIER" 
                                DataValueField ="IDENTIFIER" AutoPostBack="true" 
                                onselectedindexchanged="ddlEmpNo_SelectedIndexChanged">                               
                            </asp:DropDownList>
                            <asp:Label runat="server" id="lblEmpNo" Visible="false"/>
                            <asp:RequiredFieldValidator ID="rfvEmpNo" runat="server" ControlToValidate="ddlEmpNo"
                                ErrorMessage="Enter Employee Number" Text="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Name:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtEmpName" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmpName" runat="server" ControlToValidate="txtEmpName"
                                ErrorMessage="Enter Employee Name" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Surname:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtEmpSurname" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmpSurName" runat="server" ControlToValidate="txtEmpSurname"
                                ErrorMessage="Enter Employee Surname" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Email Id:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="width200" MaxLength="150">
                            </asp:TextBox>                            
                            <asp:RegularExpressionValidator ID="rgvEmial" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                ControlToValidate="txtEmail" ForeColor="Red" Text="Please Enter Valid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Type:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:RadioButtonList runat="server" ID="rdEmpType" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true" OnClick="btnSave_Click">
                            </asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
