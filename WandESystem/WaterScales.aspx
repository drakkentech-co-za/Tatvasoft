<%@ Page Title="W & E - Water Scales" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="WaterScales.aspx.cs" Inherits="WaterScales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Water Scale
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
                            Search Water Scale : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="WaterScalesDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvWaterScale" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvWaterScale_Sorting"
                                OnPageIndexChanging="gvWaterScale_PageIndexChanging" OnRowCommand="gvWaterScale_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="WaterScaleId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWaterScaleId" runat="server" Text='<% #Eval("WaterScaleId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date From" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="15%" SortExpression="DateFrom">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateFrom" runat="server" Text='<% #DataBinder.Eval(Container.DataItem, "DateFrom", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date To" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="15%" SortExpression="DateTo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTo" runat="server" Text='<% #DataBinder.Eval(Container.DataItem, "DateTo", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Limit" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="20%" SortExpression="Limit">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("Limit"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Multiplication Factor" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-VerticalAlign="Middle" ItemStyle-Width="20%" SortExpression="MultiplicationFactor">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.0000}", Eval("MultiplicationFactor"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditWaterScale" CommandArgument='<%#Eval("WaterScaleId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteWaterScale" CommandArgument='<%#Eval("WaterScaleId") %>'
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
