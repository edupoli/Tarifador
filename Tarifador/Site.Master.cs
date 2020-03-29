using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tarifador
{
    public partial class SiteMaster : MasterPage
    {
        
        ArrayList dstchannel = new ArrayList();
        public ArrayList nCadastrados = new ArrayList();
        Conexao con = new Conexao();
        int qtdNotificacao = 0;
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Notificacao();
            
        }
        public void Notificacao()
        {
            //Faz uma consulta de todas as chamadas a serem tarifadas, salva o resultado da consulta num DataReader e passa o total ao lblBilhetes.text
            con.AbrirCon();
            MySqlCommand cmd = new MySqlCommand("SELECT count(*) as total FROM asteriskcdrdb.cdr where (calldate between '2020-03-01 00:00:00' and now()) and(dst REGEXP '^0[1-9]{8}$' or dst regexp '^0[0-9]{2}[1-9]{8}$' or dst regexp '^0[9]{1}[1-9]{8}$' or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$' or dst regexp '^0[0300]$') and(disposition = 'ANSWERED') and(tarifado is null)", con.con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader drQTDbilhetes;
            drQTDbilhetes = cmd.ExecuteReader();
            drQTDbilhetes.Read();
            int total = int.Parse(drQTDbilhetes["total"].ToString());
            
            // Busca os registros das ligações no BD asterix filtrando atravez de REGEX somente as ligações feitas de origem interna para destino externo           
            string sql;
            MySqlCommand cmd2;
            con.AbrirCon();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();
            sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '2020-03-16 00:00:00' and now())" +
             //        "and(dstchannel like '%AudioCode3000%')"+
                    "and(dst REGEXP '^0[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0300]$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            cmd2 = new MySqlCommand(sql, con.con);
            da.SelectCommand = cmd2;
            da.Fill(dt);

            // Lista o conteudo cadastrado na tabela Tronco
            tarifadorEntities ctx = new tarifadorEntities();
            var resultRotasCadastradas = ctx.troncoes.ToList();

            // Percorre o DataTable contendo todas as chamadas a serem tarifadas e adiciona os valores do campo dstchannel e salva no ArrayLista
            foreach (DataRow item in dt.Rows)
            {
                string dstchannelLista = item["dstchannel"].ToString();
                dstchannel.Add(dstchannelLista);

            }
            // Percorre o ArrayLista verificando linha a linha se o campo dstchannel existe na tabela Tronco , caso não exista adiciona no Array nCadastrados
            for (int i = 0; i < dstchannel.Count; i++)
            {
                string rota1 = dstchannel[i].ToString();
                rota1 = rota1.Remove(rota1.Length - 9); // Remove os 9 ultimos caracteres da string dstchannel
                rota1 = rota1.Substring(4); // Remove os 4 primeiros caracteres da string dstchannel
                if (resultRotasCadastradas.Any(p => p.canal.Contains(rota1))) // verifica se o valor da linha dstchannel existe na coluna canal da tabela tronco
                {

                }
                else
                {
                    if (!nCadastrados.Contains(rota1))
                    {
                        nCadastrados.Add(rota1);
                    }
                }


            }

            if (total !=0)
            {
                qtdNotificacao = 1;
                lblBilhetes.Text = Convert.ToString(drQTDbilhetes["total"]);
                lblQTDBilhetes.Text = Convert.ToString(drQTDbilhetes["total"]);
                notfBilhetes.Visible = true;

            }
            else
            {
                notfBilhetes.Visible = false;
                badgeMenu.Visible = false;
                lblQTDBilhetes.Visible = false;
            }
            if (nCadastrados.Count != 0)
            {
                qtdNotificacao = qtdNotificacao + 1;
                notidfRotas.Visible = true;
                RotasNcadastradas.Text = GerarListaRotasncadastradas();
            }
            else
            {
                notidfRotas.Visible = false;
            }
            if (qtdNotificacao != 0)
            {
                badge.Visible = true;
                lblQTDNotificacao.Text = Convert.ToString(qtdNotificacao);
                lblNumNotificacao.Text = Convert.ToString(qtdNotificacao);
            }
            if (qtdNotificacao==0)
            {
                badge.Visible = false;
                itensNotificacao.Visible = false;
                notidfRotas.Visible = false;
                notfBilhetes.Visible = false;
            }

        }
        public string GerarListaRotasncadastradas()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul>");
            for (int i = 0; i < nCadastrados.Count; i++)
            {
                sb.AppendLine("<li>" + nCadastrados[i].ToString()+ "</li>");
            }
            
            sb.AppendLine("</ul>");
            //sb.AppendLine("<span class=\"float-left text-muted text-sm\">Não Cadastrada</span>");
            return sb.ToString();
        }
    }
}