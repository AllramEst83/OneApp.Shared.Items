using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneApp.Shared.Items.Helpers
{
    public static class ConvertHelpers
    {
        public static Guid ConvertToGuid(string stringGuid)
        {
            return new Guid(stringGuid);
        }
    }
}
