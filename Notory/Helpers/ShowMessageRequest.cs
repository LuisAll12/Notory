using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notory.Helpers
{
    public class ShowMessageRequest
    {
        public string Message { get; }

        public ShowMessageRequest(string message)
        {
            Message = message;
        }
    }
}
