<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlPager.ascx.cs" Inherits="ctlPager" %>
<table cellpadding="2" cellspacing="2" width="100%">
    <tbody>
        <tr><td colspan="2"></td></tr>
        <tr style="border:1px solid #99cbdc;background-color:#f2f2f2;">
            <td style="width: 30%;padding:10px;" nowrap="nowrap" align="left">                                   
                <asp:Label ID="lblPrefix" runat="server" ></asp:Label>&nbsp;
                <asp:Label ID="lblRecordMessage" runat="server" CssClass="labelBold" ></asp:Label>&nbsp;
                <b><%--<asp:DropDownList ID="ddlRecords" runat="server" AutoPostBack="True" Style="vertical-align: middle" Width="100px" Visible="false"> 
                    <asp:ListItem Value="10" Text="10"  /><asp:ListItem Value="20" Text="20"  /><asp:ListItem Value="50" Text="50"  /><asp:ListItem Value="200" Text="200"  />
                </asp:DropDownList>--%><asp:Localize ID="locPage" runat="server"></asp:Localize>
                </b>
            </td>
            <td style="width: 70%" align="right">
                <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagerLinkStyle" OnCommand="lnkFirst_Command"><img style="border:none;" src='<%=URLHelper.GetPath("~/App_Themes/Default/images/First.gif")%>' onmouseover='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/FirstHover.gif")%>";' onmouseout='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/First.gif")%>";' alt="First Page"/> </asp:LinkButton>
                <asp:LinkButton ID="lnkPrevious" runat="server" CssClass="PagerLinkStyle" OnCommand="lnkPrevious_Command"><img style="border:none;" src='<%=URLHelper.GetPath("~/App_Themes/Default/images/Previous.gif")%>' onmouseover='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/PreviousHover.gif")%>";' onmouseout='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/Previous.gif")%>";' alt="Previous Page"/> </asp:LinkButton>
                <asp:Repeater ID="Rptpager" runat="server" OnItemCommand="Rptpager_ItemCommand">
                    <ItemTemplate><asp:LinkButton CommandArgument='<%# Eval("intPageNo") %>' CommandName="Paging" ID="link" runat="server" CssClass='<%# (Convert.ToInt32(Eval("intPageNo")) == CurrentPageNumber ? "PagerLinkSelected" : "PagerLinkStyle") %>' Enabled='<%# (Convert.ToInt32(Eval("intPageNo")) == CurrentPageNumber ? false : true) %>'><%# Eval("intPageNo").ToString()%></asp:LinkButton></ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lnkNext" runat="server" CssClass="PagerLinkStyle" OnCommand="lnkNext_Command"><img style="border:none;" src='<%=URLHelper.GetPath("~/App_Themes/Default/images/Next.gif")%>' onmouseover='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/NextHover.gif")%>";' onmouseout='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/First.gif")%>";' alt="Next Page"/></asp:LinkButton>
                <asp:LinkButton ID="lnkLast" runat="server" CssClass="PagerLinkStyle" OnCommand="lnkLast_Command"><img style="border:none;" src='<%=URLHelper.GetPath("~/App_Themes/Default/images/Last.gif")%>' onmouseover='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/LastHover.gif")%>";' onmouseout='this.src="<%=URLHelper.GetPath("~/App_Themes/Default/images/Last.gif")%>";' alt="Last Page"/></asp:LinkButton>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
    </tbody>
</table>