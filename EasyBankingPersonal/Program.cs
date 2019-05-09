using EasyBankingPersonal.Datenaustausch;
using EasyBankingPersonal.Datenhaltung;
using EasyBankingPersonal.Datenverarbeitung;

namespace EasyBankingPersonal
{
    class Program
    {
        static void Main()
        {
            DatenaustauschTest.Run();
            DatenhaltungTest.Run();
            DatenverarbeitungTest.Run();
        }
    }
}
