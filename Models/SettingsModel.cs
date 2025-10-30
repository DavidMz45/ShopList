namespace ShopList.Models
{
    public class SettingsModel
    {
        public string OrderBy { get; set; } = "Alphabetical"; // or "Category"
        public string Theme { get; set; } = "Light"; // or "Dark"
        public bool ConfirmBeforeDelete { get; set; } = true;
    }
}
