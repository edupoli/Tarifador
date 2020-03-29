using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditOperadoras : System.Web.UI.Page
    {
        string operadoraID;
        protected void Page_Load(object sender, EventArgs e)
        {
            operadoraID = Request.QueryString["operadoraID"];
            if (!Page.IsPostBack)
            {
                buscaOperadora(int.Parse(operadoraID));
            }
            
        }
        
        private void buscaOperadora(int cod)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                operadora oper = ctx.operadoras.First(p => p.operadoraID == cod);
                textCodigo.Text = oper.codigo.ToString();
                textDescricao.Text = oper.descricao;
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                throw;
            }
            

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int cod = int.Parse(operadoraID);
                tarifadorEntities ctx = new tarifadorEntities();
                operadora oper = ctx.operadoras.First(p => p.operadoraID == cod);
                oper.codigo = int.Parse(textCodigo.Text);
                oper.descricao = textDescricao.Text;
                ctx.SaveChanges();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operadoras.aspx");
        }
    }
}