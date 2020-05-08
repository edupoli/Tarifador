<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Tarifador.home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons"
      rel="stylesheet">
<div class="wrapper">
  <!-- Navbar -->
  
  <!-- /.navbar -->

  <!-- Main Sidebar Container -->
  

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Estatísticas</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">home</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->

    <!-- Main content -->
    <section class="content">
      <div class="container-fluid"> 
        <!-- Info boxes -->
        <div class="row">
          <div class="col-12 col-sm-6 col-md-3">
            <div class="info-box">
              <span class="info-box-icon bg-primary elevation-1"><i class="fas fa-users"></i></span>

              <div class="info-box-content">
                <span class="info-box-text">Usuários</span>
                <span class="info-box-number" runat="server" id="boxUsers">
                  <small></small>
                </span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
          <!-- /.col -->
          <div class="col-12 col-sm-6 col-md-3">
            <div class="info-box mb-3">
              <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-file-invoice-dollar"></i></span>

              <div class="info-box-content">
                <span class="info-box-text">Custos</span>
                <span class="info-box-number" runat="server">
                    <small>R$</small>
                    <span runat="server" id="tt"></span>
                </span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
          <!-- /.col -->

          <!-- fix for small devices only -->
          <div class="clearfix hidden-md-up"></div>

          <div class="col-12 col-sm-6 col-md-3">
            <div class="info-box mb-3">
              <span class="info-box-icon bg-secondary elevation-1"><i class="fas fa-headphones"></i></span>

              <div class="info-box-content">
                <span class="info-box-text">Total de Chamadas no Mês corrente</span>
                <span class="info-box-number" runat="server" id="TotalChaMes"></span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
          <!-- /.col -->
          <div class="col-12 col-sm-6 col-md-3">
            <div class="info-box mb-3">
              <span class="info-box-icon bg-info elevation-1"><i class="fas fa-phone-square-alt"></i></span>

              <div class="info-box-content">
                <span class="info-box-text">Ramais</span>
                <span class="info-box-number" runat="server" id="boxRamais"></span>
              </div>
              <!-- /.info-box-content -->
            </div>
            <!-- /.info-box -->
          </div>
            
            
          <!-- /.col -->
        </div>
        <!-- /.row -->

        

        <!-- Main row -->
        <div class="row">
          <!-- Left col -->
          
            <!-- MAP & BOX PANE -->
                      <div class="col-lg-6">
            <div class="card">
              <div class="card-header border-0">
                <div class="d-flex justify-content-between">
                  <h3 class="card-title">Tarifação Semanal</h3>
                  <a href="javascript:void(0);"></a>
                </div>
              </div>
              <div class="card-body">
                <div class="d-flex">
                  <p class="d-flex flex-column">
                    <span class="text-bold text-lg">R$820,00</span>
                    <span>Custos</span>
                  </p>
                  <p class="ml-auto d-flex flex-column text-right">
                    <span class="text-success">
                      <i class="fas fa-arrow-up"></i> 12.5%
                    </span>
                    <span class="text-muted">Desde a Ultima Semana</span>
                  </p>
                </div>
                <!-- /.d-flex -->

                <div class="position-relative mb-4">
                  <canvas id="visitors-chart" height="200"></canvas>
                </div>

                <div class="d-flex flex-row justify-content-end">
                  <span class="mr-2">
                    <i class="fas fa-square text-primary"></i> Essa Semana
                  </span>

                  <span>
                    <i class="fas fa-square text-gray"></i> Semana Passada
                  </span>
                </div>
              </div>
            </div>
            <!-- /.card -->

            <div class="card">
              
            </div>
            <!-- /.card -->
          </div>
          <!-- /.col-md-6 -->
          <div class="col-lg-6">
            <div class="card">
              <div class="card-header border-0">
                <div class="d-flex justify-content-between">
                  <h3 class="card-title">Ligações por dia da semana </h3>
                  <a href="javascript:void(0);"></a>
                </div>
              </div>
              <div class="card-body">
                <div class="d-flex">
                  <p class="d-flex flex-column">
                    <span class="text-bold text-lg"></span>
                    <span></span>
                  </p>
                  <p class="ml-auto d-flex flex-column text-right">
                    <span class="text-success">
                     <!-- <i class="fas fa-arrow-down"></i> -->
                    </span>
                    <span class="text-muted"></span>
                  </p>
                </div>
                <!-- /.d-flex -->

                <div class="position-relative mb-4">
                  <canvas id="sales-chart1" height="200"></canvas>
                </div>

                <div class="d-flex flex-row justify-content-end">
                  <span class="mr-2">
                    <i class="fas fa-square text-primary"></i> Essa Semana
                  </span>

                  <span>
                    <i class="fas fa-square text-gray"></i> Semana Passada
                  </span>
                </div>
              </div>
            </div>
            </div>
            <!-- /.card -->
            
            <!-- inicio grafico eduardo -->
            <div class="col-lg-12">
            <div class="card">
              <div class="card-header border-0">
                <div class="d-flex justify-content-between">
                  <h3 class="card-title">Ligações por hora nas últimas 24h</h3>
                  <a href="javascript:void(0);"></a>
                </div>
              </div>
              <div class="card-body">
                <div class="d-flex">
                  <p class="d-flex flex-column">
                      <span>Total Hoje</span>
                    <span class="text-bold text-lg" runat="server" id="totalhoje"></span>
                      <span>Total Ontem</span>
                    <span class="text-bold text-lg" runat="server" id="totalOntem"></span>
                    
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
                    <i class="fas fa-square text-primary"></i> Ontem
                  </span>

                  <span>
                    <i class="fas fa-square text-green"></i> Hoje
                  </span>
                </div>
              </div>
            </div>
            </div>
            <!-- final grafico eduardo -->

            <!-- TABLE: LATEST ORDERS -->
            <div class="col-lg-8">
            <div class="card">
              <div class="card-header border-transparent">
                <h3 class="card-title">Ultimas Ligações</h3>

                <div class="card-tools">
                  <button type="button" class="btn btn-tool" data-card-widget="collapse">
                    <i class="fas fa-minus"></i>
                  </button>
                  <button type="button" class="btn btn-tool" data-card-widget="remove">
                    <i class="fas fa-times"></i>
                  </button>
                </div>
              </div>
              <!-- /.card-header -->
              <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewDIALLIST" CssClass="table m-0 table-bordered table-hover" runat="server" AutoGenerateColumns="False" ShowFooter="True">
                        <Columns>
                            <asp:BoundField DataField="calldate" HeaderText="Data/Hora" />
                            <asp:BoundField DataField="cnam" HeaderText="Usuário" />
                            <asp:BoundField DataField="src" HeaderText="Origem" />
                            <asp:BoundField DataField="dst" HeaderText="Destino" />
                            <asp:BoundField DataField="tempo" HeaderText="Tempo de chamada" />
                        </Columns>
                </asp:GridView>
                  
                </div>
                <!-- /.table-responsive -->
              </div>
              <!-- /.card-body -->
              
              <!-- /.card-footer -->
            </div>
            <!-- /.card -->
          </div>
          <!-- /.col -->

          <div class="col-md-4">
                    <p class="text-center">
                      <strong>Maior N° de chamadas nas ultimas 24h</strong>
                    </p>

                    <div class="progress-group">
                        <asp:Label Text="" runat="server" ID="lbl1" /> &nbsp;&nbsp; <asp:Label Text="" runat="server" ID="lbl2" />
                      <span class="float-right"><b>
                          <asp:Label Text="" runat="server" ID="lbltotalRamal" /></b>/<asp:Label Text="" runat="server" ID="lbltotal" /></span>
                      <div class="progress progress-sm">
                        <div class="progress-bar bg-primary" style="width: 80%"></div>
                      </div>
                    </div>
                    <!-- /.progress-group -->

                    <div class="progress-group">
                      <asp:Label Text="" runat="server" ID="Label3" /> &nbsp;&nbsp; <asp:Label Text="" runat="server" ID="Label4" />
                      <span class="float-right"><b>
                          <asp:Label Text="" runat="server" ID="Label1" /></b>/<asp:Label Text="" runat="server" ID="Label2" /></span>
                      <div class="progress progress-sm">
                        <div class="progress-bar bg-danger" style="width: 75%"></div>
                      </div>
                    </div>

                    <!-- /.progress-group -->
                    <div class="progress-group">
                      <span class="progress-text"><asp:Label Text="" runat="server" ID="Label5" /> &nbsp;&nbsp; <asp:Label Text="" runat="server" ID="Label6" /></span>
                      <span class="float-right"><b>
                          <asp:Label Text="" runat="server" ID="Label7" /></b>/<asp:Label Text="" runat="server" ID="Label8" /></span>
                      <div class="progress progress-sm">
                        <div class="progress-bar bg-success" style="width: 60%"></div>
                      </div>
                    </div>

                    <!-- /.progress-group -->
                    <div class="progress-group">
                      <span class="progress-text"><asp:Label Text="" runat="server" ID="Label9" /> &nbsp;&nbsp; <asp:Label Text="" runat="server" ID="Label10" /></span>
                      <span class="float-right"><b>
                          <asp:Label Text="" runat="server" ID="Label11" /></b>/<asp:Label Text="" runat="server" ID="Label12" /></span>
                      <div class="progress progress-sm">
                        <div class="progress-bar bg-warning" style="width: 50%"></div>
                      </div>
                    </div>
                    <!-- /.progress-group -->
                  </div>
          
          <!-- /.col -->
        </div>
        <!-- /.row -->
      </div><!--/. container-fluid -->
    </section>
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->

  <!-- Main Footer -->

