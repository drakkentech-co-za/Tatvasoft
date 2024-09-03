<%@ Page Title="W & E - Tariffs Detail" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="TeriffsDetail.aspx.cs" Inherits="TeriffsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Tariff Detail
    </h1>
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
                            Tariff Name :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="width200" MaxLength="150">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="txtCategoryName"
                                ErrorMessage="Enter Category From" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Tariff Id :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtCategoryValue" runat="server" CssClass="width200" MaxLength="15">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCategoryValue" runat="server" ControlToValidate="txtCategoryValue"
                                ErrorMessage="Enter Category Value" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Tariff Type:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList runat="server" ID="ddlCategoryType" CssClass="width210">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Include VAT :
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:CheckBox runat="server" ID="chkIncludeVAT" />
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
