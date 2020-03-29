using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewGrupoRamais : System.Web.UI.Page
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