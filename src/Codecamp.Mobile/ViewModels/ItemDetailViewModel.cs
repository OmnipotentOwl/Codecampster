using Codecamp.Mobile.Clients.Portable.Models;

namespace Codecamp.Mobile.Clients.Portable.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
