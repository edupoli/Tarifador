using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Tarifador
{
    public class tarifador
    {
        public void addOperadora(operadora op)
        {
            var ctx = new tarifadorEntities();
            ctx.operadoras.Add(op);
            ctx.SaveChanges();
        }
        public List<operadora> GetOperadoras()
        {
            var ctx = new tarifadorEntities();
            return ctx.operadoras.ToList();
        }
        public List<operadora> GetOperadoraCodigo(int id)
        {
            var ctx = new tarifadorEntities();
            return ctx.operadoras.Where(p => p.operadoraID == id).ToList();
        }
        public List<planotarifacao> GetPlanotarifacaos()
        {
            var ctx = new tarifadorEntities();
            return ctx.planotarifacaos.ToList();

        }
        public object getPlanos()
        {
            tarifadorEntities ctx = new tarifadorEntities();
            var resultado = (from a in ctx.planotarifacaos
                             join b in ctx.operadoras on a.operadoraID equals b.operadoraID
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.tempoMinimoChamada,
                                 a.tempoMinimoTarifacao,
                                 a.operadoraID,
                                 a.periodicidadeTarifa,
                                 a.taxaConexao,
                                 a.ligacao_0300,
                                 a.dddCelular,
                                 a.dddFixo,
                                 a.localCelular,
                                 a.localFixo,
                                 b.descricao,
                             }).ToList();
                            return resultado;
        }
        public List<gruporamal> GetGruporamals()
        {
            var ctx = new tarifadorEntities();
            return ctx.gruporamals.ToList();

        }
        public List<grupousuario> GetGrupoUsuarios()
        {
            var ctx = new tarifadorEntities();
            return ctx.grupousuarios.ToList();

        }

        public void Pattern(string numero)
        {
            Regex dddCelular = new Regex(@"^(0[1-9]{2}){0,1}(([1-3][1-9]9{0,1}[6-9][0-9]{7})|([5-9][1-9]9{0,1}[6-9][0-9]{7})|(4[1-2]9{0,1}[6-9][0-9]{7})|(4[4-9]9{0,1}[6-9][0-9]{7}))$");
            if (dddCelular.IsMatch(numero))
            {
                return;
            }
            else
            {

                numero = "falso";
            }
        }



    }
}