<%@ Page Title="W & E - Asset TYpe Detail" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="AssetTypesDetail.aspx.cs" Inherits="AssetTypesDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Asset Type Detail</h1>
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
                            Asset Type Name :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtAssetTypeName" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAssetTypeName" runat="server" ControlToValidate="txtAssetTypeName"
                                ErrorMessage="Enter Asset Type Name" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
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
