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
    
    public partial class tronco
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string ddd { get; set; }
        public string numSaida { get; set; }
        public int planoID { get; set; }
        public int grupoTroncoID { get; set; }
        public int operadoraID { get; set; }
        public string canal { get; set; }
    
        public virtual grupotronco grupotronco { get; set; }
        public virtual operadora operadora { get; set; }
        public virtual planotarifacao planotarifacao { get; set; }
    }
}