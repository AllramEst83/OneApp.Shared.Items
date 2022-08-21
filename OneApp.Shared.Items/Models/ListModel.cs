using CommunityToolkit.Mvvm.ComponentModel;

namespace OneApp.Shared.Items.Models
{
    public partial class ListModel : ObservableObject
    {
        private Guid id;

        public Guid Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string listName;

        public string ListName
        {
            get => listName;
            set => SetProperty(ref listName, value);
        }
    }
}
