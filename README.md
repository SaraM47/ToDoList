# ToDoList
En strukturerad och användarvänlig konsolapplikation byggd i C# (.NET 8.0) för att hantera uppgifter direkt i terminalen.  
Applikationen sparar automatiskt alla uppgifter lokalt i JSON-format och låter användaren lägga till, visa, filtrera, söka, exportera och ta bort uppgifter på ett smidigt sätt.

## Innehåll
- [Funktioner](#funktioner)
- [Teknisk stack](#teknisk-stack)
- [Kom igång](#kom-igång)
- [Projektstruktur](#projektstruktur)
- [Exempel på körning](#exempel-på-körning)
- [Erfarenheter](#erfarenheter)
- [Förbättringsidéer](#förbättringsidéer)

## Funktioner

### Grundläggande funktioner
- Lägg till nya uppgifter med titel, beskrivning och förfallodatum.
- Visa alla uppgifter i listform med tydlig statusmarkering.
- Markera uppgifter som klara eller ta bort dem helt.
- Automatisk lagring och laddning av data via `tasks.json`.

### Utökade funktioner
- **Filtrering och sortering** av uppgifter (via LINQ)
  - Visa endast klara / ej klara
  - Sortera efter förfallodatum  
- **Sökfunktion**  
  - Sök efter uppgifter via titel, beskrivning eller ID  
- **Export till textfil**
  - Skapar `.txt`-fil med samtliga uppgifter i mappen `Exports/`
  - Automatisk tidsstämpling (ex. `ToDoList_Export_20251025_141215.txt`)
- **Färgkodning i konsolen**
  - Grön: bekräftelse / lyckad åtgärd
  - Gul: tips och information
  - Röd: felmeddelande
  - Cyan: huvudrubriker
- **Felhantering och tipsrader**
  - Programmet hanterar ogiltiga inmatningar (t.ex. fel datumformat eller ID)
  - Tydliga återkopplingsmeddelanden och möjlighet att trycka Enter för att gå tillbaka

## Teknisk stack

| Komponent | Användning |
|------------|-------------|
| .NET 8.0 | Ramverk för körning av konsolapplikationen |
| C# | Programmeringsspråk |
| System.Text.Json | Serialisering och deserialisering av uppgifter i JSON |
| System.IO | Filhantering och export till textfiler |
| LINQ | Filtrering, sökning och sortering av data |
| OOP (Objektorientering) | Strukturering av kod i klasser, interfaces och modeller |

## Kom igång

### Förutsättningar
- .NET SDK 8.0 (eller senare) installerad  
- Ingen extern databas krävs – allt sparas lokalt i `Data/tasks.json`

### Kör projektet
```bash
dotnet restore
dotnet run
