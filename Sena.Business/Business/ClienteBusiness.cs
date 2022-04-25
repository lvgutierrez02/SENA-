using Microsoft.EntityFrameworkCore;
using Sena.Business.Abstract;
using Sena.DAL;
using Sena.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business
{
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly AppDbContext _context;
        public ClienteBusiness(AppDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.Include(c => c.TipoDocumento).ToListAsync();

        }


        public async Task<IEnumerable<Cliente>> ObtenerPorTipoDocumento(int tipoDocumento)
        {
            return await _context.Clientes.Include(c => c.TipoDocumento).Where(x => x.TipoDocumentoId == tipoDocumento).ToListAsync();

        }

        public void Crear(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            cliente.Estado = true;
            _context.Add(cliente);

        }

        public async Task<bool> GuardarCambios() {

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<Cliente> ObtenerClientePorId(int? id) { 
            
            if(id==null)
                throw new ArgumentNullException(nameof(id));  
            
            return await _context.Clientes.Include(x=>x.TipoDocumento).FirstOrDefaultAsync(x=>x.ClienteId==id);   
        }    


    } 
}
