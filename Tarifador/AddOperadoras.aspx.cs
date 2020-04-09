using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Operadoras : System.Web.UI.Page
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
            if (textCodigo.Text == "")
            {
                mensagem = "Campo Codigo é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                textCodigo.Focus();
            }else
                if (textDescricao.Text == "")
            {
                mensagem = "Campo Descrição é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                textDescricao.Focus();
            }
            else
            {
                try
                {
                    var p = new operadora();
                    p.codigo = Convert.ToInt32(textCodigo.Text);
                    p.descricao = textDescricao.Text;
                    new tarifador().addOperadora(p);
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                }
            }
            
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operadoras.aspx");
        }
    }
}