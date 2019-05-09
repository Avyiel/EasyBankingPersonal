// ===============================
// AUTOR:    Lucas Vienna
// MATRIKEL: 64700
// ===============================
using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Object reference for one row of the "VolumenNeugeschäft" table.
    /// </summary>
    public class VolumenNeugeschäft : Produkte
    {
        /// <summary>
        /// Default constructor. Negative values (exc. 0) are not accepted.
        /// </summary>
        /// <param name="konsumkredite"> Column 'Konsumkredite'</param>
        /// <param name="autokredite">Column 'Autokredite'</param>
        /// <param name="hypothekenkredite">Column 'Hypothekenkredite'</param>
        /// <param name="girokonten">Column 'Girokonten'</param>
        /// <param name="spareinlagen">Column 'Spareinlagen'</param>
        /// <param name="termingelder">Column 'Termingelder'</param>
        public VolumenNeugeschäft(
            Währung konsumkredite,
            Währung autokredite,
            Währung hypothekenkredite,
            Währung girokonten,
            Währung spareinlagen,
            Währung termingelder) : base(konsumkredite, autokredite, hypothekenkredite,
                                         girokonten, spareinlagen, termingelder)
        {
            if (
                konsumkredite < 0 ||
                autokredite < 0 ||
                hypothekenkredite < 0 ||
                girokonten < 0 ||
                spareinlagen < 0 ||
                termingelder < 0
            )
            {
                throw new Exception();
            }
        }
    }
}
