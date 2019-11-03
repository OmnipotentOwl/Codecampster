using System;
using System.Collections.Generic;
using Codecamp.Mobile.Clients.Abstractions.Services;

namespace Codecamp.Mobile.Clients.Portable.Services.Diagnostic
{
    public class DummyDiagnosticService : IDiagnosticService
    {
        public void TrackError(Exception exception, Dictionary<string, string> properties = null)
        {
            // Do nothing
        }
    }
}