using FoodDrinkApp.Services;

namespace FoodDrinkApp;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AccessibilityService.ApplyFontScale(this);

        // Load saved settings
        LargeTextSwitch.IsToggled = AccessibilityService.LargeTextEnabled;
        UpdatePreviewTextSize();
    }

    private void OnThemeSelectedIndexChanged(object? sender, EventArgs e)
    {
        var selectedIndex = ThemePicker.SelectedIndex;
        var theme = selectedIndex switch
        {
            0 => "system",
            1 => "light",
            2 => "dark",
            _ => "system"
        };

        // Apply theme change
        Application.Current.UserAppTheme = selectedIndex switch
        {
            1 => AppTheme.Light,
            2 => AppTheme.Dark,
            _ => AppTheme.Unspecified
        };

        // Show success message
        ShowSuccessMessage();
    }

    private void OnLargeTextToggled(object? sender, ToggledEventArgs e)
    {
        AccessibilityService.LargeTextEnabled = e.Value;

        // Apply font scale to all pages
        var mainPage = Application.Current.MainPage;
        if (mainPage != null)
        {
            AccessibilityService.ApplyFontScale(mainPage);
        }

        UpdatePreviewTextSize();
    }

    private void UpdatePreviewTextSize()
    {
        if (PreviewLabel != null)
        {
            PreviewLabel.FontSize = AccessibilityService.LargeTextEnabled ? 22 : 16;
        }
    }

    private async void ShowSuccessMessage()
    {
        SuccessMessage.IsVisible = true;
        await Task.Delay(2000);
        SuccessMessage.IsVisible = false;
    }
}