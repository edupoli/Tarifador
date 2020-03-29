using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Usuarios : System.Web.UI.Page
    {
        int usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getUsuarios();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            usuarioID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewUsuarios.aspx?usuarioID=" + usuarioID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            usuarioID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("EditUsuarios.aspx?usuarioID=" + usuarioID);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                usuarioID = int.Parse((sender as LinkButton).CommandArgument);
                tarifadorEntities ctx = new tarifadorEntities();
                usuario user = ctx.usuarios.First(p => p.id == usuarioID);
                ctx.usuarios.Remove(user);
                ctx.SaveChanges();
                getUsuarios();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                throw;
            }
            
        }
        private void getUsuarios()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.usuarios
                             join b in ctx.grupousuarios on a.grupoUserID equals b.id
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.emaill,
                                 a.login,
                                 a.senha,
                                 a.perfil,
                                 grupo = b.nome,

                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }
    }
}