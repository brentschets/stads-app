USE StadsApp

SET IDENTITY_INSERT Category ON
INSERT INTO Category(CategoryId, Name) VALUES
(1, 'Eten & Drinken'),
(2, 'Boeken'),
(3, 'Dieren'),
(4, 'Elektronica'),
(5, 'Kleding'),
(6, 'Wonen'),
(7, 'Speelgoed'),
(8, 'Andere')
SET IDENTITY_INSERT Category OFF

SET IDENTITY_INSERT Address ON
INSERT INTO Address(AddressId, Street, Number) VALUES
(1, 'Hopmarkt', '33'),
(2, 'Nieuwstraat', '27/29'),
(3, 'Nieuwstraat', '29'),
(4, 'Kapiteintjesstraat', '19'),
(5, 'Brusselse Steenweg', '120'),
(6, 'Nieuwstraat', '38'),
(7, 'Geraardsbergsestraat', '260'),
(8, 'Gentsesteenweg', '536'),
(9, 'Hageveld', '4'),
(10, 'Kerkstraat', '3'),
(11, 'Kattestraat', '23'),
(12, 'Geraardsbergsestraat', '2'),
(13, 'Kerkstraat','1'),
(14, 'Sint Jansstraat', '44'),
(15, 'Kerkstraat','9'),
(16, 'Kattestraat', '31'),
(17, 'Kattestraat', '35'),
(18, 'Lange Zoutstraat','21'),
(19, 'Kerkstraat', '4'),
(20, 'Gentsesteenweg', '327'),
(21, 'Geraadsbergsestraat', '260'),
(22, 'Hoveniersstraat', '1'),
(23, 'Moorselbaan', '631'),
(24, 'Gentsesteenweg', '351A'),
(25, 'Kerkstraat', '45'),
(26, 'Moorselbaan', '631'),
(27, 'Baardegem-Dorp', '89'),
(28, 'Pierre Corneliskaai', '17'),
(29, 'Nieuwstraat', '18'),
(30, 'Nieuwstraat', '41/43'),
(31, 'Esplanadeplein', '1'),
(32, 'Waverstraat', '16'),
(33, 'Korte Zoutstraat', '17'),
(34, 'Moorselbaan', '448'),
(35, 'Dendermondsesteenweg', '75'),
(36, 'Gentsesteenweg', '180'),
(37, 'Kattestraat' , '31'),
(38, 'Kattestraat', '4'),
(39, 'Kattestraat', '30'),
(40, 'Moorselbaan', '39'),
(41, 'Kapiteintjesstraat', '11/4'),
(42, 'Nieuwstraat', '53/55'),
(43, 'Grote Markt', '23/25')


SET IDENTITY_INSERT Address OFF

