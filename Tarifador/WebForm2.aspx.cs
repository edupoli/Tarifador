using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class trafficSourceData
        {
            public string label { get; set; }
            public string value { get; set; }
            public string color { get; set; }
            public string hightlight { get; set; }
        }

        [WebMethod]
        public static List<trafficSourceData> getTrafficSourceData(List<string> gData)
        {
            List<trafficSourceData> t = new List<trafficSourceData>();
            string[] arrColor = new string[] { "#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6" };

            string conn = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            using (MySqlConnection cn = new MySqlConnection(conn))
            {
                string myQuery = "SELECT HOUR(calldate) AS hora, count(*) AS total FROM asteriskcdrdb.cdr WHERE calldate >= DATE_SUB(CURDATE(), INTERVAL 1 DAY) GROUP BY hora ORDER BY hora DESC limit 24";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = myQuery;
                cmd.CommandType = CommandType.Text;
                // cmd.Parameters.AddWithValue("@year", gData[0]);
                //cmd.Parameters.AddWithValue("@month", gData[1]);
                cmd.Connection = cn;
                cn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int counter = 0;
                    while (dr.Read())
                    {
                        trafficSourceData tsData = new trafficSourceData();
                        tsData.value = dr["total"].ToString();
                        tsData.label = dr["hora"].ToString();
                        //tsData.color = arrColor[counter];
                        t.Add(tsData);
                        counter++;
                    }
                }
            }
            return t;
        }
    }
}