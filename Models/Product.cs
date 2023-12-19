using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public double Price { get; set; }
        

        public override string ToString()
        {
            return $"{Id} - {ProductName} - {Price}₴";
        }
    }
}
