﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewGrupoTroncos.aspx.cs" Inherits="Tarifador.ViewGrupoTroncos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Visualizar Grupos de Rotas</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Grupo de Rotas</li>
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
            <h3 class="card-title"><i class="fas fa-code-branch"></i></h3>
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
                  <div class="col-md-4">
                    <div class="form-group">
                      <label>Nome</label>
                        <asp:TextBox runat="server" ID="nome" CssClass="form-control" ReadOnly="true" />
                    </div>
                  </div>
               <div class="col-md-4">
                <div class="form-group">
                  <label>Operadora</label>
                    <asp:DropDownList runat="server" ID="cboxOperadora" ReadOnly="true" CssClass="form-control select2" style="width: 100%;" >
                    </asp:DropDownList>
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

    <script>
    $('[data-mask]').inputmask()
    </script>
</asp:Content>
