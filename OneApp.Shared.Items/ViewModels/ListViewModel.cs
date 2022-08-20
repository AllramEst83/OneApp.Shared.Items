using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{
    [QueryProperty("ListId", "ListId")]
    public partial class ListViewModel : ObservableObject
    {
        IConnectivity connectivity;

        [ObservableProperty]
        int listId;

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

        public ListViewModel(IConnectivity connectivity)
        {
            var data = new ObservableCollection<ListItemModel>() {
                new ListItemModel() { ListId = 1, ListItemName = "Gurka", ListItemId = 1, IsChecked = true , Category = "Grönsaker"},
                new ListItemModel() { ListId = 2, ListItemName = "Sallad", ListItemId = 2 , IsChecked = true, Category = "Grönsaker"},
                new ListItemModel() { ListId = 2, ListItemName = "Tomater", ListItemId = 3 , IsChecked = false, Category = "Röda grejer"},
                new ListItemModel() { ListId = 4, ListItemName = "Pasta", ListItemId = 4 , IsChecked = false, Category = "Kolhydrater"},
            };

            //Database query
            var checkedItems = data.Where(x => x.ListId == ListId && x.IsChecked == false);
            var uncheckedItems = data.Where(x => x.ListId == ListId && x.IsChecked == true);

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
            this.connectivity = connectivity;

            ShowHideLists();
        }

        [RelayCommand]
        async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                return;
            }

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }

            ListItemModel listItemModel = new() { IsChecked = false, ListItemName = Text, ListId = ListId };

            //Save to DB

            Items.Add(listItemModel);

            Text = string.Empty;

            ShowHideLists();
        }

        [RelayCommand]
        void Delete(ListItemModel item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        [RelayCommand]
        async Task Check(int checkedListItemId)
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }

            var unCheckedItem = Items.FirstOrDefault(x => x.ListItemId == checkedListItemId);
            if (unCheckedItem is not null)
            {
                CheckedItems.Add(unCheckedItem);
                Items.Remove(unCheckedItem);

                ShowHideLists();
            }
        }

        [RelayCommand]
        async Task Uncheck(int uncheckedListItemId)
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }

            var unCheckedItem = CheckedItems.FirstOrDefault(x => x.ListItemId == uncheckedListItemId);
            if (unCheckedItem is not null)
            {
                Items.Add(unCheckedItem);
                CheckedItems.Remove(unCheckedItem);

                //Save To DB

                ShowHideLists();
            }
        }

        [RelayCommand]
        async Task RemoveAll()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }

            if (CheckedItems.Count > 0)
            {
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

        private int listId;

        public int ListId
        {
            get => listId;
            set => SetProperty(ref listId, value);
        }
    }
}
