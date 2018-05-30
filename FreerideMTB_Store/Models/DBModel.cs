namespace FreerideMTB_Store.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<tbl_Categoria> tbl_Categoria { get; set; }
        public virtual DbSet<tbl_Encomenda> tbl_Encomenda { get; set; }
        public virtual DbSet<tbl_Produtos> tbl_Produtos { get; set; }
        public virtual DbSet<tbl_Sub_Categoria> tbl_Sub_Categoria { get; set; }
        public virtual DbSet<tbl_Intermedia_Encomenda> tbl_Intermedia_Encomenda { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.tbl_Encomenda)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.id_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Categoria>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Categoria>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Categoria>()
                .HasMany(e => e.tbl_Produtos)
                .WithRequired(e => e.tbl_Categoria)
                .HasForeignKey(e => e.Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Produtos>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Produtos>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Produtos>()
                .Property(e => e.Preco)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tbl_Produtos>()
                .Property(e => e.Foto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Produtos>()
                .Property(e => e.Stock)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Produtos>()
                .HasMany(e => e.tbl_Encomenda)
                .WithRequired(e => e.tbl_Produtos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Sub_Categoria>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Sub_Categoria>()
                .HasMany(e => e.tbl_Produtos)
                .WithOptional(e => e.tbl_Sub_Categoria)
                .HasForeignKey(e => e.Sub_Categoria);


            modelBuilder.Entity<tbl_Intermedia_Encomenda>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Intermedia_Encomenda>()
                .HasMany(e => e.tbl_Produtos)
                .WithOptional(e => e.tbl_Sub_Categoria)
                .HasForeignKey(e => e.Sub_Categoria);

        }
    }
}
