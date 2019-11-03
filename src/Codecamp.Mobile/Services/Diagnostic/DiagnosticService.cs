﻿using System;
using System.Collections.Generic;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Microsoft.AppCenter.Crashes;

namespace Codecamp.Mobile.Clients.Portable.Services.Diagnostic
{
    public class DiagnosticService : IDiagnosticService
    {
        public void TrackError(Exception exception, Dictionary<string, string> properties = null) => Crashes.TrackError(exception, properties);
    }
}