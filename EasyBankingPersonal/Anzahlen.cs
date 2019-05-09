using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Base class for storing numbers relating to the six banking products.
    /// </summary>
    public class Anzahlen
    {
        /// <summary>
        /// Number in terms of Autokredite
        /// </summary>
        public int Autokredite { get; }

        /// <summary>
        /// Number in terms of Girokonten
        /// </summary>
        public int Girokonten { get; }

        /// <summary>
        /// Number in terms of Hypothekenkredite
        /// </summary>
        public int Hypothekenkredite { get; }

        /// <summary>
        /// Number in terms of Konsumkredite
        /// </summary>
        public int Konsumkredite { get; }

        /// <summary>
        /// Number in terms of Spareinlagen
        /// </summary>
        public int Spareinlagen { get; }

        /// <summary>
        /// Number in terms of Termingelder
        /// </summary>
        public int Termingelder { get; }

        /// <summary>
        /// Default constructor. Negative values (exc. 0) are not accepted.
        /// </summary>
        /// <param name="konsumkredite">Number in terms of Konsumkredite</param>
        /// <param name="autokredite">Number in terms of Autokredite</param>
        /// <param name="hypothekenkredite">Number in terms of Hypothekenkredite</param>
        /// <param name="girokonten">Number in terms of Girokonten</param>
        /// <param name="spareinlagen">Number in terms of Spareinlagen</param>
        /// <param name="termingelder">Number in terms of Termingelder</param>
        public Anzahlen(
            int konsumkredite,
            int autokredite,
            int hypothekenkredite,
            int girokonten,
            int spareinlagen,
            int termingelder
        )
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

            Autokredite = autokredite;
            Girokonten = girokonten;
            Hypothekenkredite = hypothekenkredite;
            Konsumkredite = konsumkredite;
            Spareinlagen = spareinlagen;
            Termingelder = termingelder;
        }
    }
}
