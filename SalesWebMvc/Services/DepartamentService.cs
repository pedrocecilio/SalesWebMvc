using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartamentService
    {
        //Crio dependencia
        private readonly SalesWebMvcContext _context;

        //Crio injeção de pendencia
        public DepartamentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Retorno lista de departamentos
        public async Task<List<Departament>> FindAllAsync()
        {
            return await _context.Departament.OrderBy(x=>x.Name).ToListAsync();
        }
    }
}
