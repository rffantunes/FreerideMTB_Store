﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreerideMTB_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FreerideEntities : DbContext
    {

        public List<tbl_Imagens> ListaImagens { get; set; }




        public FreerideEntities()
            : base("name=FreerideEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tbl_Categoria> tbl_Categoria { get; set; }
        public virtual DbSet<tbl_Encomenda> tbl_Encomenda { get; set; }
        public virtual DbSet<tbl_ProdutoEncomenda> tbl_ProdutoEncomenda { get; set; }
        public virtual DbSet<tbl_Sub_Categoria> tbl_Sub_Categoria { get; set; }
        public virtual DbSet<tbl_Imagens> tbl_Imagens { get; set; }
        public virtual DbSet<tbl_Produtos> tbl_Produtos { get; set; }
        public virtual DbSet<tbl_Marca> tbl_Marca { get; set; }

    }
}
