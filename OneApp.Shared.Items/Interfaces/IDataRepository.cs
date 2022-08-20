using OneApp.Shared.Items.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Interfaces
{
    public interface IDataRepository
    {
        void SaveData(string fileName, List<ListItemModel> listOfListItemModels);
        List<ListItemModel> GetData(string fileName);
    }
}
