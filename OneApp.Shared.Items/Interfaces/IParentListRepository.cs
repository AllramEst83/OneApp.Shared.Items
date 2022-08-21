using OneApp.Shared.Items.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IParentListRepository
    {
        List<ListModel> GetAllParentLists(string filePath);
        void SaveParentList(string filePath, ListModel newParentList);
    }
}
