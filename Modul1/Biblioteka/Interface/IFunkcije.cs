using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Interface
{
    [ServiceContract]
    public interface IFunkcije
    {
        [OperationContract]
        int SlanjeLoga(string username, string pass); //

       
    }
}
