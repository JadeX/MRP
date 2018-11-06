# MRP-K/S API pro autonomní režim

![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)
![NuGet](https://img.shields.io/nuget/dt/MRP.svg)
![NuGet Pre Release](https://img.shields.io/nuget/vpre/MRP.svg)
![Maintenance](https://img.shields.io/badge/NETStandard-2.0+-blue.svg)

Neoficiální implementace API pro komunikaci s účetním systémem MRP-K/S pomocí autonomního režimu s podporou šifrování a komprese. 
[https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp](https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp)

## Aktuální stav: ALPHA
Knihovna není dostatečně otestována v reálném nasazení a chybí serializace pro některé příkazy viz tabulka:


Příkaz | Stav
-------|-------
ADREO0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
CENEO0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
EXPEO0 | ![Dostupné](https://img.shields.io/badge/Dostupnost-Od%201.0.0-blue.svg)
EXPEO1 | ![Dostupné](https://img.shields.io/badge/Dostupnost-Od%201.0.0-blue.svg)
EXPFP0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
EXPFP1 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
EXPFV0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
EXPFV1 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)
IMPEO0 | ![Nedostupné](https://img.shields.io/badge/Dostupnost-Nen%C3%AD-red.svg)


## Jak začít

Tyto instrukce popisují použití knihovny pro klientské aplikace. Pro nastavení autonomního režimu na serveru navštivte prosím [oficiální dokumentaci](https://www.mrp.cz/software/ucetnictvi/ks/autonomni-rezim.asp).

### Instalace pomocí NuGet

```sh
Install-Package MRP –IncludePrerelease
```

### Použití
**Základní použití s kompresí a šifrováním:**

```csharp
var mrpApi = new MrpApi(new MrpApiConfig()
{
    // Povinný údaj. Kam se mají zasílat požadavky včetně portu.
    Url = "http://xxx.xxx.xxx.xxx:ppppp",
    // Nepovinný údaj. Pokud je SecretKey uveden bude komunikace šifrovaná.
    // JE NUTNÉ VYGENEROVAT VLASTNÍ KLÍČ NA SERVERU!
    SecretKey = "tXxAJQ4RKX8S6g699Tg71ZhObveBExGvEJ0+QAKfT7Y=",
    //UseCompression = false, // Tímto lze vypnout kompresi, která je jinak vždy zapnutá.
    //CompressionLevel = CompressionLevel.Default, // Možnost nastavení míry komprese.
});

// Zašleme příkaz EXPEO0 a použijeme přídavné filtrování výsledků podle oficiální dokumentace
EXPEO0 result = await mrpApi.EXPEO0(new List<NameValueItem>() {
                new NameValueItem() { Name = "cisloSkladu", Value = "1" },
                new NameValueItem() { Name = "SKKAR.CISLO", Value = "1..10000" }
});

// Pokud se něco pokazilo, můžeme to vyhodit jako aplikační chybu
if (result.HasError)
{
    throw new Exception(result.ErrorMessage);
}

Console.WriteLine($"Zvoleným filtrům odpovídá {result.Products.Count} produktů a {result.Categories.Count} kategorií.");
var produktySkladem = result.Products.Where(x => x.PocetMJ > 0);
Console.WriteLine($"Z toho je {produktySkladem.Count()} produktů skladem.");
Console.WriteLine($"V celkové hodnotě {produktySkladem.Sum(x => x.CenaSDPH)} {produktySkladem.First().Mena}.");
```

**Úspěšný výstup:**
```sh
Zvoleným filtrům odpovídá 183 produktů a 29 kategorií.
Z toho je 125 produktů skladem.
V celkové hodnotě 44793 CZK.
```

## Spuštění testů

Testy API vyžadují tajné parametry, které se načítají z UserSecrets nebo EnvironmentVariables.

```sh
dotnet user-secrets set SecretKey tXxAJQ4RKX8S6g699Tg71ZhObveBExGvEJ0+QAKfT7Y= --project MRP.Tests
dotnet user-secrets set ApiUrl http://xxx.xxx.xxx.xxx:ppppp --project MRP.Tests
```

## Použité balíky

* [Portable.BouncyCastle](https://github.com/bcgit/bc-csharp) - Šifrování
* [SharpCompress](https://github.com/adamhathcock/sharpcompress) - Komprese
* [xUnit.net](https://github.com/xunit/xunit) - Testování

## Verze

Tato knihovna používá [SemVer 2.0.0](http://semver.org/) pro číslování verzí.
