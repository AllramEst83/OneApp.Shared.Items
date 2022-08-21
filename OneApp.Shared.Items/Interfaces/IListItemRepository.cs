using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IListItemRepository
    {
        List<ListItemModel> GetListItems(string filePath);
        void SaveItemList(string filePath, List<ListItemModel> listItems);
    }
}
