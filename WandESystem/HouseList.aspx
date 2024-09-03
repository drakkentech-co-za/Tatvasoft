<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HouseList.aspx.cs" Inherits="HouseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Accounts
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="2" width="98%" cellpadding="2">
                    <tr>
                        <td align="left" style="width: 86%">Search Account : &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="width200" MaxLength="100"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td style="float: right !important">
                            <a href="HouseEdit.aspx" class="add-link"><span class="notify">Add</span></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:GridView ID="gvHouse" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="false" SkinID="GridView" OnSorting="gvHouse_Sorting" OnPageIndexChanging="gvHouse_PageIndexChanging"
                                OnRowCommand="gvHouse_RowCommand" OnRowDataBound="gvHouse_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Account No" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="AccountNo" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccountNo" runat="server" Text='<% #Eval("AccountNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="Address1" ItemStyle-Width="13%" HeaderStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress1" runat="server" Text='<% # Eval("Address1") + " " + Eval("Address2")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ERF No." ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="ERFNo" ItemStyle-Width="5%" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblERFNo" runat="server" Text='<% #Eval("ERFNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ERF Size" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="ERFSize" ItemStyle-Width="7%" HeaderStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblERFSizee" runat="server" Text='<%# Convert.ToString(float.Parse(Eval("ERFSize").ToString())) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset Type" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="AssetTypeName" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssetType" runat="server" Text='<% #Eval("AssetTypeName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Electricity Type" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="ElectricityType" ItemStyle-Width="7%"
                                        HeaderStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblElectricityType" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PrePaid Allowed" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle" SortExpression="PrepaidAllowed" ItemStyle-Width="5%"
                                        HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrePaidAllowed" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Electric Unit" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="ElectricUnits" ItemStyle-Width="6%" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblElectricUnits" runat="server" Text='<% #Eval("ElectricUnits")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Meter No" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        SortExpression="MeterNo" ItemStyle-Width="12%" HeaderStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMeterNo" runat="server" Text='<% #Eval("MeterNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rental Amount" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="8%" SortExpression="RentalAmount">
                                        <ItemTemplate>
                                            <%# String.Format("{0:0.00}", Eval("RentalAmount"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                        ItemStyle-Width="6%" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton Text="Linked Tariffs" runat="server" ID="lnkCategories" CommandName="EditCategories"
                                                CommandArgument='<%#Eval("HouseId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEmployee" runat="server" Text="Linked Employee" ToolTip="Employee"
                                                PostBackUrl='<%# Eval("EmployeeNo", "EmployeeAccountMapping.aspx?eno={0}&from=acc") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkDeActive" Text="Active" OnClientClick="return confirm('Are you sure to de-activate this account?')" CssClass="linkRed" 
                                                ToolTip="Active" CommandName="DeActiveAccount" Visible='<%# Eval("IsActive") %>' CommandArgument='<%#Eval("HouseId") %>'></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkActive" Text="Deactive" OnClientClick="return confirm('Are you sure to activate this account?')"
                                                ToolTip="Deactive" CommandName="ActiveAccount" Visible='<%# Convert.ToBoolean(Eval("IsActive")) ? false :true %>' CommandArgument='<%#Eval("HouseId") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="6%" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEdit" ImageUrl="~/App_Themes/Default/Images/edit.png" ToolTip="Edit" CommandName="EditHouse" CommandArgument='<%#Eval("HouseId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IsRental" SortExpression="IsRental" HeaderText="IsRental" Visible="false" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
