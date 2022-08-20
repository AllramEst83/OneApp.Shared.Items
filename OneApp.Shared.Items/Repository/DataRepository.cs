using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Repository
{
    public class DataRepository : IDataRepository
    {
       
        public void SaveData(string fileName, List<ListItemModel> listOfListItemModels)
        {
            string dataToBeSaved = JsonSerializer.Serialize(listOfListItemModels);

            File.WriteAllText(fileName, dataToBeSaved);
        }

        public List<ListItemModel> GetData(string fileName)
        {
            string rawData = File.ReadAllText(fileName);
            List<ListItemModel> listitems = JsonSerializer.Deserialize<List<ListItemModel>>(rawData);

            return listitems;
        }
    }
}
