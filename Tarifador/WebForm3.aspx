<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="Tarifador.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    
            <canvas id="myChart"> </canvas>  

    <script>
    $(document).ready(function () {
    
	var jsonData = JSON.stringify({
	    
	});

	$.ajax({
	    type: "POST",
	    url: "WebForm3.aspx/getLineChartData",
	    data: jsonData,
	    contentType: "application/json; charset=utf-8",
	    dataType: "json",
	    success: OnSuccess_,
	    error: OnErrorCall_
	});

	function OnSuccess_(reponse) {
	    var aData = reponse.d;
	    var aLabels = aData[0];
	    var aDatasets1 = aData[1];
	   

	    var data = {
		labels: aLabels,
		datasets: [{
		    label: "My First dataset",
		    fillColor: "rgba(220,220,220,0.2)",
		    strokeColor: "rgba(220,220,220,1)",
		    pointColor: "rgba(220,220,220,1)",
		    pointStrokeColor: "#fff",
		    pointHighlightFill: "#fff",
		    pointHighlightStroke: "rgba(220,220,220,1)",
		    data: aDatasets1
		
		}]
	    };
	    
	    var ctx = $("#myChart").get(0).getContext('2d');
	    ctx.canvas.height = 300;  // setting height of canvas
	    ctx.canvas.width = 500; // setting width of canvas
	    var lineChart = new Chart(ctx).Line(data, {
		bezierCurve: false
	    });
	}
	function OnErrorCall_(repo) {
	    alert("Woops something went wrong, pls try later !");
	}
    
});
    </script>
</asp:Content>
