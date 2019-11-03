using Codecamp.Mobile.DataObjects.Graph;
using System.Threading.Tasks;

namespace Codecamp.Mobile.Services.Abstractions.Azure.Graph
{
    public interface IGraphUserService
    {
        Task<GraphUserInfo> GetCurrentUserInfoAsync();
        Task<GraphUserInfo> GetUserInfoAsync(string userId);
    }
}