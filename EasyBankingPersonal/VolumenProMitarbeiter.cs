using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Object reference for one row of the "VolumenProMitarbeiter" table.
    /// </summary>
    public class VolumenProMitarbeiter : Anzahlen
    {
        /// <summary>
        /// Default constructor. Negative values (inc. 0) are not accepted.
        /// </summary>
        /// <param name="konsumkredite"> Column 'Konsumkredite'</param>
        /// <param name="autokredite">Column 'Autokredite'</param>
        /// <param name="hypothekenkredite">Column 'Hypothekenkredite'</param>
        /// <param name="girokonten">Column 'Girokonten'</param>
        /// <param name="spareinlagen">Column 'Spareinlagen'</param>
        /// <param name="termingelder">Column 'Termingelder'</param>
        public VolumenProMitarbeiter(
            int konsumkredite,
            int autokredite,
            int hypothekenkredite,
            int girokonten,
            int spareinlagen,
            int termingelder
        ) : base(konsumkredite, autokredite, hypothekenkredite, 
                 girokonten, spareinlagen, termingelder)
        {
            if (
                konsumkredite < 1 ||
                autokredite < 1 ||
                hypothekenkredite < 1 ||
                girokonten < 1 ||
                spareinlagen < 1 ||
                termingelder < 1
            )
            {
                throw new Exception();
            }
        }
    }
}
