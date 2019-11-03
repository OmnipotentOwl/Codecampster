using System.Collections.Generic;
using Codecamp.Mobile.Clients.Abstractions.Services;

namespace Codecamp.Mobile.Clients.Portable.Services.Analytic
{
    public class DummyAnalyticService : IAnalyticService
    {
        public void TrackEvent(string name, Dictionary<string, string> properties = null)
        {
            // Do nothing
        }
    }
}