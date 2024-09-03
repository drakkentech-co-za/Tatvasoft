<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="EmployeeList"
    MasterPageFile="~/MasterPage.master" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h1> Employee </h1>
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
                            Search Employee : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="EmployeeEdit.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvEmployee_Sorting"
                                OnPageIndexChanging="gvEmployee_PageIndexChanging" OnRowCommand="gvEmployee_RowCommand"
                                OnRowDataBound="gvEmployee_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee No" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="10%"
                                        SortExpression="EmployeeNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpNo" runat="server" Text='<% #Eval("EmployeeNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="18%"
                                        SortExpression="EmployeeName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<% #Eval("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Surname" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"  ItemStyle-Width="18%"
                                        SortExpression="EmployeeSurname">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurname" runat="server" Text='<% #Eval("EmployeeSurname") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="EmployeeEmail">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<% #Eval("EmployeeEmail") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="EmployeeType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpType" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAccount" runat="server" Text="Linked Accounts" ToolTip="Account" 
                                                PostBackUrl='<%# Eval("EmployeeNo", "EmployeeAccountMapping.aspx?eno={0}&from=emp") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDeActive" Text="Active" OnClientClick="return confirm('Are you sure to de-activate this employee?')" CssClass="linkRed" 
                                                ToolTip="Active" CommandName="DeActiveAccount" Visible='<%# Eval("IsActive") %>' CommandArgument='<%#Eval("EmployeeId") %>'></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkActive" Text="Deactive" OnClientClick="return confirm('Are you sure to activate this employee?')"
                                                ToolTip="Deactive" CommandName="ActiveAccount" Visible='<%# Convert.ToBoolean(Eval("IsActive")) ? false :true %>' CommandArgument='<%#Eval("EmployeeId") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png" ToolTip="Edit" CommandName="EditEmployee" CommandArgument='<%#Eval("EmployeeId") %>' Visible='<%# Convert.ToInt16(Eval("EmployeeType"))==0?false:true%>'/>
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
