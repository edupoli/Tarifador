using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication19
{
    public partial class PlanoTarifacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            planotarifacao plano = new planotarifacao();
            plano.nome = nome.Text.Trim();
            plano.operadora = cboxOperadoras.SelectedValue;
            plano.periodicidadeTarifa = periodicidade.Text.Trim();
            plano.taxaConexao = taxaConexao.Text.Trim();
            plano.tempoMinimoChamada = tempoMinimo.Text.Trim();
            plano.tempoMinimoTarifacao = tempoTarifMinimo.Text.Trim();
            plano.C0300 = valor0300.Text.Trim();
            plano.dddCelular = valorDDDCelular.Text.Trim();
            plano.dddFixo = valorDDDFixo.Text.Trim();
            plano.localCelular = valorLocalCelular.Text.Trim();
            plano.localFixo = valorLocalFixo.Text.Trim();

            tarifadorEntities ctx = new tarifadorEntities();
            ctx.planotarifacaos.Add(plano);
            ctx.SaveChanges();
            ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);

        }
    }
}