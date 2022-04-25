using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Models.Entities
{
    public class TipoDocumento
    {
        [Key]
        public int TipoDocumentoId { get; set; }
        [Column("Tipo de documento", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }

        public virtual List<Cliente> Clientes { get; set; } 
    }
}
