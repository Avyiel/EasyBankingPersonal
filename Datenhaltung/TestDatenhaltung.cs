// Program_TEST_Datenhaltung.cs (zu Hausaufgabe 19S)
// Test des Namensraums 'Datenhaltung'
// Konsolenanwendung

/*
 * WICHTIG:
 * Die Datenbank 'Bank.accdb' muss im selben Ordner stehen
 * wie die ausführbare Datei, oder die Konstante '_LOCATION'
 * muss angepasst werden.
 * 
 */

// nächste Zeile auskommentieren für reinen Fehlerzähler ohne Ausnahme
#define EXCEPTION

using System;
using System.Linq;
using System.Reflection;
using EasyBankingPersonal;
using EasyBankingPersonal.Datenaustausch;
using EasyBankingPersonal.Datenhaltung;

namespace EasyBankingBackOffice.TestDatenhaltung.Ablauf
{

    class Program
    {
        /// <summary>
        /// Verzeichnis, in dem die Datenbank steht
        /// </summary>
        private const string _LOCATION = @".\";

        /// <summary>
        /// Name der Datenbank
        /// </summary>
        private const string _DBNAME = "Bank.accdb";

        /// <summary>
        /// Anzahl der gefundenen Fehler
        /// </summary>
        private static int _errors = 0;

        /// <summary>
        /// Farbe für fehlerhafte Ausgaben
        /// </summary>
        private const ConsoleColor _bad_color = ConsoleColor.DarkRed;

        /// <summary>
        /// Farbe für korrekte Ausgaben
        /// </summary>
        private const ConsoleColor _good_color = ConsoleColor.DarkGreen;

        /// <summary>
        /// farbiger Zeilenausdruck auf Konsole
        /// </summary>
        /// <param name="text">auszugebender Text</param>
        /// <param name="color">Hintergrundfarbe des Textes</param>
        private static void ColorPrint(string text, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.BackgroundColor = ConsoleColor.Black;
            if (color == _bad_color)
            {
                _errors++;
#if EXCEPTION
                throw new Exception();
#endif
            }
        }

        /// <summary>
        /// private Methode zum Equals-Vergleich zweier Objekte mit farblicher Rückmeldung
        /// </summary>
        /// <param name="obj">erstes Objekt</param>
        /// <param name="comp">zweites Objekt</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung</param>
        private static void CompareAndPrint(object obj, object comp, ConsoleColor trueColor = _good_color, ConsoleColor falseColor = _bad_color)
        {
            ColorPrint(obj.ToString(), obj.Equals(comp) ? trueColor : falseColor);
        }

        /// <summary>
        /// private Methode zum Vergleich zweier double-Zahlen
        /// </summary>
        /// <param name="d1">erste Zahl</param>
        /// <param name="d2">zweite Zahl</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung</param>
        /// <remarks>erlaubte Abweichung muss kleiner 10^-10 sein</remarks>
        private static void CompareAndPrintDouble(double d1, double d2, ConsoleColor trueColor = _good_color, ConsoleColor falseColor = _bad_color, double tolerance = 1E-10)
        {
            ColorPrint(d1.ToString(), Math.Abs(d1 - d2) < tolerance ? trueColor : falseColor);
        }

        /// <summary>
        /// private Methode zur Prüfung auf null
        /// </summary>
        /// <param name="obj">zu prüfende Referenz</param>
        /// <param name="trueColor">Farbe bei Übereinstimmung (Referenz ist null)</param>
        /// <param name="falseColor">Farbe bei Nichtübereinstimmung (Referenz ungleich null)</param>
        private static void IsNull(object obj, ConsoleColor trueColor = _good_color, ConsoleColor falseColor = _bad_color)
        {
            if (obj == null)
                ColorPrint("NULL", trueColor);
            else
                ColorPrint(obj.ToString(), falseColor);
        }

        /// <summary>
        /// Test der Plausibilitätsprüfungen
        /// </summary>
        /// <param name="action">Aktion, die eine Ausnahme auslösen soll</param>
        private static void ProvokeException(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                ColorPrint("Exception OK", _good_color);
                return;
            }
            ColorPrint("Exception FAIL", _bad_color);
        }

        /// <summary>
        /// Klasse für eine Zeile der Vorgabetabellen
        /// </summary>
        /// <typeparam name="T">Typ der Vorgabe</typeparam>
        private class VorgabeZeile<T>
        {
            /// <summary>
            /// Nummer der Periode (in den Austauschobjekten i.d.R. nicht enthalten)
            /// </summary>
            public int PeriodeID { get; }
            public T Zeile { get; }

            public VorgabeZeile(int periodeID, T zeile)
            {
                PeriodeID = periodeID;
                Zeile = zeile;
            }

