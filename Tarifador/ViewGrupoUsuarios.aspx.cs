using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewGrupoUsuarios : System.Web.UI.Page
    {
        string usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioID = Request.QueryString["usuarioID"];
            if (!Page.IsPostBack)
            {
                getGrupoUsuarios(int.Parse(usuarioID));
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GruposUsuarios.aspx");
        }
        private void getGrupoUsuarios(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            grupousuario gu = ctx.grupousuarios.First(p => p.id == cod);
            nome.Text = gu.nome;
        }
    }
}