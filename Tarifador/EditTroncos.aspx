﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditTroncos.aspx.cs" Inherits="Tarifador.EditTroncos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Editar Troncos</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Troncos</li>
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
            <h3 class="card-title"><i class="fas fa-exchange-alt"></i></h3>
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
                  <div class="col-md-4">
                    <div class="form-group">
                      <label>Nome</label>
                        <asp:TextBox runat="server" ID="nome" CssClass="form-control"  />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col-md-2">
                    <div class="form-group">
                      <label>DDD</label>
                        <asp:TextBox runat="server" ID="ddd" CssClass="form-control"  />
                    </div>
                </div>
               <div class="col-md-4">
                    <div class="form-group">
                      <label>Número de Saída</label>
                        <asp:TextBox runat="server" ID="numSaida" CssClass="form-control"  />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                      <label>Canal</label>
                        <asp:TextBox runat="server" ID="canal" CssClass="form-control"  />
                        <small>Atenção ao Preencher esse campo.</small>
                    </div>
                </div>
                <!-- /.form-group -->
            </div>
              <div class="row">
                <div class="col-md-4">
                <div class="form-group">
                  <label>Plano de Tarifação</label>
                    <asp:DropDownList runat="server" ID="cboxPlanoTarificao" CssClass="form-control select2" style="width: 100%;" DataSourceID="SqlDataSourcePlanoTarifacao" DataTextField="nome" DataValueField="id" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourcePlanoTarifacao" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome, tempoMinimoChamada, tempoMinimoTarifacao, operadoraID, periodicidadeTarifa, taxaConexao, `ligacao_0300`, dddCelular, dddFixo, localCelular, localFixo FROM planotarifacao"></asp:SqlDataSource>
                </div>
                </div>
                <div class="col-md-4">
                <div class="form-group">
                  <label>Grupo</label>
                    <asp:DropDownList runat="server" ID="cboxGrupoTroncos" CssClass="form-control select2" style="width: 100%;" DataSourceID="SqlDataSourceGrupoTroncos" DataTextField="nome" DataValueField="id" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceGrupoTroncos" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="SELECT id, nome, operadoraID FROM grupotronco"></asp:SqlDataSource>
                </div>
                </div>
                <div class="col-md-4">
                <div class="form-group">
                  <label>Operadora</label>
                    <asp:DropDownList runat="server" ID="cboxOperadoras" CssClass="form-control select2" style="width: 100%;" DataSourceID="SqlDataSourceOperadoras" DataTextField="descricao" DataValueField="operadoraID"  >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceOperadoras" runat="server" ConnectionString="<%$ ConnectionStrings:tarifadorConnectionString %>" ProviderName="<%$ ConnectionStrings:tarifadorConnectionString.ProviderName %>" SelectCommand="select * from operadora"></asp:SqlDataSource>
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