            public void Vergleichen(T vergleichsZeile)
            {
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    MethodInfo mi = pi.GetGetMethod();
                    Console.Write(pi.Name + ": ");
                    CompareAndPrint(mi.Invoke(Zeile, null), mi.Invoke(vergleichsZeile, null));
                }
            }
        }

        /// <summary>
        /// Delegatentyp für Methoden, die aus der Datenbank Zeilen abrufen
        /// </summary>
        /// <typeparam name="T">Typ des Austauschobjekts der Zeile</typeparam>
        /// <param name="periodenID">Nummer der Periode der Zeile</param>
        /// <returns>abgerufene Tabellenzeile</returns>
        private delegate T AbgerufeneZeile<T>(int periodenID);

        /// <summary>
        /// Methode zum Abgleich der Vorgaben mit den Werten aus der Datenbank
        /// </summary>
        /// <typeparam name="T">Typ des Austauschobjektes</typeparam>
        /// <param name="vorgaben">Array mit Vorgabezeilen</param>
        /// <param name="zeileAbrufen">zum Typ korrespondierende Methode zum Abruf einer Zeile aus der Datenbank</param>
        private static void TestDurchführen<T>(VorgabeZeile<T>[] vorgaben, AbgerufeneZeile<T> zeileAbrufen)
        {
            int maxID = -1;
            object dummy;

            Console.WriteLine("\n- " + typeof(T).Name + " -\n");
            foreach (VorgabeZeile<T> vorgabeZeile in vorgaben)
            {
                int periodeID = vorgabeZeile.PeriodeID;
                if (periodeID > maxID)
                    maxID = periodeID;
                Console.WriteLine(periodeID.ToString());
                T datenZeile = zeileAbrufen(periodeID);
                vorgabeZeile.Vergleichen(datenZeile);
            }
            ProvokeException(() => dummy = zeileAbrufen(0));
            ProvokeException(() => dummy = zeileAbrufen(maxID + 1));
        }

        /// <summary>
        /// Hauptmethode
        /// </summary>
        static void Main()
        {

            Console.WriteLine("--- Laden der Datenbank");

            // Test der Ausnahmen vor Laden der Datenbank
            object dummy;
            CompareAndPrint(Datenbank.IstGeladen, false);
            ProvokeException(() => dummy = Datenbank.PeriodenIDs);
            ProvokeException(() => dummy = Datenbank.VolumenProMitarbeiter);
            ProvokeException(() => dummy = Datenbank.Durchschnittsgrößen(1));
            ProvokeException(() => dummy = Datenbank.Personalkosten(1));
            ProvokeException(() => dummy = Datenbank.Mitarbeiter(1));
            ProvokeException(() => dummy = Datenbank.Periode(1));
            ProvokeException(() => dummy = Datenbank.VolumenNeugeschäft(1));

            // Test der Plausibilitätsprüfungen
            ProvokeException(() => Datenbank.DatenbankAuslesen(null));
            ProvokeException(() => Datenbank.DatenbankAuslesen(" "));

            // Test, ob der Pfad auch wirklich genutzt wird
            ProvokeException(() => Datenbank.DatenbankAuslesen("AA:cf4rfnu"));

            // Datenbank laden
            Datenbank.DatenbankAuslesen(_LOCATION + _DBNAME);


            Console.WriteLine("--- Testen der Eigenschaften");

            Console.WriteLine("IstGeladen");
            CompareAndPrint(Datenbank.IstGeladen, true);
            IsNull(typeof(Datenbank).GetProperty("IstGeladen").GetSetMethod());

            Console.WriteLine("PeriodenIDs");
            CompareAndPrint(Enumerable.SequenceEqual(Datenbank.PeriodenIDs, new int[] { 1, 2, 3, 4, 5 }), true);
            IsNull(typeof(Datenbank).GetProperty("PeriodenIDs").GetSetMethod());
            Datenbank.PeriodenIDs[2] = 222;
            CompareAndPrint(Datenbank.PeriodenIDs[2], 3);

            Console.WriteLine("VolumenProMitarbeiter");
            VolumenProMitarbeiter volumenProMitarbeiter = Datenbank.VolumenProMitarbeiter;
            CompareAndPrint(volumenProMitarbeiter.Konsumkredite, 200);
            CompareAndPrint(volumenProMitarbeiter.Autokredite, 150);
            CompareAndPrint(volumenProMitarbeiter.Hypothekenkredite, 50);
            CompareAndPrint(volumenProMitarbeiter.Girokonten, 900);
            CompareAndPrint(volumenProMitarbeiter.Spareinlagen, 1200);
            CompareAndPrint(volumenProMitarbeiter.Termingelder, 1200);
            IsNull(typeof(Datenbank).GetProperty("VolumenProMitarbeiter").GetSetMethod());


            Console.WriteLine("--- Testen der Methoden");

            decimal f = 1000.0M;
            VorgabeZeile<Durchschnittsgrößen>[] vorgabeDurchschnittsgrößen = new VorgabeZeile<Durchschnittsgrößen>[]
            {
                new VorgabeZeile<Durchschnittsgrößen>(1, new Durchschnittsgrößen(29 * f, 38 * f, 80 * f, 6 * f, 9 * f, 18 * f)),
                new VorgabeZeile<Durchschnittsgrößen>(2, new Durchschnittsgrößen(29 * f, 38 * f, 85 * f, 6 * f, 9 * f, 18 * f)),
                new VorgabeZeile<Durchschnittsgrößen>(3, new Durchschnittsgrößen(30 * f, 40 * f, 90 * f, 6 * f, 10 * f, 20 * f)),
                new VorgabeZeile<Durchschnittsgrößen>(4, new Durchschnittsgrößen(30 * f, 40 * f, 95 * f, 6 * f, 10 * f, 20 * f)),
                new VorgabeZeile<Durchschnittsgrößen>(5, new Durchschnittsgrößen(30 * f, 40 * f, 100 * f, 6 * f, 10 * f, 20 * f))
            };
            TestDurchführen<Durchschnittsgrößen>(vorgabeDurchschnittsgrößen, Datenbank.Durchschnittsgrößen);

            VorgabeZeile<Mitarbeiter>[] vorgabeMitarbeiter = new VorgabeZeile<Mitarbeiter>[]
            {
                new VorgabeZeile<Mitarbeiter>(1, new Mitarbeiter(550,500,410,0.095,5)),
                new VorgabeZeile<Mitarbeiter>(2, new Mitarbeiter(560,510,400,0.10,5)),
                new VorgabeZeile<Mitarbeiter>(3, new Mitarbeiter(560,505,405,0.09,5)),
                new VorgabeZeile<Mitarbeiter>(4, new Mitarbeiter(550,500,410,0.095,5)),
                new VorgabeZeile<Mitarbeiter>(5, new Mitarbeiter(560,510,400,0.10,5))
            };
            TestDurchführen<Mitarbeiter>(vorgabeMitarbeiter, Datenbank.Mitarbeiter);

            VorgabeZeile<Periode>[] vorgabePeriode = new VorgabeZeile<Periode>[]
            {
                new VorgabeZeile<Periode>(1, new Periode(1, new DateTime(2014, 1, 1), new DateTime(2014, 12, 31))),
                new VorgabeZeile<Periode>(2, new Periode(2, new DateTime(2015, 1, 1), new DateTime(2015, 12, 31))),
                new VorgabeZeile<Periode>(3, new Periode(3, new DateTime(2016, 1, 1), new DateTime(2016, 12, 31))),
                new VorgabeZeile<Periode>(4, new Periode(4, new DateTime(2017, 1, 1), new DateTime(2017, 12, 31))),
                new VorgabeZeile<Periode>(5, new Periode(5, new DateTime(2018, 1, 1), new DateTime(2018, 12, 31)))
            };
            TestDurchführen<Periode>(vorgabePeriode, Datenbank.Periode);

            VorgabeZeile<Personalkosten>[] vorgabePersonalkosten = new VorgabeZeile<Personalkosten>[]
            {
                new VorgabeZeile<Personalkosten>(1, new Personalkosten(34000.00M, 9100.00M, 17500.00M, 470.00M)),
                new VorgabeZeile<Personalkosten>(2, new Personalkosten(35000.00M, 9400.00M, 18000.00M, 480.00M)),
                new VorgabeZeile<Personalkosten>(3, new Personalkosten(36000.00M, 9700.00M, 18500.00M, 490.00M)),
                new VorgabeZeile<Personalkosten>(4, new Personalkosten(37000.00M, 10000.00M, 19000.00M, 500.00M)),
                new VorgabeZeile<Personalkosten>(5, new Personalkosten(38000.00M, 10300.00M, 19500.00M, 510.00M))
            };
            TestDurchführen<Personalkosten>(vorgabePersonalkosten, Datenbank.Personalkosten);

            f = 10000000.0M;
            VorgabeZeile<VolumenNeugeschäft>[] vorgabeVolumenNeugeschäft = new VorgabeZeile<VolumenNeugeschäft>[]
            {
                new VorgabeZeile<VolumenNeugeschäft>(1, new VolumenNeugeschäft(135 * f, 100 * f, 90 * f, 130 * f, 220 * f, 110 * f)),
                new VorgabeZeile<VolumenNeugeschäft>(2, new VolumenNeugeschäft(150 * f, 108 * f, 100 * f, 135 * f, 240 * f, 120 * f)),
                new VorgabeZeile<VolumenNeugeschäft>(3, new VolumenNeugeschäft(140 * f, 110 * f, 95 * f, 140 * f, 222 * f, 115 * f)),
                new VorgabeZeile<VolumenNeugeschäft>(4, new VolumenNeugeschäft(135 * f, 100 * f, 90 * f, 130 * f, 220 * f, 110 * f)),
                new VorgabeZeile<VolumenNeugeschäft>(5, new VolumenNeugeschäft(150 * f, 108 * f, 100 * f, 135 * f, 240 * f, 120 * f))
            };
            TestDurchführen<VolumenNeugeschäft>(vorgabeVolumenNeugeschäft, Datenbank.VolumenNeugeschäft);


            Console.WriteLine("\n\n--- ERGEBNIS ---\n");

            if (_errors > 0)
                ColorPrint("Fehler: " + _errors.ToString(), _bad_color);
            else
                ColorPrint("keine Fehler", _good_color);

            Console.ReadKey();
        }
    }
}
