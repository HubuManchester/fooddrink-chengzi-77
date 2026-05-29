using SQLite;
using FoodDrinkApp.Models;

namespace FoodDrinkApp.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private readonly string _databasePath;

    public DatabaseService()
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _databasePath = Path.Combine(folder, "fooditems.db3");
    }

    private async Task InitAsync()
    {
        if (_database != null)
            return;

        _database = new SQLiteAsyncConnection(_databasePath);
        await _database.CreateTableAsync<FoodItem>();
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        await InitAsync();
        return await _database.Table<FoodItem>().ToListAsync();
    }

    public async Task<List<FoodItem>> SearchAsync(string? query)
    {
        await InitAsync();

        if (string.IsNullOrWhiteSpace(query))
        {
            return await GetAllAsync();
        }

        var normalized = query.Trim().ToLower();
        var items = await _database.Table<FoodItem>().ToListAsync();

        return items.Where(item =>
            item.Name.ToLower().Contains(normalized) ||
            item.Category.ToLower().Contains(normalized) ||
            item.Description.ToLower().Contains(normalized) ||
            item.Tags.ToLower().Contains(normalized)
        ).ToList();
    }

    public async Task<FoodItem?> GetByIdAsync(int id)
    {
        await InitAsync();
        return await _database.Table<FoodItem>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> SaveAsync(FoodItem item)
    {
        await InitAsync();

        if (item.Id == 0)
        {
            return await _database.InsertAsync(item);
        }
        else
        {
            return await _database.UpdateAsync(item);
        }
    }

    public async Task<int> DeleteAsync(FoodItem item)
    {
        await InitAsync();
        return await _database.DeleteAsync(item);
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        await InitAsync();
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            return await _database.DeleteAsync(item);
        }
        return 0;
    }

    public async Task<bool> SeedInitialDataAsync()
    {
        await InitAsync();

        var existingCount = await _database.Table<FoodItem>().CountAsync();
        if (existingCount > 0)
        {
            return false;
        }

        var initialItems = new List<FoodItem>
        {
            new FoodItem
            {
                Name = "Avocado Toast",
                Category = "Breakfast",
                Description = "Classic creamy breakfast with avocado on sourdough",
                Calories = 320,
                Protein = 8,
                Carbs = 28,
                Fat = 22,
                AllergyNote = "Contains gluten",
                Tags = "breakfast healthy vegetarian"
            },
            new FoodItem
            {
                Name = "Berry Smoothie Bowl",
                Category = "Breakfast",
                Description = "Fresh and antioxidant-rich berry bowl",
                Calories = 350,
                Protein = 12,
                Carbs = 55,
                Fat = 8,
                AllergyNote = "Dairy-free option available",
                Tags = "breakfast vegan smoothie"
            },
            new FoodItem
            {
                Name = "Chicken Quinoa Bowl",
                Category = "Lunch",
                Description = "Protein-packed healthy grain bowl",
                Calories = 580,
                Protein = 38,
                Carbs = 58,
                Fat = 14,
                AllergyNote = "No common allergens",
                Tags = "lunch protein healthy"
            },
            new FoodItem
            {
                Name = "Banana Pancakes",
                Category = "Breakfast",
                Description = "Fluffy, naturally sweet pancake stack",
                Calories = 450,
                Protein = 12,
                Carbs = 65,
                Fat = 15,
                AllergyNote = "Contains gluten and eggs",
                Tags = "breakfast sweet pancakes"
            }
        };

        foreach (var item in initialItems)
        {
            await _database.InsertAsync(item);
        }

        return true;
    }
}