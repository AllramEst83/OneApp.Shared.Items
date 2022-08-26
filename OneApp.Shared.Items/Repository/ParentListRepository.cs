using OneApp.Shared.Items.Helpers;
using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.ViewModels;
using System.Text;
using System.Text.Json;

namespace OneApp.Shared.Items.Repository
{
    public class ParentListRepository : IParentListRepository
    {
        
        public List<ListModel> GetAllParentLists(string filePath)
        {
            string rawData;
            try
            {
                rawData = File.ReadAllText(filePath);
            }
            catch (FileNotFoundException ex)
            {
                return new List<ListModel>();
            }

            if (string.IsNullOrWhiteSpace(rawData))
            {
                return new List<ListModel>();
            }

            List<ListModel> listitems = JsonSerializer.Deserialize<List<ListModel>>(rawData);

            return listitems;
        }

        public void SaveParentList(string filePath, ListModel newParentList)
        {        
            List<ListModel> existingData = GetAllParentLists(filePath);
            existingData.Add(newParentList);

            string dataToBeSaved = JsonSerializer.Serialize(existingData);
            File.WriteAllText(filePath, dataToBeSaved, Encoding.UTF8);
        }

        public void SaveListOfParentLists(string filePath, List<ListModel> parentList)
        {
            string dataToBeSaved = JsonSerializer.Serialize(parentList);
            File.WriteAllText(filePath, dataToBeSaved, Encoding.UTF8);
        }
    }
}
