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
    public partial class login : System.Web.UI.Page
    {
        Conexao con = new Conexao();
        string user;
        string password;
        public string mensagem = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            user = usuario.Text.Trim();
            password = senha.Text.Trim();
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string senhaCriptografada = Criptografia.CalculaHash(password);
                string sql = "Select * from tarifador.usuario where login='" + user + "' and senha='" + senhaCriptografada + "'";
                MySqlCommand cmd;
                con.AbrirCon();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter();
                cmd = new MySqlCommand(sql, con.con2);
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    Session["logado"] = "SIM";
                    Session["nome"] = dt.Rows[0][1].ToString();
                    Session["perfil"] = dt.Rows[0][5].ToString();
                    Session["img"] = dt.Rows[0][7].ToString();
                    Session["cargo"] = dt.Rows[0][8].ToString();
                    Session["id"] = dt.Rows[0][0].ToString();
                    Response.Redirect("home.aspx");
                }
                else
                {
                    Session["logado"] = "NAO";
                    mensagem = "Usuário ou senha Inválidos!!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}