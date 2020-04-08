using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class AddTroncos : System.Web.UI.Page
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
                tronco tr = new tronco();
                tr.nome = nome.Text.Trim();
                tr.ddd = ddd.Text.Trim();
                tr.numSaida = numSaida.Text.Trim();
                tr.planoID = Convert.ToInt32(cboxPlanoTarificao.SelectedValue);
                tr.grupoTroncoID = Convert.ToInt32(cboxGrupoTroncos.SelectedValue);
                tr.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                tr.canal = canal.Text.Trim();
                tarifadorEntities ctx = new tarifadorEntities();
                ctx.troncoes.Add(tr);
                ctx.SaveChanges();
                Page_Load(sender, e);
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Troncos.aspx");
        }
    }
}