using CommunityToolkit.Mvvm.ComponentModel;
using OneApp.Shared.Items.Helpers;

namespace OneApp.Shared.Items.ViewModels
{
    [QueryProperty(nameof(ListItemId), nameof(ListItemId))]
    public partial class EditListItemViewModel : ObservableObject
    {

        private string listItemId;
        public string ListItemId
        {
            get => listItemId;
            set
            {
                SetProperty(ref listItemId, value);

                Guid itemId = ConvertHelpers.ConvertToGuid(ListItemId);
            }
        }

        public EditListItemViewModel()
        {
                
        }
    }
}
