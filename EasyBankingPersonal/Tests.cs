// Program_TEST_Datenaustausch.cs (zu Hausaufgabe 19S)
// Test des Namensraums 'Datenaustausch'
// Konsolenanwendung

// nächste Zeile auskommentieren für reinen Fehlerzähler ohne Ausnahme
#define EXCEPTION

using System;
using EasyBankingPersonal.Datenaustausch;

namespace EasyBankingPersonal.TestAustausch.Ablauf
{
    class Program
    {
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

        #region Hilfsmethoden
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
        private static void CompareAndPrintDouble(double d1, double d2, ConsoleColor trueColor = _good_color, ConsoleColor falseColor = _bad_color)
        {
            ColorPrint(d1.ToString(), Math.Abs(d1 - d2) < 1E-10 ? trueColor : falseColor);
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
        /// private Methode zum Test der überschriebenen Methoden ToString(), GetHashCode() und Equals()
        /// </summary>
        /// <param name="l">Liste von Objekten derselben Klasse</param>
        /// <remarks>Die ersten beiden Objekte der Liste müssen wertgleich sein, die übrigen wertverschieden von den ersten beiden.</remarks>
        private static void ObjectTest(object[] l)
        {
            Console.WriteLine("\n----------\n");

            foreach (object o in l)
                Console.WriteLine(o.ToString());

            Console.WriteLine();

            int compHashCode = l[0].GetHashCode();
            Console.WriteLine(compHashCode);
            CompareAndPrint(l[1].GetHashCode(), compHashCode, _good_color, _bad_color);
            for (int i = 2; i < l.Length; i++)
                CompareAndPrint(l[i].GetHashCode(), compHashCode, _bad_color, _good_color);

            Console.WriteLine();

            CompareAndPrint(l[1], l[0], _good_color, _bad_color);
            for (int i = 2; i < l.Length; i++)
                CompareAndPrint(l[i], l[0], _bad_color, _good_color);
            CompareAndPrint(l[0], new Object(), _bad_color, _good_color);
        }

        /// <summary>
        /// private Methode zum Testen der Methode CompareTo()
        /// </summary>
        /// <param name='l'>
        /// sortierte Liste von Objekten derselben Klasse
        /// </param>
        /// <remarks>
        /// Die ersten beiden Objekte der Liste müssen wertgleich sein, die übrigen aufsteigend sortiert.
        /// Die Methode darf nur für Klassen aufgerufen werden, die IComparable implementieren.
        /// </remarks>
        private static void SortedTest(IComparable[] l)
        {
            Console.WriteLine("\n----------\n");

            CompareAndPrint(Math.Sign((l[1]).CompareTo(l[0])), 0, _good_color, _bad_color);
            for (int i = 2; i < l.Length; i++)
            {
                CompareAndPrint(Math.Sign((l[i]).CompareTo(l[0])), 1, _good_color, _bad_color);
                CompareAndPrint(Math.Sign((l[0]).CompareTo(l[i])), -1, _good_color, _bad_color);
            }
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
        /// Ausgabe einer Fehlermeldung
        /// </summary>
        /// <param name="message">Text der Fehlermeldung</param>
        private static void ErrorMessage(string message) => ColorPrint(message, _bad_color);
        #endregion

        /// <summary>
        /// Hauptmethode
        /// </summary>
        static void Main()
        {
            #region Klasse Prozent
            Console.WriteLine("\n\n--- Klasse Prozent ---\n");

            // vorgegeben Werte
            double pk1 = 0.4;
            double pk2 = 0.3999;
            double pk3 = 140.0;
            double pk4 = 100.0;
            double pk5 = 0.399;
            string pk1s = "40,0 %";

            // Objekte anlegen, Prüfung der impliziten Konvertierung double -> Prozent
            Prozent p1 = new Prozent(pk1);
            Prozent p2 = pk1;
            Prozent p3 = new Prozent(pk2);
            Prozent p4 = pk2;
            Prozent p5 = new Prozent(pk3, pk4);
            Prozent p6 = pk5;

            // Eigenschaft prüfen, Rundung prüfen, Prüfung der impliziten Konvertierung Prozent -> double
            CompareAndPrintDouble(p1.Wert, pk1);
            CompareAndPrintDouble(p1, pk1);
            CompareAndPrintDouble(p2, pk1);
            CompareAndPrintDouble(p3, pk1);
            CompareAndPrintDouble(p4, pk1);
            CompareAndPrintDouble(p5, pk1);
            CompareAndPrintDouble(p6, pk5);

            // Test auf überschriebenes 'ToString'
            CompareAndPrint(typeof(Prozent).GetMethod("ToString").DeclaringType, typeof(Prozent));
            CompareAndPrint(p1.ToString(), pk1s);

            // Test der drei aus 'object' ererbten überschreibbaren Methoden
            object[] objekte = new object[] { p1, p2, new Prozent(0.5) };
            ObjectTest(objekte);
            #endregion

            Console.WriteLine("\n\n--- ERGEBNIS ---\n");

            if (_errors > 0)
                ColorPrint("Fehler: " + _errors.ToString(), _bad_color);
            else
                ColorPrint("keine Fehler", _good_color);

            Console.ReadKey();
        }
    }
}
