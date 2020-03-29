using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        Conexao cn = new Conexao();
        protected void Page_Load(object sender, EventArgs e)
        {
           // getLineChartData();
        }
        [WebMethod]
        public  List<object> getLineChartData()
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
            iData.Add(lst_dataItem_1);

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


    }
}