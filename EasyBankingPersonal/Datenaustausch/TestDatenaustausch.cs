// TestDatenaustausch.cs (zu Hausaufgabe 19S)
// Test des Namensraums 'Datenaustausch'
// Konsolenanwendung

using System;

namespace EasyBankingPersonal.Datenaustausch
{
    /// <summary>
    /// Klasse zum Testen der abstrakten Klasse 'Produkte'
    /// </summary>
    class ProdukteKonkret : Produkte
    {
        public ProdukteKonkret(Währung konsumkredite,
                               Währung autokredite,
                               Währung hypothekenkredite,
                               Währung girokonten,
                               Währung spareinlagen,
                               Währung termingelder) : base(konsumkredite, autokredite, hypothekenkredite,
                                                            girokonten, spareinlagen, termingelder)
        { }
    }

    static class DatenaustauschTest
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
        public static void Run()
        {
            Console.WriteLine("\n\n----- Datenaustausch -----\n");

            #region Klasse Periode
            Console.WriteLine("\n\n--- Klasse Periode ---\n");

            // vorgegebene Werte
            const int periodeNummer = 5;
            DateTime periodeBeginn = new DateTime(2017, 10, 1);
            DateTime PeriodeEnde = new DateTime(2018, 9, 30);

            // Objekt anlegen
            Periode periode = new Periode(periodeNummer, periodeBeginn, PeriodeEnde);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(periode.Nummer, periodeNummer);
            CompareAndPrint(periode.Beginn, periodeBeginn);
            CompareAndPrint(periode.Ende, PeriodeEnde);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(periode.GetType().GetProperty("Nummer").CanWrite, false);
            CompareAndPrint(periode.GetType().GetProperty("Beginn").CanWrite, false);
            CompareAndPrint(periode.GetType().GetProperty("Ende").CanWrite, false);

            // Test auf überschriebenes 'ToString'
            CompareAndPrint(periode.GetType().GetMethod("ToString").DeclaringType, typeof(Periode));

            // Test der Konstruktor-Plausibilitätsprüfungen
            ProvokeException(() => new Periode(0, periodeBeginn, PeriodeEnde));
            ProvokeException(() => new Periode(-10, periodeBeginn, PeriodeEnde));
            #endregion

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

            #region Klasse Währung
            Console.WriteLine("\n\n--- Klasse Währung ---\n");

            // vorgegeben Werte
            decimal wk = 40000000.0M;
            string wks1 = "40.000.000,00 €";
            string wks2 = "40.000 T€";
            string wks3 = "40 Mio€";
            decimal wk1 = 10.009M;
            decimal wk2 = 10.01M;

            // Objekte anlegen, Prüfung der impliziten Konvertierung decimal -> Währung
            Währung w1 = new Währung(wk);
            Währung w2 = wk;
            Währung w3 = wk1;

            // Eigenschaft prüfen, Prüfung der impliziten Konvertierung Währung -> decimal
            CompareAndPrint(w1.Betrag, wk);
            CompareAndPrint(w1 + 0.0M, wk);
            CompareAndPrint(w2 + 0.0M, wk);
            CompareAndPrint(w3 + 0.0M, wk2);

            // Test auf überschriebenes 'ToString'
            CompareAndPrint(typeof(Währung).GetMethod("ToString").DeclaringType, typeof(Währung));
            CompareAndPrint(w1.ToString(), wks1);
            CompareAndPrint(w1.ToTausenderString(), wks2);
            CompareAndPrint(w1.ToMillionenString(), wks3);

            // Test der drei aus 'object' ererbten überschreibbaren Methoden
            objekte = new object[] { w1, w2, new Währung(100.0M) };
            ObjectTest(objekte);
            #endregion

            #region Klasse Anzahlen
            Console.WriteLine("\n\n--- Klasse Anzahlen ---\n");

            // vorgegebene Werte
            const int anzahlKonsumkredite = 10;
            const int anzahlAutokredite = 20;
            const int anzahlHypothekenkredite = 30;
            const int anzahlGirokonten = 40;
            const int anzahlSpareinlagen = 50;
            const int anzahlTermingelder = 60;

            // Objekt anlegen
            Anzahlen anzahlen = new Anzahlen(anzahlKonsumkredite,
                                             anzahlAutokredite,
                                             anzahlHypothekenkredite,
                                             anzahlGirokonten,
                                             anzahlSpareinlagen,
                                             anzahlTermingelder);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(anzahlen.Konsumkredite, anzahlKonsumkredite);
            CompareAndPrint(anzahlen.Autokredite, anzahlAutokredite);
            CompareAndPrint(anzahlen.Hypothekenkredite, anzahlHypothekenkredite);
            CompareAndPrint(anzahlen.Girokonten, anzahlGirokonten);
            CompareAndPrint(anzahlen.Spareinlagen, anzahlSpareinlagen);
            CompareAndPrint(anzahlen.Termingelder, anzahlTermingelder);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(anzahlen.GetType().GetProperty("Konsumkredite").CanWrite, false);
            CompareAndPrint(anzahlen.GetType().GetProperty("Autokredite").CanWrite, false);
            CompareAndPrint(anzahlen.GetType().GetProperty("Hypothekenkredite").CanWrite, false);
            CompareAndPrint(anzahlen.GetType().GetProperty("Girokonten").CanWrite, false);
            CompareAndPrint(anzahlen.GetType().GetProperty("Spareinlagen").CanWrite, false);
            CompareAndPrint(anzahlen.GetType().GetProperty("Termingelder").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            anzahlen = new Anzahlen(0,
                                    anzahlAutokredite,
                                    anzahlHypothekenkredite,
                                    anzahlGirokonten,
                                    anzahlSpareinlagen,
                                    anzahlTermingelder);
            ProvokeException(() => anzahlen = new Anzahlen(-1,
                                                           anzahlAutokredite,
                                                           anzahlHypothekenkredite,
                                                           anzahlGirokonten,
                                                           anzahlSpareinlagen,
                                                           anzahlTermingelder));
            anzahlen = new Anzahlen(anzahlKonsumkredite,
                                    0,
                                    anzahlHypothekenkredite,
                                    anzahlGirokonten,
                                    anzahlSpareinlagen,
                                    anzahlTermingelder);
            ProvokeException(() => anzahlen = new Anzahlen(anzahlKonsumkredite,
                                                           -1,
                                                           anzahlHypothekenkredite,
                                                           anzahlGirokonten,
                                                           anzahlSpareinlagen,
                                                           anzahlTermingelder));
            anzahlen = new Anzahlen(anzahlKonsumkredite,
                                    anzahlAutokredite,
                                    0,
                                    anzahlGirokonten,
                                    anzahlSpareinlagen,
                                    anzahlTermingelder);
            ProvokeException(() => anzahlen = new Anzahlen(anzahlKonsumkredite,
                                                           anzahlAutokredite,
                                                           -1,
                                                           anzahlGirokonten,
                                                           anzahlSpareinlagen,
                                                           anzahlTermingelder));
            anzahlen = new Anzahlen(anzahlKonsumkredite,
                                    anzahlAutokredite,
                                    anzahlHypothekenkredite,
                                    0,
                                    anzahlSpareinlagen,
                                    anzahlTermingelder);
            ProvokeException(() => anzahlen = new Anzahlen(anzahlKonsumkredite,
                                                           anzahlAutokredite,
                                                           anzahlHypothekenkredite,
                                                           -1,
                                                           anzahlSpareinlagen,
                                                           anzahlTermingelder));
            anzahlen = new Anzahlen(anzahlKonsumkredite,
                                    anzahlAutokredite,
                                    anzahlHypothekenkredite,
                                    anzahlGirokonten,
                                    0,
                                    anzahlTermingelder);
            ProvokeException(() => anzahlen = new Anzahlen(anzahlKonsumkredite,
                                                           anzahlAutokredite,
                                                           anzahlHypothekenkredite,
                                                           anzahlGirokonten,
                                                           -1,
                                                           anzahlTermingelder));
            anzahlen = new Anzahlen(anzahlKonsumkredite,
                                    anzahlAutokredite,
                                    anzahlHypothekenkredite,
                                    anzahlGirokonten,
                                    anzahlSpareinlagen,
                                    0);
            ProvokeException(() => anzahlen = new Anzahlen(anzahlKonsumkredite,
                                                           anzahlAutokredite,
                                                           anzahlHypothekenkredite,
                                                           anzahlGirokonten,
                                                           anzahlSpareinlagen,
                                                           -1));
            #endregion

            #region Klasse Produkte
            Console.WriteLine("\n\n--- Klasse Produkte ---\n");

            // vorgegebene Werte
            Währung größeKonsumkredite = 10000.0M;
            Währung größeAutokredite = 20000.0M;
            Währung größeHypothekenkredite = 30000.0M;
            Währung größeGirokonten = 40000.0M;
            Währung größeSpareinlagen = 50000.0M;
            Währung größeTermingelder = 60000.0M;

            // Prüfung, ob Klasse abstrakt ist
            CompareAndPrint(typeof(Produkte).IsAbstract, true);

            // Objekt anlegen
            ProdukteKonkret produkte = new ProdukteKonkret(größeKonsumkredite,
                                                           größeAutokredite,
                                                           größeHypothekenkredite,
                                                           größeGirokonten,
                                                           größeSpareinlagen,
                                                           größeTermingelder);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(produkte.Konsumkredite, größeKonsumkredite);
            CompareAndPrint(produkte.Autokredite, größeAutokredite);
            CompareAndPrint(produkte.Hypothekenkredite, größeHypothekenkredite);
            CompareAndPrint(produkte.Girokonten, größeGirokonten);
            CompareAndPrint(produkte.Spareinlagen, größeSpareinlagen);
            CompareAndPrint(produkte.Termingelder, größeTermingelder);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(produkte.GetType().GetProperty("Konsumkredite").CanWrite, false);
            CompareAndPrint(produkte.GetType().GetProperty("Autokredite").CanWrite, false);
            CompareAndPrint(produkte.GetType().GetProperty("Hypothekenkredite").CanWrite, false);
            CompareAndPrint(produkte.GetType().GetProperty("Girokonten").CanWrite, false);
            CompareAndPrint(produkte.GetType().GetProperty("Spareinlagen").CanWrite, false);
            CompareAndPrint(produkte.GetType().GetProperty("Termingelder").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            produkte = new ProdukteKonkret(0.0M,
                                           größeAutokredite,
                                           größeHypothekenkredite,
                                           größeGirokonten,
                                           größeSpareinlagen,
                                           größeTermingelder);
            ProvokeException(() => produkte = new ProdukteKonkret(-0.1M,
                                                                  größeAutokredite,
                                                                  größeHypothekenkredite,
                                                                  größeGirokonten,
                                                                  größeSpareinlagen,
                                                                  größeTermingelder));
            produkte = new ProdukteKonkret(größeKonsumkredite,
                                           0.0M,
                                           größeHypothekenkredite,
                                           größeGirokonten,
                                           größeSpareinlagen,
                                           größeTermingelder);
            ProvokeException(() => produkte = new ProdukteKonkret(größeKonsumkredite,
                                                                  -0.1M,
                                                                  größeHypothekenkredite,
                                                                  größeGirokonten,
                                                                  größeSpareinlagen,
                                                                  größeTermingelder));
            produkte = new ProdukteKonkret(größeKonsumkredite,
                                           größeAutokredite,
                                           0.0M,
                                           größeGirokonten,
                                           größeSpareinlagen,
                                           größeTermingelder);
            ProvokeException(() => produkte = new ProdukteKonkret(größeKonsumkredite,
                                                                  größeAutokredite,
                                                                  -0.1M,
                                                                  größeGirokonten,
                                                                  größeSpareinlagen,
                                                                  größeTermingelder));
            produkte = new ProdukteKonkret(größeKonsumkredite,
                                           größeAutokredite,
                                           größeHypothekenkredite,
                                           0.0M,
                                           größeSpareinlagen,
                                           größeTermingelder);
            ProvokeException(() => produkte = new ProdukteKonkret(größeKonsumkredite,
                                                                  größeAutokredite,
                                                                  größeHypothekenkredite,
                                                                  -0.1M,
                                                                  größeSpareinlagen,
                                                                  größeTermingelder));
            produkte = new ProdukteKonkret(größeKonsumkredite,
                                           größeAutokredite,
                                           größeHypothekenkredite,
                                           größeGirokonten,
                                           0.0M,
                                           größeTermingelder);
            ProvokeException(() => produkte = new ProdukteKonkret(größeKonsumkredite,
                                                                  größeAutokredite,
                                                                  größeHypothekenkredite,
                                                                  größeGirokonten,
                                                                  -0.1M,
                                                                  größeTermingelder));
            produkte = new ProdukteKonkret(größeKonsumkredite,
                                           größeAutokredite,
                                           größeHypothekenkredite,
                                           größeGirokonten,
                                           größeSpareinlagen,
                                           0.0M);
            ProvokeException(() => produkte = new ProdukteKonkret(größeKonsumkredite,
                                                                  größeAutokredite,
                                                                  größeHypothekenkredite,
                                                                  größeGirokonten,
                                                                  größeSpareinlagen,
                                                                  -0.1M));
            #endregion

            #region Klasse Durchschnittsgrößen
            Console.WriteLine("\n\n--- Klasse Durchschnittsgrößen ---\n");

            // Prüfung: ist 'Produkte' Oberklasse?
            CompareAndPrint(typeof(Durchschnittsgrößen).BaseType, typeof(Produkte));

            // Objekt anlegen
            Durchschnittsgrößen durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                              größeAutokredite,
                                                                              größeHypothekenkredite,
                                                                              größeGirokonten,
                                                                              größeSpareinlagen,
                                                                              größeTermingelder);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(durchschnittsgrößen.Konsumkredite, größeKonsumkredite);
            CompareAndPrint(durchschnittsgrößen.Autokredite, größeAutokredite);
            CompareAndPrint(durchschnittsgrößen.Hypothekenkredite, größeHypothekenkredite);
            CompareAndPrint(durchschnittsgrößen.Girokonten, größeGirokonten);
            CompareAndPrint(durchschnittsgrößen.Spareinlagen, größeSpareinlagen);
            CompareAndPrint(durchschnittsgrößen.Termingelder, größeTermingelder);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Konsumkredite").CanWrite, false);
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Autokredite").CanWrite, false);
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Hypothekenkredite").CanWrite, false);
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Girokonten").CanWrite, false);
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Spareinlagen").CanWrite, false);
            CompareAndPrint(durchschnittsgrößen.GetType().GetProperty("Termingelder").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            durchschnittsgrößen = new Durchschnittsgrößen(0.01M,
                                                          größeAutokredite,
                                                          größeHypothekenkredite,
                                                          größeGirokonten,
                                                          größeSpareinlagen,
                                                          größeTermingelder);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(0.0M,
                                                                                 größeAutokredite,
                                                                                 größeHypothekenkredite,
                                                                                 größeGirokonten,
                                                                                 größeSpareinlagen,
                                                                                 größeTermingelder));
            durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                          0.01M,
                                                          größeHypothekenkredite,
                                                          größeGirokonten,
                                                          größeSpareinlagen,
                                                          größeTermingelder);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                                 0.0M,
                                                                                 größeHypothekenkredite,
                                                                                 größeGirokonten,
                                                                                 größeSpareinlagen,
                                                                                 größeTermingelder));
            durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                          größeAutokredite,
                                                          0.01M,
                                                          größeGirokonten,
                                                          größeSpareinlagen,
                                                          größeTermingelder);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                                 größeAutokredite,
                                                                                 0.0M,
                                                                                 größeGirokonten,
                                                                                 größeSpareinlagen,
                                                                                 größeTermingelder));
            durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                          größeAutokredite,
                                                          größeHypothekenkredite,
                                                          0.01M,
                                                          größeSpareinlagen,
                                                          größeTermingelder);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                                 größeAutokredite,
                                                                                 größeHypothekenkredite,
                                                                                 0.0M,
                                                                                 größeSpareinlagen,
                                                                                 größeTermingelder));
            durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                          größeAutokredite,
                                                          größeHypothekenkredite,
                                                          größeGirokonten,
                                                          0.01M,
                                                          größeTermingelder);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                                 größeAutokredite,
                                                                                 größeHypothekenkredite,
                                                                                 größeGirokonten,
                                                                                 0.0M,
                                                                                 größeTermingelder));
            durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                          größeAutokredite,
                                                          größeHypothekenkredite,
                                                          größeGirokonten,
                                                          größeSpareinlagen,
                                                          0.01M);
            ProvokeException(() => durchschnittsgrößen = new Durchschnittsgrößen(größeKonsumkredite,
                                                                                 größeAutokredite,
                                                                                 größeHypothekenkredite,
                                                                                 größeGirokonten,
                                                                                 größeSpareinlagen,
                                                                                 0.0M));
            #endregion

            #region Klasse Mitarbeiter
            Console.WriteLine("\n\n--- Klasse Mitarbeiter ---\n");

            // vorgegebene Werte
            const int mitarbeiterKreditberater = 5;
            const int mitarbeiterAnlageberater = 6;
            const int mitarbeiterBackofficemitarbeiter = 7;
            const double mitarbeiterFluktuation = 0.8;
            const int mitarbeiterTrainingstage = 9;

            // Objekt anlegen
            Mitarbeiter mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                                      mitarbeiterAnlageberater,
                                                      mitarbeiterBackofficemitarbeiter,
                                                      mitarbeiterFluktuation,
                                                      mitarbeiterTrainingstage);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(mitarbeiter.Kreditberater, mitarbeiterKreditberater);
            CompareAndPrint(mitarbeiter.Anlageberater, mitarbeiterAnlageberater);
            CompareAndPrint(mitarbeiter.Backofficemitarbeiter, mitarbeiterBackofficemitarbeiter);
            CompareAndPrintDouble(mitarbeiter.Fluktuation, mitarbeiterFluktuation);
            CompareAndPrint(mitarbeiter.TrainingstageProMitarbeiter, mitarbeiterTrainingstage);
            CompareAndPrint(mitarbeiter.Frontofficemitarbeiter, mitarbeiterKreditberater + mitarbeiterAnlageberater);
            CompareAndPrint(mitarbeiter.Insgesamt, mitarbeiterKreditberater + mitarbeiterAnlageberater + mitarbeiterBackofficemitarbeiter);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Kreditberater").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Anlageberater").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Backofficemitarbeiter").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Fluktuation").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("TrainingstageProMitarbeiter").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Frontofficemitarbeiter").CanWrite, false);
            CompareAndPrint(mitarbeiter.GetType().GetProperty("Insgesamt").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            mitarbeiter = new Mitarbeiter(0,
                                          mitarbeiterAnlageberater,
                                          mitarbeiterBackofficemitarbeiter,
                                          mitarbeiterFluktuation,
                                          mitarbeiterTrainingstage);
            ProvokeException(() => new Mitarbeiter(-1,
                                                   mitarbeiterAnlageberater,
                                                   mitarbeiterBackofficemitarbeiter,
                                                   mitarbeiterFluktuation,
                                                   mitarbeiterTrainingstage));
            mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                          0,
                                          mitarbeiterBackofficemitarbeiter,
                                          mitarbeiterFluktuation,
                                          mitarbeiterTrainingstage);
            ProvokeException(() => new Mitarbeiter(mitarbeiterKreditberater,
                                                   -1,
                                                   mitarbeiterBackofficemitarbeiter,
                                                   mitarbeiterFluktuation,
                                                   mitarbeiterTrainingstage));
            mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                          mitarbeiterAnlageberater,
                                          0,
                                          mitarbeiterFluktuation,
                                          mitarbeiterTrainingstage);
            ProvokeException(() => new Mitarbeiter(mitarbeiterKreditberater,
                                                   mitarbeiterAnlageberater,
                                                   -1,
                                                   mitarbeiterFluktuation,
                                                   mitarbeiterTrainingstage));
            mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                          mitarbeiterAnlageberater,
                                          mitarbeiterBackofficemitarbeiter,
                                          0.0,
                                          mitarbeiterTrainingstage);
            mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                          mitarbeiterAnlageberater,
                                          mitarbeiterBackofficemitarbeiter,
                                          1.0,
                                          mitarbeiterTrainingstage);
            ProvokeException(() => new Mitarbeiter(mitarbeiterKreditberater,
                                                   mitarbeiterAnlageberater,
                                                   mitarbeiterBackofficemitarbeiter,
                                                   -0.01,
                                                   mitarbeiterTrainingstage));
            ProvokeException(() => new Mitarbeiter(mitarbeiterKreditberater,
                                                   mitarbeiterAnlageberater,
                                                   mitarbeiterBackofficemitarbeiter,
                                                   1.01,
                                                   mitarbeiterTrainingstage));
            mitarbeiter = new Mitarbeiter(mitarbeiterKreditberater,
                                          mitarbeiterAnlageberater,
                                          mitarbeiterBackofficemitarbeiter,
                                          mitarbeiterFluktuation,
                                          0);
            ProvokeException(() => new Mitarbeiter(mitarbeiterKreditberater,
                                                   mitarbeiterAnlageberater,
                                                   mitarbeiterBackofficemitarbeiter,
                                                   mitarbeiterFluktuation,
                                                   -1));
            #endregion

            #region Klasse Personalkosten
            Console.WriteLine("\n\n--- Klasse Personalkosten ---\n");

            // vorgegebene Werte
            Währung größeDurchschnittsjahresgehalt = 10000.0M;
            Währung größeKostenProEinstellung = 20000.0M;
            Währung größeKostenProEntlassung = 30000.0M;
            Währung größeTrainingskosten = 40000.0M;

            // Objekt anlegen
            Personalkosten personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                               größeKostenProEinstellung,
                                                               größeKostenProEntlassung,
                                                               größeTrainingskosten);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(personalkosten.Durchschnittsjahresgehalt, größeDurchschnittsjahresgehalt);
            CompareAndPrint(personalkosten.KostenProEinstellung, größeKostenProEinstellung);
            CompareAndPrint(personalkosten.KostenProEntlassung, größeKostenProEntlassung);
            CompareAndPrint(personalkosten.TrainingskostenProTagUndMitarbeiter, größeTrainingskosten);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(personalkosten.GetType().GetProperty("Durchschnittsjahresgehalt").CanWrite, false);
            CompareAndPrint(personalkosten.GetType().GetProperty("KostenProEinstellung").CanWrite, false);
            CompareAndPrint(personalkosten.GetType().GetProperty("KostenProEntlassung").CanWrite, false);
            CompareAndPrint(personalkosten.GetType().GetProperty("TrainingskostenProTagUndMitarbeiter").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            personalkosten = new Personalkosten(0,
                                                größeKostenProEinstellung,
                                                größeKostenProEntlassung,
                                                größeTrainingskosten);
            ProvokeException(() => personalkosten = new Personalkosten(-1,
                                                                       größeKostenProEinstellung,
                                                                       größeKostenProEntlassung,
                                                                       größeTrainingskosten));
            personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                0,
                                                größeKostenProEntlassung,
                                                größeTrainingskosten);
            ProvokeException(() => personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                                       -1,
                                                                       größeKostenProEntlassung,
                                                                       größeTrainingskosten));
            personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                größeKostenProEinstellung,
                                                0,
                                                größeTrainingskosten);
            ProvokeException(() => personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                                       größeKostenProEinstellung,
                                                                       -1,
                                                                       größeTrainingskosten));
            personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                größeKostenProEinstellung,
                                                größeKostenProEntlassung,
                                                0);
            ProvokeException(() => personalkosten = new Personalkosten(größeDurchschnittsjahresgehalt,
                                                                       größeKostenProEinstellung,
                                                                       größeKostenProEntlassung,
                                                                       -1));
            #endregion

            #region Klasse VolumenNeugeschäft
            Console.WriteLine("\n\n--- Klasse VolumenNeugeschäft ---\n");

            // Prüfung: ist 'Produkte' Oberklasse?
            CompareAndPrint(typeof(Durchschnittsgrößen).BaseType, typeof(Produkte));

            // Objekt anlegen
            VolumenNeugeschäft volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                           größeAutokredite,
                                                                           größeHypothekenkredite,
                                                                           größeGirokonten,
                                                                           größeSpareinlagen,
                                                                           größeTermingelder);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(volumenNeugeschäft.Konsumkredite, größeKonsumkredite);
            CompareAndPrint(volumenNeugeschäft.Autokredite, größeAutokredite);
            CompareAndPrint(volumenNeugeschäft.Hypothekenkredite, größeHypothekenkredite);
            CompareAndPrint(volumenNeugeschäft.Girokonten, größeGirokonten);
            CompareAndPrint(volumenNeugeschäft.Spareinlagen, größeSpareinlagen);
            CompareAndPrint(volumenNeugeschäft.Termingelder, größeTermingelder);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Konsumkredite").CanWrite, false);
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Autokredite").CanWrite, false);
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Hypothekenkredite").CanWrite, false);
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Girokonten").CanWrite, false);
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Spareinlagen").CanWrite, false);
            CompareAndPrint(volumenNeugeschäft.GetType().GetProperty("Termingelder").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            volumenNeugeschäft = new VolumenNeugeschäft(0.0M,
                                                        größeAutokredite,
                                                        größeHypothekenkredite,
                                                        größeGirokonten,
                                                        größeSpareinlagen,
                                                        größeTermingelder);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(-0.1M,
                                                                               größeAutokredite,
                                                                               größeHypothekenkredite,
                                                                               größeGirokonten,
                                                                               größeSpareinlagen,
                                                                               größeTermingelder));
            volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                        0.0M,
                                                        größeHypothekenkredite,
                                                        größeGirokonten,
                                                        größeSpareinlagen,
                                                        größeTermingelder);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                               -0.1M,
                                                                               größeHypothekenkredite,
                                                                               größeGirokonten,
                                                                               größeSpareinlagen,
                                                                               größeTermingelder));
            volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                        größeAutokredite,
                                                        0.0M,
                                                        größeGirokonten,
                                                        größeSpareinlagen,
                                                        größeTermingelder);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                               größeAutokredite,
                                                                               -0.1M,
                                                                               größeGirokonten,
                                                                               größeSpareinlagen,
                                                                               größeTermingelder));
            volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                        größeAutokredite,
                                                        größeHypothekenkredite,
                                                        0.0M,
                                                        größeSpareinlagen,
                                                        größeTermingelder);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                               größeAutokredite,
                                                                               größeHypothekenkredite,
                                                                               -0.1M,
                                                                               größeSpareinlagen,
                                                                               größeTermingelder));
            volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                        größeAutokredite,
                                                        größeHypothekenkredite,
                                                        größeGirokonten,
                                                        0.0M,
                                                        größeTermingelder);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                               größeAutokredite,
                                                                               größeHypothekenkredite,
                                                                               größeGirokonten,
                                                                               -0.1M,
                                                                               größeTermingelder));
            volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                        größeAutokredite,
                                                        größeHypothekenkredite,
                                                        größeGirokonten,
                                                        größeSpareinlagen,
                                                        0.0M);
            ProvokeException(() => volumenNeugeschäft = new VolumenNeugeschäft(größeKonsumkredite,
                                                                               größeAutokredite,
                                                                               größeHypothekenkredite,
                                                                               größeGirokonten,
                                                                               größeSpareinlagen,
                                                                               -0.1M));
            #endregion

            #region Klasse VolumenProMitarbeiter
            Console.WriteLine("\n\n--- Klasse VolumenProMitarbeiter ---\n");

            // Prüfung: ist 'Anzahlen' Oberklasse?
            CompareAndPrint(typeof(VolumenProMitarbeiter).BaseType, typeof(Anzahlen));

            // Objekt anlegen
            VolumenProMitarbeiter volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                    anzahlAutokredite,
                                                                                    anzahlHypothekenkredite,
                                                                                    anzahlGirokonten,
                                                                                    anzahlSpareinlagen,
                                                                                    anzahlTermingelder);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(volumenProMitarbeiter.Konsumkredite, anzahlKonsumkredite);
            CompareAndPrint(volumenProMitarbeiter.Autokredite, anzahlAutokredite);
            CompareAndPrint(volumenProMitarbeiter.Hypothekenkredite, anzahlHypothekenkredite);
            CompareAndPrint(volumenProMitarbeiter.Girokonten, anzahlGirokonten);
            CompareAndPrint(volumenProMitarbeiter.Spareinlagen, anzahlSpareinlagen);
            CompareAndPrint(volumenProMitarbeiter.Termingelder, anzahlTermingelder);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Konsumkredite").CanWrite, false);
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Autokredite").CanWrite, false);
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Hypothekenkredite").CanWrite, false);
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Girokonten").CanWrite, false);
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Spareinlagen").CanWrite, false);
            CompareAndPrint(volumenProMitarbeiter.GetType().GetProperty("Termingelder").CanWrite, false);

            // Test der Konstruktor-Plausibilitätsprüfungen
            volumenProMitarbeiter = new VolumenProMitarbeiter(1,
                                                              anzahlAutokredite,
                                                              anzahlHypothekenkredite,
                                                              anzahlGirokonten,
                                                              anzahlSpareinlagen,
                                                              anzahlTermingelder);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(0,
                                                                                     anzahlAutokredite,
                                                                                     anzahlHypothekenkredite,
                                                                                     anzahlGirokonten,
                                                                                     anzahlSpareinlagen,
                                                                                     anzahlTermingelder));
            volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                              1,
                                                              anzahlHypothekenkredite,
                                                              anzahlGirokonten,
                                                              anzahlSpareinlagen,
                                                              anzahlTermingelder);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                     0,
                                                                                     anzahlHypothekenkredite,
                                                                                     anzahlGirokonten,
                                                                                     anzahlSpareinlagen,
                                                                                     anzahlTermingelder));
            volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                              anzahlAutokredite,
                                                              1,
                                                              anzahlGirokonten,
                                                              anzahlSpareinlagen,
                                                              anzahlTermingelder);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                     anzahlAutokredite,
                                                                                     0,
                                                                                     anzahlGirokonten,
                                                                                     anzahlSpareinlagen,
                                                                                     anzahlTermingelder));
            volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                              anzahlAutokredite,
                                                              anzahlHypothekenkredite,
                                                              1,
                                                              anzahlSpareinlagen,
                                                              anzahlTermingelder);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                     anzahlAutokredite,
                                                                                     anzahlHypothekenkredite,
                                                                                     0,
                                                                                     anzahlSpareinlagen,
                                                                                     anzahlTermingelder));
            volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                              anzahlAutokredite,
                                                              anzahlHypothekenkredite,
                                                              anzahlGirokonten,
                                                              1,
                                                              anzahlTermingelder);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                     anzahlAutokredite,
                                                                                     anzahlHypothekenkredite,
                                                                                     anzahlGirokonten,
                                                                                     0,
                                                                                     anzahlTermingelder));
            volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                              anzahlAutokredite,
                                                              anzahlHypothekenkredite,
                                                              anzahlGirokonten,
                                                              anzahlSpareinlagen,
                                                              1);
            ProvokeException(() => volumenProMitarbeiter = new VolumenProMitarbeiter(anzahlKonsumkredite,
                                                                                     anzahlAutokredite,
                                                                                     anzahlHypothekenkredite,
                                                                                     anzahlGirokonten,
                                                                                     anzahlSpareinlagen,
                                                                                     0));
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
