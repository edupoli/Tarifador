using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditGrupoRamais : System.Web.UI.Page
    {
        string ramalID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ramalID = Request.QueryString["ramalID"];
            if (!Page.IsPostBack)
            {
                getRamal(int.Parse(ramalID));
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int cod = int.Parse(ramalID);
                tarifadorEntities ctx = new tarifadorEntities();
                gruporamal gr = ctx.gruporamals.First(p => p.id == cod);
                gr.nome = nome.Text.Trim();
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
            Response.Redirect("GrupoRamais.aspx");
        }
        private void getRamal(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            gruporamal gr = ctx.gruporamals.First(p => p.id == cod);
            nome.Text = gr.nome;
        }
    }
}