SET IDENTITY_INSERT Store ON
INSERT INTO Store(StoreId, Name, Description, ImgPath, CategoryId) VALUES
(1, 'Mister Spaghetti', 'A Yummy Spaghetti and Pasta Restaurant in Aalst? Dat is onze focus bij Mister Spaghetti Aalst.', '/img/1.jpg', 1),
(2, 'H&M', 'H&M biedt een uitgebreid assortiment mode voor dames, heren, jongeren en kinderen. We bieden mode en kwaliteit tegen de beste prijs op een duurzamere manier.', '/img/2.jpg', 5),
(3, 'Kruidvat', 'Onder het motto ‘Steeds verrassend, altijd voordelig’ is Kruidvat de onbetwiste marktleider binnen de drogisterijbranche. Ook online hanteert Kruidvat lage prijzen en een aantrekkelijk en verrassend assortiment.', '/img/3.jpg', 8),
(4, 'Brantano', 'Ruim aanbod schoenen, diverse merken & de nieuwste modetrends. Koop of reserveer je schoenen online bij schoenenwinkel Brantano.', '/img/4.png', 5),
(5, 'Standaard Boekhandel', 'Standaard Boekhandel: Ruim aanbod aan Boeken, E-books, Muziek, Film, Games, Fotos. Altijd een Standaard Boekhandel dicht in je buurt, bestel ook online.', '/img/5.jpg', 2),
(6, 'Tom & Co', 'In onze winkel ontmoet je dynamische en gepassioneerde experts. Ze zullen je helpen het beste te kiezen voor je huisdier of zijn komst voor te bereiden!', '/img/6.jpg', 3),
(7, 'Krëfel', 'Voor al jouw aankopen van tv, hifi, video, multimedia, fotografie, telecom en elektro is onze befaamde slogan meer geworden dan een belofte: het is een engagement.', '/img/7.jpeg', 4),
(8, 'Prima Meubelen', 'PRIMA meubelen is gespecialiseerd in het bemeubelen van appartementen en studio’s.', '/img/8.png', 6),
(9, 'Ellis Gourmet Burger', 'Ellis is het concept van een groep enthousiastelingen met één doel voor ogen: betere burgers serveren. Ja, we gebruiken absoluut de beste producten, maar verfijnen onze recepten ook continu met de hulp van bekende productspecialisten en sterrenchefs.', '/img/9.png', 1),
(10, 'Lab9', 'Lab9 is uw Apple Premium Reseller & Service Provider in West-Vlaanderen en Oost-Vlaanderen.', '/img/10.png', 4),

(11, 'De Prins Drinkt Koffie','De Prins drinkt koffie is een trendy, hippe koffiebar aan de rand van de stad, met een ruime mogelijkheid tot parkeren.','/img/11.jpg',1),
(12, 'De Zwarte Kat','Sinds 1994 zijn Steven en Annick de gastheren van het restaurant, met zicht op het oudste Belfort der Nederlanden.','/img/12.jpg',1),
(13, 'Den Den','Café Den Den in de Sint-Janstraat, onder carnavalisten bekend als het café waar je moét geweest zijn.','/img/13.jpg',1),
(14, 'In ''t Filetpuurken','Midden in het centrum van Aalst bevindt zich Brasserie In''t Filetpurken. In''t Filetpurken is de ideale plaats om lekker te eten in een gezellig en warm kader.','/img/14.jpg',1),

(15, 'De Slegte', 'Op zoek naar een origineel boek dat niet in iedere boekwinkel in de top tien staat? Kijk dan eens bij Boekhandel De Slegte. Wij hebben een uitgebreide collectie bijzondere en voordelige boeken. Zowel in onze winkels als in de Vlaamse webwinkel. Ook kopen wij uw boeken graag in.','/img/15.jpg',2),
(16, 'Club', 'Boekenhandel Club is een van de weinige die er nog is. Kom zeker eens langs bij ons!', '/img/16.jpg', 2),
(17, 'De Press shop', 'Press Shop is een krantenwinkel waar u terecht kan voor pers, rookwaren, snoepgoed, boeken, Nationale Loterij.','/img/17.png',2),
(18, 'Dallas', 'Dit is een krantenwinkel waar u terecht kan voor bijna alles.','/img/18.png',2),

(19, 'Maxi Zoo', 'Op zoek naar een kwalitatieve dierenwinkel bij jou in de buurt? Al je dierenbenodigdheden vind je bij dierenspeciaalzaak Maxi Zoo Aalst','/img/19.png',3),
(20, 'Tom & Co', 'Onze winkel bevindt zich in de commerciële zone van Delhaize, vlakbij het Osbroek bos van Aalst. ','/img/20.png',3),
(21, 'Dunkie', 'Breng vrijblijvend een bezoekje aan onze winkel in Aalst, missschien hebben wij het hondje dat u zoekt.','/img/21.png',3),
(22, 'Schockaert Rud', 'Diensten en producten: Dierenvoeding, Tuinbenodigdheden, Bloemen -en groenteplanten, Duivenvoer, Paardenvoer.','/img/22.png',3),

