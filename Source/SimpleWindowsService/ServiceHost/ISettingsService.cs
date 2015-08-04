using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWindowsService
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface ISettingsService
    {
        [OperationContract]
        string GetOutputMessage();
        [OperationContract]
        void SetOutputMessage(string outputMessage);
    }

}
