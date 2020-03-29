<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm-jason.aspx.cs" Inherits="Tarifador.WebForm_jason" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<div class="wrapper">
  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <section class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-12 col-sm-6 col-md-3">
              <canvas id="canvas" width="100%" height="100%"></canvas>
              <div class="col-12 col-sm-6 col-md-3">
              </div>
          </div>
        
        <div class="row">
            <div class="col-lg-6">
                <div class="card">
                </div>
            </div>
            <div class="col-lg-8">
              <asp:DropDownList id="ddlSelectYear" runat="server">
                  <asp:ListItem Text="2014" Value="2014" />
                  <asp:ListItem Text="2015" Value="2015" />
              </asp:DropDownList> 
                <asp:Button id="myButton" runat="server" Text="Get Car Lists"></asp:Button> 
                    <div id="contentHolder"></div>
                
              </div>

            <div class="col-lg-12">
            <div class="card">
              <div class="card-header border-0">
                <div class="d-flex justify-content-between">
                  <h3 class="card-title">Quantidade de Chamadas por Hora</h3>
                  <a href="javascript:void(0);"></a>
                </div>
              </div>
              <div class="card-body">
                <div class="d-flex">
                  <p class="d-flex flex-column">
                    <span class="text-bold text-lg">$18,230.00</span>
                    <span>Total</span>
                  </p>
                  <p class="ml-auto d-flex flex-column text-right">
                    <span class="text-success">
                      <i class="fas fa-arrow-up"></i> 33.1%
                    </span>
                    <span class="text-muted">Desde o ultimo mês</span>
                  </p>
                </div>
                <!-- /.d-flex -->

                <div class="position-relative mb-4">
                  <canvas id="edu1-chart" height="200"></canvas>
                </div>

                <div class="d-flex flex-row justify-content-end">
                  <span class="mr-2">
                    <i class="fas fa-square text-primary"></i> Este Ano
                  </span>

                  <span>
                    <i class="fas fa-square text-gray"></i> Ano Anterior
                  </span>
                </div>
              </div>
            </div>
            </div>

            </div>
          </div>
        </div>
    </section>
  </div>
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Control sidebar content goes here -->
  </aside>
</div>

<!-- PAGE SCRIPTS -->
<script src="dist/js/pages/dashboard2.js"></script>
    <script src="dist/js/pages/dashboard3.js"></script>
    
    <script>
    var ctx = document.getElementById("canvas").getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Labels)%>,
            datasets: [{
                label: '<%=Legend%>',
                data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data)%>,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero:true
                    }
                }]
            }
        }
    });
    </script>

    <script>
        $(function () {

        var ticksStyle = {
            fontColor: '#495057',
            fontStyle: 'bold'
            }
            var mode = 'index'
            var intersect = true
        var ctx = $('#edu1-chart');
        var chartGraph = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ["00h", "01h", "02h", "03h", "04h", "05h", "06h", "07h", "08h", "09h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h", "19h", "20h", "21h", "22h", "23h"],
                datasets: [{
                    label: "LIGAÇÕES POR HORA",
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data)%>,
                    borderWidth: 6,
                    borderColor: 'rgba(77,166,253,0.85)',
                    backgroundColor: 'transparent'
                }]
            },
            options: {
      maintainAspectRatio: false,
      tooltips           : {
        mode     : mode,
        intersect: intersect
      },
      hover              : {
        mode     : mode,
        intersect: intersect
      },
      legend             : {
        display: false
      },
      scales             : {
        yAxes: [{
          // display: false,
          gridLines: {
            display      : true,
            lineWidth    : '4px',
            color        : 'rgba(0, 0, 0, .2)',
            zeroLineColor: 'transparent'
          },
          ticks    : $.extend({
            beginAtZero : true,
            suggestedMax: 200
          }, ticksStyle)
        }],
        xAxes: [{
          display  : true,
          gridLines: {
            display: false
          },
          ticks    : ticksStyle
        }]
      }
    }
        });
        });

    </script>
</asp:Content>
