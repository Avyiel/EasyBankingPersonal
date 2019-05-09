using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Object reference for one row of the "Mitarbeiter" table.
    /// </summary>
    public class Mitarbeiter
    {
        /// <summary>
        /// Column "Anlageberater"
        /// </summary>
        public int Anlageberater { get; }

        /// <summary>
        /// Column "Backofficemitarbeiter"
        /// </summary>
        public int Backofficemitarbeiter { get; }

        /// <summary>
        /// Column "Fluktuation"
        /// </summary>
        public Prozent Fluktuation { get; }

        /// <summary>
        /// Sum of <code>Anlageberater</code> and <code>Kreditberater</code>
        /// </summary>
        public int Frontofficemitarbeiter { get; }

        /// <summary>
        /// Sum of <code>Frontofficemitarbeiter</code> and 
        /// <code>Backofficemitarbeiter</code>
        /// </summary>
        public int Insgesamt { get; }

        /// <summary>
        /// Column "Kreditberater"
        /// </summary>
        public int Kreditberater { get; }

        /// <summary>
        /// Column "TrainingstageProMitarbeiter"
        /// </summary>
        public int TrainingstageProMitarbeiter { get; }

        /// <summary>
        /// Default constructor. Negative values are not accepted,
        /// and <code>fluktuation</code> must be between 0 and 100 inclusive.
        /// </summary>
        /// <param name="kreditberater"></param>
        /// <param name="anlagenberater"></param>
        /// <param name="backofficemitarbeiter"></param>
        /// <param name="fluktuation"></param>
        /// <param name="trainingstageProMitarbeiter"></param>
        public Mitarbeiter(
            int kreditberater,
            int anlagenberater,
            int backofficemitarbeiter,
            double fluktuation,
            int trainingstageProMitarbeiter
        )
        {
            if (
                kreditberater < 0 ||
                anlagenberater < 0 ||
                backofficemitarbeiter < 0 ||
                trainingstageProMitarbeiter < 0 ||
                fluktuation < 0d ||
                fluktuation > 1d
            )
            {
                throw new Exception();
            }

            Kreditberater = kreditberater;
            Anlageberater = anlagenberater;
            Backofficemitarbeiter = backofficemitarbeiter;
            Fluktuation = fluktuation;
            TrainingstageProMitarbeiter = trainingstageProMitarbeiter;
            Frontofficemitarbeiter = kreditberater + anlagenberater;
            Insgesamt = Frontofficemitarbeiter + Backofficemitarbeiter;
        }
    }
}
