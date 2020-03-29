<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bilhetes.aspx.cs" Inherits="Tarifador.Bilhetes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
        <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Bilhetes</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Bilhetes</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
      <section class="content">
      <div class="container-fluid">
        <!-- SELECT2 EXAMPLE -->
        <div class="card card-default">
          <div class="card-header">
            <h3 class="card-title"><i class="fas fa-tag"></i></h3>
            <div class="card-tools">
              
              
              <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-remove"></i></button>
            </div>
          </div>
          <!-- /.card-header -->
          <div class="card-body">
            <div class="row">
                  <div class="col">
                    <div class="form-group">
                  <label>Data Inicial:</label>

                  <div class="input-group" >
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <i class="far fa-calendar-alt"></i>
                      </span>
                    </div>
                      <asp:TextBox runat="server" CssClass="form-control float-right datepick" ID="dataInicial"/>
                  </div>
                </div>
                  </div>
                <!-- /.form-group -->
                <div class="col">
                    <div class="form-group">
                  <label>Data Final:</label>

                  <div class="input-group" >
                    <div class="input-group-prepend">
                      <span class="input-group-text">
                        <i class="far fa-calendar-alt"></i>
                      </span>
                    </div>
                      <asp:TextBox runat="server" CssClass="form-control float-right datepick1" ID="dataFinal"/>
                  </div>
                </div>
                  </div>
               <div class="col">
                    <div class="form-group">
                        <label>Tipo de Ligação:</label>
                        <asp:DropDownList runat="server" CssClass="custom-select" ID="tipoChamada">
                            <asp:ListItem Text="Todos" Value="todos" />
                            <asp:ListItem Text="Fixo Local" Value="Fixolocal" />
                            <asp:ListItem Text="Celular Local" Value="CelularLocal" />
                            <asp:ListItem Text="DDD Fixo" Value="dddLocal" />
                            <asp:ListItem Text="DDD Celular" Value="Celularddd" />
                            <asp:ListItem Text="0300" Value="_0300regex" />
                        </asp:DropDownList>
                      </div>
                </div>
               <div class="col">
                    <div class="form-group">
                        <label>Canal:</label>
                            <asp:DropDownList runat="server"  CssClass="custom-select" ID="cboxCanal" >
                            </asp:DropDownList>
                        
                      </div>
                </div>
                <!-- /.form-group -->
                <div class="col">
                  <div class="form-group">
                    <label>&nbsp;</label>
                        <div class="input-group" >
                            <div class="input-group-prepend">
                                <span class="input-group"></span>
                            </div>
                            <asp:Button Text="Consultar" runat="server" CssClass="btn btn-primary" ID="btnConsultar" OnClick="btnConsultar_Click" />&nbsp;
                        </div>
                       </div>
                  </div>
                <style>
                    .btn{
                        font-weight:bold
                    }
                </style>
                
            </div>
        </div>
    </div>
      </div>
          
    </section>
        <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
            <section class="content">
      <div class="row">
        <div class="col-12">
          <div class="card">
            
            <!-- /.card-header -->
              <div class="card-body">
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" emptydatatext="Não foram encontrados dados" class="table table-bordered table-hover">
                  <Columns>
                    <asp:BoundField DataField="idcdr" HeaderText="ID" />
                    <asp:BoundField DataField="calldate" HeaderText="Data da Ligação" />
                    <asp:BoundField DataField="cnam" HeaderText="Usuário" />
                    <asp:BoundField DataField="src" HeaderText="Ramal" />
                    <asp:BoundField DataField="dst" HeaderText="Numero Discado" />
                    <asp:BoundField DataField="tempo" HeaderText="Duração" />
                    <asp:BoundField DataField="tipoChamada" HeaderText="Tipo de Chamada" />
                    <asp:BoundField DataField="canal" HeaderText="Canal" />
                    <asp:BoundField DataField="valor" HeaderText="Valor" />            
                </Columns>
          </asp:GridView>

            </div>
            <!-- /.card-body -->

          </div>
          <!-- /.card -->

        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
      </div>
    </section>
     
        </div>
  </div>

    <script type="text/javascript" src="plugins/pt-br.js"></script>
<script  src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/js/bootstrap-datetimepicker.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker.min.css">



  <script>
        $(function () {
            $('.datepick').datetimepicker({
                format: 'YYYY-MM-DD 00:00:00',
                locale: 'pt-br',
                showClear: true,
                showClose: true
            });
            $('.datepick1').datetimepicker({
                format: 'YYYY-MM-DD 23:59:59',
                locale: 'pt-br',
                showClear: true,
                showClose: true
            });
        });
    </script>

     <script type="text/javascript">
    $(function () {
      $('[data-toggle="tooltip"]').tooltip()
    })

    </script>
    <script type="text/javascript">
        function erro() {
            toastr.error('Favor definir as datas de Inicio e Fim para pesquisa!!!')        
      };
    </script>
    <script>
            $(document).ready(function () {
            $('#<%= GridView1.ClientID%>').prepend($("<thead></thead>").append($("#<%= GridView1.ClientID%>").find("tr:first"))).DataTable({
                "bJQueryUI": true,
                "autoWidth": true,
                 
                "oLanguage": {
                    "sProcessing":   "Processando...",
                    "sLengthMenu":   "Mostrar _MENU_ registros",
                    "sZeroRecords":  "Não foram encontrados resultados",
                    "sInfo":         "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                    "sInfoEmpty":    "Mostrando de 0 até 0 de 0 registros",
                    "sInfoFiltered": "",
                    "sInfoPostFix":  "",
                    "sSearch":       "Pesquisar:",
                    "sUrl":          "",
                    "oPaginate": {
                        "sFirst":    "Primeiro",
                        "sPrevious": "Anterior",
                        "sNext":     "Seguinte",
                        "sLast":     "Último"
                    }
                }
            }) 
            });
    </script>
</asp:Content>
