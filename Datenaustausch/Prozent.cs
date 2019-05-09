// ===============================
// AUTOR:    Lucas Vienna
// MATRIKEL: 64700
// ===============================
using System;
using System.Globalization;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Percentual value, rounded to 1 decimal digit.
    /// </summary>
    public struct Prozent
    {
        /// <summary>
        /// Percentual value with one decimal digit.
        /// </summary>
        public double Wert { get; }

        /// <summary>
        /// Default constructor, treats a double as a percentage.
        /// </summary>
        /// <param name="wert">Value to be interpreted as a percentage.</param>
        public Prozent(double wert)
        {
            Wert = Math.Round(wert, 3, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Special constructor that instantiates a new <code>Prozent</code> based 
        /// on the difference between two values.
        /// </summary>
        /// <param name="neuerWert">New value.</param>
        /// <param name="alterWert">Old value.</param>
        public Prozent(double neuerWert, double alterWert)
        {
            Wert = Math.Round((neuerWert - alterWert) / alterWert, 3, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Implicit conversion from double to Prozent.
        /// </summary>
        /// <param name="op">Double to be converted.</param>
        public static implicit operator Prozent(double op) => new Prozent(op);

        /// <summary>
        /// Implicit conversion from Prozent to double.
        /// </summary>
        /// <param name="op">Prozent to be converted.</param>
        public static implicit operator double(Prozent op) => op.Wert;

        /// <summary>
        /// Calculates the hash value of the object.
        /// </summary>
        /// <returns>Hash value as an Int32</returns>
        public override int GetHashCode() => Wert.GetHashCode();

        /// <summary>
        /// Compares the current object with a given object through hash codes.
        /// </summary>
        /// <param name="obj">The object to be compared against.</param>
        /// <returns>A boolean value indicating sameness.</returns>
        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

        /// <summary>
        /// Printable representation of the percentual value.
        /// </summary>
        /// <returns>Percentage with 1 decimal digit in the de_DE locale.</returns>
        public override string ToString()
        {
            return Wert.ToString("P1", CultureInfo.CreateSpecificCulture("de-DE"));
        }
    }
}