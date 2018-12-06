USE StadsApp;

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
(11, 'Kattestraat', '23')
SET IDENTITY_INSERT Address OFF

SET IDENTITY_INSERT Store ON
INSERT INTO Store(StoreId, Name, Description, ImgPath, CategoryId) VALUES
(1, 'Mister Spaghetti', 'A Yummy Spaghetti and Pasta Restaurant in Aalst? Dat is onze focus bij Mister Spaghetti Aalst.', 'https://stadsapprestapi.azurewebsites.net/img/1.jpg', 1),
(2, 'H&M', 'H&M biedt een uitgebreid assortiment mode voor dames, heren, jongeren en kinderen. We bieden mode en kwaliteit tegen de beste prijs op een duurzamere manier.', 'https://stadsapprestapi.azurewebsites.net/img/2.jpg', 5),
(3, 'Kruidvat', 'Onder het motto ‘Steeds verrassend, altijd voordelig’ is Kruidvat de onbetwiste marktleider binnen de drogisterijbranche. Ook online hanteert Kruidvat lage prijzen en een aantrekkelijk en verrassend assortiment.', 'https://stadsapprestapi.azurewebsites.net/img/3.jpg', 8),
(4, 'Brantano', 'Ruim aanbod schoenen, diverse merken & de nieuwste modetrends. Koop of reserveer je schoenen online bij schoenenwinkel Brantano.', 'https://stadsapprestapi.azurewebsites.net/img/4.png', 5),
(5, 'Standaard Boekhandel', 'Standaard Boekhandel: Ruim aanbod aan Boeken, E-books, Muziek, Film, Games, Fotos. Altijd een Standaard Boekhandel dicht in je buurt, bestel ook online.', 'https://stadsapprestapi.azurewebsites.net/img/5.jpg', 2),
(6, 'Tom & Co', 'In onze winkel ontmoet je dynamische en gepassioneerde experts. Ze zullen je helpen het beste te kiezen voor je huisdier of zijn komst voor te bereiden!', 'https://stadsapprestapi.azurewebsites.net/img/6.jpg', 3),
(7, 'Krëfel', 'Voor al jouw aankopen van tv, hifi, video, multimedia, fotografie, telecom en elektro is onze befaamde slogan meer geworden dan een belofte: het is een engagement.', 'https://stadsapprestapi.azurewebsites.net/img/7.jpg', 4),
(8, 'Prima Meubelen', 'PRIMA meubelen is gespecialiseerd in het bemeubelen van appartementen en studio’s.', 'https://stadsapprestapi.azurewebsites.net/img/8.png', 6),
(9, 'Ellis Gourmet Burger', 'Ellis is het concept van een groep enthousiastelingen met één doel voor ogen: betere burgers serveren. Ja, we gebruiken absoluut de beste producten, maar verfijnen onze recepten ook continu met de hulp van bekende productspecialisten en sterrenchefs.', 'https://stadsapprestapi.azurewebsites.net/img/9.png', 1),
(10, 'Lab9', 'Lab9 is uw Apple Premium Reseller & Service Provider in West-Vlaanderen en Oost-Vlaanderen.', 'https://stadsapprestapi.azurewebsites.net/img/10.png', 4)
SET IDENTITY_INSERT Store OFF

SET IDENTITY_INSERT Establishment ON
INSERT INTO Establishment(EstablishmentId, ImgPath, Visited, AddressId, StoreId) VALUES
(1, 'https://stadsapprestapi.azurewebsites.net/img/establishments/1.jpg', 156, 1, 1),
(2, 'https://stadsapprestapi.azurewebsites.net/img/establishments/2.jpg', 84, 2, 2),
(3, 'https://stadsapprestapi.azurewebsites.net/img/establishments/3.png', 25, 3, 3),
(4, 'https://stadsapprestapi.azurewebsites.net/img/establishments/4.jpg', 13, 4, 3),
(5, 'https://stadsapprestapi.azurewebsites.net/img/establishments/5.jpg', 136, 5, 4),
(6, 'https://stadsapprestapi.azurewebsites.net/img/establishments/6.jpg', 87, 6, 5),
(7, 'https://stadsapprestapi.azurewebsites.net/img/establishments/7.png', 45, 7, 6),
(8, 'https://stadsapprestapi.azurewebsites.net/img/establishments/8.png', 96, 8, 7),
(9, 'https://stadsapprestapi.azurewebsites.net/img/establishments/9.JPG', 150, 9, 8),
(10, 'https://stadsapprestapi.azurewebsites.net/img/establishments/10.jpg', 236, 10, 9),
(11, 'https://stadsapprestapi.azurewebsites.net/img/establishments/11.jpg', 71, 11, 10)
SET IDENTITY_INSERT Establishment OFF

SET IDENTITY_INSERT Event ON
INSERT INTO Event(EventId, Name, Description, Visited, EstablishmentId) VALUES
(1, 'Opendeurdag', 'Neem een kijkje achter de schermen in Lab9', 12, 11)
SET IDENTITY_INSERT Event OFF

SET IDENTITY_INSERT Promotion ON
INSERT INTO Promotion(PromotionId, Name, Visited, StoreId) VALUES
(1, '3 DVD''s kopen, een vierde gratis', 80, 3),
(2, '10% korting op alle damesschoenen', 43, 4)
SET IDENTITY_INSERT Promotion OFF

SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([UserId], [FirstName], [LastName], [UserName], [PasswordHash], [PasswordSalt]) VALUES (1, N'Tiel', N'Van Hecke', N'TielTaxi', 0xF8C714928E0A8248F81010BEBE3E5D97BD1D8AA975E8078ADA4DD151764FE76215105CC127BB7AB6EAD709AEA545D406D12637D86163309CB40410A3C2012F38, 0x154A7F8D231F7E3CB000266A066EEC7D2121A3B341DE1F73B40521A5CBC27AF0D0BF71EACAA192734223298AF9EF1F1568E9CB4D7AE5A0373EC1F40F6706E7B8A82EA840BA785A70611A3914010FBD409EA65CE7BBAEDCFEE88F9B4C8099821F176E6B57AB579D617C1ACE4188C297D331B1C450138FD8AB3740DFD38A88F55B)
SET IDENTITY_INSERT [dbo].[User] OFF

INSERT INTO UserEstablishment (UserId, EstablishmentId) VALUES
(1, 3),
(1, 10),
(1, 11)