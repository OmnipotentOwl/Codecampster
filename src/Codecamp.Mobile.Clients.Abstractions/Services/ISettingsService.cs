namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface ISettingsService
    {
        bool UseMocks { get; set; }
        bool PushNotificationsEnabled { get; set; }
        bool FavoritesOnly { get; set; }
        bool ShowAllCategories { get; set; }
        string FilteredCategories { get; set; }


        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        void AddOrUpdateValue(string key, bool value);
        void AddOrUpdateValue(string key, string value);
    }
}