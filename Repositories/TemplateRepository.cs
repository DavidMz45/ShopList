using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using ShopList.Data;
using ShopList.Models;

namespace ShopList.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private const string TemplateFileName = "templates.json";
    private readonly Database _database;

    public TemplateRepository(Database database)
    {
        _database = database;
    }

    public async Task<IList<TemplateList>> GetAllAsync()
    {
        await _database.InitializeAsync();
        var query = _database.Connection.Table<TemplateList>().OrderBy(t => t.Name);
        return await query.ToListAsync();
    }

    public async Task<TemplateList?> GetByIdAsync(int id)
    {
        await _database.InitializeAsync();
        return await _database.Connection.FindAsync<TemplateList>(id);
    }

    public async Task<int> SaveAsync(TemplateList template)
    {
        await _database.InitializeAsync();
        template.Name = template.Name.Trim();
        template.ItemsJson = template.ItemsJson ?? "[]";

        int result = template.Id == 0
            ? await _database.Connection.InsertAsync(template)
            : await _database.Connection.UpdateAsync(template);

        await PersistTemplatesToFileAsync();
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        await _database.InitializeAsync();
        await _database.Connection.DeleteAsync<TemplateList>(id);
        await PersistTemplatesToFileAsync();
    }

    public async Task<IList<TemplateList>> LoadFromFileAsync()
    {
        await _database.InitializeAsync();
        var path = GetTemplateFilePath();
        if (!File.Exists(path))
        {
            await PersistTemplatesToFileAsync();
            return await GetAllAsync();
        }

        try
        {
            var json = await File.ReadAllTextAsync(path);
            var templates = JsonSerializer.Deserialize<List<TemplateList>>(json) ?? new List<TemplateList>();
            foreach (var template in templates)
            {
                template.ItemsJson ??= "[]";
                if (template.Id == 0)
                {
                    await _database.Connection.InsertAsync(template);
                }
                else
                {
                    var existing = await _database.Connection.FindAsync<TemplateList>(template.Id);
                    if (existing == null)
                    {
                        await _database.Connection.InsertAsync(template);
                    }
                    else
                    {
                        existing.Name = template.Name;
                        existing.ItemsJson = template.ItemsJson;
                        await _database.Connection.UpdateAsync(existing);
                    }
                }
            }
        }
        catch
        {
            // ignore corrupted file and recreate
        }

        await PersistTemplatesToFileAsync();
        return await GetAllAsync();
    }

    private async Task PersistTemplatesToFileAsync()
    {
        var path = GetTemplateFilePath();
        var templates = await _database.Connection.Table<TemplateList>().ToListAsync();
        var json = JsonSerializer.Serialize(templates, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(path, json);
    }

    private static string GetTemplateFilePath()
        => Path.Combine(FileSystem.AppDataDirectory, TemplateFileName);
}
