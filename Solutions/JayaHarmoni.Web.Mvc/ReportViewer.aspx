<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="JayaHarmoni.Web.Mvc.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laporan</title>
    <script src="<%= ResolveUrl("~/Scripts/kendo/2013.1.514/jquery.min.js") %>"></script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            setTimeout(printReport, 1500);
        });

        function printReport() {
            var autoPrint = getUrlParameter('autoPrint');
            if (autoPrint == 1)
                window.print();
        }
        function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1);
            var sURLVariables = sPageURL.split('&');
            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                if (sParameterName[0] == sParam) {
                    return sParameterName[1];
                }
            }
            return null;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rv" runat="server" Width="98%" Height="650px"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
