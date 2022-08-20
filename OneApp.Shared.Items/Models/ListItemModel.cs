using CommunityToolkit.Mvvm.ComponentModel;

namespace OneApp.Shared.Items.Models
{
    public partial class ListItemModel : ObservableObject
    {
        private bool category;

        public bool Category
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

        private int listItemId;

        public int ListItemId
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

        private int parentListId;

        public int ParentListId
        {
            get => parentListId;
            set => SetProperty(ref parentListId, value);
        }
    }
}
