--Ertellen der Datenbank
IF DB_ID('Gastronomieverwaltung') IS NULL
CREATE DATABASE Gastronomieverwaltung;
GO
USE Gastronomieverwaltung;
GO

--Löschen der Tabellen
IF OBJECT_ID('Artikel_Verkauf') IS NOT NULL DROP TABLE Artikel_Verkauf;
IF OBJECT_ID('Artikel') IS NOT NULL DROP TABLE Artikel;
IF OBJECT_ID('Verkauf') IS NOT NULL DROP TABLE Verkauf;
IF OBJECT_ID('Kategorie') IS NOT NULL DROP TABLE Kategorie;
GO

--Erstellen der Tabellen
CREATE TABLE Kategorie (
Kategorie_ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Bezeichnung NVARCHAR(100)
);

CREATE TABLE Artikel (
Artikel_ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Name NVARCHAR(100),
Preis DECIMAL(10,2),
Bestand INT,
Mindestbestand INT,
Kategorie_ID INT NOT NULL,
FOREIGN KEY (Kategorie_ID) REFERENCES Kategorie(Kategorie_ID)
);

CREATE TABLE Verkauf (
Verkauf_ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Zahlungsart NVARCHAR(100),
Zeitpunkt DATETIME
);


CREATE TABLE Artikel_Verkauf (
Artikel_ID INT NOT NULL,
Verkauf_ID INT NOT NULL,
Menge INT,
Einzelpreis DECIMAL(10,2),
PRIMARY KEY (Artikel_ID, Verkauf_ID),
FOREIGN KEY (Artikel_ID) REFERENCES Artikel(Artikel_ID),
FOREIGN KEY (Verkauf_ID) REFERENCES Verkauf(Verkauf_ID)
);

--Eintrag in die Tabellen
INSERT INTO Kategorie(Bezeichnung)
VALUES ('Kalte Getränke'),
       ('Heiße Getränke'),
       ('Warme Speise'),
       ('Süßigkeiten'),
       ('Eis');

INSERT INTO Artikel(Name,Preis,Bestand,Mindestbestand,Kategorie_ID)
VALUES ('Limo',1.40,20,12,1),
       ('Kaffee',1.20,14,8,2),
       ('Pommes',2.20,22,10,3),
       ('Gummibärchen',0.99,9,4,4),
       ('Schokolade',1.00,5,2,4);

INSERT INTO Verkauf(Zahlungsart,Zeitpunkt)
VALUES ('Karte','20191106 11:25'),
       ('Bar','20191106 11:40'),
       ('Bar','20191106 12:10'),
       ('Karte','20191106 12:33'),
       ('Karte','20191106 13:00');

INSERT INTO Artikel_Verkauf(Artikel_ID,Verkauf_ID,Menge,Einzelpreis)
VALUES (1,1,2,1.40),
       (4,1,1,0.99),
       (2,2,1,1.20),
       (3,3,2,2.20),
       (1,3,2,1.40),
       (3,4,1,2.20),
       (5,4,1,1.00),
       (2,5,1,1.20),
       (4,5,2,0.99);

--Abfrage--
SELECT v.Verkauf_ID, v.Zeitpunkt, v.Zahlungsart,
       a.Name, av.Menge, av.Einzelpreis,
       av.Menge * av.Einzelpreis AS Summe
FROM Verkauf v
JOIN Artikel_Verkauf av ON v.Verkauf_ID = av.Verkauf_ID
JOIN Artikel a ON av.Artikel_ID = a.Artikel_ID
ORDER BY v.Verkauf_ID;