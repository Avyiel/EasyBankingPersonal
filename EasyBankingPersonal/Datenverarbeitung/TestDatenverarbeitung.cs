// TestDatenverarbeitung.cs (zu Hausaufgabe 19S)
// Test des Namensraums 'Datenverarbeitung'
// Konsolenanwendung

using System;
using System.Reflection;
using EasyBankingPersonal.Datenaustausch;

namespace EasyBankingPersonal.Datenverarbeitung
{
    static class DatenverarbeitungTest
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
            #region Klasse BenötigteMitarbeiter
            Console.WriteLine("\n\n--- Klasse BenötigteMitarbeiter ---\n");

            // vorgegebene Werte
            const int anzahlKonsumkredite = 10;
            const int anzahlAutokredite = 20;
            const int anzahlHypothekenkredite = 30;
            const int anzahlGirokonten = 40;
            const int anzahlSpareinlagen = 50;
            const int anzahlTermingelder = 60;
            const int backoffice = 70;
            const int frontoffice = 210;
            const int insgesamt = 280;
            const int nachFluktuation = 300;
            const int nachFluktuationUndTraining1 = 290;
            const int nachFluktuationUndTraining2 = 280;
            const int nachFluktuationUndTraining3 = 270;
            const int neueinstellungen1 = 0;
            const int entlassungen1 = 10;
            const int neueinstellungen2 = 0;
            const int entlassungen2 = 0;
            const int neueinstellungen3 = 10;
            const int entlassungen3 = 0;

