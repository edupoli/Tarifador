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
    
    public partial class grupotronco
    {
        public grupotronco()
        {
            this.troncoes = new HashSet<tronco>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public int operadoraID { get; set; }
    
        public virtual ICollection<tronco> troncoes { get; set; }
        public virtual operadora operadora { get; set; }
    }
}
