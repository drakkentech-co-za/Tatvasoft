<%@ Page Title="W & E - Periods" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PeriodMonthMappingList.aspx.cs" Inherits="PeriodMonthMappingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Period Month Mapping
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
                            Search Period : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="PeriodMonthMappingEdit.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvPeriods" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvPeriods_Sorting" OnPageIndexChanging="gvPeriods_PageIndexChanging"
                                OnRowCommand="gvPeriods_RowCommand">
                                <Columns>                                   
                                    <asp:TemplateField HeaderText="Period Id" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="20%" SortExpression="PeriodId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriodId" runat="server" Text='<% #Eval("PeriodId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="40%" SortExpression="Month">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Convert.ToDateTime("01/"+Eval("Month").ToString()+"/2000").ToString("MMMM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="30%" SortExpression="Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" runat="server" Text='<% #Eval("Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditPeriod" CommandArgument='<%#Eval("PeriodMonthId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeletePeriod" CommandArgument='<%#Eval("PeriodMonthId") %>'
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
