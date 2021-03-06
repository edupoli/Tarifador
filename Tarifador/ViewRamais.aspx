﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRamais.aspx.cs" Inherits="Tarifador.ViewRamais" %>
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
                        <asp:TextBox runat="server" ID="numero" CssClass="form-control" ReadOnly="true" />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col">
                    <div class="form-group">
                      <label>Grupo de Ramais</label>
                        <asp:DropDownList runat="server" ID="cboxGrupoRamais" CssClass="form-control" ReadOnly="true" DataSourceID="SqlDataSourceGrupoRamais" DataTextField="nome" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourceGrupoRamais" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome FROM gruporamal"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col">
                <div class="form-group">
                  <label>Usuário</label>
                    <asp:DropDownList runat="server" ID="cboxUsuario" CssClass="form-control" ReadOnly="true" DataSourceID="SqlDataSourceUsuarios" DataTextField="nome" DataValueField="id" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome, emaill, login, senha, perfil, grupoUserID FROM usuario"></asp:SqlDataSource>
                </div>
                </div>
                  <div class="col">
                    <div class="form-group">
                      <label>Servidor</label>
                        <asp:TextBox runat="server" ID="servidor" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                
               
                <!-- /.form-group -->
            </div>
              <div class="row">
                  <div class="col">
                    <div class="form-group">
                      <label>Observações</label>
                        <asp:TextBox runat="server" ID="observacao" TextMode="MultiLine" CssClass="form-control" rows="2" cols="10" ReadOnly="true"/>
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
</asp:Content>
