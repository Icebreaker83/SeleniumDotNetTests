Ovaj direktorijum sadrži Selenium automatske testove izrađene u .Net Framework-u koristeći [MSTest][mstest] biblioteku.

## Upotreba

Projekat se pokreće za razvoj koristeći Visual Studio 2017 i solution fajl `SeleniumNetTests`. 

Testovi se mogu izvršiti iz PowerShell-a upotrebom [Invoke-Build][ib] PowerShell skripta koji je uključen u repozitorijum.

Visual Studio ne mora biti instaliran za pokretanje (npr. na **build serveru** ili korišćenjem *Visual Studio Code* editora) već se pre prvog pokretanja sistemske zavisnosti mogu instalirati pozivom build taska `DepsChoco`: 

```powershell
# Mora se izvršiti u administratorskoj PowerShell sesiji
.\Invoke-Build.ps1 DepsChoco
```
Komande se izvršavaju iz direktorijuma projekta:

```powershell
# izvrši testove u default browser-u (Firefox)
.\Invoke-Build.ps1 Run 

# izvrši testove koristeći Internet Explorer koristeći aliajs
.\Invoke-Build.ps1 Run -Browser internetexplorer
```

Komanda `.\Invoke-Build.ps1 ?` daje listu svih raspoloživih build taskova.

**Rezultat testova** generiše se u direktorijumu `TestResults` u standardnom VSTest `.trx` fajlu koji je dostupan u artifaktima build-a.

## Napomene

- Da bi se podesila test konfiguracija u Visual Studio, koristi se opcija _Test -> Test Settings -> Select Test Settings File_. 
- Za konfiguraciju testova koristeći `.runsettings` više informacija se može naći na [online MS dokumentaciji][rsfile].
- Verzija browser-a ne sme biti updatovana automatski jer to može oboriti testove. Firefox update je trenutno blokiran putem polise koja se može videti u direktorijumu [misc/firefox/distribution](misc/firefox/distribution).

[mstest]: https://en.wikipedia.org/wiki/Visual_Studio_Unit_Testing_Framework
[rsfile]: https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2017
[ib]:     https://www.powershellgallery.com/packages/InvokeBuild
