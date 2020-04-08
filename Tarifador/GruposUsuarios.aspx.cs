using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class GruposUsuarios : System.Web.UI.Page
    {
        string usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = new tarifador().GetGrupoUsuarios();
                GridView1.DataBind();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            usuarioID = ((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewGrupoUsuarios.aspx?usuarioID="+usuarioID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                usuarioID = ((sender as LinkButton).CommandArgument);
                Response.Redirect("EditGruposUsuarios.aspx?usuarioID=" + usuarioID);
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
                    int cod = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    tarifadorEntities ctx = new tarifadorEntities();
                    grupousuario gu = ctx.grupousuarios.First(p => p.id == cod);
                    ctx.grupousuarios.Remove(gu);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    GridView1.DataSource = new tarifador().GetGrupoUsuarios();
                    GridView1.DataBind();
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    //throw;
                }
            }
        }
    }
}