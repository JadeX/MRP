# MRP-K/S API pro autonomní režim

[![MIT License](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/JadeX/MRP/blob/master/LICENSE.txt)
[![NuGet Downloads](https://img.shields.io/nuget/dt/JadeX.MRP.svg)](https://www.nuget.org/packages/JadeX.MRP/)
[![NuGet](https://img.shields.io/nuget/vpre/JadeX.MRP.svg)](https://www.nuget.org/packages/JadeX.MRP/)
[![.NET Standard](https://img.shields.io/badge/NETStandard-2.0/2.1-blue.svg)](javascript:)
[![.NET 6-8](https://img.shields.io/badge/NET-6.0/7.0/8.0-purple.svg)](javascript:)
[![publish](https://github.com/JadeX/MRP/actions/workflows/build.yml/badge.svg)](https://github.com/JadeX/MRP/actions/workflows/build.yml)

Neoficiální implementace API pro komunikaci s účetním systémem MRP-K/S pomocí autonomního režimu s podporou šifrování a komprese.
[https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp](https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp)

## Aktuální stav: BETA

Knihovna není dostatečně otestována v reálném nasazení a chybí serializace pro některé příkazy viz tabulka:

| Příkaz | Stav                                                                                    |
| ------ | --------------------------------------------------------------------------------------- |
| EXPEO0 | ![Dostupné](https://img.shields.io/badge/Od-1.0.0--alpha.1-yellow.svg)   |
| EXPEO1 | ![Dostupné](https://img.shields.io/badge/Od-1.0.0--alpha.1-yellow.svg)   |
| IMPEO0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| CENEO0 | ![Dostupné](https://img.shields.io/badge/Od-1.0.0--alpha.2-yellow.svg)   |
| ADREO0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFV0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFV1 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFV2 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFVPDF | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFP0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPFP1 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg) |
| EXPOP0 | ![Dostupné](https://img.shields.io/badge/Od-1.0.0--beta.5-yellow.svg) |

## Začínáme

Tyto instrukce popisují použití knihovny pouze pro klientské aplikace. Pro nastavení autonomního režimu na serveru navštivte prosím [oficiální dokumentaci](https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp).

### Instalace pomocí NuGet

```sh
Install-Package MRP –IncludePrerelease
```

### Použití

**Doporučené základní nastavení s kompresí a šifrováním:**

```csharp
var mrpApi = new MrpApi("http://xxx.xxx.xxx.xxx:ppppp") // Jediný povinný parametr, url serveru včetně portu kam se mají zasílat požadavky
    .WithEncryption("xxxXXXxXxXxxXXxXXxxxXXxXXxXXxXXxxXXxXXXxXXX=") // Přepne komunikaci na šifrovaný režim, klíč nutno vygenerovat na serveru
    .WithCompression() // Zapne zlib kompresi
    .WithTimeout(TimeSpan.FromSeconds(10)); // Jak dlouho počkat na odpověď požadavku

// Zašleme příkaz EXPEO0 a použijeme přídavné filtrování výsledků (viz oficiální dokumentace)
var response = await this.MrpApi.EXPEO0(x => x
    .Filter("cisloSkladu", "1")
    .Filter("SKKAR.CISLO", "1..1000")
);

// Něco se nezdařilo, můžeme to vyhodit jako aplikační chybu
if (result.HasError)
{
    throw new Exception(result.ErrorMessage);
}

// Vypíšeme výsledky do konzole
Console.WriteLine($"Zvoleným filtrům odpovídá {result.Products.Count} produktů a {result.Categories.Count} kategorií.");
var produktySkladem = result.Products.Where(x => x.PocetMJ > 0);
Console.WriteLine($"Z toho je {produktySkladem.Count()} produktů skladem.");
Console.WriteLine($"V celkové hodnotě {produktySkladem.Sum(x => x.CenaSDPH)} {produktySkladem.First().Mena} s DPH.");
```

**Příklad úspěšného výstupu:**

```sh
Zvoleným filtrům odpovídá 183 produktů a 29 kategorií.
Z toho je 125 produktů skladem.
V celkové hodnotě 44793 CZK s DPH.
```

## Spuštění testů

Testy API vyžadují tajné parametry, které se načítají z UserSecrets nebo EnvironmentVariables.

### User Secrets

```sh
PM> dotnet user-secrets set SecretKey xxxXXXxXxXxxXXxXXxxxXXxXXxXXxXXxxXXxXXXxXXX= --project MRP.Tests
PM> dotnet user-secrets set ApiUrl http://xxx.xxx.xxx.xxx:ppppp --project MRP.Tests
```

### .devcontainer/devcontainer.env

```
SecretKey = xxxXXXxXxXxxXXxXXxxxXXxXXxXXxXXxxXXxXXXxXXX=
ApiUrl = http://xxx.xxx.xxx.xxx:ppppp
```

\*Testy ověřují pouze zda byla úspěšná komunikace se serverem a zda byl vrácen alespoň nějaký výsledek.
\*\*Testy jsou dynamicky označeny jako přeskočené pokud server vrátí chybu, že daný příkaz nemá povoleno obsloužení.

## Použité balíky

-   [Portable.BouncyCastle](https://github.com/bcgit/bc-csharp) - Šifrování
-   [SharpCompress](https://github.com/adamhathcock/sharpcompress) - Komprese
-   [xUnit.net](https://github.com/xunit/xunit) - Testování
    -   [Shouldly](https://github.com/shouldly/shouldly) - Assertion framework pro testování

## Verze

Tato knihovna používá [SemVer 2.0.0](https://semver.org/) pro číslování verzí.
