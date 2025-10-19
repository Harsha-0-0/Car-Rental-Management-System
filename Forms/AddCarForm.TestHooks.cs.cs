using System.Collections.Generic;
using System.Text.Json;
using Car_Rental_Management_System.Models;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Car_Rental_Management_System.Tests")]

namespace Car_Rental_Management_System.Forms
{
    public partial class AddCarForm
    {
        internal static List<Car> ParseCarsJson(string json)
        {
            var cars = new List<Car>();
            if (string.IsNullOrWhiteSpace(json)) return cars;

            using var doc = JsonDocument.Parse(json);
            foreach (var el in doc.RootElement.EnumerateArray())
            {
                if (el.TryGetProperty("LuxuryTaxRate", out var tax))
                {
                    cars.Add(new LuxuryCar
                    {
                        CarId = el.GetProperty("CarId").GetInt32(),
                        Brand = el.GetProperty("Brand").GetString(),
                        Model = el.GetProperty("Model").GetString(),
                        PricePerDay = el.GetProperty("PricePerDay").GetDecimal(),
                        ImagePath = el.GetProperty("ImagePath").GetString(),
                        LuxuryTaxRate = tax.GetDecimal()
                    });
                }
                else
                {
                    cars.Add(new Car
                    {
                        CarId = el.GetProperty("CarId").GetInt32(),
                        Brand = el.GetProperty("Brand").GetString(),
                        Model = el.GetProperty("Model").GetString(),
                        PricePerDay = el.GetProperty("PricePerDay").GetDecimal(),
                        ImagePath = el.GetProperty("ImagePath").GetString()
                    });
                }
            }
            return cars;
        }

        internal static int NextCarId(List<Car> cars) => cars.Count > 0 ? cars[^1].CarId + 1 : 1;

        internal static string AddCarAndSerialize(List<Car> cars, string brand, string model, decimal price, string imageRel, bool isLuxury, decimal defaultLuxuryTax = 0.15m)
        {
            Car newCar = isLuxury
                ? new LuxuryCar { CarId = NextCarId(cars), Brand = brand.Trim(), Model = model.Trim(), PricePerDay = price, ImagePath = imageRel, LuxuryTaxRate = defaultLuxuryTax }
                : new Car { CarId = NextCarId(cars), Brand = brand.Trim(), Model = model.Trim(), PricePerDay = price, ImagePath = imageRel };

            cars.Add(newCar);

            // Flatten to the same JSON shape your form writes
            var list = new List<Dictionary<string, object>>();
            foreach (var c in cars)
            {
                var d = new Dictionary<string, object>
                {
                    ["CarId"] = c.CarId,
                    ["Brand"] = c.Brand,
                    ["Model"] = c.Model,
                    ["PricePerDay"] = c.PricePerDay,
                    ["ImagePath"] = c.ImagePath
                };
                if (c is LuxuryCar L) d["LuxuryTaxRate"] = L.LuxuryTaxRate;
                list.Add(d);
            }

            return JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
