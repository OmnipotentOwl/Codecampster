namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface IOpenUriService
    {
        void OpenUri(string uri);
        void OpenPhoneUri(string number);
        void OpenMapUri(string address);
    }
}