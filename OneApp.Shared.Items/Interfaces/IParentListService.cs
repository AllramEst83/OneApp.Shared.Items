﻿using OneApp.Shared.Items.ViewModels;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IParentListService
    {
        List<ListModel> GetParentLists();
        void SaveParentList(ListModel parenList);
        void UpdateParentList(Guid parentListId, string parentListName);
        void DeleteParentList(Guid parentListId);
    }
}
