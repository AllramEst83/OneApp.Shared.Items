using OneApp.Shared.Items.Helpers;
using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.ViewModels;
using OneApp.Shared.Items.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Services
{
    public class ParentListService : IParentListService
    {
        IParentListRepository parentListRepository;
        private const string fileName = "parent_list.txt";

        public ParentListService(IParentListRepository parentListRepository)
        {
            this.parentListRepository = parentListRepository;
        }

        public List<ListModel> GetParentLists()
        {
            string filePath = FileHelper.GetFilePath(fileName);
            var parentLists = parentListRepository.GetAllParentLists(filePath);

            return parentLists;
        }

        public void SaveParentList(ListModel parenList)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            parentListRepository.SaveParentList(filePath, parenList);
        }

        public void UpdateParentList(Guid parentListId, string parentListName)
        {
            string filePath = FileHelper.GetFilePath(fileName);
            var listOfParentLists = parentListRepository.GetAllParentLists(filePath);

            var parentList = listOfParentLists.FirstOrDefault(x => x.Id == parentListId);
            if (parentList is not null)
            {
                parentList.ListName = parentListName;

                parentListRepository.SaveListOfParentLists(filePath, listOfParentLists);
            }
        }
    }
}
