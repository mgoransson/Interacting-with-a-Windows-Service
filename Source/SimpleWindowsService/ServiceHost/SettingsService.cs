using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWindowsService
{
    public class SettingsService : ISettingsService
    {
        // Implement the ISettingsService methods.
        public string GetOutputMessage()
        {
            return Settings.OutputMessage;
        }

        public void SetOutputMessage(string outputMessage)
        {
            Settings.OutputMessage = outputMessage;
        }
    }
}
