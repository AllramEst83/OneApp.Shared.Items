using OneApp.Shared.Items.Helpers;
using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Services
{
    public class ListsItemService : IListsItemService
    {
        private const string fileName = "list_items.txt";
        IListItemRepository listItemRepository;
        public ListsItemService(IListItemRepository listItemRepository)
        {
            this.listItemRepository = listItemRepository;
        }

        public List<ListItemModel> GetListById(Guid listId)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            List<ListItemModel> data = listItemRepository.GetListItems(filePath);

            List<ListItemModel> listToReturn = data.Where(x => x.ParentListId == listId).ToList();

            return listToReturn;
        }

        public void RemoveAllCheckedItems(Guid parentListGuid)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            List<ListItemModel> data = listItemRepository.GetListItems(filePath);

            data.RemoveAll(x => x.ParentListId == parentListGuid && x.IsChecked == true);
            listItemRepository.SaveItemList(filePath, data);
        }

        public void CheckOrUnCheckItem(Guid itemId, bool checkValue)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            List<ListItemModel> data = listItemRepository.GetListItems(filePath);
            if (data is not null)
            {

                foreach (var item in data)
                {
                    if (item.ListItemId == itemId)
                    {
                        item.IsChecked = checkValue;
                    }
                }

                listItemRepository.SaveItemList(filePath, data);
            }
        }

        public void DeleteListItemById(Guid listItemId)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            List<ListItemModel> data = listItemRepository.GetListItems(filePath);
            if (data is not null)
            {
                data.RemoveAll(x => x.ListItemId == listItemId);

                listItemRepository.SaveItemList(filePath, data);
            }
        }

        public void SaveListItem(ListItemModel listItem)
        {
            if (listItem is not null)
            {
                string filePath = FileHelper.GetFilePath(fileName);
                List<ListItemModel> data = listItemRepository.GetListItems(filePath);
                if (data is not null)
                {
                    data.Add(listItem);
                }
                else
                {
                    data = new List<ListItemModel>
                    {
                        listItem
                    };
                }

                listItemRepository.SaveItemList(filePath, data);
            }
        }

        public void DeleteListItemsByParentId(Guid parentListId)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            List<ListItemModel> data = listItemRepository.GetListItems(filePath);

            data.RemoveAll(x => x.ParentListId == parentListId);

            listItemRepository.SaveItemList(filePath, data);
        }
    }
}
