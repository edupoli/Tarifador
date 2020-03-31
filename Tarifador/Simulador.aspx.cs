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
        public void calculoTarfa(string valorMinuto, string tempoMinimoTarifacao, string periodicidadeTarifa, string tempoChamada, string taxaConexao)
        {
            double tempoTotal = TimeSpan.Parse("00:" + tempoChamada).TotalSeconds; //converte o valor de Minuto/Segundos digitados no textbox em Segundos
            if (tempoTotal < int.Parse(tempoMinimoTarifacao)) // compara se o tempo total digitado é menor do que o tempoMinimo de tarifaçao se true considera meia tarifa
            {
                if (int.Parse(tempoMinimoTarifacao) > 30)
                {
                    decimal tarifaMinima = decimal.Parse(valorMinuto.ToString());
                    decimal tarifaMinimaArredondada = Math.Round(tarifaMinima, 2);
                    lblResultado.Text = "O Valor da Tarifa calculado é R$" + tarifaMinimaArredondada + "";
                }
                else
                {
                    decimal tarifaMinima = decimal.Parse(valorMinuto.ToString()) / 2;
                    decimal tarifaMinimaArredondada = Math.Round(tarifaMinima, 2);
                    lblResultado.Text = "O Valor da Tarifa calculado é R$" + tarifaMinimaArredondada + "";
                }
                
            }
            else
            {
                if (tempoTotal > int.Parse(tempoMinimoTarifacao))
                {
                    if (int.Parse(tempoMinimoTarifacao) >30)
                    {
                        decimal ta = Convert.ToDecimal(tempoTotal) / 60;
                        decimal arredondado= Math.Ceiling(ta);
                        decimal valorTotalCobrado = decimal.Parse(valorMinuto) * arredondado;
                        lblResultado.Text = "O Valor da Tarifa calculado é R$" + valorTotalCobrado + "";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "teste();", true);

                    }
                    else
                    {
                        decimal meiatarifa = decimal.Parse(valorMinuto.ToString()) / 2;
                        decimal valorporsegundo = meiatarifa / int.Parse(tempoMinimoTarifacao);
                        decimal adcional = valorporsegundo * int.Parse(periodicidadeTarifa);
                        double tempoEmSegundos = TimeSpan.Parse("00:" + tempoChamada).TotalSeconds;
                        decimal tempo = decimal.Parse(tempoEmSegundos.ToString()) - int.Parse(tempoMinimoTarifacao);
                        tempo = tempo / int.Parse(periodicidadeTarifa);
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
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            if (tempoChamada.Text == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "tempoChamada();", true);
                tempoChamada.Focus();
            }
            else
            
                if (tempoTarifMinimo.Text == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "tempoTarifMinimo();", true);
                    tempoTarifMinimo.Focus();
                }
            
            else
                if (periodicidade.Text == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "periodicidade();", true);
                    periodicidade.Focus();
                }
                else
                if (valor.Text == "")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "valor();", true);
                        valor.Focus();
                    }
            else
            {
                calculoTarfa(valor.Text, tempoTarifMinimo.Text, periodicidade.Text, tempoChamada.Text, taxaConexao.Text);
            }
            
        }
    }
}