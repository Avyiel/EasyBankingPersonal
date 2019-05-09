using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Object reference for one row of the "Perioden" table.
    /// </summary>
    public class Periode
    {
        /// <summary>
        /// 'ID' column
        /// </summary>
        public int Nummer { get; }

        /// <summary>
        /// 'Beginn' column
        /// </summary>
        public DateTime Beginn { get; }

        /// <summary>
        /// 'Ende' column
        /// </summary>
        public DateTime Ende { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="nummer">'ID' column</param>
        /// <param name="beginn">'Beginn' column</param>
        /// <param name="ende">'Ende' column</param>
        public Periode(int nummer, DateTime beginn, DateTime ende)
        {
            if (nummer <= 0) throw new Exception("ID is not a positive number.");

            Nummer = nummer;
            Beginn = beginn;
            Ende = ende;
        }

        /// <summary>
        /// Represents the object as "ID: ##; Beginn: ##; Ende: ##".
        /// </summary>
        /// <returns>A string representing the object</returns>
        public override string ToString()
        {
            return $"ID: {Nummer}; Beginn: {Beginn.ToShortDateString()}; Ende: {Ende.ToShortDateString()}";
        }
    }
}
