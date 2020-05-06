using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace Tarifador
{
    public partial class EditPlanoTarifacao : System.Web.UI.Page
    {
        int planoID;
        public string mensagem = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            planoID = Convert.ToInt32(Request.QueryString["planoID"]);
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
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
                    buscarPlano(planoID);
                }
                
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nome.Text == "")
            {
                mensagem = "Campo Nome é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                nome.Focus();
            }
            else
                if (tempoMinimo.Text == "")
            {
                mensagem = "Campo Tempo Mínimo é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                tempoMinimo.Focus();
            }
            else
                if (tempoTarifMinimo.Text == "")
            {
                mensagem = "Campo Tempo de Tarifação Mínimo é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                tempoTarifMinimo.Focus();
            }
            else
                if (periodicidade.Text == "")
            {
                mensagem = "Campo Periodicidade  é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                periodicidade.Focus();
            }
            else
                if (valor0300.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valor0300.Focus();
            }
            else
                if (valorDDDCelular.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorDDDCelular.Focus();
            }
            else
                if (valorDDDFixo.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorDDDFixo.Focus();
            }
            else
                if (valorLocalCelular.Text == "")
            {
                mensagem = "Todos os valores das tarifas são obrigatorios";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                valorLocalCelular.Focus();
            }
            else
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
                    int cod = (planoID);
                    tarifadorEntities ctx = new tarifadorEntities();
                    planotarifacao pla = ctx.planotarifacaos.First(p => p.id == cod);
                    pla.nome = nome.Text.Trim();
                    pla.tempoMinimoChamada = tempoMinimo.Text.Trim();
                    pla.tempoMinimoTarifacao = tempoTarifMinimo.Text.Trim();
                    pla.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                    pla.periodicidadeTarifa = periodicidade.Text.Trim();
                    pla.taxaConexao = decimal.Parse(taxaConexao.Text.Trim(), CultureInfo.InvariantCulture);
                    pla.ligacao_0300 = decimal.Parse(valor0300.Text.Trim(), CultureInfo.InvariantCulture);
                    pla.dddCelular = decimal.Parse(valorDDDCelular.Text.Trim(), CultureInfo.InvariantCulture);
                    pla.dddFixo = decimal.Parse(valorDDDFixo.Text.Trim(), CultureInfo.InvariantCulture);
                    pla.localCelular = decimal.Parse(valorLocalCelular.Text, CultureInfo.InvariantCulture);
                    pla.localFixo = decimal.Parse(valorLocalFixo.Text.Trim(), CultureInfo.InvariantCulture);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o Seguinte erro: "+ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                }
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
            catch (Exception ex)
            {
                mensagem = "Ocorreu o Seguinte erro: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
            }
        }
    }
}