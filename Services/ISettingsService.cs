using Microsoft.Maui.Controls;

namespace ShopList.Services;

public enum ListOrderOption
{
    Alphabetical,
    ByCategory
}

public interface ISettingsService
{
    AppTheme GetTheme();
    void SetTheme(AppTheme theme);
    ListOrderOption GetListOrder();
    void SetListOrder(ListOrderOption option);
    bool GetConfirmDeletion();
    void SetConfirmDeletion(bool value);
    bool GetConfirmClear();
    void SetConfirmClear(bool value);
    double GetFontSizeMultiplier();
    void SetFontSizeMultiplier(double value);
}
