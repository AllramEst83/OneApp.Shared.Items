using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneApp.Shared.Items.Helpers;
using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.Services;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{
    [QueryProperty(nameof(ParentListName), nameof(ParentListName))]
    [QueryProperty(nameof(ListId), nameof(ListId))]
    public partial class ListViewModel : ObservableObject
    {

        private string listId;
        public string ListId
        {
            get => listId;
            set
            {
                SetProperty(ref listId, value);

                parentListGuid = ConvertHelpers.ConvertToGuid(ListId);

                InitiateList();
            }
        }

        [ObservableProperty]
        Guid parentListGuid;

        [ObservableProperty]
        ObservableCollection<ListItemModel> items;

        [ObservableProperty]
        ObservableCollection<ListItemModel> checkedItems;

        [ObservableProperty]
        bool noItemsTextIsVisible = false;

        [ObservableProperty]
        bool checkedListIsEmpty = false;

        [ObservableProperty]
        bool itemsListIsVisible = false;

        [ObservableProperty]
        bool showRemoveAllBtn = false;

        [ObservableProperty]
        string text;
        [ObservableProperty]
        string parentListName;

        IConnectivity connectivity;
        IListsItemService listsItemService;

        public ListViewModel(IConnectivity connectivity, IListsItemService listsItemService)
        {
            this.connectivity = connectivity;
            this.listsItemService = listsItemService;
        }

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return;
            }

            await CheckConnectivity();

            ListItemModel listItemModel = new() { ListItemId = Guid.NewGuid(), IsChecked = false, ListItemName = Text, ParentListId = parentListGuid };

            listsItemService.SaveListItem(listItemModel);

            Items.Add(listItemModel);

            Text = string.Empty;

            ShowHideLists();
        }

        [RelayCommand]
        void DeleteListItem(ListItemModel item)
        {
            if (Items.Contains(item))
            {
                listsItemService.DeleteListItemById(item.ListItemId);

                Items.Remove(item);
            }
        }

        [RelayCommand]
        async Task Check(Guid checkedListItemId)
        {
            await CheckConnectivity();

            ListItemModel unCheckedItem = Items.FirstOrDefault(x => x.ListItemId == checkedListItemId);
            if (unCheckedItem is not null)
            {
                listsItemService.CheckOrUnCheckItem(unCheckedItem.ListItemId, true);

                CheckedItems.Add(unCheckedItem);
                Items.Remove(unCheckedItem);

                ShowHideLists();
            }
        }

        [RelayCommand]
        async Task Uncheck(Guid uncheckedListItemId)
        {
            await CheckConnectivity();

            ListItemModel unCheckedItem = CheckedItems.FirstOrDefault(x => x.ListItemId == uncheckedListItemId);
            if (unCheckedItem is not null)
            {
                listsItemService.CheckOrUnCheckItem(unCheckedItem.ListItemId, false);
                Items.Add(unCheckedItem);
                CheckedItems.Remove(unCheckedItem);

                ShowHideLists();
            }
        }

        [RelayCommand]
        async Task RemoveAllCheck()
        {
            await CheckConnectivity();

            if (CheckedItems.Count > 0)
            {
                listsItemService.RemoveAllCheckedItems(parentListGuid);
                CheckedItems.Clear();
                CheckedListIsEmpty = true;
                ShowRemoveAllBtn = false;
            }
        }

        private void ShowHideLists()
        {
            bool containsItems = Items.Count.Equals(0);
            NoItemsTextIsVisible = containsItems;
            ItemsListIsVisible = !containsItems;

            bool checkedItemsContainsItems = CheckedItems.Count > 0;
            ShowRemoveAllBtn = checkedItemsContainsItems;
            CheckedListIsEmpty = !checkedItemsContainsItems;
        }

        private async Task CheckConnectivity()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
        }
        private void InitiateList()
        {
            List<ListItemModel> data = listsItemService.GetListById(parentListGuid);

            IEnumerable<ListItemModel> checkedItems = data.Where(x => x.ParentListId == parentListGuid && x.IsChecked == true);
            IEnumerable<ListItemModel> uncheckedItems = data.Where(x => x.ParentListId == parentListGuid && x.IsChecked == false);

            Items = new ObservableCollection<ListItemModel>();
            CheckedItems = new ObservableCollection<ListItemModel>();

            foreach (var item in uncheckedItems)
            {
                Items.Add(item);
            }

            foreach (var item in checkedItems)
            {
                CheckedItems.Add(item);
            }

            ShowHideLists();
        }
    }

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
