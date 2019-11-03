using System;
using System.Net;
using Codecamp.Mobile.Clients.Abstractions.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Codecamp.Mobile.Clients.Portable.Services.OpenUri
{
    public class OpenUriService : IOpenUriService
    {
        private readonly IDiagnosticService _diagnosticService;

        public OpenUriService(IDiagnosticService diagnosticService)
        {
            _diagnosticService = diagnosticService;
        }

        public void OpenUri(string uri) => Device.OpenUri(new Uri(uri));

        public void OpenPhoneUri(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                _diagnosticService.TrackError(anEx);
            }
            catch (FeatureNotSupportedException ex)
            {
                _diagnosticService.TrackError(ex);
            }
            catch (Exception ex)
            {
                _diagnosticService.TrackError(ex);
            }
        }

        public void OpenMapUri(string address)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Device.OpenUri(
                        new Uri($"http://maps.apple.com/?q={WebUtility.UrlEncode(address)}"));
                    break;
                case Device.Android:
                    Device.OpenUri(
                        new Uri($"geo:0,0?q={WebUtility.UrlEncode(address)}"));
                    break;
                case Device.UWP:
                    break;
            }
        }
    }
}