using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Car_Rental_Management_System.Data
{
    public class Repository<T> where T : class
    {
        private readonly string _filePath;

        public Repository(string filePath)
        {
            _filePath = filePath;
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath)) return new List<T>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void Save(List<T> items)
        {
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
