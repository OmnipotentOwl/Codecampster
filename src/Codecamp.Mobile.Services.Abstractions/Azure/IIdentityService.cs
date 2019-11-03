using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codecamp.Mobile.Services.Abstractions.Azure
{
    public interface IIdentityService
    {
        Task LogoutAsync();
        Task<string> LoginAsync();
        Task<string> GetTokenForScope(string[] scopes);
    }
}