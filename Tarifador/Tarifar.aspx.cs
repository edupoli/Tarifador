using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Tarifador
{
    public partial class Tarifar : System.Web.UI.Page
    {
        Conexao con = new Conexao();
        int numResultados;
        ArrayList chamadas = new ArrayList();

        public class lista
        {
            public lista() { }

            public int idcdr { get; set; }
            public string calldate { get; set; }
            public string cnam { get; set; }
            public string src { get; set; }
            public string dst { get; set; }
            public string tempo { get; set; }
            public string tipoChamada { get; set; }
            public string canal { get; set; }
            public string valor { get; set; }

            public lista(int idcdr, string calldate,string cnam, string src,string dst,string tempo, string tipoChamada, string canal, string valor)
            {
                this.idcdr = idcdr;
                this.calldate = calldate;
                this.cnam = cnam;
                this.src = src;
                this.dst = dst;
                this.tempo = tempo;
                this.tipoChamada = tipoChamada;
                this.canal = canal;
                this.valor = valor;

            }
        }
        static List<lista> Chamadas;
        


        //List<string> chamadas = new List<string>();


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
            
            if (canals=="todos" && tipoChamada =="todos")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dst REGEXP '^0[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0300]$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals != "todos" && tipoChamada == "todos")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and(dst REGEXP '^0[1-9]{8}$'" + 
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" + 
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" + 
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" + 
                    "or dst regexp '^0[0300]$')" + 
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals=="todos" && tipoChamada== "Fixolocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dst REGEXP '^0[1-9]{8}$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals == "todos" && tipoChamada == "CelularLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals == "todos" && tipoChamada == "dddLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals == "todos" && tipoChamada == "Celularddd")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals == "todos" && tipoChamada == "_0300regex")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and dst regexp '^0[0300]$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }

            if (canals != "todos" && tipoChamada == "Fixolocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and(dst REGEXP '^0[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals != "todos" && tipoChamada == "CelularLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals != "todos" && tipoChamada == "dddLocal")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals != "todos" && tipoChamada == "Celularddd")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }
            if (canals != "todos" && tipoChamada == "_0300regex")
            {
                sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(calldate between '" + dataI + "' and '" + dataF + "')" +
                    "and(dstchannel like '%" + canals + "%')" +
                    "and dst regexp '^0[0300]$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            }



            cmd = new MySqlCommand(sql, con.con);
            da.SelectCommand = cmd;
            da.Fill(dt);
            numResultados = dt.Rows.Count;
            dt.Columns.Add("tipoChamada", typeof(string)); //adiciona a coluna tipoChamada no datatable dt que contem os dados da tabela cdr no BD asterix
            dt.Columns.Add("valor", typeof(string)); //adiciona a coluna valor no datatable dt que contem os dados da tabela cdr no BD asterix
            dt.Columns.Add("canal", typeof(string));

            // faz a varedura no datatable dt linha a linha 
            foreach (DataRow linha in dt.Rows)
            {
                string numero = linha["dst"].ToString();
                string canal = linha["dstchannel"].ToString();
                int tempoChamada = int.Parse(linha["duration"].ToString());
                canal = canal.Remove(canal.Length - 9); // Remove os ultimos 9 caracteres da string
                canal = canal.Substring(4); //  Remove os 4 primeiros caracteres da string

                tarifadorEntities ctx = new tarifadorEntities();
                var resultado = (from p in ctx.planotarifacaos
                                 join t in ctx.troncoes on p.id equals t.planoID
                                 where t.canal.Contains(canal)
                                 select new
                                 {
                                     p.id,
                                     p.nome,
                                     p.tempoMinimoChamada,
                                     p.tempoMinimoTarifacao,
                                     p.operadora,
                                     p.periodicidadeTarifa,
                                     p.taxaConexao,
                                     p.ligacao_0300,
                                     p.dddCelular,
                                     p.dddFixo,
                                     p.localCelular,
                                     p.localFixo,
                                 }).ToList();

                //recupera os valores do plano de tarifação de acordo com  o canal que é setado no plano de tarifação
                foreach (var item in resultado)
                {
                    int tempoMinimoChamada = int.Parse(item.tempoMinimoChamada);
                    int tempoMinimoTarifacao = int.Parse(item.tempoMinimoTarifacao);
                    int periodicidadeTarifa = int.Parse(item.periodicidadeTarifa);
                    decimal _0300 = decimal.Parse(item.ligacao_0300.ToString());
                    decimal dddCelular = decimal.Parse(item.dddCelular.ToString());
                    decimal dddFixo = decimal.Parse(item.dddFixo.ToString());
                    decimal localCelular = decimal.Parse(item.localCelular.ToString());
                    decimal localFixo = decimal.Parse(item.localFixo.ToString());
                    decimal taxaConexao = decimal.Parse(item.taxaConexao.ToString());
                    string nome = item.nome;

                    string Fixolocal = @"^0[1-9]{8}$";
                    string CelularLocal = @"^0[9]{1}[1-9]{8}$";
                    string dddLocal = @"^0[0-9]{2}[1-9]{8}$";
                    string Celularddd = @"^0[0-9]{2}[9]{1}[1-9]{8}$";
                    string _0300regex = @"^0[0300]$";
                    Match match_Fixolocal = Regex.Match(numero, Fixolocal);
                    Match match_CelularLocal = Regex.Match(numero, CelularLocal);
                    Match match_dddLocal = Regex.Match(numero, dddLocal);
                    Match match_dddCelular = Regex.Match(numero, Celularddd);
                    Match match_0300 = Regex.Match(numero, _0300regex);
                    linha["canal"] = canal;
                    if (match_Fixolocal.Success)
                    {
                        linha["tipoChamada"] = "Fixo Local";
                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            linha["valor"] = Math.Round((localFixo / 2), 2);
                        }
                        else
                        {
                            decimal meiatarifa = localFixo / 2;
                            decimal valorporsegundo = meiatarifa / tempoMinimoTarifacao;
                            decimal adcional = valorporsegundo * periodicidadeTarifa;
                            decimal tempo = tempoChamada - tempoMinimoTarifacao;
                            tempo = tempo / periodicidadeTarifa;
                            decimal valor = tempo * adcional;
                            decimal valorFinal = valor + meiatarifa + taxaConexao;
                            decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                            linha["valor"] = valorFinalAredondado;
                        }
                    }
                    if (match_CelularLocal.Success)
                    {
                        linha["tipoChamada"] = "Celular Local";
                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            linha["valor"] = Math.Round((localCelular / 2), 2);
                        }
                        else
                        {
                            decimal meiatarifa = localCelular / 2;
                            decimal valorporsegundo = meiatarifa / tempoMinimoTarifacao;
                            decimal adcional = valorporsegundo * periodicidadeTarifa;
                            decimal tempo = tempoChamada - tempoMinimoTarifacao;
                            tempo = tempo / periodicidadeTarifa;
                            decimal valor = tempo * adcional;
                            decimal valorFinal = valor + meiatarifa + taxaConexao;
                            decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                            linha["valor"] = valorFinalAredondado;
                        }
                    }
                    if (match_dddLocal.Success)
                    {
                        linha["tipoChamada"] = "DDD Fixo";
                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            linha["valor"] = Math.Round((dddFixo / 2), 2);
                        }
                        else
                        {
                            decimal meiatarifa = dddFixo / 2;
                            decimal valorporsegundo = meiatarifa / tempoMinimoTarifacao;
                            decimal adcional = valorporsegundo * periodicidadeTarifa;
                            decimal tempo = tempoChamada - tempoMinimoTarifacao;
                            tempo = tempo / periodicidadeTarifa;
                            decimal valor = tempo * adcional;
                            decimal valorFinal = valor + meiatarifa + taxaConexao;
                            decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                            linha["valor"] = valorFinalAredondado;
                        }
                    }
                    if (match_dddCelular.Success)
                    {
                        linha["tipoChamada"] = "DDD Celular";
                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            linha["valor"] = Math.Round((dddCelular / 2), 2);
                        }
                        else
                        {
                            decimal meiatarifa = dddCelular / 2;
                            decimal valorporsegundo = meiatarifa / tempoMinimoTarifacao;
                            decimal adcional = valorporsegundo * periodicidadeTarifa;
                            decimal tempo = tempoChamada - tempoMinimoTarifacao;
                            tempo = tempo / periodicidadeTarifa;
                            decimal valor = tempo * adcional;
                            decimal valorFinal = valor + meiatarifa + taxaConexao;
                            decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                            linha["valor"] = valorFinalAredondado;
                        }
                    }
                    if (match_0300.Success)
                    {
                        linha["tipoChamada"] = "0300";
                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            linha["valor"] = Math.Round((_0300 / 2), 2);
                        }
                        else
                        {
                            decimal meiatarifa = _0300 / 2;
                            decimal valorporsegundo = meiatarifa / tempoMinimoTarifacao;
                            decimal adcional = valorporsegundo * periodicidadeTarifa;
                            decimal tempo = tempoChamada - tempoMinimoTarifacao;
                            tempo = tempo / periodicidadeTarifa;
                            decimal valor = tempo * adcional;
                            decimal valorFinal = valor + meiatarifa + taxaConexao;
                            decimal valorFinalAredondado = Math.Round(valorFinal, 2);
                            linha["valor"] = valorFinalAredondado;
                        }
                    }
                    dt.AcceptChanges();
                }
                int resul = resultado.Count;
                if (resul==0)
                {
                    linha["canal"] = canal + "  NÃO CADASTRADO";
                }
                
            }
            
            Chamadas = new List<lista>();
            foreach (DataRow item in dt.Rows)
            {
                if (item["valor"].ToString() != "")
                {
                    Chamadas.Add(new lista(int.Parse(item["idcdr"].ToString()),
                    item["calldate"].ToString(),
                    item["cnam"].ToString(),
                    item["src"].ToString(),
                    item["dst"].ToString(),
                    item["tempo"].ToString(),
                    item["tipoChamada"].ToString(),
                    item["canal"].ToString(),
                    item["valor"].ToString()));
                }
                
            }
            GridView1.Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        
        
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (dataInicial.Text=="" || dataFinal.Text=="")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                tarifar(dataInicial.Text, dataFinal.Text,tipoChamada.SelectedValue, cboxCanal.SelectedValue);
            }
        }

        protected void btnBilhetar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Chamadas == null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroDatatable();", true);
                }
                if (Chamadas.Count == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroBilhetagem();", true);
                }
                else
                {

                    foreach (lista item in Chamadas)
                    {
                        string sql;
                        MySqlCommand cmd;
                        con.AbrirCon();
                        // NESTA SQL É FEITO UM SELECT PESQUISANDO SE EXISTE VALORES IGUAIS ANTES DE INSERIR , PARA EVITAR DUPLICIDADE NO BD
                        sql = "INSERT INTO tarifador.bilhetes(idcdr,calldate,cnam,src,dst,tempo,tipoChamada,canal,valor)" + 
                              "SELECT * FROM (SELECT @idcdr as idcdr, @calldate as calldate, @cnam as cnam, @src as src, @dst as dst,"+ 
                              "@tempo as tempo, @tipoChamada as tipoChamada, @canal as canal, @valor as valor)"+
                              " AS tmp WHERE NOT EXISTS(SELECT idcdr FROM tarifador.bilhetes WHERE idcdr = @idcdr) LIMIT 1";
                        
                        cmd = new MySqlCommand(sql, con.con2);
                        cmd.Parameters.AddWithValue("@idcdr", item.idcdr);
                        cmd.Parameters.AddWithValue("@calldate", Convert.ToDateTime(item.calldate));
                        cmd.Parameters.AddWithValue("@cnam", item.cnam);
                        cmd.Parameters.AddWithValue("@src", item.src);
                        cmd.Parameters.AddWithValue("@dst", item.dst);
                        cmd.Parameters.AddWithValue("@tempo", item.tempo);
                        cmd.Parameters.AddWithValue("@tipoChamada", item.tipoChamada);
                        cmd.Parameters.AddWithValue("@canal", item.canal);
                        cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(item.valor));

                        cmd.ExecuteNonQuery();

                        //GRAVA NO BANCO DE DADOS ASTERIX 
                        asteriskcdrdbEntities asterix = new asteriskcdrdbEntities();
                        cdr bilhetes = asterix.cdrs.First(p => p.idcdr == item.idcdr);
                        bilhetes.tarifado = "SIM";
                        asterix.SaveChanges();

                        ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
                        con.FecharCon();
                    }
                    Chamadas.Clear();
                    GridView1.Visible = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}