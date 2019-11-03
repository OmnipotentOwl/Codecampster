namespace Codecamp.Mobile.Clients.Portable.ViewModels
{
    public class SpeakerDetailsViewModel: ViewModelBase
    {
        bool hasAdditionalSessions;
        public bool HasAdditionalSessions
        {
            get { return hasAdditionalSessions; }
            set { SetProperty(ref hasAdditionalSessions, value); }
        }
        private string sessionId;
    }
}
