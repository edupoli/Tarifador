using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditPlanoTarifacao : System.Web.UI.Page
    {
        int planoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            planoID = Convert.ToInt32(Request.QueryString["planoID"]);
            if (!Page.IsPostBack)
            {
                buscarPlano(planoID);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int cod = (planoID);
                tarifadorEntities ctx = new tarifadorEntities();
                planotarifacao pla = ctx.planotarifacaos.First(p => p.id == cod);
                pla.nome = nome.Text.Trim();
                pla.tempoMinimoChamada = tempoMinimo.Text.Trim();
                pla.tempoMinimoTarifacao = tempoTarifMinimo.Text.Trim();
                pla.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                pla.periodicidadeTarifa = periodicidade.Text.Trim();
                pla.taxaConexao = decimal.Parse(taxaConexao.Text.Trim());
                pla.ligacao_0300 = decimal.Parse(valor0300.Text.Trim());
                pla.dddCelular = decimal.Parse(valorDDDCelular.Text.Trim());
                pla.dddFixo = decimal.Parse(valorDDDFixo.Text.Trim());
                pla.localCelular = decimal.Parse(valorLocalCelular.Text.Trim());
                pla.localFixo = decimal.Parse(valorLocalFixo.Text.Trim());
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
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
        private void buscarPlano(int cod)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                planotarifacao pla = ctx.planotarifacaos.First(p => p.id == cod);
                nome.Text = pla.nome;
                tempoMinimo.Text = pla.tempoMinimoChamada;
                tempoTarifMinimo.Text = pla.tempoMinimoTarifacao;
                cboxOperadoras.SelectedValue =Convert.ToString(pla.operadoraID);
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
    }
}