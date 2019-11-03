using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Codecamp.Mobile.Clients.Portable.Helpers;
using Codecamp.Mobile.Clients.Portable.Models.Extensions;
using Codecamp.Mobile.DataObjects;
using Microsoft.Extensions.Logging;
using MvvmHelpers;
using Xamarin.Forms;

namespace Codecamp.Mobile.Clients.Portable.ViewModels
{
    public class SessionsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settings;
        public SessionsViewModel(
            ISettingsService settings)
        {
            _settings = settings;
        }

        public ObservableRangeCollection<Session> Sessions { get; } = new ObservableRangeCollection<Session>();
        public ObservableRangeCollection<Session> SessionsFiltered { get; } = new ObservableRangeCollection<Session>();
        public ObservableRangeCollection<Grouping<string, Session>> SessionsGrouped { get; } = new ObservableRangeCollection<Grouping<string, Session>>();

        #region Properties
        Session selectedSession;
        public Session SelectedSession
        {
            get { return selectedSession; }
            set
            {
                selectedSession = value;
                OnPropertyChanged();
                if (selectedSession == null)
                    return;

                MessagingService.Current.SendMessage(MessageKeys.NavigateToSession, selectedSession);

                SelectedSession = null;
            }
        }
        string filter = string.Empty;
        public string Filter
        {
            get { return filter; }
            set
            {
                if (SetProperty(ref filter, value))
                    ExecuteFilterSessionsAsync();

            }
        }
        #endregion

        #region Filtering and Sorting
        void SortSessions()
        {
            SessionsGrouped.ReplaceRange(SessionsFiltered.FilterAndGroupByDate());
        }
        bool noSessionsFound;
        public bool NoSessionsFound
        {
            get { return noSessionsFound; }
            set { SetProperty(ref noSessionsFound, value); }
        }

        string noSessionsFoundMessage;
        public string NoSessionsFoundMessage
        {
            get { return noSessionsFoundMessage; }
            set { SetProperty(ref noSessionsFoundMessage, value); }
        }
        #endregion

        #region Commands
        ICommand forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
            forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync()));

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadSessionsAsync(true);
        }

        ICommand filterSessionsCommand;
        public ICommand FilterSessionsCommand =>
            filterSessionsCommand ?? (filterSessionsCommand = new Command(async () => await ExecuteFilterSessionsAsync()));

        async Task ExecuteFilterSessionsAsync()
        {
            IsBusy = true;
            NoSessionsFound = false;

            // Abort the current command if the query has changed and is not empty
            if (!string.IsNullOrEmpty(Filter))
            {
                var query = Filter;
                await Task.Delay(250);
                if (query != Filter) 
                    return;
            }

            SessionsFiltered.ReplaceRange(Sessions.Search(Filter));
            SortSessions();

            if(SessionsGrouped.Count == 0)
            {
                if(Settings.Current.FavoritesOnly)
                {
                    if(!Settings.Current.ShowPastSessions && !string.IsNullOrWhiteSpace(Filter))
                        NoSessionsFoundMessage = "You haven't favorited\nany sessions yet.";
                    else
                        NoSessionsFoundMessage = "No Sessions Found";
                }
                else
                    NoSessionsFoundMessage = "No Sessions Found";

                NoSessionsFound = true;
            }
            else
            {
                NoSessionsFound = false;
            }

            IsBusy = false;
        }

        ICommand loadSessionsCommand;
        public ICommand LoadSessionsCommand =>
            loadSessionsCommand ?? (loadSessionsCommand = new Command<bool>(async (f) => await ExecuteLoadSessionsAsync()));


        async Task<bool> ExecuteLoadSessionsAsync(bool force = false)
        {
            if (IsBusy)
                return false;

            try
            {
                NextForceRefresh = DateTime.UtcNow.AddMinutes(45);
                IsBusy = true;
                NoSessionsFound = false;
                Filter = string.Empty;

#if DEBUG
                await Task.Delay(1000);
#endif

                Sessions.ReplaceRange(await StoreManager.SessionStore.GetItemsAsync(force));

                SessionsFiltered.ReplaceRange(Sessions);
                SortSessions();

                if (SessionsGrouped.Count == 0)
                {

                    if (Settings.Current.FavoritesOnly)
                    {
                        if (!Settings.Current.ShowPastSessions)
                            NoSessionsFoundMessage = "You haven't favorited\nany sessions yet.";
                        else
                            NoSessionsFoundMessage = "No Sessions Found";
                    }
                    else
                        NoSessionsFoundMessage = "No Sessions Found";

                    NoSessionsFound = true;
                }
                else
                {
                    NoSessionsFound = false;
                }



            }
            catch (Exception ex)
            {
                Logger<>.Report(ex, "Method", "ExecuteLoadSessionsAsync");
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }

        ICommand favoriteCommand;
        public ICommand FavoriteCommand =>
            favoriteCommand ?? (favoriteCommand = new Command<Session>(async (s) => await ExecuteFavoriteCommandAsync(s)));

        async Task ExecuteFavoriteCommandAsync(Session session)
        {
            var toggled = await FavoriteService.ToggleFavorite(session);
            if (toggled && Settings.Current.FavoritesOnly)
                SortSessions();
        }
        #endregion

        public IEnumerable<Grouping<string, Session>> FilterAndGroupByDate(IList<Session> sessions)
        {
            if (_settings.FavoritesOnly)
            {
                sessions = sessions.Where(s => s.IsFavorite).ToList();
            }

            var tba = sessions.Where(s => !s.StartTime.HasValue || !s.EndTime.HasValue || s.StartTime.Value.IsTBA());


            //var showPast = _settings.ShowPastSessions;
            var showAllCategories = _settings.ShowAllCategories;
            var filteredCategories = _settings.FilteredCategories;
            var utc = DateTime.UtcNow;


            //is not tba
            //has not started or has started and hasn't ended or ended 20 minutes ago
            //filter then by category and filters
            var grouped = (from session in sessions
                where session.StartTime.HasValue && session.EndTime.HasValue && !session.StartTime.Value.IsTBA() && (showPast || (utc <= session.StartTime.Value || utc <= session.EndTime.Value.AddMinutes(20)))
                      && (showAllCategories || filteredCategories.IndexOf(session?.MainCategory?.Name ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0)
                orderby session.StartTimeOrderBy, session.Title
                group session by session.GetSortName()
                into sessionGroup
                select new Grouping<string, Session>(sessionGroup.Key, sessionGroup)).ToList();

            if (tba.Any())
                grouped.Add(new Grouping<string, Session>("TBA", tba));

            return grouped;
        }
    }
}