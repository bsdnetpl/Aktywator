Aktywator - Generowanie i Weryfikacja Kluczy Licencyjnych

Opis

Aktywator to usługa C# umożliwiająca generowanie i weryfikację kluczy licencyjnych na podstawie numeru NIP. Wykorzystuje kryptografię RSA oraz bibliotekę BouncyCastle do obsługi kluczy publicznych i prywatnych w formacie PEM.

Wymagania

.NET 6+ (lub nowszy)

Biblioteka BouncyCastle

Instalacja

Dodaj zależność do BouncyCastle w swoim projekcie:

Install-Package BouncyCastle.Crypto

Struktura Kluczy

Pliki kluczy RSA powinny znajdować się w katalogu KEY/:

public.pem – klucz publiczny do weryfikacji podpisów

private.pem – klucz prywatny do generowania podpisów

Użycie

1. Generowanie klucza licencyjnego

AktywatorService aktywator = new AktywatorService();
string kluczLicencyjny = aktywator.GenerateLicenseKey("1234567890");
Console.WriteLine($"Wygenerowany klucz: {kluczLicencyjny}");

2. Weryfikacja klucza licencyjnego

bool isValid = aktywator.ValidateLicenseKey(kluczLicencyjny, "1234567890");
Console.WriteLine(isValid ? "Klucz poprawny" : "Klucz niepoprawny");

Implementacja

Główne funkcje:

GenerateLicenseKey(string nip) – generuje podpisany klucz licencyjny na podstawie numeru NIP.

ValidateLicenseKey(string key, string nip) – sprawdza, czy podany klucz licencyjny jest poprawny dla danego NIP.

ImportPublicKey(string publicKeyPem) – konwertuje klucz publiczny PEM do obiektu RSA.

ImportPrivateKey(string privateKeyPem) – konwertuje klucz prywatny PEM do obiektu RSA.

Zabezpieczenia

Podpisywanie kluczy odbywa się za pomocą RSA + SHA256.

Klucze są przechowywane w formacie PEM.

Weryfikacja używa klucza publicznego, dzięki czemu nie jest wymagana obecność klucza prywatnego w aplikacji klienckiej.

Licencja

Projekt jest dostępny na licencji MIT.
