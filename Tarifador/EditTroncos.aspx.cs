using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class EditTroncos : System.Web.UI.Page
    {
        int troncoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            troncoID = int.Parse(Request.QueryString["troncoID"]);
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
                getVTroncos(troncoID);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                tarifadorEntities ctx = new tarifadorEntities();
                tronco tr = ctx.troncoes.First(p => p.id == troncoID);
                tr.nome = nome.Text.Trim();
                tr.ddd = ddd.Text.Trim();
                tr.numSaida = numSaida.Text.Trim();
                tr.planoID = Convert.ToInt32(cboxPlanoTarificao.SelectedValue);
                tr.grupoTroncoID = Convert.ToInt32(cboxGrupoTroncos.SelectedValue);
                tr.operadoraID = Convert.ToInt32(cboxOperadoras.SelectedValue);
                tr.canal = canal.Text.Trim();
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
            cboxGrupoTroncos.SelectedValue = Convert.ToString(tr.grupoTroncoID);
            planotarifacao pla = ctx.planotarifacaos.First(p => p.id == tr.planoID);
            cboxPlanoTarificao.SelectedValue = Convert.ToString(pla.id);
            grupotronco gr = ctx.grupotroncoes.First(p => p.id == tr.grupoTroncoID);
            cboxGrupoTroncos.SelectedValue = Convert.ToString(gr.id);
            operadora oper = ctx.operadoras.First(p => p.operadoraID == tr.operadoraID);
            cboxOperadoras.SelectedValue = Convert.ToString(oper.operadoraID);
        }
    }
}