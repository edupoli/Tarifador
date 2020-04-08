using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class AddGrupoTroncos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            if (Session["perfil"].ToString() != "administrador")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                Response.Redirect("login.aspx");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                grupotronco gtronco = new grupotronco();
                gtronco.nome = nome.Text.Trim();
                gtronco.operadoraID = int.Parse(cboxOperadora.SelectedValue);
                ctx.grupotroncoes.Add(gtronco);
                ctx.SaveChanges();
                nome.Text = "";
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
    }
}