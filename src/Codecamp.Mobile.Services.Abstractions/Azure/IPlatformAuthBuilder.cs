using Microsoft.Identity.Client;

namespace Codecamp.Mobile.Services.Abstractions.Azure
{
    public interface IPlatformAuthBuilder
    {
        object GetPlatformWindow();
        IPublicClientApplication BuildPlatformClient();
    }
}