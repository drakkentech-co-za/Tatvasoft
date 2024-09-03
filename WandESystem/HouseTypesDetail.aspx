<%@ Page Title="W & E - House Type Details" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HouseTypesDetail.aspx.cs" Inherits="HouseTypesDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1> House Type Detail</h1>
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
                           House Type Name :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtHouseTypeName" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvHouseTypeName" runat="server" ControlToValidate="txtHouseTypeName"
                                ErrorMessage="Enter House Type Name" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save"  CausesValidation="true" OnClick="btnSave_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click"
                                ></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

