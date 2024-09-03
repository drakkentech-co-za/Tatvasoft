<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ManualTokenImport.aspx.cs" Inherits="ManualTokenImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="ChoosenJS/chosen.css" rel="stylesheet" type="text/css" />
    <script src="ChoosenJS/chosen.jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">        
        $(document).ready(function () {
            $(".chzn-select").chosen();
        });          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>
        Manual Token Import
    </h1>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table border="0" cellspacing="0" width="100%" cellpadding="10">
                    <tr>
                        <td align="left" style="width: 15%">
                            Account No:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:DropDownList ID="ddlAccountNo" runat="server" Width="300" data-placeholder="Select Account Number"
                                class="chzn-select" DataTextField="AccountNo" DataValueField="HouseId" 
                                AutoPostBack="true" onselectedindexchanged="ddlAccountNo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label runat="server" ID="lblAccountNo" Visible="false" />
                            <asp:RequiredFieldValidator ID="rfvAccountNo" runat="server" ControlToValidate="ddlAccountNo"
                                ErrorMessage="Enter Account Number" Text="*" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Address:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:Label ID="lblAddress" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Token Units:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:Label ID="lblTokenUnits" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                            Token Number:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtTokenNumber" runat="server" CssClass="width200">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvERFSize" runat="server" ControlToValidate="txtTokenNumber"
                                ErrorMessage="Enter Token Number" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 15%">
                           Meter No:
                        </td>
                        <td align="left" style="width: 85%">
                            <asp:TextBox ID="txtMeterNo" runat="server" CssClass="width200" MaxLength="100">
                            </asp:TextBox>                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnImport" runat="server" Text="Import" CausesValidation="true" 
                                onclick="btnImport_Click" OnClientClick='return confirm("Are you sure you want to import this account?");'>
                            </asp:Button>                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
