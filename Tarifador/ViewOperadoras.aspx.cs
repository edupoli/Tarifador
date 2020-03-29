using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewOperadoras : System.Web.UI.Page
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operadoras.aspx");
        }
        private void buscaOperadora(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            operadora op = ctx.operadoras.First(p => p.operadoraID == cod);
            textCodigo.Text = Convert.ToString(op.codigo);
            textDescricao.Text = op.descricao;

        }
    }
}