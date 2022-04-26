using Sena.Business.DTOs.usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business.Abstract
{
    public interface IUsuarioBusiness
    {
        Task<UsuarioDto> ObtenerUsuarioDtoPorEmail(string email);
        Task<string> Crear(RegistrarUsuarioDto registrarUsuarioDto);
        Task<IEnumerable<UsuarioDto>> ObtenerListaUsuarios();
    }
}
