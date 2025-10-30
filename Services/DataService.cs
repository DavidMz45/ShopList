using ShopList.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopList.Services
{
    public class DataService
    {
        private static DataService _instance;
        public static DataService Instance => _instance ??= new DataService();

        const string FILE = "shoplist_data.json";

        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<TemplateList> Templates { get; set; } = new();
        public List<HistoryItem> History { get; set; } = new();
        public SettingsModel Settings { get; set; } = new();

        private DataService() { }

        public async Task LoadAsync()
        {
            try
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, FILE);
                if (!File.Exists(path))
                {
                    // Seed
                    Categories = new List<Category>
                    {
                        new Category { Name = "General" },
                        new Category { Name = "Frutas" },
                        new Category { Name = "Verduras" }
                    };
                    Products = new List<Product>();
                    Templates = new List<TemplateList>();
                    History = new List<HistoryItem>();
                    Settings = new SettingsModel();
                    await SaveAsync();
                    return;
                }

                var json = await File.ReadAllTextAsync(path);
                var doc = JsonSerializer.Deserialize<PersistedData>(json);
                Products = doc.Products ?? new List<Product>();
                Categories = doc.Categories ?? new List<Category>();
                Templates = doc.Templates ?? new List<TemplateList>();
                History = doc.History ?? new List<HistoryItem>();
                Settings = doc.Settings ?? new SettingsModel();
            }
            catch
            {
                Products = new List<Product>();
                Categories = new List<Category>();
                Templates = new List<TemplateList>();
                History = new List<HistoryItem>();
                Settings = new SettingsModel();
            }
        }

        public async Task SaveAsync()
        {
            var doc = new PersistedData
            {
                Products = this.Products,
                Categories = this.Categories,
                Templates = this.Templates,
                History = this.History,
                Settings = this.Settings
            };
            var json = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            var path = Path.Combine(FileSystem.AppDataDirectory, FILE);
            await File.WriteAllTextAsync(path, json);
        }

        private class PersistedData
        {
            public List<Product> Products { get; set; }
            public List<Category> Categories { get; set; }
            public List<TemplateList> Templates { get; set; }
            public List<HistoryItem> History { get; set; }
            public SettingsModel Settings { get; set; }
        }
    }
}
