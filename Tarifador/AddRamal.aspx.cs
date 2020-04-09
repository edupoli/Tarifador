using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class AddRamal : System.Web.UI.Page
    {
        public string mensagem = "";
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
            if (numero.Text == "")
            {
                mensagem = "O Numero do ramal é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                numero.Focus();
            }
            else
            {
                try
                {
                    tarifadorEntities ctx = new tarifadorEntities();
                    ramal ra = new ramal();
                    ra.numero = numero.Text.Trim();
                    ra.grupoRamalID = int.Parse(cboxGrupoRamais.SelectedValue);
                    ra.usuarioID = int.Parse(cboxUsuario.SelectedValue);
                    ra.servidor = servidor.Text.Trim();
                    ra.observacao = observacao.Text.Trim();
                    ctx.ramals.Add(ra);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    throw;
                }
            }
            
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ramais.aspx");
        }
    }
}