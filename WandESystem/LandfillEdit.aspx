<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LandfillEdit.aspx.cs" Inherits="LandfillEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Landfill Details</h1>
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
                            PeriodId :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Landfill Amount :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtLandfillAmount" runat="server" CssClass="width200" MaxLength="8" onkeypress="return BlockNonNumbers(this,event,true,false)">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvERFSize" runat="server" ControlToValidate="txtLandfillAmount"
                                ErrorMessage="Enter Landfill Amount" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
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
