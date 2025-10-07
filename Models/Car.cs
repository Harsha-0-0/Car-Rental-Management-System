using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_Management_System.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} - ${PricePerDay}/day";
        }
    }
}
