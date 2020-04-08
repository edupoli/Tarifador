using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class PlanoTarifacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            if (Session["perfil"].ToString() != "administrador")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                Response.Redirect("login.aspx");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                planotarifacao plano = new planotarifacao();
                plano.nome = nome.Text.Trim();
                plano.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                plano.periodicidadeTarifa = periodicidade.Text.Trim();
                plano.taxaConexao = decimal.Parse(taxaConexao.Text.Trim());
                plano.tempoMinimoChamada =  tempoMinimo.Text.Trim();
                plano.tempoMinimoTarifacao = tempoTarifMinimo.Text.Trim();
                plano.ligacao_0300 = decimal.Parse(valor0300.Text.Trim());
                plano.dddCelular = decimal.Parse(valorDDDCelular.Text.Trim());
                plano.dddFixo = decimal.Parse(valorDDDFixo.Text.Trim());
                plano.localCelular = decimal.Parse(valorLocalCelular.Text.Trim());
                plano.localFixo = decimal.Parse(valorLocalFixo.Text.Trim());

                tarifadorEntities ctx = new tarifadorEntities();
                ctx.planotarifacaos.Add(plano);
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planos.aspx");
        }
    }
}