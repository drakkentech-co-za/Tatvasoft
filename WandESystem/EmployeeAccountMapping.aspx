<%@ Page Title="W &  E - Employee Account Mapping" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="EmployeeAccountMapping.aspx.cs" Inherits="EmployeeAccountMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            choosen();
        });

        function choosen() {
            $(".chzn-select").chosen();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="UpdatePanelloading" id="divProgress" style="width: 100%; height: 100%;">
                <table id="ProgressTable" cellpadding="0" cellspacing="0" border="0" style="width: 100%;
                    height: 100%;">
                    <tr align="center" valign="middle">
                        <td class="LoadingText" align="center" valign="middle">
                            <img src="App_Themes/Default/Images/indicator.gif" alt="Loading" />&nbsp;&nbsp;&nbsp;Please
                            Wait..
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upContent">
        <ContentTemplate>
            <h1>
                Employee - Account Mapping</h1>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="float: right !important">
                        <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form_field top_pad">
                            <div class="titlebg">
                                <h2>
                                    Employee
                                </h2>
                            </div>
                            <div class="content_block">
                                <table border="0" cellspacing="2" width="100%" cellpadding="2">
                                    <tr>
                                        <td align="left" style="width: 15%" valign="middle">
                                            Employee No:
                                        </td>
                                        <td align="left" style="width: 20%" valign="middle">
                                            <asp:DropDownList ID="ddlEmployeeNo" runat="server" CssClass="width200 chzn-select"
                                                AutoPostBack="true" data-placeholder="Select Employee Number" OnSelectedIndexChanged="ddlEmployeeNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Label runat="server" ID="lblEmpNo" Visible="false" />
                                            <asp:RequiredFieldValidator ID="rfvEmpNo" runat="server" ControlToValidate="ddlEmployeeNo"
                                                Display="Dynamic" ErrorMessage="Enter Employee Number" Text="*" ForeColor="Red"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="right" width="15%" valign="middle">
                                            <asp:Label runat="server" ID="lblEName" Text="Employee Name  :"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label runat="server" ID="lblEmployeeName"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form_field top_pad">
                            <div class="titlebg">
                                <h2>
                                    Mapped Accounts
                                </h2>
                            </div>
                            <div class="content_block" style="padding: 0px; margin: 0px;">
                                <asp:GridView ID="gvMapACcount" runat="server" AllowPaging="True" SkinID="GridView"
                                    AllowSorting="false" OnPageIndexChanging="gvMapACcount_PageIndexChanging" OnRowCommand="gvMapACcount_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgRemove" ImageUrl="~/App_Themes/Default/Images/delete.png"
                                                    CausesValidation="true" ToolTip="Remove" CommandName="RemoveAccount" CommandArgument='<%#Eval("HouseId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HouseId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="5%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHouseId" runat="server" Text='<% #Eval("HouseId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Number" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" runat="server" Text='<% #Eval("AccountNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ERFNo" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblERFNo" runat="server" Text='<% #Eval("ERFNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ERFSize" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# String.Format("{0:0.00}", Eval("ERFSize"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address1" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress1" runat="server" Text='<% #Eval("Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address2" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress2" runat="server" Text='<% #Eval("Address2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form_field top_pad">
                            <div class="titlebg">
                                <h2>
                                    Search Criteria
                                </h2>
                            </div>
                            <div class="content_block" style="padding-right: 0px !important;">
                                <table width="100%" style="padding: 0px !important; margin: 0px !important;">
                                    <tr>
                                        <td>
                                            Account Number :
                                        </td>
                                        <td>                                           
                                            <asp:TextBox runat="server" ID="txtAccountNumber"/>
                                        </td>
                                        <td>
                                            Address1 :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddress1" MaxLength="250" CssClass="width180"></asp:TextBox>
                                        </td>
                                        <td>
                                            Address2 :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAddress2" MaxLength="250" CssClass="width180"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Button runat="server" ID="btnSearch" Text="Search" CausesValidation="false"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="form_field top_pad">
                            <div class="titlebg">
                                <h2>
                                    Accounts
                                </h2>
                            </div>
                            <div class="content_block" style="padding: 0px; margin: 0px;">
                                <asp:GridView ID="gvAccounts" runat="server" AllowPaging="True" SkinID="GridView"
                                    AllowSorting="false" OnRowCommand="gvAccounts_RowCommand" OnPageIndexChanging="gvAccounts_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgAdd" ImageUrl="~/App_Themes/Default/Images/ic_edit.png"
                                                    ToolTip="Add" CommandName="AddAccount" CommandArgument='<%#Eval("HouseId") %>'
                                                    CausesValidation="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HouseId" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="5%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHouseId" runat="server" Text='<% #Eval("HouseId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Account Number" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" runat="server" Text='<% #Eval("AccountNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ERFNo" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblERFNo" runat="server" Text='<% #Eval("ERFNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ERFSize" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# String.Format("{0:0.00}", Eval("ERFSize"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address1" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress1" runat="server" Text='<% #Eval("Address1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address2" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                            ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress2" runat="server" Text='<% #Eval("Address2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
