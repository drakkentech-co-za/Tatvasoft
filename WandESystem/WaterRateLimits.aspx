<%@ Page Title="W & E - Water Rate Limits" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="WaterRateLimits.aspx.cs" Inherits="WaterRateLimits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Water Rate Limits
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
                            Search Water Rate Limit : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="WaterRateLimitsDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvWaterRate" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvWaterRate_Sorting"
                                OnPageIndexChanging="gvWaterRate_PageIndexChanging" OnRowCommand="gvWaterRate_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="WaterRateLimitId" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle" ItemStyle-Width="0%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWaterRateLimitId" runat="server" Text='<% #Eval("WaterRateLimitId") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Rate1" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="Rate1">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("Rate1"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate2" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="Rate2">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("Rate2"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate3" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="Rate3">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("Rate3"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="House Type" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="HouseTypeName" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHouseTypeName" runat="server" Text='<% #Eval("HouseTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditWaterRate" CommandArgument='<%#Eval("WaterRateLimitId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteWaterRate" CommandArgument='<%#Eval("WaterRateLimitId") %>'
                                                OnClientClick="return confirm('It will update all existing limit records. Are you sure you want to continue with your change?')" />
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
