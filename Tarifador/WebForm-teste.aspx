<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm-teste.aspx.cs" Inherits="Tarifador.WebForm_teste" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    
<script type="text/c#" runat="server">
[System.Web.Services.WebMethod]
public static string search()
{
    return "worked";
}
</script>


    
        <asp:ScriptManager ID="scm" runat="server" EnablePageMethods="true" />
        <button id="btnSearch" onclick="search(); return false;" >Search</button>
    

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script type="text/javascript">
        function search() {
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~/WebForm-teste.aspx/search") %>',
                data: '{ }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    alert(msg.d)
                }
            });
        }
    </script>

</asp:Content>
