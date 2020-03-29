using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class WebForm_jason : System.Web.UI.Page
    {
        Conexao cn = new Conexao();
        public string[] Labels { get; set; }
        public string Legend { get; set; }
        public int[] Data { get; set; }
        public List<object> dd = new List<object>();
        public List<string> ll = new List<string>();
        public int inicio;
        public int[] indice = new int[24];
        protected void Page_Load(object sender, EventArgs e)
        {
            Labels = new string[] { "00h", "01h", "02h", "03h", "04h", "05h", "06h", "07h", "08h", "09h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h", "19h", "20h", "21h", "22h", "23h" };
            Legend = "# of Votes";
            //Data = new int[] { 12, 19, 3, 5, 2, 3 };
            getLineChartData();
            Listar();
            getData();

            for (int i = 0; i < 23; i++)
            {
                int[] indice = new int[24];
                indice[i] = i;
            }

        }

        public List<object> getLineChartData()
        {
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            //First get distinct Month Name for select Year.
            string query1 = "SELECT HOUR(calldate) AS hora FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora DESC limit 24";

            DataTable dtLabels = commonFuntionGetData(query1);
            foreach (DataRow drow in dtLabels.Rows)
            {
                
                labels.Add(drow["hora"].ToString());
                
                
            }
            iData.Add(labels);
            

            string query_DataSet_1 = "SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora DESC limit 24";

            DataTable dtDataItemsSets_1 = commonFuntionGetData(query_DataSet_1);
            List<int> lst_dataItem_1 = new List<int>();
            foreach (DataRow dr in dtDataItemsSets_1.Rows)
            {
                lst_dataItem_1.Add(Convert.ToInt32(dr["total"].ToString()));
                
            }
            dd.Add(lst_dataItem_1);
            iData.Add(lst_dataItem_1);
            //iData.CopyTo(Labels);

            return iData;
        }
        public DataTable commonFuntionGetData(string strQuery)
        {
            cn.AbrirCon();
            MySqlDataAdapter dap = new MySqlDataAdapter(strQuery, cn.con);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }

        private void Listar()
        {
            ArrayList al = new ArrayList();
            string sql;
            MySqlDataReader dr;
            MySqlCommand cmd;
            cn.AbrirCon();
            DataTable dt = new DataTable();
            
            MySqlDataAdapter da = new MySqlDataAdapter();
            sql = "SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora DESC limit 24";
            cmd = new MySqlCommand(sql, cn.con);
            dr = cmd.ExecuteReader();
            da.SelectCommand = cmd;
            //da.Fill(dt);

            
            while (dr.Read())
            {
                //Object[] du = new Object[dr.FieldCount];
                //dr.GetValues(values);
            }

            cn.FecharCon();

        }

        private void getData()
        {
            string sql;
            MySqlDataReader dr;
            MySqlCommand cmd;
            cn.AbrirCon();
            DataTable dt = new DataTable();
            sql = "SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora ASC limit 24";
            cmd = new MySqlCommand(sql, cn.con);
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
                    /*

                for (int i2 = 0; i2 < indice.Length; i2++)
                {
                    if (valor == i2)
                    {
                        dados.Add(Convert.ToInt32(dr["total"]));
                    }
                    else
                    {
                        dados.Add(0);
                    }
                }
                /*
                else
                {
                    while (indice[i] < valor)
                    {
                        dados.Add(0);
                        //valor++;
                    }
                }*/
            }
            /*
            for (int i = 0; i < linhas; i++)
            {
                dr.Read();
                int valor = Convert.ToInt32(dr["hora"]);
                
                for (int i1 = 0; i1 < valor; i1++)
                {
                    int[] indice = new int[24];
                    if (inicio == valor)
                    {
                        dados.Add(0);
                    }
                    else
                    if (inicio == valor)
                    {
                        dados.Add(Convert.ToInt32(dr["total"]));
                    }
                    inicio++;
                }

            }

            */
            
            string[] get = total.ToArray();
            Labels = total.ToArray();
            Data = indice.ToArray();
            cn.FecharCon();

        }
    }
}