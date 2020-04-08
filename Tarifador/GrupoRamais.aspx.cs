using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class GrupoRamais : System.Web.UI.Page
    {
        string ramalID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = new tarifador().GetGruporamals();
                GridView1.DataBind();
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            ramalID = (sender as LinkButton).CommandArgument;
            Response.Redirect("ViewGrupoRamais.aspx?ramalID=" + ramalID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                ramalID = (sender as LinkButton).CommandArgument;
                Response.Redirect("EditGrupoRamais.aspx?ramalID=" + ramalID);
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
                    int cod = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    tarifadorEntities ctx = new tarifadorEntities();
                    gruporamal gr = ctx.gruporamals.First(p => p.id == cod);
                    ctx.gruporamals.Remove(gr);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    GridView1.DataSource = new tarifador().GetGruporamals();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    //throw (ex);
                }
            }
            
        }
        
    }
}