﻿using MySql.Data.MySqlClient;
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

        // dicionario de dados é como um ArrayList, mais multidimensional
        Dictionary<string, string> ListRamaisNCadastrados = new Dictionary<string, string>();
        Dictionary<string, string> myList = new Dictionary<string, string>();
        public ArrayList nCadastrados = new ArrayList();
        Conexao con = new Conexao();
        int qtdNotificacao = 0;      


        protected void Page_Load(object sender, EventArgs e)
        {
            ramaisElastix();
            Notificacao();
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                imgUser.ImageUrl = "dist/img/users/" + Session["img"].ToString();
                imgUser1.ImageUrl = "dist/img/users/" + Session["img"].ToString();
                lblNome.Text = Session["nome"].ToString();
                lblCargo.Text = Session["cargo"].ToString();
            }

        }

        public void ramaisElastix()
        {
            con.AbrirCon();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand("SELECT user,description FROM asterisk.devices order by user asc", con.con);
            da.SelectCommand = cmd;
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                string dst = item["user"].ToString();
                string description = item["description"].ToString();
                myList.Add(dst, description); 
            }
            tarifadorEntities ctx = new tarifadorEntities();
            var resultados = ctx.ramals.ToList();

            foreach (KeyValuePair<string, string> item in myList)
            {
                
                if (!resultados.Any(p => p.numero.Contains(item.Key)))
                {
                    ListRamaisNCadastrados.Add(item.Key, item.Value);
                }
                
            }

        }

        public void Notificacao()
        {
            //Faz uma consulta de todas as chamadas a serem tarifadas, salva o resultado da consulta num DataReader e passa o total ao lblBilhetes.text
            con.AbrirCon();
            MySqlCommand cmd = new MySqlCommand("SELECT count(*) as total FROM asteriskcdrdb.cdr where (dst REGEXP '^0[1-9]{8}$' or dst regexp '^0[0-9]{2}[1-9]{8}$' or dst regexp '^0[9]{1}[1-9]{8}$' or dst regexp '^0[0-9]{2}[9]{1}[1-9]{8}$' or dst regexp '^0[0300]$') and(disposition = 'ANSWERED') and(tarifado is null)", con.con);
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
              //       "(calldate between '2020-03-16 00:00:00' and now())" +
             //        "and(dstchannel like '%AudioCode3000%')"+
                    "(dst REGEXP '^0[1-9]{8}$'" +
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
            if (ListRamaisNCadastrados.Count != 0)
            {
                qtdNotificacao = qtdNotificacao + ListRamaisNCadastrados.Count;
                RamaisNcadastrados.Text = GerarListaRamaisNcadastrados();
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
        public string GerarListaRamaisNcadastrados()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> item in ListRamaisNCadastrados)
            {
                sb.Append("<div class='dropdown-divider'></div><a href = 'AddRamal.aspx' class='dropdown-item'>");
                sb.Append("<i class='fas fa-fax mr-2'></i> Ramal " + item.Key +
                    " <span class='float-right text-muted text-sm'>Não Cadastrado</span></a><div class='dropdown-divider'></div>");
            }
            return sb.ToString();
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewUsuarios.aspx?usuarioID=" + Session["id"].ToString());
        }

        protected void CadasPlanoTarif_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/AddPlanoTarifacao.aspx");
            }
        }

        protected void notificacaoTarifar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/Tarifar.aspx");
            }
        }

        protected void notificacaoRotas_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/AddTroncos.aspx");
            }
        }

        protected void linkTarifar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/Tarifar.aspx");
            }
        }

        protected void linkOperadora_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/AddOperadoras.aspx");
            }
            
        }

        protected void linkTronco_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("~/AddTroncos.aspx");
            }
            
        }

        protected void linkGrupoTroncos_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("AddGrupoTroncos.aspx");
            }
            
        }

        protected void linkUsuario_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("AddUsuarios.aspx");
            }
            
        }

        protected void linkGrupoUsuario_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("AddGruposUsuarios.aspx");
            }
            
        }

        protected void linkRamal_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("AddRamal.aspx");
            }
            
        }

        protected void linkGrupoRamais_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                Response.Redirect("AddGrupoRamais.aspx");
            }
            
        }
    }
}