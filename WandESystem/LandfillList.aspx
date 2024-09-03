<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LandfillList.aspx.cs" Inherits="LandfillList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Landfill
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
                        <td style="float: right !important" colspan="2">
                            <a href="LandfillEdit.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvLandfill" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvLandfill_Sorting"
                                OnPageIndexChanging="gvLandfill_PageIndexChanging" OnRowCommand="gvLandfill_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Period" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="Period" ItemStyle-Width="13%" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriod" runat="server" Text='<% # Eval("Period")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Landfill Amount" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="LandfillAmount" ItemStyle-Width="5%"
                                        HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLandfillAmount" runat="server" Text='<% #Eval("LandfillAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="7%" HeaderStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditLandfill" CommandArgument='<%#Eval("LandfillId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteLandfill" CommandArgument='<%#Eval("LandfillId") %>'
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
