<%@ Page Title="W & E - Tariffs" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Teriffs.aspx.cs" Inherits="Teriffs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Tariffs
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
                            Search Tariffs : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="TeriffsDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvTeriffs" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvTeriffs_Sorting" OnPageIndexChanging="gvTeriffs_PageIndexChanging"
                                OnRowCommand="gvTeriffs_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="TariffId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTeriffId" runat="server" Text='<% #Eval("TeriffId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tariff Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="40%" SortExpression="CategoryName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<% #Eval("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tariff Id" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="20%" SortExpression="CategoryValue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryValue" runat="server" Text='<% #Eval("CategoryValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Include VAT" SortExpression="bIncludeVAT" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <%#Convert.ToString(Eval("bIncludeVAT")) == "True" ?  "Yes" : "No" %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tariff Type" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                         SortExpression="CategoryType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryType" runat="server" Text='<%# General.FindCategoryType(Convert.ToInt32(Eval("CategoryType"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditTeriffs" CommandArgument='<%#Eval("TeriffId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteTeriffs" CommandArgument='<%#Eval("TeriffId") %>'
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
