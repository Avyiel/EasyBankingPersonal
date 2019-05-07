using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// This structure encapsulates a decimal value as a percentage.
    /// </summary>
    public struct Prozent
    {
        
        public double Wert { get; }

        public Prozent(double wert)
        {
            Wert = Round(wert);
        }

        public Prozent(double neuerWert, double alterWert)
        {
            Wert = Round((neuerWert - alterWert) / alterWert);
        }

        /// <summary>
        /// Calculates the hash value for a given percentual value.
        /// </summary>
        /// <returns>Hash value as an Int32</returns>
        public override int GetHashCode()
        {
            return this.Wert.GetHashCode();
        }

        /// <summary>
        /// Implicit conversion from double to Prozent.
        /// </summary>
        /// <param name="op">Double to be converted.</param>
        public static implicit operator Prozent(double op)
        {
            return new Prozent(op);
        }

        /// <summary>
        /// Implicit conversion from Prozent to double.
        /// </summary>
        /// <param name="op">Prozent to be converted.</param>
        public static implicit operator double(Prozent op)
        {
            return op.Wert;
        }

        /// <summary>
        /// Compares a percentual value with a given object through hash codes.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        /// <summary>
        /// Printable representation of the percentual value.
        /// </summary>
        /// <returns>Percentage with 1 decimal place in the de_DE locale.</returns>
        public override string ToString()
        {
            return this.Wert.ToString("P1", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        /// <summary>
        /// Business rounding, uses <code>AwayFromZero</code> internally.
        /// </summary>
        /// <param name="val">Value to be rounded.</param>
        /// <returns></returns>
        private static double Round(double val)
        {
            return Math.Round(val, 3, MidpointRounding.AwayFromZero);
        }
    }

    public struct Währung
    {
    }

    class DataExchange
    {
    }
}
