using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Codecamp.Mobile.DataObjects;

namespace Codecamp.Mobile.Services.Abstractions.Backend
{
    public interface ISpeakerService
    {
        Task<ObservableCollection<Speaker>> GetSpeakers(bool forceRefresh = false);
        Task<Speaker> GetSpeaker(string id);
    }
}