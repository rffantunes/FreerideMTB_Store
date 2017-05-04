namespace FreerideMTB_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Encomenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Encomenda { get; set; }

        public int id_Produto { get; set; }

        [Required]
        [StringLength(128)]
        public string id_User { get; set; }

        public int Quantidade { get; set; }

        public DateTime Data { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual tbl_Produtos tbl_Produtos { get; set; }
    }
}
