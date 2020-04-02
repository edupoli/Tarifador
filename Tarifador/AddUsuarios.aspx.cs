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
        string password;
        string logado;
        protected void Page_Load(object sender, EventArgs e)
        {
            //logado = HttpContext.Current.Session["logado"].ToString();
            if (HttpContext.Current.Session == null || HttpContext.Current.Session["logado"] == null || HttpContext.Current.Session["perfil"].ToString() == "comum")
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                imgSel.ImageUrl = "dist/img/users/user-160x160.png";
            }
            lblCaminhoImg.Visible = false;
            password = senha.Text;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string senhaCriptografada = Criptografia.CalculaHash(password);
                tarifadorEntities ctx = new tarifadorEntities();
                usuario user = new usuario();
                user.nome = nome.Text.Trim();
                user.emaill = email.Text.Trim();
                user.login = login.Text.Trim();
                user.senha = senhaCriptografada;
                user.perfil = cboxPerfil.SelectedValue;
                user.grupoUserID = int.Parse(cboxGrupo.SelectedValue);
                user.img = lblCaminhoImg.Text.Trim();
                user.cargo = cargo.Text.Trim();
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (img.HasFiles)
                {
                    lblCaminhoImg.Text = img.FileName;
                    img.SaveAs(Server.MapPath("dist/img/users/" + img.FileName));
                    imgSel.ImageUrl = "dist/img/users/" + img.FileName;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "uploadSucesso();", true);
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "uploadErro();", true);
                throw;
            }
        }

    }
}