using Car_Rental_Management_System.Interface;
using System;

namespace Car_Rental_Management_System.Models
{
    public class Car : PriceCalculateInterface
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImagePath { get; set; }

        // Implement interface method
        public virtual decimal CalculatePrice(decimal baseRate, int days)
        {
            return baseRate * days;
        }

        // Optional: Keep compatibility with old method
        public virtual decimal GetTotalPrice(int days)
        {
            return CalculatePrice(PricePerDay, days);
        }

        public override string ToString()
        {
            return $"{Brand} {Model} - ${PricePerDay}/day";
        }
    }

    public class LuxuryCar : Car
    {
        public decimal LuxuryTaxRate { get; set; } = 0.15m;

        public override decimal CalculatePrice(decimal baseRate, int days)
        {
            var basePrice = baseRate * days;
            return basePrice + (basePrice * LuxuryTaxRate);
        }

        public override decimal GetTotalPrice(int days)
        {
            // Use the interface method internally
            return CalculatePrice(PricePerDay, days);
        }
    }
}
