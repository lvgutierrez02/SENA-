﻿using Sena.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business.Abstract
{
    public interface IClienteBusiness
    {

        Task<IEnumerable<Cliente>> ObtenerClientes();
        Task<IEnumerable<Cliente>> ObtenerPorTipoDocumento(int tipoDocumento);
        Task<Cliente> ObtenerClientePorId(int? id);
        void Crear(Cliente cliente);
        void Editar(Cliente cliente);
        Task<bool> GuardarCambios();
    }
}
