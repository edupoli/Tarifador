using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Planos : System.Web.UI.Page
    {
        int planoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getPlanos();
            }
            
        }
        


        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                planoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Response.Redirect("EditPlanoTarifacao.aspx?planoID=" + planoID);
            }
                
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                try
                {
                    planoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    tarifadorEntities ctx = new tarifadorEntities();
                    planotarifacao pla = ctx.planotarifacaos.First(p => p.id == planoID);
                    ctx.planotarifacaos.Remove(pla);
                    ctx.SaveChanges();
                    GridView1.DataSource = new tarifador().GetPlanotarifacaos();
                    GridView1.DataBind();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    //throw;
                }
            }
                
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            planoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Response.Redirect("VisualizarPlano.aspx?planoID=" + planoID);
        }
        private void getPlanos()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.planotarifacaos
                             join b in ctx.operadoras on a.operadoraID equals b.operadoraID
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.tempoMinimoChamada,
                                 a.tempoMinimoTarifacao,
                                 a.operadoraID,
                                 a.periodicidadeTarifa,
                                 a.taxaConexao,
                                 a.ligacao_0300,
                                 a.dddCelular,
                                 a.dddFixo,
                                 a.localCelular,
                                 a.localFixo,
                                 b.descricao,
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();

        }
    }
}