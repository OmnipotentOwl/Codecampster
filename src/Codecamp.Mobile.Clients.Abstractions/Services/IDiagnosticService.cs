using System;
using System.Collections.Generic;

namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface IDiagnosticService
    {
        void TrackError(Exception exception, Dictionary<string, string> properties = null);
    }
}