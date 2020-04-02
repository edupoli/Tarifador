<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Simulador.aspx.cs" Inherits="Tarifador.Simulador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Simulador de Tarifação</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Simulador de Tarifação</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
        <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <!-- SELECT2 EXAMPLE -->
        <div class="card card-default">
          <div class="card-header">
            <h3 class="card-title"><i class="fas fa-cogs"></i></h3>
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
                      <label>Duração da Chamada</label>
                        <asp:TextBox runat="server" ID="tempoChamada" TextMode="Time" CssClass="form-control"  />
                        <small>Tempo total da chamada</small>
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col">
                    <div class="form-group">
                        <label>Forma de Cobrança</label>
                        <asp:DropDownList runat="server" ID="cboxFormaCobranca" CssClass="form-control">
                            <asp:ListItem Text="30s / 6s  Cadência 30/6" Value="30/6" />
                            <asp:ListItem Text="60s / 60s   Minuto Cheio" Value="60/60" />
                        </asp:DropDownList>
                    </div>
                </div> 
               
                <div class="col">
                    <div class="form-group">
                      <label>Valor por Minuto </label>
                        <asp:TextBox runat="server" ID="valor" CssClass="form-control" placeholder="Valor em Reais" data-inputmask='"mask": "9,99"' data-mask="" />
                        <small>Valor da tarifa por minuto</small>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                      <label>Taxa de conexão</label>
                        <asp:TextBox runat="server" ID="taxaConexao" CssClass="form-control"  placeholder="Valor em Reais" data-inputmask='"mask": "9,99"' data-mask=""  />
                        <small>Caso exista taxa adicional de conexão</small>
                    </div>
                </div>
             </div>
              <div class="row">
                  <asp:Button Text="Calcular" runat="server" ID="btnCalcular" CssClass="btn btn-primary swalDefaultSuccess"  OnClick="btnCalcular_Click"/>
              </div>
        </div>
    </div>
   </div>
  </section>
      <section>
          <div class="card-body">
              <asp:Label Text="" runat="server" ID="lblResultado" />
          </div>
      </section>
     
        </div>
  </div>

    <script type="text/javascript" src="plugins/pt-br.js"></script>
<script  src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/js/bootstrap-datetimepicker.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.43/css/bootstrap-datetimepicker.min.css">

    <script type="text/javascript">
        function sucesso() {
            toastr.success('Gravado com Sucesso!!!')        
      };
    </script>
    <script type="text/javascript">
        function tempoChamada() {
            toastr.error('É necessário informar o tempo de chamada para o calculo!!!')        
        };
        function tempoMinimoChamada() {
            toastr.error('O valor do tempo de chamada deve ser superior a 3 Segundos !!!')
        };
        function periodicidade() {
            toastr.error('É necessário informar a periodicidade de tempo para o calculo!!!')
        };
        function valor() {
            toastr.error('É necessário informar o valor do minuto para o calculo!!!')
      };
    </script>

    <script>
    $('[data-mask]').inputmask()
    </script>

    <script type="text/javascript">
        $(function teste() {
            const Toast = Swal.mixin({})
                Toast.fire({
                    toast: true,
                    type: 'success',
                    position: 'center-end',
                    showConfirmButton: false,
                    timer: 9000,
                    title: ' <%= lblResultado.Text %>'
                })

        });
      </script>
</asp:Content>
