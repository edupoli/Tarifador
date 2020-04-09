using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class home : System.Web.UI.Page
    {
        Conexao con = new Conexao();
        public string[] Labels { get; set; }
        public string Legend { get; set; }
        public int[] Data { get; set; }
        public int[] Data2 { get; set; }
        public List<object> dd = new List<object>();
        public int[] Data3 { get; set; }
        public int[] Data4 { get; set; }
        public List<object> dd2 = new List<object>();
        public List<string> ll = new List<string>();
        public int inicio;
        public int[] indice = new int[24];
        public int[] indice2 = new int[24];
        public int[] indice3 = new int[7];
        public int[] indice4 = new int[7];
        public string qtdChaOntem;
        public string qtdChahoje;
        public string[] userRamal = new string[5];
        public string[] numRamal = new string[5];
        public string[] totalRamal = new string[5];

        protected void Page_Load(object sender, EventArgs e)
        {
            tt.InnerText = "50";
            Listar();
            //Labels = new string[] { "00h", "01h", "02h", "03h", "04h", "05h", "06h", "07h", "08h", "09h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h", "19h", "20h", "21h", "22h", "23h" };
            GetChart();
            getData();
            getData2();
            topRamais();
            getSemana();
            getSemanaAnterior();
            totalUsers();
            totalChamadasMes();
        }

        // Função que mostra na tabela as 10 ligações mais recentes
        private void Listar()
        {
            string sql;
            MySqlCommand cmd;
            con.AbrirCon();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();
            sql = "SELECT calldate,src,dst,sec_to_time(duration) as tempo, cnam FROM asteriskcdrdb.cdr order by idcdr desc limit 10";
            cmd = new MySqlCommand(sql, con.con);
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridViewDIALLIST.DataSource = dt;
            GridViewDIALLIST.DataBind();
        }

        // função que verifica a quantidade de usuarios e ramais cadastrados no Elastix
        private void totalUsers()
        {
            con.AbrirCon();
            MySqlCommand cmd = new MySqlCommand("SELECT count(extension) as total FROM asterisk.users", con.con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            string qtdaUser = Convert.ToString(dr["total"]);
            boxUsers.InnerText = qtdaUser;
            boxRamais.InnerText=qtdaUser;
        }

        private void totalChamadasMes()
        {
            con.AbrirCon();
            MySqlCommand cmd = new MySqlCommand("SELECT count(*) as total FROM asteriskcdrdb.cdr WHERE YEAR(calldate) = YEAR(CURRENT_DATE()) AND MONTH(calldate) = MONTH(CURRENT_DATE()) and(dst REGEXP '^0[1-9]{8}$' or dst regexp '^0[0-9]{2}[1-9]{8}$' or dst regexp '^0[9]{1}[1-9]{8}$' or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$' or dst regexp '^0[0300]$') and(disposition = 'ANSWERED')", con.con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            string qtdChamadas = Convert.ToString(dr["total"]);
            TotalChaMes.InnerText = qtdChamadas;
            
        }

        // Função que faz a contabilização do total de chamadas por hora nas ultimas 24horas 
        [WebMethod]
        public static void GetChart()
        {
            string conec = "SERVER=10.0.2.128;UID=admin;PWD=ask%123;Allow User Variables=True";
            MySqlConnection conn = new MySqlConnection(conec);
            string query = string.Format("SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora DESC limit 24");
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            conn.Open();
            MySqlDataReader sdr = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            while (sdr.Read())
            {
                sb.Append("{");
                System.Threading.Thread.Sleep(50);
                string color = String.Format("#{0:X6}", new Random().Next(0x1000000));
                sb.Append(string.Format("text :'{0}', value:{1}, color: '{2}'", sdr[0], sdr[1], color));
                sb.Append("},");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            conn.Close();
            //return sb.ToString();
            
        }
        private void getData()
        {
            string sql;
            string sql2;
            MySqlDataReader dr;
            MySqlCommand cmd;
            con.AbrirCon();
            DataTable dt = new DataTable();
            
            // faz a contabilizacao de quantidades de chamadas por hora nas ultimas 24horas do dia anterior
            sql = "SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate BETWEEN DATE(NOW()) - INTERVAL 1 DAY AND DATE(NOW()) GROUP BY hora ORDER BY hora DESC limit 24";

            // pega o total geral de chamadas feita no dia anterior
            sql2 = "SELECT count(*) as total FROM asteriskcdrdb.cdr WHERE calldate BETWEEN DATE(NOW()) - INTERVAL 1 DAY AND DATE(NOW())";
            cmd = new MySqlCommand(sql, con.con);
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<string> total = new List<string>();
            List<int> dados = new List<int>();
            int linhas = dt.Rows.Count;
            dr = cmd.ExecuteReader();

            for (int i = 0; i < linhas; i++)
            {
                dr.Read();
                int valor = Convert.ToInt32(dr["hora"]);
                int dd = Convert.ToInt32(dr["total"]);
                indice[valor] = dd;

            }

            dr.Close();
            MySqlDataReader dr2;
            MySqlCommand cmd2;
            cmd2 = new MySqlCommand(sql2, con.con);
            dr2 = cmd2.ExecuteReader();
            dr2.Read();
            qtdChaOntem = Convert.ToString(dr2["total"]);
            totalOntem.InnerText = qtdChaOntem;

            string[] get = total.ToArray();
            Labels = total.ToArray();
            Data = indice.ToArray();

            con.FecharCon();

        }
        private void getData2()
        {
            string sql;
            MySqlDataReader dr2;
            MySqlCommand cmd2;
            con.AbrirCon();
            DataTable dt2 = new DataTable();
            string sql2 = "Select  HOUR(calldate) AS hora, count(*) AS total  from asteriskcdrdb.cdr WHERE DATE(calldate) = DATE( NOW() )  GROUP BY hora ORDER BY hora desc ";
            sql = "Select count(*) AS total  from asteriskcdrdb.cdr WHERE DATE(calldate) = DATE( NOW() )";
            cmd2 = new MySqlCommand(sql2, con.con);
            dr2 = cmd2.ExecuteReader();
            dt2.Load(dr2);
            List<string> total = new List<string>();
            List<int> dados2 = new List<int>();
            int linhas2 = dt2.Rows.Count;
            dr2 = cmd2.ExecuteReader();

            for (int i = 0; i < linhas2; i++)
            {
                dr2.Read();
                int valor = Convert.ToInt32(dr2["hora"]);
                int dd2 = Convert.ToInt32(dr2["total"]);
                indice2[valor] = dd2;

            }

            dr2.Close();
            MySqlDataReader dr;
            MySqlCommand cmd;
            cmd = new MySqlCommand(sql, con.con);
            dr = cmd.ExecuteReader();
            dr.Read();
            qtdChahoje = Convert.ToString(dr["total"]);
            totalhoje.InnerText = qtdChahoje;

            string[] get = total.ToArray();
            Labels = total.ToArray();
            Data2 = indice2.ToArray();

            con.FecharCon();

        }
        private void topRamais()
        {
            con.AbrirCon();
            MySqlCommand cmd = new MySqlCommand("Select src,cnam, count(src) as total from asteriskcdrdb.cdr WHERE DATE(calldate) = DATE( NOW() )  GROUP BY src ORDER BY total desc ", con.con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    userRamal[i] = Convert.ToString(dr["cnam"]);
                    numRamal[i] = Convert.ToString(dr["src"]);
                    totalRamal[i] = Convert.ToString(dr["total"]);
                    lbl1.Text = userRamal[0];
                    lbl2.Text = numRamal[0];
                    lbltotal.Text = qtdChahoje;
                    lbltotalRamal.Text = totalRamal[0];
                    Label3.Text = userRamal[1];
                    Label4.Text = numRamal[1];
                    Label2.Text = qtdChahoje;
                    Label1.Text = totalRamal[1];
                    Label5.Text = userRamal[2];
                    Label6.Text = numRamal[2];
                    Label8.Text = qtdChahoje;
                    Label7.Text = totalRamal[2];
                    Label9.Text = userRamal[3];
                    Label10.Text = numRamal[3];
                    Label12.Text = qtdChahoje;
                    Label11.Text = totalRamal[3];
                }
            }
            else
            {
                
                lbl1.Text = "";
                lbl2.Text = "";
                lbltotal.Text = "";
                lbltotalRamal.Text = "";
                Label3.Text = "";
                Label4.Text = "";
                Label2.Text = "";
                Label1.Text = "";
                Label5.Text = "";
                Label6.Text = "";
                Label8.Text = "";
                Label7.Text = "";
                Label9.Text = "";
                Label10.Text = "";
                Label12.Text = "";
                Label11.Text = "";
            } 
            
            
        }
        public void getSemana()
        {
            DayOfWeek diaDaSemana = DateTime.Now.DayOfWeek;
            string sql=null;
            MySqlCommand cmd;
            MySqlDataReader dr;
            DataTable dt = new DataTable();
            con.AbrirCon();

            switch (diaDaSemana)
            {
                case DayOfWeek.Sunday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >= now() group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Monday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 1 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Tuesday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 2 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Wednesday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 3 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Thursday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 4 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Friday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 5 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Saturday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate >=  (DATE(NOW()) - INTERVAL 6 DAY) AND DATE(NOW()) group by DAYOFWEEK(calldate)";
                    break;
                default:
                    break;
            }

            cmd = new MySqlCommand(sql, con.con);
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<string> total = new List<string>();
            List<int> dia = new List<int>();
            int linhas = dt.Rows.Count;
            dr = cmd.ExecuteReader();
            for (int i = 0; i < linhas; i++)
            {
                dr.Read();
                int diaS = Convert.ToInt32(dr["diasemana"]);
                int totalD = Convert.ToInt32(dr["total"]);
                indice3[diaS -1] = totalD;
            }
            Data3 = indice3.ToArray();
            con.FecharCon();
            
        }

        public void getSemanaAnterior()
        {
            DayOfWeek diaDaSemana = DateTime.Now.DayOfWeek;
            string sql = null;
            MySqlCommand cmd;
            MySqlDataReader dr;
            DataTable dt = new DataTable();
            con.AbrirCon();

            switch (diaDaSemana)
            {
                case DayOfWeek.Sunday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 8 DAY) AND (DATE(NOW()))  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Monday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 9 DAY) AND (DATE(NOW()) - INTERVAL 1 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Tuesday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 10 DAY) AND (DATE(NOW()) - INTERVAL 2 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Wednesday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 11 DAY) AND (DATE(NOW()) - INTERVAL 3 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Thursday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 12 DAY) AND (DATE(NOW()) - INTERVAL 4 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Friday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 13 DAY) AND (DATE(NOW()) - INTERVAL 5 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                case DayOfWeek.Saturday:
                    sql = "SELECT calldate, date_format(calldate,'%W') as dia, DAYOFWEEK(calldate) as diasemana, src, dst, sec_to_time(duration) as tempo, cnam, count(*) as total FROM asteriskcdrdb.cdr	WHERE calldate between  (DATE(NOW()) - INTERVAL 14 DAY) AND (DATE(NOW()) - INTERVAL 6 DAY)  group by DAYOFWEEK(calldate)";
                    break;
                default:
                    break;
            }

            cmd = new MySqlCommand(sql, con.con);
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            List<string> total = new List<string>();
            List<int> dia = new List<int>();
            int linhas = dt.Rows.Count;
            dr = cmd.ExecuteReader();
            for (int i = 0; i < linhas; i++)
            {
                dr.Read();
                int diaS = Convert.ToInt32(dr["diasemana"]);
                int totalD = Convert.ToInt32(dr["total"]);
                indice4[diaS - 1] = totalD;
            }
            Data4 = indice4.ToArray();
            con.FecharCon();

        }


    }
}