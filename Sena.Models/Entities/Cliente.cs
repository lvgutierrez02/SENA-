using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        [DisplayName("Nombres")]
        [Column("NombreCliente", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio")]
        [Range(9999, 99999999999999, ErrorMessage = "Número de documento fuera de rango")]
        public string Documento { get; set; }

        public bool Estado { get; set; }


        [DisplayName("Tipo de documento")]
        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }


        //public class Cliente
        //{
        //    public int ClienteId { get; set; }
        //    public string Nombres { get; set; }
        //    public string Email { get; set; }
        //    public string Documento { get; set; }
        //    public bool Estado { get; set; }
        //    public int TipoDocumentoId { get; set; }
        //    public virtual TipoDocumento TipoDocumento { get; set; }
        //}
    }
}
