using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB1.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Browsable(false)]
        public Product Product { get; set; }
        [Browsable(false)]
        public Client Client { get; set; }
        public string DeliveryAdress { get; set; }
        public DateTime OrderDate { get; set; }
        [NotMapped]
        public string ProductName
        {
            get
            {
                return Product.ProductName;
            }
        }
        [NotMapped]
        public string ClientFullname
        {
            get
            {
                return $"{Client.FirstName} {Client.LastName} {Client.MiddleName}";
            }
        }
        [NotMapped]
        public string OrderSumm
        {
            get
            {
                return Product.Price.ToString() + "₴";
            }
        }
    }
}
