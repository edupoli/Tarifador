//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tarifador
{
    using System;
    using System.Collections.Generic;
    
    public partial class ramal
    {
        public int id { get; set; }
        public string numero { get; set; }
        public int grupoRamalID { get; set; }
        public int usuarioID { get; set; }
        public string servidor { get; set; }
        public string observacao { get; set; }
    
        public virtual gruporamal gruporamal { get; set; }
        public virtual usuario usuario { get; set; }
    }
}