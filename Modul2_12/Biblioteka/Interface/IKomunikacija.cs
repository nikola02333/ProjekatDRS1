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
    public interface IKomunikacija
    {
        [OperationContract]
        void PartnerskiZahtev(string hiring, string outsourcing);

        [OperationContract]
        void OdgovorNaPartnerstvo(bool potvrda, string hiring, string outsourcing);

        [OperationContract]
        void ZahtevProjekat(Projekat projekat, string outsourcing);

        [OperationContract]
        void OdgovorNaProjekat(bool potvrda, Projekat projekat, string outsourcing);
    }
}
