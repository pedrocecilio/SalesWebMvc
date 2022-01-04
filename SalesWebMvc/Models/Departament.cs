using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //Contrutor sem argumentos
        public Departament()
        {
        }

        //Construtor com argumentos
        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Adiciona o vendedor
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        //Retorna total de vendas 
        public double TotalSales(DateTime inittial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(inittial, final));
        }
    }
}
