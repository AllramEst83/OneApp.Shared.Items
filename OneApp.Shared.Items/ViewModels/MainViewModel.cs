using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace OneApp.Shared.Items.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;

        [ObservableProperty]
        ObservableCollection<ListModel> listNames;

        public MainViewModel(IConnectivity connectivity)
        {
            ListNames = new ObservableCollection<ListModel>()
            {
                new ListModel(){ Id = 4, ListName = "Matlista"},
                new ListModel(){ Id = 1, ListName = "Räkningar"},
                new ListModel(){ Id = 2, ListName = "Att köpa idag"}
            };
            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task AddNewList()
        {
            string newListName = await Shell.Current.DisplayPromptAsync("List name", "Add list name");
            if (string.IsNullOrWhiteSpace(newListName))
            {
                return;
            }

            //Save to DB

            ListNames.Add(new ListModel()
            {
                ListName = newListName
            });
        }

            [RelayCommand]
            async Task GoToList(int listId)
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                    return;
                }

                //Check If list exists before routing
                await Shell.Current.GoToAsync($"{nameof(ListPage)}?ListId={listId}");
            }
    }
    public partial class ListModel : ObservableObject
    {
        private int id;

        public int Id
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
