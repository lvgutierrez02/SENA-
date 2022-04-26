using Sena.Business.DTOs.clientes;
using Sena.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business.Abstract
{
    public interface IClienteBusiness
    {

        Task<IEnumerable<ClienteDetalleGestionarDto>> ObtenerClientes();
        void Crear(RegistroClienteDto registroClienteDto);
        Task<IEnumerable<Cliente>> ObtenerClientesPorTipoDocumento(int tipoDocumento);
        Task<Cliente> ObtenerClientePorId(int? id);
        void Editar(Cliente cliente);
        Task<bool> GuardarCambios();
    }
}
