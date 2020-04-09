<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPlanoTarifacao.aspx.cs" Inherits="Tarifador.EditPlanoTarifacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
        <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Plano de Tarifação</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Plano Tarifação</li>
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
            <h3 class="card-title"><i class="fas fa-dollar-sign"></i></h3>
            <div class="card-tools">
              <asp:Button Text="Editar" CssClass="btn btn-sm btn-info" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
              <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-remove"></i></button>
            </div>
          </div>
          <!-- /.card-header -->
          <div class="card-body">
            <div class="row">
                  <div class="col-md-4">
                    <div class="form-group">
                      <label>Nome</label>
                        <asp:TextBox runat="server" ID="nome" CssClass="form-control"  />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col-md-4">
                    <div class="form-group">
                      <label>Tempo Mínimo de Chamada</label>
                        <asp:TextBox runat="server" ID="tempoMinimo" CssClass="form-control"  />
                    </div>
                </div>
               <div class="col-md-4">
                    <div class="form-group">
                      <label>Tempo de Tarifação Mínimo</label>
                        <asp:TextBox runat="server" ID="tempoTarifMinimo" CssClass="form-control"  />
                    </div>
                </div>
                <!-- /.form-group -->
            </div>
              <div class="row">
                <div class="col-md-4">
                <div class="form-group">
                  <label>Operadora</label>
                    <asp:DropDownList runat="server" ID="cboxOperadoras" CssClass="form-control select2" style="width: 100%;" DataSourceID="SqlDataSourceOperadoras" DataTextField="descricao" DataValueField="operadoraID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceOperadoras" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT operadoraID, codigo, descricao FROM operadora"></asp:SqlDataSource>
                </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                      <label>Periodicidade de Tarifa</label>
                        <asp:TextBox runat="server" ID="periodicidade" CssClass="form-control"  />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                      <label>Taxa de conexão</label>
                        <asp:TextBox runat="server" ID="taxaConexao" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask="" />
                    </div>
                </div>
             </div>
        </div>
    </div>
      </div>
    </section>
      <section class="content">
        <div class="container-fluid">
            <div class="card card-outline">
                <div class="card-header">
            <div class="card-tools">
              <button type="button" class="btn btn-tool" data-card-widget="maximize"><i class="fas fa-expand"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
              <button type="button" class="btn btn-tool" data-card-widget="remove"><i class="fas fa-remove"></i></button>
            </div>
          </div>
                <div class="card-body">
                    <h5>Tarifas</h5>
            <div class="row">
              <div class="col-5 col-sm-3">
                <div class="nav flex-column nav-tabs h-100" id="vert-tabs-tab" role="tablist" aria-orientation="vertical">
                  <a class="nav-link active" id="vert-tabs-0300-tab" data-toggle="pill" href="#vert-tabs-0300" role="tab" aria-controls="vert-tabs-0300" aria-selected="true">0300</a>
                  <a class="nav-link" id="vert-tabs-DDDCelular-tab" data-toggle="pill" href="#vert-tabs-DDDCelular" role="tab" aria-controls="vert-tabs-DDDCelular" aria-selected="false">DDD Celular</a>
                  <a class="nav-link" id="vert-tabs-DDDFixo-tab" data-toggle="pill" href="#vert-tabs-DDDFixo" role="tab" aria-controls="vert-tabs-DDDFixo" aria-selected="false">DDD Fixo</a>
                  <a class="nav-link" id="vert-tabs-LocalCelular-tab" data-toggle="pill" href="#vert-tabs-LocalCelular" role="tab" aria-controls="vert-tabs-LocalCelular" aria-selected="false">Local Celular</a>
                  <a class="nav-link" id="vert-tabs-LocalFixo-tab" data-toggle="pill" href="#vert-tabs-LocalFixo" role="tab" aria-controls="vert-tabs-LocalFixo" aria-selected="false">Local Fixo</a>
                </div>
              </div>
              <div class="col-7 col-sm-9">
                <div class="tab-content" id="vert-tabs-tabContent">
                  <div class="tab-pane text-left fade show active" id="vert-tabs-0300" role="tabpanel" aria-labelledby="vert-tabs-0300-tab">
                     <div class="col-md-4">
                        <div class="form-group">
                          <label>Valor</label>
                            <asp:TextBox runat="server" ID="valor0300" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask="" />
                        </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="vert-tabs-DDDCelular" role="tabpanel" aria-labelledby="vert-tabs-DDDCelular-tab" />
                     <div class="col-md-4">
                        <div class="form-group">
                          <label>Valor</label>
                            <asp:TextBox runat="server" ID="valorDDDCelular" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask="" />
                        </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="vert-tabs-DDDFixo" role="tabpanel" aria-labelledby="vert-tabs-DDDFixo-tab"  >
                     <div class="col-md-4">
                        <div class="form-group">
                          <label>Valor</label>
                            <asp:TextBox runat="server" ID="valorDDDFixo" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask="" />
                        </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="vert-tabs-LocalCelular" role="tabpanel" aria-labelledby="vert-tabs-LocalCelular-tab">
                     <div class="col-md-4">
                        <div class="form-group">
                          <label>Valor</label>
                            <asp:TextBox runat="server" ID="valorLocalCelular" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask="" />
                        </div>
                    </div>
                  </div>
                  <div class="tab-pane fade" id="vert-tabs-LocalFixo" role="tabpanel" aria-labelledby="vert-tabs-LocalFixo-tab">
                     <div class="col-md-4">
                        <div class="form-group">
                          <label>Valor</label>
                            <asp:TextBox runat="server" ID="valorLocalFixo" CssClass="form-control" data-inputmask='"mask": "9.99"' data-mask=""  />
                        </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
                </div>
            </div>
          </section>
        </div>
  </div>
    <script type="text/javascript">
  function sucesso() {
    toastr.success('Alterado com Sucesso!!!')        
        };
        function erro() {
    toastr.error('Erro ao Alterar Plano de Tarifação!!!')        
  };
    </script>
    <script>
    $('[data-mask]').inputmask()
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
    <script type="text/javascript">
        function erroGeral() {
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
            toastr["error"]("<%= mensagem %>", "Erro")
        };
    </script>
</asp:Content>
