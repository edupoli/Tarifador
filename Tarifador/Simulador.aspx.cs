using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class Simulador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void calculoTarfa(string valorMinuto, string formaCobranca, string tempoChamada, string taxaConexao)
        {
            double tempoTotal = TimeSpan.Parse("00:" + tempoChamada).TotalSeconds; //converte o valor de Minuto/Segundos digitados no textbox em Segundos
            if (formaCobranca == "60/60")
            {
                if (tempoTotal < 60)
                {
                    decimal tarifaMinima = decimal.Parse(valorMinuto.ToString());
                    decimal tarifaMinimaArredondada = Math.Round(tarifaMinima, 2);
                    lblResultado.Text = "O Valor da Tarifa calculado é R$" + tarifaMinimaArredondada + "";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);
                }
                else
                {
                    decimal ta = Convert.ToDecimal(tempoTotal) / 60;
                    decimal arredondado = Math.Ceiling(ta);
                    decimal valorTotalCobrado = decimal.Parse(valorMinuto) * arredondado;
                    lblResultado.Text = "O Valor da Tarifa calculado é R$" + valorTotalCobrado + "";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);
                }
            }
            if (formaCobranca == "30/6")
            {
                if (tempoTotal < 30)
                {
                    decimal tarifaMinima = decimal.Parse(valorMinuto.ToString()) / 2;
                    decimal tarifaMinimaArredondada = Math.Round(tarifaMinima, 2);
                    lblResultado.Text = "O Valor da Tarifa calculado é R$" + tarifaMinimaArredondada + "";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);
                }
                else
                {
                    decimal meiatarifa = decimal.Parse(valorMinuto.ToString()) / 2;
                    decimal valorporsegundo = meiatarifa / 30;
                    decimal adcional = valorporsegundo * 6;
                    double tempoEmSegundos = TimeSpan.Parse("00:" + tempoChamada).TotalSeconds;
                    decimal tempo = decimal.Parse(tempoEmSegundos.ToString()) - 30;
                    tempo = tempo / 6;
                    decimal valor = tempo * adcional;

                    if (taxaConexao != "")
                    {
                        decimal taxa = decimal.Parse(taxaConexao.ToString());
                        decimal valorFinal = valor + meiatarifa + taxa;
                        decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                        lblResultado.Text = "O Valor da Tarifa calculado é R$" + valorFinalAredondado + "";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);
                    }
                    else
                    {
                        decimal valorFinal = valor + meiatarifa;
                        decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                        lblResultado.Text = "O Valor da Tarifa calculado é R$" + valorFinalAredondado + "";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);
                    }
                }
            }

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            if (tempoChamada.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "tempoChamada();", true);
                tempoChamada.Focus();
            }
            else
            if (tempoChamada.Text == "00:00" || tempoChamada.Text == "00:01" || tempoChamada.Text == "00:02" || tempoChamada.Text == "00:03")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "tempoMinimoChamada();", true);
                tempoChamada.Focus();
            }else
                if (valor.Text == "")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "valor();", true);
                        valor.Focus();
                    }
            else
            {
                calculoTarfa(valor.Text, cboxFormaCobranca.SelectedValue, tempoChamada.Text, taxaConexao.Text);
            }
            
        }
    }
}