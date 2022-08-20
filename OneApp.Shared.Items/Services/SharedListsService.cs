using OneApp.Shared.Items.Interfaces;
using OneApp.Shared.Items.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Services
{
    public class SharedListsService
    {
        private const string fileName = "OneApp_Shared_Lists";
        IDataRepository dataRepository;
        public SharedListsService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public void SaveList(List<ListItemModel> listToBeSaved)
        {
            var data = dataRepository.GetData(fileName);

        }
    }
}
