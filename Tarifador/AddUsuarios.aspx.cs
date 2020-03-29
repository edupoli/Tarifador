using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class AddUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                usuario user = new usuario();
                user.nome = nome.Text.Trim();
                user.emaill = email.Text.Trim();
                user.login = login.Text.Trim();
                user.senha = senha.Text.Trim();
                user.perfil = cboxPerfil.SelectedValue;
                user.grupoUserID = int.Parse(cboxGrupo.SelectedValue);
                ctx.usuarios.Add(user);
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                nome.Text = string.Empty;
                email.Text = string.Empty;
                login.Text = string.Empty;
                senha.Text = string.Empty;
                cboxPerfil.SelectedIndex = -1;
                cboxGrupo.SelectedIndex = -1;
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                throw;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }
    }
}