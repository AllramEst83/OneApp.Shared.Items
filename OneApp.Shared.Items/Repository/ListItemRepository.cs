using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Repository
{
    public class ListItemRepository : IListItemRepository
    {

        public void SaveItemList(string filePath, List<ListItemModel> listItems)
        {
                string dataToBeSaved = JsonSerializer.Serialize(listItems);
                File.WriteAllText(filePath, dataToBeSaved, Encoding.UTF8);
        }
        public List<ListItemModel> GetListItems(string filePath)
        {
            string rawData;
            try
            {
                rawData = File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (FileNotFoundException ex)
            {
                return new List<ListItemModel>();
            }

            //testa vad som händer om listan är to
            if (string.IsNullOrWhiteSpace(rawData))
            {
                return new List<ListItemModel>();
            }

            List<ListItemModel> listitems = JsonSerializer.Deserialize<List<ListItemModel>>(rawData);

            return listitems;
        }
    }
}
