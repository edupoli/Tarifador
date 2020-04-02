using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditUsuarios : System.Web.UI.Page
    {
        int usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioID = Convert.ToInt32(Request.QueryString["usuarioID"]);
            if (!Page.IsPostBack)
            {
                getUsuarios(usuarioID);
            }
            lblCaminhoImg.Visible = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                usuario user = ctx.usuarios.First(p => p.id == usuarioID);
                user.nome = nome.Text.Trim();
                user.emaill = email.Text.Trim();
                user.login = login.Text.Trim();
                user.senha = senha.Text.Trim();
                user.perfil = cboxPerfil.SelectedValue;
                user.grupoUserID = int.Parse(cboxGrupo.SelectedValue);
                user.img = lblCaminhoImg.Text;
                user.cargo = cargo.Text;
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
            Response.Redirect("Usuarios.aspx");
        }
        private void getUsuarios(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            usuario user = ctx.usuarios.First(p => p.id == cod);
            nome.Text = user.nome;
            email.Text = user.emaill;
            login.Text = user.login;
            senha.Text = user.senha;
            cboxPerfil.SelectedValue = user.perfil;
            cboxGrupo.SelectedValue = Convert.ToString(user.grupoUserID);
            imgSel.ImageUrl = "dist/img/users/" + user.img;
            lblCaminhoImg.Text = user.img;
            cargo.Text = user.cargo;

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