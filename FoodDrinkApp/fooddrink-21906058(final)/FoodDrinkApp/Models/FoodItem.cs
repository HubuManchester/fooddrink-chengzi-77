using SQLite;
using System.Text.Json.Serialization;

namespace FoodDrinkApp.Models;

[Table("FoodItems")]
public sealed class FoodItem
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Column("category")]
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [Column("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [Column("calories")]
    [JsonPropertyName("calories")]
    public int Calories { get; set; }

    [Column("protein")]
    [JsonPropertyName("protein")]
    public int Protein { get; set; }

    [Column("carbs")]
    [JsonPropertyName("carbs")]
    public int Carbs { get; set; }

    [Column("fat")]
    [JsonPropertyName("fat")]
    public int Fat { get; set; }

    [Column("allergy_note")]
    [JsonPropertyName("allergyNote")]
    public string AllergyNote { get; set; } = string.Empty;

    [Column("tags")]
    [JsonPropertyName("tags")]
    public string Tags { get; set; } = string.Empty;

    [Column("is_favourite")]
    public bool IsFavourite { get; set; }

    [Column("image_path")]
    public string ImagePath { get; set; } = string.Empty;

    [Ignore]
    public string CaloriesLabel => $"{Calories} kcal";

    [Ignore]
    public string MacroSummary => $"Protein {Protein}g, carbs {Carbs}g, fat {Fat}g";

    [Ignore]
    public string AccessibleSummary => $"{Name}. {Category}. {Calories} kcal. {MacroSummary}. {AllergyNote}";

    [Ignore]
    public string DisplayImagePath
    {
        get
        {
            if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
                return ImagePath;
            return $"{Name?.ToLower().Replace(" ", "_")}.png";
        }
    }
}