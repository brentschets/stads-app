# Stads-App groep A2

Van Hecke Tiel
Schets Brent
https://stadsappinstall.azurewebsites.net

# Offline uitvoeren

## Database aanmaken

Om de app offline te kunnen uitvoeren, moet eerst de database aangemaakt worden. Hiervoor is een named instance (SQLSERVER) van Microsoft SQL Server nodig. Verbind met de SQL server via SQL Server Management Studio of een andere IDE. Voer het bijgeleverde SQL script 'dummydatascript.sql' uit.

## Broncode aanpassen

De broncode van dit project is ingesteld om de offline database te gebruiken. Controleer dit vooralleer het project uit te voeren.

### RESTAPI

- 'Startup.cs': De connectionstring 'Local' moet gebruikt worden
- 'Utils/Appsettings.cs': De URL voor localhost moet gebruikt worden. Zorg ervoor dat deze niet in commentaar staat en de URL voor de Azure website in commentaar staat. 

### Stads-App

- 'Utils/StadsAppRestApiClient.cs': De constructor die aangeduid staat met 'local' moet gebruikt worden. De constructor die met 'deploy' aangeduid is moet in commentaar staan.
- 'Utils/StadsAppRestApiClient.cs': De URL voor localhost moet gebruikt worden, deze staat aangeduid met 'local'. De URL voor de online Rest API moet in commentaar staan.

## User Secret

Opdat de Rest API correct zou werken, moet men het backend geheim instellen. Dit kan door in Visual Studio met de rechtermuisknop op het project 'RESTAPI' te klikken en 'Manage User Secrets' te selecteren. Plaats daar vervolgens deze tekst:

    {
        "JWTSecret":  "DE KAT KRABT DE KROLLEN VAN DE TRAP"
    }

## Deploy app

Ga met de rechtermuisklik op de solution in Visual Studio naar de Configuration Manager en vink 'Build' en 'Deploy' aan bij het Stads-App project.

## Volgorde van uitvoeren

Voor een goed verloop moet de Rest API voor de app opgestart worden. Dit kan handmatig door de eerst de Rest API uit te voeren in Debug mode (dit is noodzakelijk) en daarna de app. Het is makkelijker om de startup sequentie in de solution aan te passen.

Dat kan door in Visual Studio met de rechtermuisknop op de solution te klikken en 'Properties' te selecteren. Onder 'Common Properties' -> 'Startup Project' selecteert u dan 'Multiple startup projects' en zet voor beide projecten 'Action' op 'Start'. Zorg er ook voor dat het RESTAPI project boven het Stads-App project staat in de lijst. Start dan beide projecten met de 'Start' knop bovenaan in Visual Studio.
    
# Online uitvoeren

Alternatief kan de app via de online Rest API gebruikt worden. Hiervoor is geen lokale database nodig. Ga daarvoor naar de website https://stadsappinstall.azurewebsites.net.

## Certificaat installeren

Vooralleer de app geïnstalleerd kan worden, moet men eerst het certificaat downloaden. Dit kan via het menu 'Additional links' -> 'Publisher Certificate'. Installeer het certificaat in de 'Trusted Root Certification Authorities' store.

## App installeren

Als het certificaat geïnstalleerd is, kan men de app downloaden. Klik op 'Get the app'. Dit is een magnet link. Eventueel vraagt uw browser u om bevestiging om App Installer to openen. Bevestig dat u de app wilt installeren.

## Automatische updates

Wanneer u de app via de online website installeert, worden eventuele toekomstige updates automatsich geïnstalleerd. U hoeft de app dus niet steeds opnieuw te installeren.

# Accounts

Om de functionaliteit van de app uit te testen, bevat het dummydata script twee accounts: één normale gebruiker en één handelaar.

## Bezoeker

De logingegevens van de normale gebruiker zijn:

- Gebruikersnaam: Brent
- Wachtwoord: Brent

Deze gebruiker is al geabonneerd op drie winkels.

## Handelaar

De logingegevens van de handelaar zijn:

- Gebruikersnaam: Tiel
- Wachtwoord: Tiel

Deze gebruiker is de eigenaar van ALDI. Deze winkel heeft momenteel één vestiging, maar men kan er meer toevoegen (er zijn drie ALDI's in Aalst).

