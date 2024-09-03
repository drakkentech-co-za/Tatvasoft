<%@ Page Title="W & E - House Type" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HouseTypes.aspx.cs" Inherits="HouseTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1> House Types </h1>
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
                            Search House Type: &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="HouseTypesDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvHouseType" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" 
                                OnSorting="gvHouseType_Sorting" OnPageIndexChanging="gvHouseType_PageIndexChanging"
                                OnRowCommand="gvHouseType_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="HouseTypeId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHouseTypeId" runat="server" Text='<% #Eval("HouseTypeId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="House Type Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="80%"
                                        SortExpression="HouseTypeName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHouseTypeName" runat="server" Text='<% #Eval("HouseTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15%">
                                        <ItemTemplate >
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"  ToolTip="Edit"
                                                CommandName="EditHouseType" CommandArgument='<%#Eval("HouseTypeId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png" ToolTip="Delete"
                                                CommandName="DeleteHouseType" CommandArgument='<%#Eval("HouseTypeId") %>' OnClientClick="return confirm('Are you sure to Delete?')" />
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

