using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{

    //Todo - Bryt ut upprepande kod
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        ObservableCollection<string> checkedItems;

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

        public MainViewModel(IConnectivity connectivity)
        {
            Items = new ObservableCollection<string>()
            {
                "Gurka",
                "Tomater",
                "Stövlar"
            };
            CheckedItems = new ObservableCollection<string>()  {
                "Gurka",
                "Tomater",
                "Stövlar"
            };
            this.connectivity = connectivity;

            bool containsItems = Items.Count.Equals(0);
            bool checkedItemsContainsItems = CheckedItems.Count > 0;

            NoItemsTextIsVisible = containsItems;
            ItemsListIsVisible = !containsItems;
            ShowRemoveAllBtn = checkedItemsContainsItems;
            CheckedListIsEmpty = !checkedItemsContainsItems;
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

            Items.Add(Text);

            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }

        [RelayCommand]
        void Check(string item)
        {
            var unCheckedItem = items.FirstOrDefault(x => x == item);
            if (unCheckedItem is not null)
            {
                checkedItems.Add(unCheckedItem);
                Items.Remove(unCheckedItem);

                bool containsItems = Items.Count.Equals(0);
                NoItemsTextIsVisible = containsItems;
                itemsListIsVisible = !containsItems;

                bool checkedItemsContainsItems = CheckedItems.Count > 0;
                ShowRemoveAllBtn = checkedItemsContainsItems;
                CheckedListIsEmpty = !checkedItemsContainsItems;
            }
        }

        [RelayCommand]
        void Uncheck(string checkedItem)
        {
            var unCheckedItem = checkedItems.FirstOrDefault(x => x == checkedItem);
            if (unCheckedItem is not null)
            {
                items.Add(unCheckedItem);
                CheckedItems.Remove(checkedItem);

                bool checkedItemsContainsItems = CheckedItems.Count > 0;
                ShowRemoveAllBtn = checkedItemsContainsItems;
                CheckedListIsEmpty = !checkedItemsContainsItems;
            }
        }

        [RelayCommand]
        void RemoveAll()
        {
            if (CheckedItems.Count > 0)
            {
                CheckedItems.Clear();
                CheckedListIsEmpty = true;
                ShowRemoveAllBtn = false;
            }
        }
    }
}
