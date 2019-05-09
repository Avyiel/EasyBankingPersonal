using System;
using System.Globalization;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Currency value rounded to 2 decimal digits
    /// </summary>
    public struct Währung
    {
        /// <summary>
        /// Currency's amount in €.
        /// </summary>
        public decimal Betrag { get; }

        /// <summary>
        /// Default constructor, rounds the given decimal to 2 decimal digits.
        /// </summary>
        /// <param name="betrag">The decimal value to be treated as a currency.</param>
        public Währung(decimal betrag)
        {
            Betrag = Math.Round(betrag, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Implicit conversion from decimal to Währung.
        /// </summary>
        /// <param name="op">Decimal to be converted.</param>
        public static implicit operator Währung(decimal op) => new Währung(op);

        /// <summary>
        /// Implicit conversion from Währung to decimal.
        /// </summary>
        /// <param name="op">Währung to be converted.</param>
        public static implicit operator decimal(Währung op) => op.Betrag;

        /// <summary>
        /// Calculates the hash value of the object.
        /// </summary>
        /// <returns>Hash value as an Int32</returns>
        public override int GetHashCode() => Betrag.GetHashCode();

        /// <summary>
        /// Compares the current object with a given object through hash codes.
        /// </summary>
        /// <param name="obj">The object to be compared against.</param>
        /// <returns>A boolean value indicating sameness.</returns>
        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

        /// <summary>
        /// Printable representation of the currency's amount.
        /// </summary>
        /// <returns>Amount with 2 decimal digit in Euros.</returns>
        public override string ToString()
        {
            return Betrag.ToString("C2", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        /// <summary>
        /// Printable representation of the currency's amount as thousands.
        /// </summary>
        /// <returns>Integer amount in thousands of Euros.</returns>
        public string ToTausenderString()
        {
            return $"{Math.Round(Betrag / 1000, 0, MidpointRounding.AwayFromZero).ToString("N0", CultureInfo.CreateSpecificCulture("de-DE"))} T€";
        }

        /// <summary>
        /// Printable representation of the currency's amount as thousands.
        /// </summary>
        /// <returns>Integer amount in millions of Euros.</returns>
        public string ToMillionenString()
        {
            return $"{Math.Round(Betrag / 1000000, 0, MidpointRounding.AwayFromZero).ToString()} Mio€";
        }
    }
}
