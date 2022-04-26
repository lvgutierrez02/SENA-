using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business.DTOs.usuarios
{
    public class UsuarioAsignarRol
    {
        public string Id { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
    }
}
