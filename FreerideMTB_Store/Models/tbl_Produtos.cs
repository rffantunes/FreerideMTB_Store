namespace FreerideMTB_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Produtos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Produtos()
        {
            tbl_Encomenda = new HashSet<tbl_Encomenda>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_produto { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Column(TypeName = "money")]
        public decimal? Preco { get; set; }

        public string Foto { get; set; }

        public int Categoria { get; set; }

        public int? Sub_Categoria { get; set; }

        [Required]
        [StringLength(10)]
        public string Stock { get; set; }

        public virtual tbl_Categoria tbl_Categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Encomenda> tbl_Encomenda { get; set; }

        public virtual tbl_Sub_Categoria tbl_Sub_Categoria { get; set; }
    }
}
