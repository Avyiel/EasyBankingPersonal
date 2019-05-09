# Vorlesung Programmieren 2
## Hausaufgabe im Sommersemester 2019

## Prof. Dr. Sven C. Martin, Hochschule Karlsruhe


Die Hausaufgabe besteht in einem Programm mit den folgenden Aufgaben:

In Vorgriff auf die Vorlesung ‚Finanzwirtschaft‘ im 3. Semester, in der Sie in einem Planspiel die Rolle eines Bankvorstands übernehmen und aufgrund von Geschäftsberichten Entscheidungen fällen werden. Für die Aufgabe soll ein solcher Geschäftsbericht programmiert werden. Dazu müssen Sie

- eine Datenbank auslesen, 
- die ausgelesenen Rohdaten in darzustellende Daten umrechnen, 
- die errechneten Daten in einer einfachen grafischen Oberfläche darstellen.

Konkret geht es um einen Geschäftsbericht, der die erwarteten Personalaufwände einer Bank darstellt. Aus den in einer Datenbank enthaltenen Angaben zu den Geschäftstätigkeiten wird das Volumen des Neugeschäfts einer Planungsperiode berechnet und daraus die Anzahl der benötigten Mitarbeiter im Front Office abgeleitet. Daraus, aus der Fluktuation (Abwanderung) sowie den Arbeitszeitverlusten durch das Training der Mitarbeiter werden die Anzahlen der Einstellungen bzw. Entlassungen und daraus schlussendlich die erwarteten Personalaufwände berechnet.

Die Datenbank enthält die folgenden Tabellen:

- Der Geschäftsbericht bezieht sich auf eine bestimmte Periode, die ein Jahr dauert. Die erfassten Perioden sind in der Tabelle Perioden enthalten.
- Die Bank vertreibt sechs Produkte: Konsumkredite, Autokredite, Hypothekenkredite, Girokonten, Spareinlagen und Termingelder. Die Tabelle Durchschnittsgrößen enthält die durchschnittlichen Größen dieser Produkte für eine abgelaufene Periode in T€.
- Die Tabelle VolumenNeugeschäft enthält die Volumina der Neugeschäfte in den abgelaufenen Perioden für jedes einzelne der sechs Produkte in Einheiten von 10Mio€.
- Die Tabelle Mitarbeiter enthält die Anzahlen der Mitarbeiter sowie Fluktuation (im Dezimalsystem, also z.B. 0,15 für 15% Mitarbeiterverlust durch freiwillige Kündigungen) und Trainingstage in den abgelaufenen Perioden.
- Die Tabelle Personalkosten enthält die Durchschnittsjahresgehälter sowie die Kosten für je eine Einstellung oder Entlassung und je Tag Training in €.
- Die Tabelle VolumenProMitarbeiter enthält die Anzahl der Produkte, die ein Mitarbeiter im Front Office in einer Periode bearbeiten kann.


Der Ablauf ist recht einfach:

- Die anzuzeigende Datenbank wird ausgewählt (grafische Darstellung, Tipp: Klasse OpenFileDialog aus dem Namensraum Microsoft.Win32).
- Nach erfolgreichem Laden der Datenbank wird die zuletzt abgelaufene Periode ermittelt.
- Das Hauptfenster wird angezeigt. Die Werte der zuletzt abgelaufenen Periode werden als unveränderliche historische Werte oder als Vorgaben angezeigt (grafische Darstellung).
- Die Vorgaben können abgeändert werden. Nach Betätigung der Schaltfläche Berechnung werden die Aufwände berechnet und dargestellt (grafische Darstellung).


Das Programm folgt einer Dreischichtenarchitektur mit je einem Namensraum pro Schicht:

- Der Namensraum Datenhaltung enthält die Klasse zum verkapselten Zugriff auf die Datenbank. Sie liefert die ausgelesenen Daten.
- Der Namensraum Datenverarbeitung enthält die Berechnung der Aufwände aus den ausgelesenen Daten.
- Der Namensraum Präsentation enthält eine Klasse zur Darstellung des Fensters.
- Der zusätzliche Namensraum Datenaustausch enthält Klassen für den Transport der Informationen aus der Datenhaltungsschicht.

Zur Übersicht gibt es ein Klassendiagramm. Details zur Implementierung und zur Berechnung finden Sie auf diesen HTML-Hilfeseiten.


