using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace ShopList.Services;

public class SettingsService : ISettingsService
{
    private const string ThemeKey = "settings_theme";
    private const string OrderKey = "settings_order";
    private const string ConfirmDeleteKey = "settings_confirm_delete";
    private const string ConfirmClearKey = "settings_confirm_clear";
    private const string FontScaleKey = "settings_font_scale";

    public AppTheme GetTheme()
    {
        var stored = Preferences.Get(ThemeKey, AppTheme.Unspecified.ToString());
        return Enum.TryParse<AppTheme>(stored, out var theme) ? theme : AppTheme.Unspecified;
    }

    public void SetTheme(AppTheme theme)
    {
        Preferences.Set(ThemeKey, theme.ToString());
    }

    public ListOrderOption GetListOrder()
    {
        var stored = Preferences.Get(OrderKey, ListOrderOption.Alphabetical.ToString());
        return Enum.TryParse<ListOrderOption>(stored, out var order) ? order : ListOrderOption.Alphabetical;
    }

    public void SetListOrder(ListOrderOption option)
    {
        Preferences.Set(OrderKey, option.ToString());
    }

    public bool GetConfirmDeletion()
        => Preferences.Get(ConfirmDeleteKey, true);

    public void SetConfirmDeletion(bool value)
        => Preferences.Set(ConfirmDeleteKey, value);

    public bool GetConfirmClear()
        => Preferences.Get(ConfirmClearKey, true);

    public void SetConfirmClear(bool value)
        => Preferences.Set(ConfirmClearKey, value);

    public double GetFontSizeMultiplier()
        => Preferences.Get(FontScaleKey, 1.0);

    public void SetFontSizeMultiplier(double value)
        => Preferences.Set(FontScaleKey, value);
}
