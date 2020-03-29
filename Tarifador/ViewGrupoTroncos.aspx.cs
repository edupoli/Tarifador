using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewGrupoTroncos : System.Web.UI.Page
    {
        string troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            troncoID = Request.QueryString["troncoID"];
            if (!Page.IsPostBack)
            {
                buscaTronco(int.Parse(troncoID));
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrupoTroncos.aspx");
        }
        private void buscaTronco(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            grupotronco gr = ctx.grupotroncoes.First(p => p.id == cod);
            nome.Text = gr.nome;
            int op = gr.operadoraID;
            operadora oo = ctx.operadoras.First(p => p.operadoraID == op);
            string du = oo.descricao;
            cboxOperadora.Items.Insert(0, new ListItem(du, "1"));
        }
    }
}