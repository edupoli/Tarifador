using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class GrupoTroncos : System.Web.UI.Page
    {
        int troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getGrupoTroncos();
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
                try
                {
                    troncoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    Response.Redirect("EditGrupoTroncos.aspx?troncoID=" + troncoID);
                }
                catch (Exception)
                {

                    throw;
                }
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
                    troncoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    tarifadorEntities ctx = new tarifadorEntities();
                    grupotronco gr = ctx.grupotroncoes.First(p => p.id == troncoID);
                    ctx.grupotroncoes.Remove(gr);
                    ctx.SaveChanges();
                    getGrupoTroncos();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    //throw;
                }
            }
                
        }
        private void getGrupoTroncos()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.grupotroncoes
                             join b in ctx.operadoras on a.operadoraID equals b.operadoraID
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 b.descricao,
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            troncoID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewGrupoTroncos.aspx?troncoID=" + troncoID);
        }
    }
}