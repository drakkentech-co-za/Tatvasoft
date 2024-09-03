<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControls_Footer" %>
<div id="footer">
    Water Electricity & Token System
    <%= DateTime.Now.Year %>
</div>
<script language="javascript" type="text/javascript">

    function ChangeMenuCSSOnClick(Value) {
        var objInvoice = document.getElementById("lnkInvoice");
        var objAdmin = document.getElementById("lnkAdmin");
        var objInvoiceCompare = document.getElementById("lnkInvoiceCompare");
        var objReport = document.getElementById("lnkReport");
        var objTokenRequest = document.getElementById("lnkTokenRequest");
        var objCharts = document.getElementById("lnkWECharts");
        var objChangePassword = document.getElementById("lnkChangePassword");

        switch (Value) {
            case "lnkInvoice":
                if (objInvoice)
                    objInvoice.className = 'nav_link active';
                break;
            case "lnkAdmin":
                if (objAdmin)
                    objAdmin.className = 'nav_link active';
                break;
            case "lnkInvoiceCompare":
                if (objInvoiceCompare)
                    objInvoiceCompare.className = 'nav_link active';
                break;
            case "lnkReport":
                if (objReport)
                    objReport.className = 'nav_link active';
                break;
            case "lnkTokenRequest":
                if (objTokenRequest)
                    objTokenRequest.className = 'nav_link active';
                break;
            case "lnkCharts":
                if (objCharts)
                    objCharts.className = 'nav_link active';
                break;
            case "lnkChangePassword":
                if (objChangePassword)
                    objChangePassword.className = 'nav_link active';
                break;
        }
    }
          
</script>
