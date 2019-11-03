using System.Collections.Generic;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Microsoft.AppCenter.Analytics;

namespace Codecamp.Mobile.Clients.Portable.Services.Analytic
{
    public class AnalyticService : IAnalyticService
    {
        public void TrackEvent(string name, Dictionary<string, string> properties = null) => Analytics.TrackEvent(name, properties);
    }
}