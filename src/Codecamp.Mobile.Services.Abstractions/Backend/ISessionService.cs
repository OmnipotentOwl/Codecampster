using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Codecamp.Mobile.DataObjects;

namespace Codecamp.Mobile.Services.Abstractions.Backend
{
    public interface ISessionService
    {
        Task<ObservableCollection<Session>> GetSessions(bool forceRefresh = false);
        Task<ObservableCollection<Session>> GetSpeakerSessionsAsync(string speakerId);
        Task<ObservableCollection<Session>> GetNextSessions();
        Task<Session> GetAppIndexSession(string id);
    }
}