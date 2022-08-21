using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IParentListService
    {
        List<ListModel> GetParentLists();
        void SaveParentList(ListModel parenList);
    }
}
