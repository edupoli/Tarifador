using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarifador
{
    public class Conexao
    {
        public string conec = "SERVER=10.0.2.128;UID=admin;PWD=ask%123;Allow User Variables=True";
        public string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True";
        public MySqlConnection con = null;
        public MySqlConnection con2 = null;
        public void AbrirCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con2 = new MySqlConnection(conecLocal);
                con.Open();
                con2.Open();
                //HttpContext.Current.Response.Write("Conectado");
            }
            catch (System.Exception ex)
            {

                HttpContext.Current.Response.Write("Erro ao conectar" + ex);
            }
        }
        public void FecharCon()
        {
            try
            {
                con = new MySqlConnection(conec);
                con2 = new MySqlConnection(conecLocal);
                con.Close();
                con2.Close();
            }
            catch (System.Exception ex)
            {

                HttpContext.Current.Response.Write("Erro ao conectar" + ex);
            }
        }
    }
}