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
        int numResultados;
        ArrayList chamadas = new ArrayList();
        public string mensagem = string.Empty;

        public class lista
        {
            public lista() { }

            public int idcdr { get; set; }
            public string calldate { get; set; }
            public string cnam { get; set; }
            public string src { get; set; }
            public string dst { get; set; }
            public string tempo { get; set; }
            public string duration { get; set; }
            public string tipoChamada { get; set; }
            public string canal { get; set; }
            public string valor { get; set; }

            public lista(int idcdr, string calldate, string cnam, string src, string dst, string tempo,string duration, string tipoChamada, string canal, string valor)
            {
                this.idcdr = idcdr;
                this.calldate = calldate;
                this.cnam = cnam;
                this.src = src;
                this.dst = dst;
                this.tempo = tempo;
                this.duration = duration;
                this.tipoChamada = tipoChamada;
                this.canal = canal;
                this.valor = valor;

            }
        }
        static List<lista> Chamadas;
        static List<lista> ChamadasSemRotas;


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
            cboxCanal.Items.Insert(0, new ListItem("Todos", "todos"));
            foreach (var item in resultado)
            {
                cboxCanal.Items.Add(new ListItem(item.canal, item.canal));
            }

        }

        public void tarifarGeral()
        {
            string conec = "SERVER=10.0.2.128;UID=admin;PWD=ask%123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conec);
            string sql = "";
            MySqlCommand cmd;
            con.Open();
            DataTable dt = new DataTable("TotalChamadasGeral");
            MySqlDataAdapter da = new MySqlDataAdapter();
            sql = "SELECT idcdr,calldate,cnam,src,dst,duration,sec_to_time(duration) as tempo,dstchannel FROM asteriskcdrdb.cdr where" +
                    "(dst REGEXP '^0[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[1-9]{8}$'" +
                    "or dst regexp '^0[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$'" +
                    "or dst regexp '^0[0300]$')" +
                    "and(disposition = 'ANSWERED')" +
                    "and(tarifado is null); ";
            cmd = new MySqlCommand(sql, con);
            da.SelectCommand = cmd;
            da.Fill(dt);
            numResultados = dt.Rows.Count;
            dt.Columns.Add("tipoChamada", typeof(string));
            dt.Columns.Add("valor", typeof(string));
            dt.Columns.Add("canal", typeof(string));
            ChamadasSemRotas = new List<lista>();
            foreach (DataRow linha in dt.Rows)
            {
                string numero = linha["dst"].ToString();
                string canal = linha["dstchannel"].ToString();
                int tempoChamada = int.Parse(linha["duration"].ToString());
                canal = canal.Remove(canal.Length - 9);
                canal = canal.Substring(4);

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
                if (resul == 0)
                {
                    ChamadasSemRotas.Add(new lista(int.Parse(linha["idcdr"].ToString()),
                    linha["calldate"].ToString(),
                    linha["cnam"].ToString(),
                    linha["src"].ToString(),
                    linha["dst"].ToString(),
                    linha["tempo"].ToString(),
                    linha["duration"].ToString(),
                    linha["tipoChamada"].ToString(),
                    canal + "  NÃO CADASTRADO",
                    linha["valor"].ToString()));
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
                    item["duration"].ToString(),
                    item["tipoChamada"].ToString(),
                    item["canal"].ToString(),
                    item["valor"].ToString()));
                }

            }
            con.Close();

        }


        public void tarifar(string dataI, string dataF, string tipoChamada, string canals)
        {
            // Busca os registros das ligações no BD asterix filtrando atravez de REGEX somente as ligações feitas de origem interna para destino externo           
            string conec = "SERVER=10.0.2.128;UID=admin;PWD=ask%123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conec);
            string sql = "";
            MySqlCommand cmd;
            con.Open();
            DataTable dt = new DataTable("TotalChamadas");
            MySqlDataAdapter da = new MySqlDataAdapter();

            if (canals == "todos" && tipoChamada == "todos")
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
            if (canals == "todos" && tipoChamada == "Fixolocal")
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

            cmd = new MySqlCommand(sql, con);
            da.SelectCommand = cmd;
            da.Fill(dt);
            numResultados = dt.Rows.Count;
            dt.Columns.Add("tipoChamada", typeof(string)); //adiciona a coluna tipoChamada no datatable dt que contem os dados da tabela cdr no BD asterix
            dt.Columns.Add("valor", typeof(string)); //adiciona a coluna valor no datatable dt que contem os dados da tabela cdr no BD asterix
            dt.Columns.Add("canal", typeof(string));

            ChamadasSemRotas = new List<lista>();

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
                if (resul == 0)
                {

                    ChamadasSemRotas.Add(new lista(int.Parse(linha["idcdr"].ToString()),
                    linha["calldate"].ToString(),
                    linha["cnam"].ToString(),
                    linha["src"].ToString(),
                    linha["dst"].ToString(),
                    linha["tempo"].ToString(),
                    linha["duration"].ToString(),
                    linha["tipoChamada"].ToString(),
                    canal + "  NÃO CADASTRADO",
                    linha["valor"].ToString()));

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
                    item["duration"].ToString(),
                    item["tipoChamada"].ToString(),
                    item["canal"].ToString(),
                    item["valor"].ToString()));
                }

            }
            con.Close();

        }

        protected void btnBilhetar_Click(object sender, EventArgs e)
        {
            string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            if (dataInicial.Text == "" || dataFinal.Text == "")
            {
                mensagem = "Favor definir o Período Datas de Inicio e Fim para Tarifação";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
            }
            else if (dataInicial.Text != "" && dataFinal.Text != "")
            {
                tarifar(dataInicial.Text, dataFinal.Text, tipoChamada.SelectedValue, cboxCanal.SelectedValue);
            }
            else if (Chamadas == null)
            {

            }
            else if (ChamadasSemRotas == null)
            {

            }
            else if (Chamadas.Count == 0 && ChamadasSemRotas.Count == 0 && dataInicial.Text != "" && dataFinal.Text != "")
            {
                mensagem = "Não Foram encontradas chamadas para serem Tarifadas no Periodo Pesquisado !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
            }
            else if (Chamadas.Count > 0 && dataInicial.Text != "" && dataFinal.Text != "")
            {
                try
                {
                    foreach (lista item in Chamadas)
                    {
                        string sql;
                        MySqlCommand cmd;
                        // NESTA SQL É FEITO UM SELECT PESQUISANDO SE EXISTE VALORES IGUAIS ANTES DE INSERIR , PARA EVITAR DUPLICIDADE NO BD
                        sql = "INSERT INTO tarifador.bilhetes(idcdr,calldate,cnam,src,dst,tempo,duration,tipoChamada,canal,valor)" +
                              "SELECT * FROM (SELECT @idcdr as idcdr, @calldate as calldate, @cnam as cnam, @src as src, @dst as dst," +
                              "@tempo as tempo,@duration as duration, @tipoChamada as tipoChamada, @canal as canal, @valor as valor)" +
                              " AS tmp WHERE NOT EXISTS(SELECT idcdr FROM tarifador.bilhetes WHERE idcdr = @idcdr) LIMIT 1";

                        cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@idcdr", item.idcdr);
                        cmd.Parameters.AddWithValue("@calldate", Convert.ToDateTime(item.calldate));
                        cmd.Parameters.AddWithValue("@cnam", item.cnam);
                        cmd.Parameters.AddWithValue("@src", item.src);
                        cmd.Parameters.AddWithValue("@dst", item.dst);
                        cmd.Parameters.AddWithValue("@tempo", item.tempo);
                        cmd.Parameters.AddWithValue("@duration", item.duration);
                        cmd.Parameters.AddWithValue("@tipoChamada", item.tipoChamada);
                        cmd.Parameters.AddWithValue("@canal", item.canal);
                        cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(item.valor));

                        cmd.ExecuteNonQuery();

                        //GRAVA NO BANCO DE DADOS ASTERIX 
                        asteriskcdrdbEntities asterix = new asteriskcdrdbEntities();
                        cdr bilhetes = asterix.cdrs.First(p => p.idcdr == item.idcdr);
                        bilhetes.tarifado = "SIM";
                        asterix.SaveChanges();
                    }
                    con.Close();
                    if (Chamadas.Count > 0 && ChamadasSemRotas.Count == 0 && dataInicial.Text != "" && dataFinal.Text != "")
                    {
                        GridView1.DataSource = Chamadas;
                        GridView1.DataBind();
                        mensagem = Chamadas.Count + " Chamadas Foram Tarifadas com Sucesso!!";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
                    }
                    if (Chamadas.Count > 0 && ChamadasSemRotas.Count > 0 && dataInicial.Text != "" && dataFinal.Text != "")
                    {
                        var listaGeral = Chamadas.Union(ChamadasSemRotas);
                        GridView1.DataSource = listaGeral;
                        GridView1.DataBind();
                        mensagem = Chamadas.Count + " Chamadas foram Tarifadas com Sucesse mas " + ChamadasSemRotas.Count + " Chamadas não foram Tarifadas pois estão sem Rota Cadastrada";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
                    }
                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o Seguinte erro ao tentar bilhetar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
                }
            }
            else if (Chamadas.Count == 0 && ChamadasSemRotas.Count > 0 && dataInicial.Text != "" && dataFinal.Text != "")
            {
                GridView1.DataSource = ChamadasSemRotas;
                GridView1.DataBind();
                mensagem = "Existem " + ChamadasSemRotas.Count + " Chamadas sem Rota Cadastrada, não sendo possivel Tarifalas";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "InfoGeral();", true);
            }
            else if (Chamadas.Count == 0 && ChamadasSemRotas.Count == 0 && dataInicial.Text != "" && dataFinal.Text != "")
            {
                mensagem = "Não Foram encontradas chamadas para serem Tarifadas no Periodo Pesquisado !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "InfoGeral();", true);
            }
            else if (Chamadas.Count != 0 && ChamadasSemRotas.Count != 0 && dataInicial.Text != "" && dataFinal.Text != "")
            {
                var listaGeral = Chamadas.Union(ChamadasSemRotas);
                GridView1.DataSource = listaGeral;
                GridView1.DataBind();
                mensagem = Chamadas.Count + " Chamadas foram Tarifadas com Sucesso mas " + ChamadasSemRotas.Count + " Chamadas não foram Tarifadas pois estão sem Rota Cadastrada";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
            }

        }

        protected void btnBilhetarGeral_Click(object sender, EventArgs e)
        {
            string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            tarifarGeral();
            if (Chamadas == null)
            {

            }
            else
              if (ChamadasSemRotas == null)
            {

            }
            else
            if (Chamadas.Count == 0 && ChamadasSemRotas.Count == 0)
            {
                mensagem = "Não Foram encontradas chamadas para serem Tarifadas!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
            }
            else
             if (Chamadas.Count > 0)
            {
                try
                {
                    foreach (lista item in Chamadas)
                    {
                        string sql;
                        MySqlCommand cmd;
                        // NESTA SQL É FEITO UM SELECT PESQUISANDO SE EXISTE VALORES IGUAIS ANTES DE INSERIR , PARA EVITAR DUPLICIDADE NO BD
                        sql = "INSERT INTO tarifador.bilhetes(idcdr,calldate,cnam,src,dst,tempo, duration,tipoChamada,canal,valor)" +
                              "SELECT * FROM (SELECT @idcdr as idcdr, @calldate as calldate, @cnam as cnam, @src as src, @dst as dst," +
                              "@tempo as tempo,@duration as duration, @tipoChamada as tipoChamada, @canal as canal, @valor as valor)" +
                              " AS tmp WHERE NOT EXISTS(SELECT idcdr FROM tarifador.bilhetes WHERE idcdr = @idcdr) LIMIT 1";

                        cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@idcdr", item.idcdr);
                        cmd.Parameters.AddWithValue("@calldate", Convert.ToDateTime(item.calldate));
                        cmd.Parameters.AddWithValue("@cnam", item.cnam);
                        cmd.Parameters.AddWithValue("@src", item.src);
                        cmd.Parameters.AddWithValue("@dst", item.dst);
                        cmd.Parameters.AddWithValue("@tempo", item.tempo);
                        cmd.Parameters.AddWithValue("@duration", item.duration);
                        cmd.Parameters.AddWithValue("@tipoChamada", item.tipoChamada);
                        cmd.Parameters.AddWithValue("@canal", item.canal);
                        cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(item.valor));

                        cmd.ExecuteNonQuery();

                        //GRAVA NO BANCO DE DADOS ASTERIX 
                        asteriskcdrdbEntities asterix = new asteriskcdrdbEntities();
                        cdr bilhetes = asterix.cdrs.First(p => p.idcdr == item.idcdr);
                        bilhetes.tarifado = "SIM";
                        asterix.SaveChanges();
                    }
                    con.Close();
                    if (Chamadas.Count > 0 && ChamadasSemRotas.Count == 0)
                    {
                        GridView1.DataSource = Chamadas;
                        GridView1.DataBind();
                        mensagem = Chamadas.Count + " Chamadas Foram Tarifadas com Sucesso!!";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
                    }
                    if (Chamadas.Count > 0 && ChamadasSemRotas.Count > 0)
                    {
                        var listaGeral = Chamadas.Union(ChamadasSemRotas);
                        GridView1.DataSource = listaGeral;
                        GridView1.DataBind();
                        mensagem = Chamadas.Count + " Chamadas foram Tarifadas com Sucesso mas " + ChamadasSemRotas.Count + " Chamadas não foram Tarifadas pois estão sem Rota Cadastrada";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
                    }

                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o Seguinte erro ao tentar bilhetar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
                }
            }
            else
            if (Chamadas.Count == 0 && ChamadasSemRotas.Count > 0)
            {
                GridView1.DataSource = ChamadasSemRotas;
                GridView1.DataBind();
                mensagem = "Existem " + ChamadasSemRotas.Count + " Chamadas sem Rota Cadastrada, não sendo possivel Tarifalas";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "InfoGeral();", true);
            }
        }

        public void retarifar(DateTime dataI, DateTime dataF)
        {
            tarifadorEntities ctx = new tarifadorEntities();
            List<lista> itens = new List<lista>();

            var bilhetes = (from p in ctx.bilhetes
                            where
            p.calldate >= dataI && p.calldate <= dataF
                            select new
                            {
                                p.idcdr,
                                p.calldate,
                                p.cnam,
                                p.src,
                                p.dst,
                                p.tempo,
                                p.duration,
                                p.tipoChamada,
                                p.canal,
                                p.valor
                            }).ToList();

            for (int i = 0; i < bilhetes.Count; i++)
            {
                string numero = bilhetes[i].dst;
                string canal = bilhetes[i].canal;
                int tempoChamada = int.Parse(bilhetes[i].duration);

                var tarifas = (from p in ctx.planotarifacaos
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

                for (int a = 0; a < tarifas.Count; a++)
                {
                    int tempoMinimoChamada = int.Parse(tarifas[a].tempoMinimoChamada);
                    int tempoMinimoTarifacao = int.Parse(tarifas[a].tempoMinimoTarifacao);
                    int periodicidadeTarifa = int.Parse(tarifas[a].periodicidadeTarifa);
                    decimal _0300 = decimal.Parse(tarifas[a].ligacao_0300.ToString());
                    decimal dddCelular = decimal.Parse(tarifas[a].dddCelular.ToString());
                    decimal dddFixo = decimal.Parse(tarifas[a].dddFixo.ToString());
                    decimal localCelular = decimal.Parse(tarifas[a].localCelular.ToString());
                    decimal localFixo = decimal.Parse(tarifas[a].localFixo.ToString());
                    decimal taxaConexao = decimal.Parse(tarifas[a].taxaConexao.ToString());
                    string nome = tarifas[a].nome;

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

                    if (match_Fixolocal.Success)
                    {

                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            string valor = Convert.ToString(Math.Round((localFixo / 2), 2));
                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            valor));
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

                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            Convert.ToString(valorFinalAredondado)));
                        }
                    }
                    if (match_CelularLocal.Success)
                    {

                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            string valor = Convert.ToString(Math.Round((localCelular / 2), 2));
                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            valor));
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

                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            Convert.ToString(valorFinalAredondado)));
                        }
                    }
                    if (match_dddLocal.Success)
                    {

                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            string valor = Convert.ToString(Math.Round((dddFixo / 2), 2));
                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            valor));
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

                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            Convert.ToString(valorFinalAredondado)));
                        }
                    }
                    if (match_dddCelular.Success)
                    {

                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            string valor = Convert.ToString(Math.Round((dddCelular / 2), 2));
                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            valor));
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

                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            Convert.ToString(valorFinalAredondado)));
                        }
                    }
                    if (match_0300.Success)
                    {

                        if (tempoChamada < tempoMinimoTarifacao)
                        {
                            string valor = Convert.ToString(Math.Round((_0300 / 2), 2));
                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            valor));
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

                            itens.Add(new lista(bilhetes[i].idcdr,
                            Convert.ToString(bilhetes[i].calldate),
                            bilhetes[i].cnam,
                            bilhetes[i].src,
                            bilhetes[i].dst,
                            bilhetes[i].tempo,
                            bilhetes[i].duration,
                            bilhetes[i].tipoChamada,
                            bilhetes[i].canal,
                            Convert.ToString(valorFinalAredondado)));
                        }
                    }
                }                
            }
            string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            try
            {
                foreach (var item in itens)
                {
                    string sql;
                    MySqlCommand cmd;
                    sql = "UPDATE tarifador.bilhetes SET valor = @valor WHERE(idcdr = @idcdr);";

                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@idcdr", item.idcdr);
                    cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(item.valor));

                    cmd.ExecuteNonQuery();
                }
                GridView1.DataSource = itens;
                GridView1.DataBind();
                mensagem = itens.Count + " Chamadas Foram Retarifadas com Sucesso!!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "SucessoBilhetes();", true);
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao Retarifar:  " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
            }
            con.Close();
        }
            

        protected void btnRetarifar_Click(object sender, EventArgs e)
        {
            if (dataInicial.Text == "" || dataFinal.Text == "")
            {
                mensagem = "Favor definir o Período Datas de Inicio e Fim para Retarifação";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "ErroGeral();", true);
            }
            else if (dataInicial.Text != "" && dataFinal.Text != "")
            {
                retarifar(Convert.ToDateTime(dataInicial.Text), Convert.ToDateTime(dataFinal.Text));
            }
        }
    }
}

