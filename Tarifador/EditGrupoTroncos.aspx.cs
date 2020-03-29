using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditGrupoTroncos : System.Web.UI.Page
    {
        int troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            troncoID = Convert.ToInt32(Request.QueryString["troncoID"]);
            if (!Page.IsPostBack)
            {
                buscarGrupoTronco(troncoID);
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                grupotronco gr = ctx.grupotroncoes.First(g => g.id == troncoID);
                gr.nome = nome.Text.Trim();
                gr.operadoraID = Convert.ToInt32(cboxOperadora.SelectedValue);
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrupoTroncos.aspx");
        }
        private void buscarGrupoTronco(int cod)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                grupotronco gr = ctx.grupotroncoes.First(g => g.id == cod);
                nome.Text = gr.nome.ToString();
                cboxOperadora.SelectedValue = Convert.ToString(gr.operadoraID);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}