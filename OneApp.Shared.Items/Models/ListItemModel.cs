using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Models
{
    public partial class ListItemModel : ObservableObject
    {
        private bool isChecked;

        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }

        private int listItemId;

        public int ListItemId
        {
            get => listItemId;
            set => SetProperty(ref listItemId, value);
        }

        private string listItemName;

        public string ListItemName
        {
            get => listItemName;
            set => SetProperty(ref listItemName, value);
        }

        private int listId;

        public int ListId
        {
            get => listId;
            set => SetProperty(ref listId, value);
        }
    }
}
