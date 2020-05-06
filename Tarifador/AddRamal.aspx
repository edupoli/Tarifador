<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRamal.aspx.cs" Inherits="Tarifador.AddRamal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Cadastro de Ramais</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Cadastro de Ramais</li>
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
            <h3 class="card-title"><i class="fas fa-fax"></i></h3>
            <div class="card-tools">
              <asp:Button Text="Salvar" CssClass="btn btn-sm btn-info" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
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
                      <label>Número</label>
                        <asp:TextBox runat="server" ID="numero" CssClass="form-control"  />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col">
                    <div class="form-group">
                      <label>Grupo de Ramais</label>
                        <asp:DropDownList runat="server" ID="cboxGrupoRamais" CssClass="form-control" DataSourceID="SqlDataSourceGrupoRamais" DataTextField="nome" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceGrupoRamais" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome FROM gruporamal"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                <div class="form-group">
                  <label>Usuário</label>
                    <asp:DropDownList runat="server" ID="cboxUsuario" CssClass="form-control" DataSourceID="SqlDataSourceGrupoUsuarios" DataTextField="nome" DataValueField="id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceGrupoUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome, emaill, login, senha, perfil, grupoUserID FROM usuario"></asp:SqlDataSource>
                </div>
                </div>
                  <div class="col">
                    <div class="form-group">
                      <label>Cadastrar</label>
                        <asp:Button Text="Cadastrar Novo Usuário" CssClass="btn btn-primary form-control" runat="server" ID="btnAddUsuario" OnClick="btnAddUsuario_Click"/>
                        
                    </div>
                </div>
                
               
                <!-- /.form-group -->
            </div>
              <div class="row">
                  <div class="col">
                    <div class="form-group">
                      <label>Observações</label>
                        <asp:TextBox runat="server" ID="observacao" TextMode="MultiLine" CssClass="form-control" rows="2" cols="10"/>
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
            toastr.success('Gravado com Sucesso!!!')        
      };
    </script>
    <script type="text/javascript">
        function erro() {
            toastr.error('Erro ao Gravar!!!')        
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
