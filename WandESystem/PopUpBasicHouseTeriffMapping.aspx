<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpBasicHouseTeriffMapping.aspx.cs"
    Inherits="PopUpBasicHouseTeriffMapping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.2.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css"
        type="text/css" />
    <script src="Scripts/CommonFun.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validateCategories() {

            var count = 0;
            $('.checkme').each(function () {
                if ($(this).children().is(":checked")) {
                    count++;
                }
            });

            if (count == 0) {
                alert("Please select Tariff Categories")
                return false;
            }
            else {
                return true;
            }
        }

        function checkall(chkbox) {

            if (chkbox.checked) {
                $(".checkme").children().prop("checked", "checked");
                $("input[id*=txtAdditionalTeriff]").val("1");
            }
            else {
                $(".checkme").children().removeProp("checked");
                $("input[id*=txtAdditionalTeriff]").val("0");
            }
        }

        function checkChange(e) {
            
            if ($("input:checkbox[id$='chkSelect']").length == $("input:checkbox[id$='chkSelect']:checked").length) {
                $("input:checkbox[id$='chkAll']").prop("checked", "checked");
            }
            else {
                $("input:checkbox[id$='chkAll']").removeProp("checked");
            }
           
            if ($(e).is(":checked")) {
                $(e).parent().parent().parent().find("input[id*=txtAdditionalTeriff]").val("1");
            }
            else {
                $(e).parent().parent().parent().find("input[id*=txtAdditionalTeriff]").val("0");
            }
        }

        function setDefaultValue(cntrl) {
            if ($(cntrl).val() == "") {
                $(cntrl).val("0");
            }
        }

        function openHouseTeriffMappingPage(path) {
            top.window.location.href = path;

        }
        
    </script>
    <style type="text/css">
        .row1 td, .row2 td, .grid th
        {
            border: solid thin #cfcfcf;
        }
        table
        {
            border-collapse: collapse;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: auto; width: 96%;">
        <table border="0" cellspacing="2" width="100%" cellpadding="2">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div class="titlebg">
                        <h2>
                            Tariff Category
                        </h2>
                    </div>
                    <div class="content_block" style="padding: 0px; margin: 0px;">
                        <asp:Repeater runat="server" ID="rptHouseTeriff">
                            <HeaderTemplate>
                                <table cellpadding="2" cellspacing="0" class="grid" width="100%">
                                    <tr>
                                        <th width="70px" align="center">
                                            <asp:CheckBox runat="server" ID="chkAll" onclick="checkall(this);" />
                                        </th>
                                        <th align="left">
                                            Category
                                        </th>
                                        <th align="left">
                                            Category Name
                                        </th>
                                        <th align="left">
                                            Additional Tariff
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="row1">
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="chkSelect" value='<%# Eval("TeriffId")%>' CssClass="checkme"
                                            onclick="checkChange(this);" />
                                    </td>
                                    <td>
                                        <%# Eval("CategoryValue")%>
                                    </td>
                                    <td>
                                        <%# Eval("CategoryName")%>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdditionalTeriff" onkeypress="return OnlyNumeric(event)"
                                            onblur="setDefaultValue(this)" Text='<%# Eval("AdditionalTeriff")%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="row2">
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="chkSelect" value='<%# Eval("TeriffId")%>' CssClass="checkme"
                                            onclick="checkChange(this);" />
                                    </td>
                                    <td>
                                        <%# Eval("CategoryValue")%>
                                    </td>
                                    <td>
                                        <%# Eval("CategoryName")%>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdditionalTeriff" onkeypress="return OnlyNumeric(event)"
                                            onblur="setDefaultValue(this)" Text='<%# Eval("AdditionalTeriff")%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateCategories();" />&nbsp;
                    <asp:Button ID="btnAddMore" runat="server" Text="Add More" OnClick="btnAddMore_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
