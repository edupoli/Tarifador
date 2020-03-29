<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planos.aspx.cs" Inherits="Tarifador.Planos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <div class="content-header">
      <div class="container-fluid">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1 class="m-0 text-dark">Planos de Tarifação</h1>
          </div><!-- /.col -->
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">home</a></li>
              <li class="breadcrumb-item active">Planos Tarifação</li>
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
            <section class="content">
      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Planos de Tarifação Cadastrados</h3>
            </div>
            <!-- /.card-header -->
              <div class="card-body">
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  emptydatatext="Não existem Planos de Tarifações Cadastrados!!" class="table table-bordered table-hover">
              <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nome" HeaderText="Nome" />
            <asp:BoundField DataField="descricao" HeaderText="Operadora" />
            
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton class="btn badge-secondary" Text="" ID="btnVisualizar" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnVisualizar_Click" ><i class="far fa-eye"></i></asp:LinkButton>
                    <asp:LinkButton class="btn badge-info" Text="" ID="btnEditar" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnEditar_Click" ><i class="fas fa-edit"></i></asp:LinkButton>
                    <asp:LinkButton class="btn badge-danger" Text="" ID="btnExcluir" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnExcluir_Click" OnClientClick="return confirm('Tem Certeza que deseja Excluir ?')" ><i class="fas fa-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
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
      <section class="content">
        <div class="container-fluid">
            
            </div>
          </section>
        </div>
  </div>
     <script type="text/javascript">
    $(function () {
      $('[data-toggle="tooltip"]').tooltip()
    })
        function sucesso() {
            toastr.success('Deletado com Sucesso!!!')        
      };
    </script>
    <script type="text/javascript">
        function erro() {
            toastr.error('Erro ao Deletar!!!')        
      };
    </script>
</asp:Content>
