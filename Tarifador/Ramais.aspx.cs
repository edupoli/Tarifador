using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Ramais : System.Web.UI.Page
    {
        int ramalID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getRamais();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            ramalID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewRamais.aspx?ramalID=" + ramalID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                ramalID = int.Parse((sender as LinkButton).CommandArgument);
                Response.Redirect("EditRamais.aspx?ramalID=" + ramalID);
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
                    ramalID = int.Parse((sender as LinkButton).CommandArgument);
                    tarifadorEntities ctx = new tarifadorEntities();
                    ramal ra = ctx.ramals.First(p => p.id == ramalID);
                    ctx.ramals.Remove(ra);
                    ctx.SaveChanges();
                    getRamais();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);

                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    throw;
                }
            }
                
        }
        private void getRamais()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.ramals
                             join b in ctx.gruporamals on a.grupoRamalID equals b.id
                             join c in ctx.usuarios on a.usuarioID equals c.id
                             select new
                             {
                                 a.id,
                                 a.numero,
                                 grupo = b.nome,
                                 usuario = c.nome,
                                 
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }
    }
}