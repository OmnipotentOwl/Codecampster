using System.Threading.Tasks;

namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface IDialogService
    {
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task DisplayAlert(string title, string message, string cancel);
    }
}