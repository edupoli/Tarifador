using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Troncos : System.Web.UI.Page
    {
        int troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getTroncos();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            troncoID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewTroncos.aspx?troncoID=" + troncoID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            troncoID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("EditTroncos.aspx?troncoID=" + troncoID);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                troncoID = int.Parse((sender as LinkButton).CommandArgument);
                tarifadorEntities ctx = new tarifadorEntities();
                tronco tr = ctx.troncoes.First(p => p.id == troncoID);
                ctx.troncoes.Remove(tr);
                ctx.SaveChanges();
                getTroncos();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        private void getTroncos()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.troncoes
                             join b in ctx.operadoras on a.operadoraID equals b.operadoraID
                             join c in ctx.grupotroncoes on a.grupoTroncoID equals c.id
                             join d in ctx.planotarifacaos on a.planoID equals d.id
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.ddd,
                                 a.numSaida,
                                 plano= d.nome, //criar alias em expressoes LINQ
                                 grupo= c.nome,
                                 b.descricao,
                                 a.canal
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }
    }
}