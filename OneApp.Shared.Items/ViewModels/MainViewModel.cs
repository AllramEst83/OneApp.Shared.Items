using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneApp.Shared.Items.Interfaces;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        IParentListService parentListService;

        [ObservableProperty]
        ObservableCollection<ListModel> listNames;

        public MainViewModel(IConnectivity connectivity, IParentListService parentListService)
        {
            this.parentListService = parentListService;
            this.connectivity = connectivity;
            ListNames = new ObservableCollection<ListModel>();
            var parentLists = parentListService.GetParentLists();
            foreach (var item in parentLists)
            {
                ListNames.Add(item);
            }
        }

        [RelayCommand]
        async Task AddNewList()
        {
            await CheckConnectivity();

            string newListName = await Shell.Current.DisplayPromptAsync("List name", "Add list name");
            if (string.IsNullOrWhiteSpace(newListName))
            {
                return;
            }
            ListModel newList = new ListModel()
            {
                Id = Guid.NewGuid(),
                ListName = newListName
            };

            parentListService.SaveParentList(newList);

            ListNames.Add(newList);
        }

        [RelayCommand]
        async Task GoToList(ListModel listModel)
        {
            await CheckConnectivity();

            //Check If list exists before routing
            await Shell.Current.GoToAsync($"{nameof(ListPage)}?ListId={listModel.Id}&ParentListName={listModel.ListName}");
        }

        private async Task CheckConnectivity()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
        }
    }
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
