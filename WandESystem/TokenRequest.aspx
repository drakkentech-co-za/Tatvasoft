<%@ Page Title="W & E - Token Request" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="TokenRequest.aspx.cs" Inherits="TokenRequest" %>

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
    <h1>
        Token Request
    </h1>
    <br />
    <div class="form_field top_pad">
        <div class="titlebg">
            <h2>
                Employee Details
            </h2>
        </div>
        <div class="content_block">
            <table border="0" cellspacing="2" width="100%" cellpadding="2">
                <tr>
                    <td align="left" colspan="3">
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </td>
                </tr>
                <tr id="trEmployeeNumber" runat="server">
                    <td align="left" width="15%">
                        Employee Number :
                    </td>
                    <td align="left" width="25%">
                        <asp:DropDownList runat="server" ID="ddlEmployeeNumber" CssClass="chzn-select"
                            data-placeholder="Select Employee Number" Width="225px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvEmployee" ControlToValidate="ddlEmployeeNumber"
                            runat="server" ForeColor="Red" Display="Dynamic" Text="*" ErrorMessage="Select Employee Number"
                            InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right">
                        <asp:Button runat="server" ID="btnTokenRequest" Text="Request Token" CssClass="btn_medium"
                            CausesValidation="true" OnClick="btnTokenRequest_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
