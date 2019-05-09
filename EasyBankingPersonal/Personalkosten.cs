using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Object reference for one row of the "Personalkosten" table.
    /// </summary>
    public class Personalkosten
    {
        /// <summary>
        /// Column 'Durchschnittsjahresgehalt'
        /// </summary>
        public Währung Durchschnittsjahresgehalt { get; }

        /// <summary>
        /// Column 'KostenProEinstellung'
        /// </summary>
        public Währung KostenProEinstellung { get; }

        /// <summary>
        /// Column 'KostenProEntlassung'
        /// </summary>
        public Währung KostenProEntlassung { get; }

        /// <summary>
        /// Column 'TrainingskostenProTagUndMitarbeiter'
        /// </summary>
        public Währung TrainingskostenProTagUndMitarbeiter { get; }

        /// <summary>
        /// Default constructor. Negative values (inc. 0) are not accepted.
        /// </summary>
        /// <param name="durchschnittsjahresgehalt">Column 'Durchschnittsjahresgehalt'</param>
        /// <param name="kostenProEinstellung">Column 'KostenProEinstellung'</param>
        /// <param name="kostenProEntlassung">Column 'KostenProEntlassung'</param>
        /// <param name="trainingskostenProTagUndMitarbeiter">Column 'TrainingskostenProTagUndMitarbeiter'</param>
        public Personalkosten(
            Währung durchschnittsjahresgehalt,
            Währung kostenProEinstellung,
            Währung kostenProEntlassung,
            Währung trainingskostenProTagUndMitarbeiter
        )
        {
            if (
                durchschnittsjahresgehalt < 0 ||
                kostenProEinstellung < 0 ||
                kostenProEntlassung < 0 ||
                trainingskostenProTagUndMitarbeiter < 0
            )
            {
                throw new Exception();
            }

            Durchschnittsjahresgehalt = durchschnittsjahresgehalt;
            KostenProEinstellung = kostenProEinstellung;
            KostenProEntlassung = kostenProEntlassung;
            TrainingskostenProTagUndMitarbeiter = trainingskostenProTagUndMitarbeiter;
        }
    }
}
