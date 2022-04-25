using Microsoft.EntityFrameworkCore;
using Sena.Business.Abstract;
using Sena.DAL;
using Sena.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sena.Business.Business
{
    public  class TipoDocumentoBusiness:ITipoDocumentoBusiness
    {
        private readonly AppDbContext _context;
        public TipoDocumentoBusiness(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumento()
        {
            return await _context.TiposDocumento.ToListAsync(); 

        }





    }
}
