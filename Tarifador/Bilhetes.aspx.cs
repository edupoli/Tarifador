using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Bilhetes : System.Web.UI.Page
    {
        Conexao con = new Conexao();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreencherCbox();
            }
        }

        private void PreencherCbox()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from t in ctx.troncoes
                             select new
                             {
                                 t.id,
                                 t.canal,
                             });
            cboxCanal.Items.Insert(0, new ListItem("Todos","todos"));
            foreach (var item in resultado)
            {
                cboxCanal.Items.Add(new ListItem(item.canal, item.canal));
            }
        
        }

        public void tarifar(string dataI, string dataF, string tipoChamada, string canals)
        {
            // Busca os registros das ligações no BD asterix filtrando atravez de REGEX somente as ligações feitas de origem interna para destino externo           
            string sql = "";
            MySqlCommand cmd;
            con.AbrirCon();
            DataTable dt = new DataTable("TotalChamadas");
            MySqlDataAdapter da = new MySqlDataAdapter();

            if (canals == "todos" && tipoChamada == "todos")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dst REGEXP '^0[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0300]$')";
            }
            if (canals != "todos" && tipoChamada == "todos")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and(dst REGEXP '^0[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0300]$')";
            }
            if (canals == "todos" && tipoChamada == "Fixolocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dst REGEXP '^0[1-9]{8}$')";
            }
            if (canals == "todos" && tipoChamada == "CelularLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[9]{1}[1-9]{8}$'";
            }
            if (canals == "todos" && tipoChamada == "dddLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0-9]{2}[1-9]{8}$'";
            }
            if (canals == "todos" && tipoChamada == "Celularddd")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'";
            }
            if (canals == "todos" && tipoChamada == "_0300regex")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0300]$')";
            }

            if (canals != "todos" && tipoChamada == "Fixolocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and(dst REGEXP '^0[1-9]{8}$'";
            }
            if (canals != "todos" && tipoChamada == "CelularLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and dst regexp '^0[9]{1}[1-9]{8}$'";
            }
            if (canals != "todos" && tipoChamada == "dddLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and dst regexp '^0[0-9]{2}[1-9]{8}$'";
            }
            if (canals != "todos" && tipoChamada == "Celularddd")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'";
            }
            if (canals != "todos" && tipoChamada == "_0300regex")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,tempo,canal,tipoChamada,valor FROM tarifador.bilhetes where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(canal like '%" + canals + "%')" +
                    "and dst regexp '^0[0300]$')";
            }
            cmd = new MySqlCommand(sql, con.con2);
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dataInicial.Text == "" || dataFinal.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                tarifar(dataInicial.Text, dataFinal.Text, tipoChamada.SelectedValue, cboxCanal.SelectedValue);
            }
        }
    }
}