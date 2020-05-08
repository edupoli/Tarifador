using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditRamais : System.Web.UI.Page
    {
        string ramalID;
        public string mensagem = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ramalID = Request.QueryString["ramalID"];
            if (!Page.IsPostBack)
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
                getRamal(int.Parse(ramalID));
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
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
                    int cod = int.Parse(ramalID);
                    tarifadorEntities ctx = new tarifadorEntities();
                    ramal ra = ctx.ramals.First(p => p.id == cod);
                    ra.numero = numero.Text.Trim();
                    ra.grupoRamalID = int.Parse(cboxGrupoRamais.SelectedValue);
                    ra.usuarioID = int.Parse(cboxUsuario.SelectedValue);
                    ra.observacao = observacao.Text.Trim();
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
        private void getRamal(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            ramal ra = ctx.ramals.First(p => p.id == cod);
            numero.Text = ra.numero;
            cboxGrupoRamais.SelectedValue = Convert.ToString(ra.grupoRamalID);
            cboxUsuario.SelectedValue = Convert.ToString(ra.usuarioID);
            observacao.Text = ra.observacao;
        }

        protected void btnAddUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUsuarios.aspx");
        }
    }
}