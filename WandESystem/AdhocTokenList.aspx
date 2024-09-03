<%@ Page Title="W & E - Periods" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdhocTokenList.aspx.cs" Inherits="AdhocTokenList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Adhoc Token 
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
              <div class="form_field top_pad">
        <div class="content_block" style="border-top: 1px solid #d4d4d4">
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>                   
                    <td align="left" width="80">
                        Month-Year:
                    </td>
                    <td align="left" width="150">
                        <asp:DropDownList ID="ddlPeriodId" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td width="1%">
                        &nbsp;
                    </td>
                    <td  width="100">
                        <asp:Button Text="Search" runat="server" ID="btnSearch" OnClick="btnSearch_Click" />
                    </td>
                     <td align="right" style="float: right !important">
                            <a href="AdhocTokenDetail.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                </tr>
            </table>
        </div>
    </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="98%" cellpadding="2">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvExtraTokens" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvExtraTokens_Sorting" OnPageIndexChanging="gvExtraTokens_PageIndexChanging"
                                OnRowCommand="gvExtraTokens_RowCommand" DataKeyNames="TokenRequestId">
                                <Columns>
                                    <asp:TemplateField HeaderText="Account No" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="AccountNo" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccountNo" runat="server" Text='<% #Eval("AccountNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="15%" SortExpression="EmployeeName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Token Units" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="NoOfUnits">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfUnits" runat="server" Text='<% #Eval("NoOfUnits")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Token Number" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="TokenNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTokenNumber" runat="server" Text='<% #Eval("TokenNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meter Number" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="10%" SortExpression="MeterNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMeterNumber" runat="server" Text='<% #Eval("MeterNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Date" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="8%" SortExpression="DateIssue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateIssue" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}",Eval("DateIssue"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                ToolTip="Edit" CommandName="EditTokenRequestID" CommandArgument='<%#Eval("TokenRequestId") %>' />
                                            &nbsp;
                                            <asp:ImageButton runat="server" ID="imgDelete" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                ToolTip="Delete" CommandName="DeleteTokenRequestID" CommandArgument='<%#Eval("TokenRequestId") %>'
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
