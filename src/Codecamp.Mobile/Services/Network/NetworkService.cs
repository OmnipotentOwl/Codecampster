using System.Linq;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Xamarin.Essentials;

namespace Codecamp.Mobile.Clients.Portable.Services.Network
{
    public class NetworkService : INetworkService
    {
        private NetworkAccess access;
        private ConnectionProfile profile;
        public NetworkService()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        public bool ConnectionStatus()
        {
            if (access == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            access = e.NetworkAccess;
            var profiles = e.ConnectionProfiles;

            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                profile = ConnectionProfile.WiFi;
            }
        }
    }
}