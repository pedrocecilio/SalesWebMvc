using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //Crio dependencia
        private readonly SalesWebMvcContext _context;

        //Crio injeção de pendencia
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Retorno lista de Vendedores
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        //Cria novo vendedor
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        //Busca por Id do vendedor
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Departament).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        
        //Remove um vendedor
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
           

        }
    }
}