            // Objekte anlegen
            BenötigteMitarbeiter benötigteMitarbeiter1 = new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                                                  anzahlAutokredite,
                                                                                  anzahlHypothekenkredite,
                                                                                  anzahlGirokonten,
                                                                                  anzahlSpareinlagen,
                                                                                  anzahlTermingelder,
                                                                                  backoffice,
                                                                                  nachFluktuation,
                                                                                  nachFluktuationUndTraining1);
            BenötigteMitarbeiter benötigteMitarbeiter2 = new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                                                  anzahlAutokredite,
                                                                                  anzahlHypothekenkredite,
                                                                                  anzahlGirokonten,
                                                                                  anzahlSpareinlagen,
                                                                                  anzahlTermingelder,
                                                                                  backoffice,
                                                                                  nachFluktuation,
                                                                                  nachFluktuationUndTraining2);
            BenötigteMitarbeiter benötigteMitarbeiter3 = new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                                                  anzahlAutokredite,
                                                                                  anzahlHypothekenkredite,
                                                                                  anzahlGirokonten,
                                                                                  anzahlSpareinlagen,
                                                                                  anzahlTermingelder,
                                                                                  backoffice,
                                                                                  nachFluktuation,
                                                                                  nachFluktuationUndTraining3);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(benötigteMitarbeiter1.Konsumkredite, anzahlKonsumkredite);
            CompareAndPrint(benötigteMitarbeiter1.Autokredite, anzahlAutokredite);
            CompareAndPrint(benötigteMitarbeiter1.Hypothekenkredite, anzahlHypothekenkredite);
            CompareAndPrint(benötigteMitarbeiter1.Girokonten, anzahlGirokonten);
            CompareAndPrint(benötigteMitarbeiter1.Spareinlagen, anzahlSpareinlagen);
            CompareAndPrint(benötigteMitarbeiter1.Termingelder, anzahlTermingelder);
            CompareAndPrint(benötigteMitarbeiter1.Backoffice, backoffice);
            CompareAndPrint(benötigteMitarbeiter1.Frontoffice, frontoffice);
            CompareAndPrint(benötigteMitarbeiter1.Insgesamt, insgesamt);
            CompareAndPrint(benötigteMitarbeiter1.VorhandeneMitarbeiterNachFluktuation, nachFluktuation);
            CompareAndPrint(benötigteMitarbeiter1.VorhandeneMitarbeiterNachFluktuationUndTraining, nachFluktuationUndTraining1);
            CompareAndPrint(benötigteMitarbeiter1.Neueinstellungen, neueinstellungen1);
            CompareAndPrint(benötigteMitarbeiter1.Entlassungen, entlassungen1);

            CompareAndPrint(benötigteMitarbeiter2.VorhandeneMitarbeiterNachFluktuationUndTraining, nachFluktuationUndTraining2);
            CompareAndPrint(benötigteMitarbeiter2.Neueinstellungen, neueinstellungen2);
            CompareAndPrint(benötigteMitarbeiter2.Entlassungen, entlassungen2);

            CompareAndPrint(benötigteMitarbeiter3.VorhandeneMitarbeiterNachFluktuationUndTraining, nachFluktuationUndTraining3);
            CompareAndPrint(benötigteMitarbeiter3.Neueinstellungen, neueinstellungen3);
            CompareAndPrint(benötigteMitarbeiter3.Entlassungen, entlassungen3);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            foreach (PropertyInfo pi in typeof(BenötigteMitarbeiter).GetProperties())
            {
                Console.Write(pi.Name + " änderbar: ");
                CompareAndPrint(pi.CanWrite, false);
            }

            // Test der Konstruktor-Plausibilitätsprüfungen
            new BenötigteMitarbeiter(0,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(-1,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     0,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            -1,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     0,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            -1,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     0,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            -1,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     0,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            -1,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     0,
                                     backoffice,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            -1,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     0,
                                     nachFluktuation,
                                     nachFluktuationUndTraining1);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            -1,
                                                            nachFluktuation,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     0,
                                     0);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            -1,
                                                            nachFluktuationUndTraining1));
            new BenötigteMitarbeiter(anzahlKonsumkredite,
                                     anzahlAutokredite,
                                     anzahlHypothekenkredite,
                                     anzahlGirokonten,
                                     anzahlSpareinlagen,
                                     anzahlTermingelder,
                                     backoffice,
                                     nachFluktuation,
                                     0);
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            -1));
            ProvokeException(() => new BenötigteMitarbeiter(anzahlKonsumkredite,
                                                            anzahlAutokredite,
                                                            anzahlHypothekenkredite,
                                                            anzahlGirokonten,
                                                            anzahlSpareinlagen,
                                                            anzahlTermingelder,
                                                            backoffice,
                                                            nachFluktuation,
                                                            nachFluktuation + 1));
            #endregion

            #region Klasse Personalaufwand
            Console.WriteLine("\n\n--- Klasse Personalaufwand ---\n");

            // vorgegebene Werte
            Währung gehaltskosten = 30000M;
            Währung kostenEinstellungen = 10000M;
            Währung kostenEntlassungen = 12000M;
            Währung kostenTraining = 300M;
            Währung aufwandInsgesamt1 = 40300M;
            Währung aufwandInsgesamt2 = 42300M;

            // Objekte anlegen
            Personalaufwand personalaufwand1 = new Personalaufwand(gehaltskosten,
                                                                   kostenEinstellungen,
                                                                   0.0M,
                                                                   kostenTraining);
            Personalaufwand personalaufwand2 = new Personalaufwand(gehaltskosten,
                                                                   0.0M,
                                                                   kostenEntlassungen,
                                                                   kostenTraining);

            // Test auf korrekte Werte der Eigenschaften
            CompareAndPrint(personalaufwand1.Gehaltskosten, gehaltskosten);
            CompareAndPrint(personalaufwand1.KostenEinstellungen, kostenEinstellungen);
            CompareAndPrint(personalaufwand2.KostenEinstellungen, new Währung(0.0M));
            CompareAndPrint(personalaufwand1.KostenEntlassungen, new Währung(0.0M));
            CompareAndPrint(personalaufwand2.KostenEntlassungen, kostenEntlassungen);
            CompareAndPrint(personalaufwand1.KostenTraining, kostenTraining);
            CompareAndPrint(personalaufwand1.Insgesamt, aufwandInsgesamt1);
            CompareAndPrint(personalaufwand2.Insgesamt, aufwandInsgesamt2);

            // Test auf Nicht-Änderbarkeit der Eigenschaften
            foreach (PropertyInfo pi in typeof(Personalaufwand).GetProperties())
            {
                Console.Write(pi.Name + " änderbar: ");
                CompareAndPrint(pi.CanWrite, false);
            }

            // Test der Konstruktor-Plausibilitätsprüfungen
            new Personalaufwand(0.0M,
                                kostenEinstellungen,
                                0.0M,
                                kostenTraining);
            ProvokeException(() => new Personalaufwand(-0.01M,
                                                       kostenEinstellungen,
                                                       0.0M,
                                                       kostenTraining));
            new Personalaufwand(gehaltskosten,
                                0.0M,
                                0.0M,
                                kostenTraining);
            ProvokeException(() => new Personalaufwand(gehaltskosten,
                                                       -0.01M,
                                                       0.0M,
                                                       kostenTraining));
            new Personalaufwand(gehaltskosten,
                                kostenEinstellungen,
                                0.0M,
                                kostenTraining);
            ProvokeException(() => new Personalaufwand(gehaltskosten,
                                                       kostenEinstellungen,
                                                       -0.01M,
                                                       kostenTraining));
            new Personalaufwand(gehaltskosten,
                                kostenEinstellungen,
                                0.0M,
                                0.0M);
            ProvokeException(() => new Personalaufwand(gehaltskosten,
                                                       kostenEinstellungen,
                                                       0.0M,
                                                       -0.01M));
            ProvokeException(() => new Personalaufwand(gehaltskosten,
                                                       kostenEinstellungen,
                                                       kostenEntlassungen,
                                                       kostenTraining));

            #endregion

            #region Klasse Berechnungen
            Console.WriteLine("\n\n--- Klasse Berechnungen ---\n");

            // sind Klasse und Methode statisch?
            CompareAndPrint(typeof(Berechnungen).IsAbstract && typeof(Berechnungen).IsSealed, true);
            CompareAndPrint(typeof(Berechnungen).GetMethod("Rechnen").IsStatic, true);

            // vorgegebene Werte
            VolumenNeugeschäft volumenNeugeschäftAP = new VolumenNeugeschäft(1500000000.0M,
                                                                             1080000000.0M,
                                                                             1000000000.0M,
                                                                             1350000000.0M,
                                                                             2400000000.0M,
                                                                             1200000000.0M);
            VolumenNeugeschäft volumenNeugeschäftAPB = new VolumenNeugeschäft(1350000000.0M,
                                                                               972000000.0M,
                                                                               900000000.0M,
                                                                              1215000000.0M,
                                                                              2160000000.0M,
                                                                              1080000000.0M);
            Durchschnittsgrößen durchschnittsgrößenAP = new Durchschnittsgrößen(30000.0M,
                                                                                 40000.0M,
                                                                                100000.0M,
                                                                                  6000.0M,
                                                                                 10000.0M,
                                                                                 20000.0M);
            VolumenProMitarbeiter volumenProMitarbeiter = new VolumenProMitarbeiter(200,
                                                                                     150,
                                                                                      50,
                                                                                     900,
                                                                                    1200,
                                                                                    1200);
            int benötigteMitarbeiterBackOfficeAP = 400;
            int vorhandeneMitarbeiterVP = 1470;
            Prozent fluktuationAP = new Prozent(0.1);
            Prozent fluktuationAPB = new Prozent(0.0);
            int trainigstageProMitarbeiterAP = 5;
            int trainigstageProMitarbeiterAPB = 2;
            Personalkosten personalkostenAP = new Personalkosten(38000.0M,
                                                                 10300.0M,
                                                                 19500.0M,
                                                                   510.0M);

            Berechnungen.Rechnen(volumenNeugeschäftAP,
                                 durchschnittsgrößenAP,
                                 volumenProMitarbeiter,
                                 benötigteMitarbeiterBackOfficeAP,
                                 vorhandeneMitarbeiterVP,
                                 fluktuationAP,
                                 trainigstageProMitarbeiterAP,
                                 personalkostenAP,
                                 out Anzahlen anzahlen,
                                 out BenötigteMitarbeiter benötigteMitarbeiter,
                                 out Personalaufwand personalaufwand);
            Berechnungen.Rechnen(volumenNeugeschäftAPB,
                                 durchschnittsgrößenAP,
                                 volumenProMitarbeiter,
                                 benötigteMitarbeiterBackOfficeAP,
                                 vorhandeneMitarbeiterVP,
                                 fluktuationAPB,
                                 trainigstageProMitarbeiterAPB,
                                 personalkostenAP,
                                 out Anzahlen anzahlenB,
                                 out BenötigteMitarbeiter benötigteMitarbeiterB,
                                 out Personalaufwand personalaufwandB);

            // Test auf korrekte Werte der Ergebnisse

            CompareAndPrint(anzahlen.Konsumkredite, 50000);
            CompareAndPrint(anzahlen.Autokredite, 27000);
            CompareAndPrint(anzahlen.Hypothekenkredite, 10000);
            CompareAndPrint(anzahlen.Girokonten, 225000);
            CompareAndPrint(anzahlen.Spareinlagen, 240000);
            CompareAndPrint(anzahlen.Termingelder, 60000);

            CompareAndPrint(benötigteMitarbeiter.Konsumkredite, 250);
            CompareAndPrint(benötigteMitarbeiter.Autokredite, 180);
            CompareAndPrint(benötigteMitarbeiter.Hypothekenkredite, 200);
            CompareAndPrint(benötigteMitarbeiter.Girokonten, 250);
            CompareAndPrint(benötigteMitarbeiter.Spareinlagen, 200);
            CompareAndPrint(benötigteMitarbeiter.Termingelder, 50);
            CompareAndPrint(benötigteMitarbeiter.Frontoffice, 1130);
            CompareAndPrint(benötigteMitarbeiter.Backoffice, 400);
            CompareAndPrint(benötigteMitarbeiter.Insgesamt, 1530);
            CompareAndPrint(benötigteMitarbeiter.VorhandeneMitarbeiterNachFluktuation, 1323);
            CompareAndPrint(benötigteMitarbeiter.VorhandeneMitarbeiterNachFluktuationUndTraining, 1293);
            CompareAndPrint(benötigteMitarbeiter.Neueinstellungen, 237);
            CompareAndPrint(benötigteMitarbeiter.Entlassungen, 0);

            CompareAndPrint(personalaufwand.Gehaltskosten, new Währung(58140000.0M));
            CompareAndPrint(personalaufwand.KostenEinstellungen, new Währung(2441100.0M));
            CompareAndPrint(personalaufwand.KostenEntlassungen, new Währung(0.0M));
            CompareAndPrint(personalaufwand.KostenTraining, new Währung(3901500.0M));
            CompareAndPrint(personalaufwand.Insgesamt, new Währung(64482600.0M));

            CompareAndPrint(anzahlenB.Konsumkredite, 45000);
            CompareAndPrint(anzahlenB.Autokredite, 24300);
            CompareAndPrint(anzahlenB.Hypothekenkredite, 9000);
            CompareAndPrint(anzahlenB.Girokonten, 202500);
            CompareAndPrint(anzahlenB.Spareinlagen, 216000);
            CompareAndPrint(anzahlenB.Termingelder, 54000);

            CompareAndPrint(benötigteMitarbeiterB.Konsumkredite, 225);
            CompareAndPrint(benötigteMitarbeiterB.Autokredite, 162);
            CompareAndPrint(benötigteMitarbeiterB.Hypothekenkredite, 180);
            CompareAndPrint(benötigteMitarbeiterB.Girokonten, 225);
            CompareAndPrint(benötigteMitarbeiterB.Spareinlagen, 180);
            CompareAndPrint(benötigteMitarbeiterB.Termingelder, 45);
            CompareAndPrint(benötigteMitarbeiterB.Frontoffice, 1017);
            CompareAndPrint(benötigteMitarbeiterB.Backoffice, 400);
            CompareAndPrint(benötigteMitarbeiterB.Insgesamt, 1417);
            CompareAndPrint(benötigteMitarbeiterB.VorhandeneMitarbeiterNachFluktuation, 1470);
            CompareAndPrint(benötigteMitarbeiterB.VorhandeneMitarbeiterNachFluktuationUndTraining, 1457);
            CompareAndPrint(benötigteMitarbeiterB.Neueinstellungen, 0);
            CompareAndPrint(benötigteMitarbeiterB.Entlassungen, 40);

            CompareAndPrint(personalaufwandB.Gehaltskosten, new Währung(53846000.0M));
            CompareAndPrint(personalaufwandB.KostenEinstellungen, new Währung(0.0M));
            CompareAndPrint(personalaufwandB.KostenEntlassungen, new Währung(780000.0M));
            CompareAndPrint(personalaufwandB.KostenTraining, new Währung(1445340.0M));
            CompareAndPrint(personalaufwandB.Insgesamt, new Währung(56071340.0M));

            // Test der Plausibilitätsprüfungen

            ProvokeException(() => Berechnungen.Rechnen(null,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        null,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        null,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        -1,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        -1,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        new Prozent(-0.001),
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        new Prozent(1.001),
                                                        trainigstageProMitarbeiterAP,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        -1,
                                                        personalkostenAP,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));
            ProvokeException(() => Berechnungen.Rechnen(volumenNeugeschäftAP,
                                                        durchschnittsgrößenAP,
                                                        volumenProMitarbeiter,
                                                        benötigteMitarbeiterBackOfficeAP,
                                                        vorhandeneMitarbeiterVP,
                                                        fluktuationAP,
                                                        trainigstageProMitarbeiterAP,
                                                        null,
                                                        out anzahlen,
                                                        out benötigteMitarbeiter,
                                                        out personalaufwand));

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
