using Android.Content.Res;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OneApp.Shared.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ListInfoViewmodel(IParentListService parentListService)
        {
            this.parentListService = parentListService;
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
    }
}
