<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Tarifador.WebForm1" %>

<script type="text/c#" runat="server">
[System.Web.Services.WebMethod(EnableSession = true)]
public static string search()
{
    return "worked";
}
</script>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true" />
        <button id="btnSearch" onclick="search(); return false;" >Search</button>
    </form>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script type="text/javascript">
        function search() {
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebForm1.aspx/search") %>',
                data: '{ }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    alert(msg.d)
                }
            });
        }
    </script>
</body>
</html>
