// ===============================
// AUTOR:    Lucas Vienna
// MATRIKEL: 64700
// ===============================
using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Abstract base class for monetary values relating to 
    /// the six banking products.
    /// </summary>
    public abstract class Produkte
    {
        /// <summary>
        /// Monetary value for Autokredite
        /// </summary>
        public Währung Autokredite { get; }

        /// <summary>
        /// Monetary value for Girokonten
        /// </summary>
        public Währung Girokonten { get; }

        /// <summary>
        /// Monetary value for Hypothekenkredite
        /// </summary>
        public Währung Hypothekenkredite { get; }

        /// <summary>
        /// Monetary value for Konsumkredite
        /// </summary>
        public Währung Konsumkredite { get; }

        /// <summary>
        /// Monetary value for Spareinlagen
        /// </summary>
        public Währung Spareinlagen { get; }

        /// <summary>
        /// Monetary value for Termingelder
        /// </summary>
        public Währung Termingelder { get; }

        /// <summary>
        /// Default constructor. Negative values (exc. 0) are not accepted.
        /// </summary>
        /// <param name="konsumkredite">Monetary value for Konsumkredite</param>
        /// <param name="autokredite">Monetary value for Autokredite</param>
        /// <param name="hypothekenkredite">Monetary value for Hypothekenkredite</param>
        /// <param name="girokonten">Monetary value for Girokonten</param>
        /// <param name="spareinlagen">Monetary value for Spareinlagen</param>
        /// <param name="termingelder">Monetary value for Termingelder</param>
        public Produkte(
            Währung konsumkredite,
            Währung autokredite,
            Währung hypothekenkredite,
            Währung girokonten,
            Währung spareinlagen,
            Währung termingelder
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
