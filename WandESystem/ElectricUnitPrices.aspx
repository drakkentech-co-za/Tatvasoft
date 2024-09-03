<%@ Page Title="W & E - Electric Unit Prices" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ElectricUnitPrices.aspx.cs" Inherits="ElectricUnitPrices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>
        Electric Unit Prices
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
                            Search Electric Unit Prices : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="ElectricUnitPricesDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvUnitPrice" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvUnitPrice_Sorting"
                                OnPageIndexChanging="gvUnitPrice_PageIndexChanging" OnRowCommand="gvUnitPrice_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="ElectricUnitPriceId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblElectricUnitPriceId" runat="server" Text='<% #Eval("ElectricUnitPriceId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="15%" SortExpression="StartDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<% #DataBinder.Eval(Container.DataItem, "StartDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="15%" SortExpression="EndDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<% #DataBinder.Eval(Container.DataItem, "EndDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Electric Unit Price" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="20%" SortExpression="UnitPrice">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("UnitPrice"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditUnitPrice" CommandArgument='<%#Eval("ElectricUnitPriceId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteUnitPrice" CommandArgument='<%#Eval("ElectricUnitPriceId") %>'
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

