﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tarifadorEntities : DbContext
    {
        public tarifadorEntities()
            : base("name=tarifadorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<bilhete> bilhetes { get; set; }
        public DbSet<gruporamal> gruporamals { get; set; }
        public DbSet<grupotronco> grupotroncoes { get; set; }
        public DbSet<grupousuario> grupousuarios { get; set; }
        public DbSet<operadora> operadoras { get; set; }
        public DbSet<planotarifacao> planotarifacaos { get; set; }
        public DbSet<ramal> ramals { get; set; }
        public DbSet<tronco> troncoes { get; set; }
        public DbSet<usuario> usuarios { get; set; }
    }
}
