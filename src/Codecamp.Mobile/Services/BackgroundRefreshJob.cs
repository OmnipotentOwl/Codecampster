using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Shiny.Jobs;

namespace Codecamp.Mobile.Clients.Portable.Services
{
    public class BackgroundRefreshJob : IJob
    {
        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to refresh items: {ex}");
                return false;
            }
            return true;
        }
    }
}
