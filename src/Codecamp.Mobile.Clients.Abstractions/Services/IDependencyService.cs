namespace Codecamp.Mobile.Clients.Abstractions.Services
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}