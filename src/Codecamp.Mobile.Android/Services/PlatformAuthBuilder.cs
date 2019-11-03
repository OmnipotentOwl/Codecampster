using Codecamp.Mobile.Services.Abstractions.Azure;
using Microsoft.Identity.Client;

namespace Codecamp.Mobile.Droid.Services
{
    public class PlatformAuthBuilder : IPlatformAuthBuilder
    {
        public IPublicClientApplication BuildPlatformClient()
        {
            return PublicClientApplicationBuilder.Create(GlobalSettings.ClientId)
                .WithRedirectUri($"msal{GlobalSettings.ClientId}://auth")
                .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                .WithParentActivityOrWindow(() => MainActivity.instance)
                .Build();
        }

        public object GetPlatformWindow()
        {
            return MainActivity.instance;
        }
    }
}