(23, 'Van Den Borre', 'De specialist in TV, video, hifi, elektro, multimedia en GSM in België. Gemakkelijk te vergelijken en online kopen uit een ruime keuze.','/img/23.jpg',4),
(24, 'Mediamarkt', 'Computer & Multimedia - Telefoon & Navigatie - Televisie & Audio - Foto & Video - Keuken - Huishouden & Verzorging - Gaming & Entertainment - ...','/img/24.jpg',4),
(25, 'Selexion', 'Eenvoudig online vergelijken & bestellen op onze Selexion webshop, de specialist in TV, Audio, Foto, IT, Telefonie, Elektro & Cookware. Ontdek ons gamma!','/img/25.png',4),
(26, 'Electrostock', 'Bij ElectroStock hebben we bijna alles op voorraad in de winkel! Wilt u meteen van uw aankoop kunnen genieten, dan moet u bij ElectroStock zijn!','/img/26.png',4),

(27, 'C&A', 'Ontdek modieuze kleding voor dames, heren en kinderen bij C&A! Topkwaliteit ? Duurzame producten.','/img/27.png',5),
(28, 'CoolCat', 'Kleding in allerlei stijlen, kleuren & prints. Go get yours ? Shop nu! Snelle Levering. Gratis Ophalen in Winkels. Typen: New, Sale, Basics, Bestsellers, Party wear, Festival outfits.','/img/28.jpg',5),
(29, 'TIFFANYS Dames', 'Bij Dressed by Tiffanys & Trent kan je steeds terecht voor sterke modemerken. Beleef je mode bij ons en bezoek onze merken pagina.','/img/29.png',5),
(30, 'Sint Elooi', 'Sint Elooi is een gevestigde waarde in het centrum van Aalst. Deze familiezaak bestaat al meer dan 70 jaar.','/img/30.png',5),

(31, 'Van Praet Interieur & Kantoorinrichting', 'Bij VAN PRAET kunt u terecht om uw woonwerelden volledig te laten aanpassen aan uw wensen en verwachtingen. Ons team van ervaren interieurarchitecten zullen u graag begeleiden en adviseren, geheel kosteloos, om samen de mooiste meubelstukken in te passen in uw plannen.','/img/31.png',6),
(32, 'Euromeubels', 'Voor alle meubels moet u bij Euromeubel!','/img/32.png',6),
(33, 'Merckx Aalst Teak', 'Merckx Meubelen. In en outdoor meubelen. Extraordinary furniture for extraordinary people. Teak meubelen.','/img/33.png',6),
(34, 'Leen Bakker', 'Mooi wonen voor lage prijzen. En altijd verrassende acties. Bezoek één van onze Leen Bakker winkels of bestel online.','/img/34.jpg',6),

(35, 'De Speelvogel', 'Wij bieden enkel leuk, kwalitatief en verantwoord speelgoed.','/img/35.png',7),
(36, 'Bart Smit Speelgoedwinkel', 'Het beste speelgoed vindt u in de Bart Smit.','/img/36.jpg',7),
(37, 'Modelbouw', 'Enkel en alleen de gevel is prachtig! Binnen is het een waar paradijs voor de modelbouwers.','/img/37.png',7),
(38, 'Fox & Cie Aalst', 'Fox & Cie is de beste speelgoed winkel in Aalst.','/img/38.png',7),

(39, 'Eurotuin', 'Eurotuin biedt je in 3 vestigingen en in de webshop een groot assortiment voor huis, tuin en dier. ','/img/39.jpg',8),
(40, 'Action', 'Welkom bij Action in Aalst. In onze winkel vind je een uitgebreid assortiment aan woonartikelen, decoratie, kantoorartikelen, gereedschap, accessoires, ...','/img/40.jpg',8),
(41, 'Veritas', 'Voor al uw naaigerief kom bij ons!','/img/41.jpg',8),
(42, 'Zeeman', 'Alles voor de feestdagen vindt u bij ons!','/img/42.jpg',8)
SET IDENTITY_INSERT Store OFF

