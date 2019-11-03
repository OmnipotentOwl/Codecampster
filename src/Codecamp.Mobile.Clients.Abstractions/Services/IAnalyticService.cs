using System.Collections.Generic;

namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface IAnalyticService
    {
        void TrackEvent(string name, Dictionary<string, string> properties = null);
    }
}