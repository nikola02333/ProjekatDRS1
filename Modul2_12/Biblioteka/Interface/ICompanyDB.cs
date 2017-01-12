using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Access
{
    [ServiceContract]
    public interface ICompanyDB
    {
        [OperationContract]
        bool AddAction(Osoba action);

        [OperationContract]
        bool ReplaceAction(Osoba zaposleni, Osoba izmenjeni);

        [OperationContract]
        bool PopunjavanjeTabele();

        [OperationContract]
        List<Osoba> Read();

        [OperationContract]
        bool AddActionTim(Tim action);

      

        
        [OperationContract]
        bool AddActionCompany(Kompanija action);

        [OperationContract]
        bool PopunjavanjeKompanija();

        [OperationContract]
        List<Kompanija> ReadCompany();

        [OperationContract]
        bool ReplaceActionCompany(Kompanija kompanija);
		////////////////////////////////////////////////////
		[OperationContract]
		bool ActionsCompanyOut(KompanijaOUT action);

		[OperationContract]
        bool PopunjavanjeBaze();  // Outsorsing kopmanije
		
		[OperationContract]
		List<KompanijaOUT> ReadCompanyOutsorsing();
        ///////////////////////////////////////////////
        [OperationContract]
        bool AddActionProject(Projekat action);

        [OperationContract]
        List<Projekat> ReadProjects();





    }
}