SET IDENTITY_INSERT Establishment ON
INSERT INTO Establishment(EstablishmentId, ImgPath, Visited, AddressId, StoreId) VALUES
(2, '/img/establishments/2.jpg', 84, 2, 2),
(3, '/img/establishments/3.PNG', 25, 3, 3),
(4, '/img/establishments/4.jpg', 13, 4, 3),
(5, '/img/establishments/5.jpg', 136, 5, 4),
(6, '/img/establishments/6.jpg', 87, 6, 5),
(1, '/img/establishments/1.jpg', 156, 1, 1),
(7, '/img/establishments/7.PNG', 45, 7, 6),
(9, '/img/establishments/9.JPG', 150, 9, 8),
(8, '/img/establishments/8.PNG', 96, 8, 7),
(10, '/img/establishments/10.jpg', 236, 10, 9),
(11, '/img/establishments/11.jpg', 71, 11, 10),

(12, '/img/establishments/12.jpg', 123, 13, 12),
(13, '/img/establishments/13.jpg', 123, 14, 13),
(14, '/img/establishments/14.jpg', 123,15,14),
(15, '/img/establishments/15.jpg',34,12,11),

(16, '/img/establishments/16.jpg', 43,16,16),
(17, '/img/establishments/17.jpg', 45, 17,15),
(18, '/img/establishments/18.jpg', 45, 18,17),
(19, '/img/establishments/19.png', 45, 19,18),

(20, '/img/establishments/20.png', 45, 20, 19),
(21, '/img/establishments/21.png', 45, 21, 20),
(22, '/img/establishments/22.png', 45, 22, 21),
(23, '/img/establishments/23.png', 45, 23, 22),

(24, '/img/establishments/24.jpg', 45, 24, 23),
(25, '/img/establishments/25.jpg', 45, 25, 24),
(26, '/img/establishments/26.png', 45, 26, 25),
(27, '/img/establishments/27.png', 45, 27, 26),

(28, '/img/establishments/28.jpg', 45, 28, 27),
(29, '/img/establishments/29.jpg', 45, 29, 28),
(30, '/img/establishments/30.jpg', 45, 30, 29),
(31, '/img/establishments/31.jpg', 45, 31, 30),

(32, '/img/establishments/32.jpg', 45, 32, 31),
(33, '/img/establishments/33.jpg', 45, 33, 32),
(34, '/img/establishments/34.jpg', 45, 34, 33),
(35, '/img/establishments/35.jpg', 45, 35, 34),

(36, '/img/establishments/36.jpg', 45, 36, 35),
(37, '/img/establishments/37.jpg', 45, 37, 36),
(38, '/img/establishments/38.jpg', 45, 38, 37),
(39, '/img/establishments/39.jpg', 45, 39, 38),

(40, '/img/establishments/40.jpg', 45, 40, 39),
(41, '/img/establishments/41.jpg', 45, 41, 40),
(42, '/img/establishments/42.jpg', 45, 42, 41),
(43, '/img/establishments/43.jpg', 45, 43, 42)

SET IDENTITY_INSERT Establishment OFF

SET IDENTITY_INSERT Event ON
INSERT INTO Event(EventId, Name, Description, Visited, EstablishmentId) VALUES
(1, 'Opendeurdag Lab9', 'Neem een kijkje achter de schermen in Lab9', 12, 11),
(2, 'Opendeurdag Maxi Zoo', 'Kom eens kijken naar hoe wij instaan voor alle dieren', 9, 20),
(3, 'Openbedrijvendag', 'Kom een kijkje nemen naar hoe wij achter de schermen werken', 13, 34)
SET IDENTITY_INSERT Event OFF

SET IDENTITY_INSERT Promotion ON
INSERT INTO Promotion(PromotionId, Name, Visited, StoreId) VALUES
(1, '3 DVD''s kopen, een vierde gratis', 80, 3),
(2, '10% korting op alle damesschoenen', 43, 4),
(3, 'T-Shirts voor maar 2 euro', 50, 42),
(4, '2 zakken hondenbrokken en een gratis speeltje', 60, 19)
SET IDENTITY_INSERT Promotion OFF
