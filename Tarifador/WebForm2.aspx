<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Tarifador.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    
    <canvas id="myChart" width="200" height="200"></canvas>

    <script>
        $(document).ready(function () {
            
                
                var gData = [];
                //gData[0] = $("#ddlyear").val();
                //gData[1] = $("#ddlMonth").val();

                var jsonData = JSON.stringify({
                    gData: gData
                });
                $.ajax({
                    type: "POST",
                    url: "WebForm2.aspx/getTrafficSourceData",
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = response.d;
                    var dataarr = [];
                    var Labelarr = [];
                    var Colorarr = [];
                    $.each(aData, function (inx, val) {
                        dataarr.push(val.value);
                        Labelarr.push(val.label);
                        Colorarr.push(val.color);
                    });
                    var ctx = $("#myChart").get(0).getContext("2d");
                    var config = {
                        type: 'pie',
                        data: {
                            datasets: [{
                                data: dataarr,
                                backgroundColor: Colorarr,
                            }],
                            labels: Colorarr
                        },
                        options: {
                            responsive: true
                        }
                    };
                    var myPieChart = new Chart(ctx, config);
                }
                function OnErrorCall_(response) { }
                e.preventDefault();
            

        });
    </script>

</asp:Content>
