\# KI-Nutzung



\*\*Tool:\*\* Claude (claude.ai)



\## Prompts



\### 1. Funktionale Anforderungen (26.10.2026)

> Nenne mindestens 10 funktionale Anforderungen für eine Gastronomieverwaltung

> eines Schwimmbadkiosks (Bestand und Verkauf), jeweils als „Das System muss …“.



Antwort: `planung/ki\_anforderungen\_roh.txt`

==============================================================
  KI-PROMPTS IM PROJEKT GASTRONOMIEVERWALTUNG
  Verwendetes Tool: Claude (Anthropic), claude.ai
==============================================================

Die Prompts sind wörtlich übernommen. Bei diesen Fragen hat die
KI nichts erstellt, sondern nur erklärt. Die Antworten sind
gekürzt.


--------------------------------------------------------------
PROMPT 1 – Arbeiten in der Schule und zu Hause (23.10.2026)
--------------------------------------------------------------
Prompt:
"Da ich zuhause das laufwerk H nicht habe wie greife ich drauf
zu"

Antwort der KI (gekürzt):
Das Laufwerk H: wird zu Hause nicht gebraucht, weil GitHub den
Austausch zwischen Schule und zu Hause übernimmt. Zu Hause wird
das Repository einmalig in einen beliebigen Ordner geklont.
Danach gilt an beiden Orten dieselbe Regel:
- vor dem Arbeiten: git pull (neuesten Stand holen)
- nach dem Arbeiten: git add, git commit, git push
Wichtig: In der Schule immer pushen, bevor man geht, sonst
liegen die Änderungen nur auf H:.


--------------------------------------------------------------
PROMPT 2 – Bedeutung der Git-Befehle (27.10.2026)
--------------------------------------------------------------
Prompt:
"was machen die befehle"

Antwort der KI (gekürzt):
Erklärt am Beispiel eines Pakets:
- git add .     legt geänderte Dateien ins Paket (vormerken)
- git commit -m klebt das Paket zu und beschriftet es; der
                Zwischenstand ist mit Datum gespeichert, aber
                nur auf dem eigenen PC
- git push      schickt das Paket ab, erst dann ist es auf
                GitHub sichtbar
- git pull      holt Pakete ab, die an einem anderen PC
                verschickt wurden
- &&            führt den nächsten Befehl nur aus, wenn der
                vorherige geklappt hat
Ablauf immer: pull, arbeiten, add, commit, push.


--------------------------------------------------------------
PROMPT 3 – Zählt ein Upload über die Webseite? (28.10.2026)
--------------------------------------------------------------
Prompt:
"zält das al comit"

Antwort der KI (gekürzt):
Ja. Ein Upload über die GitHub-Webseite erzeugt einen normalen
Commit mit Nachricht und Datum und zählt auch für die
"5 verschiedenen Tage". GitHub vermerkt aber, dass er über die
Webseite entstanden ist. Das ist kein Problem, solange die
meisten Commits über die Kommandozeile laufen, denn die Aufgabe
verbietet nur, ausschließlich den Upload zu benutzen.


--------------------------------------------------------------
PROMPT 4 – Speicherort der Bestelldatei (29.10.2026)
--------------------------------------------------------------
Prompt:
"ist das egal wo die txt liegt"

Antwort der KI (gekürzt):
Für die Funktion ist es egal, solange das Programm dort
schreiben darf. Der Dokumente-Ordner ist gut geeignet, weil es
ihn auf jedem Windows-Rechner gibt; der Lehrer muss nichts
einstellen. Ungünstig wären:
- ein fester Pfad wie H:\..., den es auf anderen Rechnern
  nicht gibt (das Programm würde einen Fehler melden)
- der Ordner bin\Debug, weil er beim Neukompilieren geleert
  wird und wegen .gitignore nicht auf GitHub landet
Tipp: eine Beispieldatei mit Testbestellungen ins Repository
legen, damit der Lehrer das Format ohne Programmstart sieht.


--------------------------------------------------------------
PROMPT 5 – Stand des Projekts prüfen (29.10.2026)
--------------------------------------------------------------
Prompt:
"Sind jetz alle aufgaben des prokektes bearbeitet"

Antwort der KI (gekürzt):
Noch nicht ganz. Abgleich mit den Projektaufgaben:
Erledigt:
- Git-Repository mit README, drei Ordnern und Commits an
  5 verschiedenen Tagen
- ER-Diagramm (PNG und draw.io) und SQL-Skript
- Anforderungen (roh und final); Begründung und
  Stundenschätzung selbst prüfen
Offen:
- datenbank_design.md mit Begründung der Tabellen,
  Beziehungen und Datentypen
- Papierprototyp als Bild im Ordner planung
- Console-Prototyp: Es wurde WPF statt einer Konsolenanwendung
  gebaut; beim Lehrer nachfragen, ob das zählt
- KI_Nutzung.md vollständig ausfüllen
- README: Name und Hinweis zur Datenbank ergänzen
- Abgabe per E-Mail bis 27.11. um 23:59

==============================================================
