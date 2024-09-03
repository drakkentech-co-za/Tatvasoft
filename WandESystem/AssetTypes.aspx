<%@ Page Title="W & E - Asset Types" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="AssetTypes.aspx.cs" Inherits="AssetTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Asset Types
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="98%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 86%">
                            Search Asset Type: &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="AssetTypesDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvAssetType" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvAssetType_Sorting"
                                OnPageIndexChanging="gvAssetType_PageIndexChanging" OnRowCommand="gvAssetType_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="AssetTypeId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssetTypeId" runat="server" Text='<% #Eval("AssetTypeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset Type Name" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" ItemStyle-Width="80%" SortExpression="AssetTypeName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssetTypeName" runat="server" Text='<% #Eval("AssetTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png" Visible='<%# Convert.ToBoolean(Eval("IsDefault")) ? false : true%>'
                                                ToolTip="Edit" CommandName="EditAssetType" CommandArgument='<%#Eval("AssetTypeId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png" Visible='<%# Convert.ToBoolean(Eval("IsDefault")) ? false : true%>'
                                                ToolTip="Delete" CommandName="DeleteAssetType" CommandArgument='<%#Eval("AssetTypeId") %>'
                                                OnClientClick="return confirm('Are you sure to Delete?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
