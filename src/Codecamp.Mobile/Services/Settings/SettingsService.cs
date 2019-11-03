using System;
using System.Collections.Generic;
using System.Text;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Xamarin.Essentials;

namespace Codecamp.Mobile.Clients.Portable.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants
        private const string IdUseMocks = "use_mocks";
        private readonly bool UseMocksDefault = false;
        private const string PushNotificationsEnabledKey = "push_enabled";
        private static readonly bool PushNotificationsEnabledDefault = false;
        private const string FavoriteModeKey = "favorites_only";
        private static readonly bool FavoriteModeDefault = false;
        private const string ShowAllCategoriesKey = "all_categories";
        private static readonly bool ShowAllCategoriesDefault = true;
        private const string FilteredCategoriesKey = "filtered_categories";
        private static readonly string FilteredCategoriesDefault = string.Empty;
        #endregion

        #region Settings Properties
        public bool UseMocks
        {
            get => GetValueOrDefault(IdUseMocks, UseMocksDefault);
            set => AddOrUpdateValue(IdUseMocks, value);
        }
        public bool PushNotificationsEnabled
        {
            get => GetValueOrDefault(PushNotificationsEnabledKey, PushNotificationsEnabledDefault);
            set => AddOrUpdateValue(PushNotificationsEnabledKey, value);
        }
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to see favorites only.
        /// </summary>
        /// <value><c>true</c> if favorites only; otherwise, <c>false</c>.</value>
        public bool FavoritesOnly
        {
            get => GetValueOrDefault(FavoriteModeKey, FavoriteModeDefault);
            set => AddOrUpdateValue(FavoriteModeKey, value);
        }
        /// <summary>
        /// Gets or sets a value indicating whether the user wants to show all categories.
        /// </summary>
        /// <value><c>true</c> if show all categories; otherwise, <c>false</c>.</value>
        public bool ShowAllCategories
        {
            get => GetValueOrDefault(ShowAllCategoriesKey, ShowAllCategoriesDefault);
            set => AddOrUpdateValue(ShowAllCategoriesKey, value);
        }
        public string FilteredCategories
        {
            get => GetValueOrDefault(FilteredCategoriesKey, FilteredCategoriesDefault);
            set => AddOrUpdateValue(FilteredCategoriesKey, value);
        }
        #endregion

        #region Public Methods
        public void AddOrUpdateValue(string key, bool value) => Preferences.Set(key, value);
        public void AddOrUpdateValue(string key, string value) => Preferences.Set(key, value);
        public bool GetValueOrDefault(string key, bool defaultValue) => Preferences.Get(key, defaultValue);
        public string GetValueOrDefault(string key, string defaultValue) => Preferences.Get(key, defaultValue);
        #endregion
    }
}
