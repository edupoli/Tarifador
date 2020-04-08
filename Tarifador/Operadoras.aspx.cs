using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Operadoras1 : System.Web.UI.Page
    {
        int operadoraID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = new tarifador().GetOperadoras();
                GridView1.DataBind();
            }

        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                operadoraID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                Response.Redirect("EditOperadoras.aspx?operadoraID=" + operadoraID);
            }
            
        }
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                try
                {
                    operadoraID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    int cod = operadoraID;
                    tarifadorEntities ctx = new tarifadorEntities();
                    operadora oper = ctx.operadoras.First(p => p.operadoraID == cod);
                    ctx.operadoras.Remove(oper);
                    ctx.SaveChanges();
                    GridView1.DataSource = new tarifador().GetOperadoras();
                    GridView1.DataBind();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }

                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    //throw;
                }
            }
                
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            operadoraID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewOperadoras.aspx?operadoraID=" + operadoraID);
        }
    }
}