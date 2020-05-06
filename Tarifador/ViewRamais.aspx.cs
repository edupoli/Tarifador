using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewRamais : System.Web.UI.Page
    {
        string ramalID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ramalID = Request.QueryString["ramalID"];
            if (!Page.IsPostBack)
            {
                getRamais(int.Parse(ramalID));
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ramais.aspx");
        }
        private void getRamais(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            ramal ra = ctx.ramals.First(p => p.id == cod);
            numero.Text = ra.numero;
            cboxGrupoRamais.SelectedValue = Convert.ToString(ra.grupoRamalID);
            cboxUsuario.SelectedValue = Convert.ToString(ra.usuarioID);
        }
    }
}