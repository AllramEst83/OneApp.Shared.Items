using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneApp.Shared.Items.Helpers;
using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.Views;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{
    [QueryProperty("ReloadData", "ReloadData")]
    public partial class MainViewModel : ObservableObject
    {
        private bool reloadData;
        public bool ReloadData
        {
            get => reloadData;
            set
            {
                SetProperty(ref reloadData, value);

                if (ReloadData is true)
                {
                    LoadData();
                }
            }
        }
        IConnectivity connectivity;
        IParentListService parentListService;

        [ObservableProperty]
        ObservableCollection<ListModel> listNames;

        public MainViewModel(IConnectivity connectivity, IParentListService parentListService)
        {


            this.parentListService = parentListService;
            this.connectivity = connectivity;

            LoadData();
        }

        [RelayCommand]
        async Task AddNewList()
        {             
            await CheckConnectivity();

            string newListName = await Shell.Current.DisplayPromptAsync("New list", "Enter new list name.");
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

        [RelayCommand]
        public async Task GoToListInfo(ListModel list)
        {
            await CheckConnectivity();

            await Shell.Current.GoToAsync($"{nameof(ListInfoPage)}",
                new Dictionary<string, object>
                {
                    ["List"] = list
                });
        }

        private async Task CheckConnectivity()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
        }

        private void LoadData()
        {
            ListNames = new ObservableCollection<ListModel>();
            var parentLists = parentListService.GetParentLists();
            foreach (var item in parentLists)
            {
                ListNames.Add(item);
            }
        }
    }
}
