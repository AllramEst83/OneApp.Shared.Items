using OneApp.Shared.Items.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IListsItemService
    {
        void SaveListItem(ListItemModel listItem);
        List<ListItemModel> GetListById(Guid listId);
        void RemoveAllCheckedItems();
        void CheckOrUnCheckItem(Guid itemId, bool checkValue);
    }
}
