<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOperadoras.aspx.cs" Inherits="Tarifador.Operadoras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Operadoras</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">Operadoras</li>
            </ol>
          </div><!-- /.col -->
        </div><!-- /.row -->
      </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class="content">
      <div class="container-fluid">
        <!-- Horizontal Form -->
            <div class="card card-default">
              <div class="card-header">
                <h3 class="card-title"><i class="nav-icon fas fa-phone-square-alt"></i></h3>
                 <div class="card-tools">
                  <asp:Button Text="Salvar" CssClass="btn btn-sm btn-info" runat="server" data-toggle="tooltip" title="Salvar os dados" data-placement="auto" ID="btnSalvar" OnClick="btnSalvar_Click" />
                  <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
                  <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
                  <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                  <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-remove"></i></button>
                </div>
              </div>
              <!-- /.card-header -->
              <!-- form start -->
                <div class="card-body">
                  <div class="form-group row">
                    <label for="codigo" class="col-sm-2 col-form-label">Codigo</label>
                    <div class="col-sm-2">
                        <asp:TextBox runat="server" CssClass="form-control" ID="textCodigo" placeholder="Codigo" />  
                    </div>
                  </div>
                  <div class="form-group row">
                    <label for="descricao" class="col-sm-2 col-form-label">Nome</label>
                    <div class="col-sm-4">
                        <asp:TextBox runat="server" CssClass="form-control" ID="textDescricao" placeholder="Nome" />
                    </div>
                  </div>
                  
                </div>
                <!-- /.card-body -->
                
            </div>
          
            <!-- /.card -->
      </div><!--/. container-fluid -->
    </section>



    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->

  <!-- Control Sidebar -->
 
</div>
<!-- ./wrapper -->
    <script type="text/javascript">
    $(function () {
          $('[data-toggle="tooltip"]').tooltip()
        })
        function sucesso() {
            toastr.success('Gravado com Sucesso!!!')        
      };
    </script>
    <script type="text/javascript">
        function erro() {
            toastr.error('Erro ao Gravar!!!')        
      };
    </script>

    <script type="text/javascript">
        function acessoNegado() {
            toastr.options = {
              "closeButton": false,
              "debug": false,
              "newestOnTop": true,
              "progressBar": true,
              "positionClass": "toast-top-full-width",
              "preventDuplicates": true,
              "onclick": null,
              "showDuration": "300",
              "hideDuration": "1000",
              "timeOut": "8000",
              "extendedTimeOut": "1000",
              "showEasing": "swing",
              "hideEasing": "linear",
              "showMethod": "fadeIn",
              "hideMethod": "fadeOut"
            }
            toastr["info"]("Acesso restrito a usuarios Administradores. ", "Erro")
      };
    </script>


</asp:Content>
