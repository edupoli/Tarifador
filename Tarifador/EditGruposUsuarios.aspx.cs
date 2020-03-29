using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditGruposUsuarios : System.Web.UI.Page
    {
        string usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioID = Request.QueryString["usuarioID"];
            if (!Page.IsPostBack)
            {
                getGrupoUsuario(int.Parse(usuarioID));
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int cod = int.Parse(usuarioID);
                tarifadorEntities ctx = new tarifadorEntities();
                grupousuario gu = ctx.grupousuarios.First(p => p.id == cod);
                gu.nome = nome.Text.Trim();
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                throw;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GruposUsuarios.aspx");
        }
        private void getGrupoUsuario(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            grupousuario gu = ctx.grupousuarios.First(p => p.id == cod);
            nome.Text = gu.nome;
        }
    }
}