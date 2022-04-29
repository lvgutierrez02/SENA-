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
        
        public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Cliente> Clientes { get; set; }
    }
}