Randbedingungen:

- Die Lösung muss mit C# erstellt werden. Für den Namensraum Präsentation muss die Windows Presentation Foundation mit XAML verwendet werden.
- Achten Sie auf Wiederverwendung -- doppelter Code sollte vermieden werden.
- Die Klassen müssen verkapselt sein: Zugriff auf die Inhalte nur über öffentliche Methoden und Eigenschaften.
- Die Klassen müssen exakt so gestaltet sein wie im Klassendiagramm und den HTML-Hilfeseiten vorgegeben. Achten Sie auch neben der Syntax auf die Punkte 'Ausnahmen' und 'Anmerkungen'.
- Der Namensraum Präsentation kann frei gestalten werden (muss aber natürlich auf den anderen Klassen aufsetzen). Für ihn gilt aber:
	
	- Die Lösung muss alle Angaben der Beispielseite enthalten.
	- Die Lösung muss fehlerhafte Eingaben abfangen (nicht nur fehlerhafte Darstellungen von Werten, sondern auch fehlerhafte Werte selbst).
	- Sobald auch nur eine Vorgabe geändert wurde dürfen zuvor errechneten Werte nicht mehr angezeigt werden.

- Legen Sie für jede Klasse eine eigene Datei an. Jede Datei muss Name und Matrikelnummer des Autors beinhalten.
- Die Klassen und deren öffentliche Member müssen Dokumentationskommentare aufweisen.
- Zu jedem der Namensräume werden Tests bereitgestellt. Bitte prüfen Sie, dass Ihre Lösung mit dem jeweiligen Test funktioniert, bevor Sie sie einreichen.
- Die Aufgabe wird in Gruppen zu 3, 4 oder 5 Studierenden bearbeitet.

	- Jedes Gruppenmitglied ist für genau einen Namensraum verantwortlich.
	- Bei Dreiergruppen entfällt der Namensraum Präsentation; benutzen Sie stattdessen die Konsole zu Testzwecken (ohne Verantwortlichkeit dafür). Bei Vierer- oder Fünfergruppen kümmern sich ein bzw. zwei Studierende um den Namensraum Präsentation.
	- Sie können gerne einander helfen. Sollte jedoch eine Lösung, verglichen mit einer anderen, keine Eigenleistung erkennen lassen, dann werden beide disqualifiziert, d.h. es gibt in diesem Semester keine Zulassung/Punkte. (Und, nein, es reicht nicht als Eigenleistung, Variablennamen oder Kommentare zu ändern.) Jede Einreichung wird auf Plagiat geprüft!
	- Jede Gruppe schickt mir bitte bis zum 10.05.19 24h eine Mail mit den Gruppenmitgliedern (Name und Matrikelnummer) sowie den jeweiligen Verantwortlichkeiten. Wer keine Gruppe findet oder nur zu zweit ist, kann sich bei mir bis zum o.g. Termin melden. Ich werde die Betroffenen dann Gruppen zuordnen. Danach gibt es noch bis zum 17.05.19 24h die Möglichkeit einer Korrektur. Danach sind Änderungen nicht mehr möglich.
	- Jeder Teilnehmer reicht die Quellcodedateien (Namensraum Präsentation: XAML- und Code-Behind-Dateien) seines Namensraums in das Übungsmodul im ILIAS ein.Die Einreichung kann jederzeit erfolgen und auch nachträglich bis zum Abgabezeitpunkt korrigiert werden. Bitte beachten Sie die verschiedenen Abgabezeitpunkte:



|                   | Erstversuch | Zweitversuch |
|-------------------|-------------|--------------|
| Datenaustausch    | 22.05.19    | 05.06.19     |
| Datenhaltung      | 05.06.19    | 19.06.19     |
| Datenverarbeitung | 05.06.19    | 19.06.19     |
| Präsentation      | 12.06.19    | 26.06.19     |

Wer im Erstversuch erfolgreich abgibt erhält 3 Punkte für die Klausur (die Kappung bei 10 Punkten bleibt aber bestehen).

Wer teilnimmt, obwohl er bereits eine Zulassung hat, kann sich anstelle der Zulassung 5 Punkte gutschreiben lassen. Auch hier bleibt die Kappung bei 10 Punkten bestehen.

Für Fragen zur Hausaufgabe gibt es im ILIAS ein Forum.

