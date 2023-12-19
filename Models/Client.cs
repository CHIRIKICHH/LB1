using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string LastName { get; set; }     
        public string FirstName { get; set; }     
        public string MiddleName { get; set; }     
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        
        public override string ToString()
        {
            return $"{Id} - {LastName} {FirstName} {MiddleName} - {ContactPhone}";
        }
    }
}
