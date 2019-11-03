using Codecamp.Mobile.Clients.Abstractions.Services;
using Codecamp.Mobile.Clients.Portable.Services.Dialog;
using Codecamp.Mobile.Clients.Portable.Services.Network;
using Codecamp.Mobile.Clients.Portable.Services.Settings;
using Codecamp.Mobile.Clients.Portable.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Codecamp.Mobile.Clients.Portable
{
    public static class ViewModelDependencyInjection
    {
        public static IServiceCollection AddGlobalServices(this IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IDependencyService, Services.Dependency.DependencyService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<INetworkService, NetworkService>();
            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<SessionsViewModel>();
            services.AddTransient<SpeakerDetailsViewModel>();
            return services;
        }
    }
}
