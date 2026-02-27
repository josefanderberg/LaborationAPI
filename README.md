# RecipeApi - Laboration

Detta är ett enkelt Web API för att hantera recept, skapat som en del av en laboration. Applikationen är byggd med .NET 9.

## Funktioner

- **Recepthantering**: Skapa, läsa, uppdatera och ta bort (CRUD) recept.
- **Ingredienser**: Hantera ingredienser kopplade till recepten.
- **Swagger**: Inbyggd dokumentation och testning via Swagger UI.

## Teknisks stack

- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **Dokumentation**: Swagger (Swashbuckle)
- **Arkitektur**: Repository- och Service-mönster

## Kom igång

### Förutsättningar

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Köra projektet lokalt

1. Klona repot eller ladda ner källkoden.
2. Öppna en terminal i mappen `RecipeApi`.
3. Kör följande kommando:
   ```bash
   dotnet run
   ```
4. Öppna webbläsaren på `http://localhost:5011/swagger` (eller den port som visas i terminalen) för att se API-dokumentationen.

## Tester

För att köra enhetstesterna, navigera till rotmappen och kör:
```bash
dotnet test
```

## Projektstruktur

- `RecipeApi`: Själva API-applikationen.
- `RecipeApi.Tests`: Enhetstester för applikationen.
- `Models`: Innehåller DTO:er och domänmodeller.
- `Repositories`: Datatillgångslagret (använder in-memory lagring i denna laboration).
- `Services`: Affärslogik.
