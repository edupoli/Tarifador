using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class ViewTroncos : System.Web.UI.Page
    {
        int troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            troncoID = Convert.ToInt32(Request.QueryString["troncoID"]);
            getVTroncos(troncoID);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Troncos.aspx");
        }
        private void getVTroncos(int cod)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            tronco tr = ctx.troncoes.First(p => p.id == cod);
            nome.Text = tr.nome;
            ddd.Text = tr.ddd;
            numSaida.Text = tr.numSaida;
            canal.Text = tr.canal;
            planotarifacao pla = ctx.planotarifacaos.First(p => p.id == tr.planoID );
            string op = pla.nome;
            cboxPlanoTarificao.Items.Insert(0, new ListItem(op, "1"));
            grupotronco gr = ctx.grupotroncoes.First(p => p.id == tr.grupoTroncoID);
            string gg = gr.nome;
            cboxGrupoTroncos.Items.Insert(0, new ListItem(gg, "1"));
            operadora oper = ctx.operadoras.First(p => p.operadoraID == tr.operadoraID);
            string oo = oper.descricao;
            cboxOperadoras.Items.Insert(0, new ListItem(oo, "1"));
        }
    }
}