</div>
<!-- ./wrapper -->


<!-- PAGE SCRIPTS -->
<script src="dist/js/pages/dashboard2.js"></script>
    <script src="dist/js/pages/dashboard3.js"></script>
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
                datasets: [
                    {
                    label: "ONTEM",
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data)%>,
                    borderWidth: 4,
                    borderColor: 'rgba(77,166,253,0.85)',
                    pointBorderColor    :'rgba(77,166,253,0.85)',
                    pointBackgroundColor:'rgba(77,166,253,0.85)',
                    backgroundColor: 'transparent'
                    },

                    {
                    label: "HOJE",
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data2)%>,
                    borderWidth: 4,
                    borderColor: 'rgba(6,204,6,0.85)',
                    pointBorderColor    :'rgba(6,204,6,0.85)',
                    pointBackgroundColor:'rgba(6,204,6,0.85)',
                    backgroundColor: 'transparent'
                    },
                ]
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
    <script>
        $(function () {
            var ticksStyle = {
                fontColor: '#495057',
                fontStyle: 'bold'
                }
            var mode = 'index'
            var intersect = true
            var $salesChart = $('#sales-chart1')
            var salesChart  = new Chart($salesChart, {
            type   : 'bar',
            data   : {
              labels  : ['DOM', 'SEG', 'TER', 'QUA', 'QUI', 'SEX', 'SAB'],
              datasets: [
                {
                  backgroundColor: '#007bff',
                  borderColor    : '#007bff',
                  data           : <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data3)%>,
                },
                {
                  backgroundColor: '#ced4da',
                  borderColor    : '#ced4da',
                  data           : <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data4)%>,
                }
              ]
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
                    beginAtZero: true,

                    // Include a dollar sign in the ticks
                    callback: function (value, index, values) {
                      if (value >= 1000) {
                        value /= 1000
                        value += 'k'
                      }
                      return '' + value
                    }
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
          })
        })
    </script>

</asp:Content>
