using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneApp.Shared.Items.Interfaces;

namespace OneApp.Shared.Items.ViewModels
{
    [QueryProperty(nameof(List), nameof(List))]
    public partial class ListInfoViewmodel : ObservableObject
    {
        [ObservableProperty]
        ListModel list;

        [ObservableProperty]
        string newListName;

        IParentListService parentListService;
        IListsItemService listsItemService;

        public ListInfoViewmodel(IParentListService parentListService, IListsItemService listsItemService)
        {
            this.parentListService = parentListService;
            this.listsItemService = listsItemService;
        }

        [RelayCommand]
        public async Task UpdateList(Guid listId)
        {
            if (string.IsNullOrWhiteSpace(NewListName))
            {
                return;
            }

            parentListService.UpdateParentList(listId, newListName);

            NewListName = string.Empty;

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?ReloadData=true");
        }

        [RelayCommand]
        public async Task DeleteList(Guid listId)
        {
            listsItemService.DeleteListItemsByParentId(listId);

            parentListService.DeleteParentList(listId);

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?ReloadData=true");
        }
    }
}
