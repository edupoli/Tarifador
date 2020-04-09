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
        public string mensagem = "";
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
            if (nome.Text == "")
            {
                mensagem = "Campo Nome é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                nome.Focus();
            }else
                if (tempoMinimo.Text == "")
            {
                mensagem = "Campo Tempo Mínimo é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                tempoMinimo.Focus();
            }else
                if (tempoTarifMinimo.Text == "")
            {
                mensagem = "Campo Tempo de Tarifação Mínimo é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                tempoTarifMinimo.Focus();
            }else
                if (periodicidade.Text =="")
            {
                mensagem = "Campo Periodicidade  é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                periodicidade.Focus();
            }else
                if (valor0300.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valor0300.Focus();
            }else
                if (valorDDDCelular.Text =="")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorDDDCelular.Focus();
            }else
                if (valorDDDFixo.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorDDDFixo.Focus();
            }else
                if (valorLocalCelular.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorLocalCelular.Focus();
            }else
                if (valorLocalFixo.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorLocalFixo.Focus();
            }
            else
            {
                try
                {
                    planotarifacao plano = new planotarifacao();
                    plano.nome = nome.Text.Trim();
                    plano.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                    plano.periodicidadeTarifa = periodicidade.Text.Trim();
                    plano.taxaConexao = decimal.Parse(taxaConexao.Text.Trim());
                    plano.tempoMinimoChamada = tempoMinimo.Text.Trim();
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
            
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planos.aspx");
        }
    }
}