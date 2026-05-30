using FoodDrinkApp.Services;
using FoodDrinkApp.Models;

namespace FoodDrinkApp;

public partial class MainPage : ContentPage
{
    private DatabaseService _databaseService;

    public MainPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AccessibilityService.ApplyFontScale(this);

        // Seed initial data if database is empty
        await _databaseService.SeedInitialDataAsync();

        await LoadFoodItemsAsync();
    }

    private async Task LoadFoodItemsAsync(string? query = null)
    {
        var items = await _databaseService.SearchAsync(query);
        FoodCollection.ItemsSource = items;
        UpdateDailyStats(items);
    }

    private void UpdateDailyStats(IReadOnlyList<FoodItem> items)
    {
        var todayTotal = items.Sum(item => item.Calories);
        var targetCalories = 2000;

        if (CurrentCaloriesLabel != null)
        {
            CurrentCaloriesLabel.Text = todayTotal.ToString();
            var progress = (double)todayTotal / targetCalories;
            CaloriesProgressBar.Progress = progress > 1 ? 1 : progress;
        }

        var totalProtein = items.Sum(item => item.Protein);
        var totalCarbs = items.Sum(item => item.Carbs);
        var totalFat = items.Sum(item => item.Fat);
        var totalMacros = totalProtein + totalCarbs + totalFat;

        if (totalMacros > 0 && ProteinLabel != null && CarbsLabel != null && FatLabel != null)
        {
            ProteinLabel.Text = $"{(int)((double)totalProtein / totalMacros * 100)}%";
            CarbsLabel.Text = $"{(int)((double)totalCarbs / totalMacros * 100)}%";
            FatLabel.Text = $"{(int)((double)totalFat / totalMacros * 100)}%";
        }
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }

    private async void OnDetailsClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int id)
        {
            await Shell.Current.GoToAsync($"{nameof(FoodDetailPage)}?id={id}");
        }
    }

    private async void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        await LoadFoodItemsAsync(e.NewTextValue);
    }

    private async void OnSearchButtonPressed(object? sender, EventArgs e)
    {
        await LoadFoodItemsAsync(SearchFoodBar.Text);
    }

    private async void OnRefreshing(object? sender, EventArgs e)
    {
        await LoadFoodItemsAsync(SearchFoodBar.Text);
        FoodRefreshView.IsRefreshing = false;
        SemanticScreenReader.Announce("Food list refreshed from database.");
    }

    private async void OnLogMealClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }

    private async void OnScanBarcodeClicked(object? sender, EventArgs e)
    {
        await DisplayAlert("Scan Barcode", "Camera feature will be implemented soon", "OK");
    }

    private async void OnQuickAddClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddItemPage));
    }

    private async void OnInsightsClicked(object? sender, EventArgs e)
    {
        var items = FoodCollection.ItemsSource as IReadOnlyList<FoodItem>;
        if (items != null && items.Any())
        {
            var totalCalories = items.Sum(i => i.Calories);
            var avgCalories = items.Average(i => i.Calories);
            var topMeal = items.OrderByDescending(i => i.Calories).FirstOrDefault();

            var insightMessage = $"Today's total: {totalCalories} calories. " +
                                 $"Average per meal: {avgCalories:F0} calories. " +
                                 $"Highest calorie meal: {topMeal?.Name} with {topMeal?.Calories} calories.";
            await DisplayAlert("Nutrition Insights", insightMessage, "OK");
            SemanticScreenReader.Announce(insightMessage);
        }
        else
        {
            await DisplayAlert("Nutrition Insights", "Add some meals first to see insights", "OK");
        }
    }

    private async void OnSeeAllClicked(object? sender, EventArgs e)
    {
        if (SearchFoodBar != null)
        {
            SearchFoodBar.Text = string.Empty;
            await LoadFoodItemsAsync(null);
        }
    }

    // Swipe to delete handler
    private async void OnSwipeDeleteInvoked(object? sender, EventArgs e)
    {
        try
        {
            var swipeItem = sender as SwipeItem;
            if (swipeItem?.CommandParameter is not int id)
                return;

            // Find the item to delete
            var items = FoodCollection.ItemsSource as IReadOnlyList<FoodItem>;
            var itemToDelete = items?.FirstOrDefault(x => x.Id == id);

            if (itemToDelete == null)
                return;

            // Confirm deletion
            var confirm = await DisplayAlert("Delete Item",
                $"Are you sure you want to delete '{itemToDelete.Name}'?",
                "Yes", "No");

            if (!confirm)
                return;

            // Delete from database
            await _databaseService.DeleteByIdAsync(id);

            // Refresh the list
            await LoadFoodItemsAsync(SearchFoodBar.Text);

            // Announce deletion for accessibility
            SemanticScreenReader.Announce($"{itemToDelete.Name} has been deleted.");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete: {ex.Message}", "OK");
        }
    }
}