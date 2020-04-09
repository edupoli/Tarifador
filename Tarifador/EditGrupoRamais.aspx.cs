using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditGrupoRamais : System.Web.UI.Page
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
            if (nome.Text == "")
            {
                mensagem = "Campo Nome é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                nome.Focus();
            }
            else
            {
                try
                {
                    int cod = int.Parse(ramalID);
                    tarifadorEntities ctx = new tarifadorEntities();
                    gruporamal gr = ctx.gruporamals.First(p => p.id == cod);
                    gr.nome = nome.Text.Trim();
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