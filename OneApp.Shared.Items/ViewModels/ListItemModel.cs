using CommunityToolkit.Mvvm.ComponentModel;

namespace OneApp.Shared.Items.ViewModels
{
    public partial class ListItemModel : ObservableObject
    {
        private string category;

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        private bool isChecked;

        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }

        private Guid listItemId;

        public Guid ListItemId
        {
            get => listItemId;
            set => SetProperty(ref listItemId, value);
        }

        private string listItemName;

        public string ListItemName
        {
            get => listItemName;
            set => SetProperty(ref listItemName, value);
        }

        private Guid parentListId;

        public Guid ParentListId
        {
            get => parentListId;
            set => SetProperty(ref parentListId, value);
        }
    }
}
