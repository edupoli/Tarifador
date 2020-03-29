using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class VisualizarPlano : System.Web.UI.Page
    {
        string planoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            planoID = Request.QueryString["planoID"];
            if (!Page.IsPostBack)
            {
                buscarPlano(int.Parse(planoID));
            }
        }
        private void buscarPlano(int cod)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                planotarifacao pla = ctx.planotarifacaos.First(p => p.id == cod);
                nome.Text = pla.nome;
                tempoMinimo.Text = pla.tempoMinimoChamada;
                tempoTarifMinimo.Text = pla.tempoMinimoTarifacao;
                string op = Convert.ToString(pla.operadoraID);
                cboxOperadoras.Items.Insert(0, new ListItem(op,"1"));
                periodicidade.Text = pla.periodicidadeTarifa;
                taxaConexao.Text = pla.taxaConexao.ToString();
                valor0300.Text = pla.ligacao_0300.ToString();
                valorDDDCelular.Text = pla.dddCelular.ToString();
                valorDDDFixo.Text = pla.dddFixo.ToString();
                valorLocalCelular.Text = pla.localCelular.ToString();
                valorLocalFixo.Text = pla.localFixo.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planos.aspx");
        }
    }
}