using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace Aktywator.Services
    {
    public class AktywatorService : IAktywatorService
        {
        private const string PublicKeyPath = "KEY/public.pem";
        private const string PrivateKeyPath = "KEY/private.pem";

        private RSA LoadPublicKey()
            {
            return ImportPublicKey(File.ReadAllText(PublicKeyPath));
            }

        private RSA LoadPrivateKey()
            {
            return ImportPrivateKey(File.ReadAllText(PrivateKeyPath));
            }

        public string GenerateLicenseKey(string nip)
            {
            if (nip.Length != 10 || !long.TryParse(nip, out _))
                {
                throw new ArgumentException("Nieprawidłowy format NIP. Powinien mieć 10 cyfr.");
                }

            byte[] data = Encoding.UTF8.GetBytes(nip.Trim()); // Usuwamy ewentualne białe znaki

            using (RSA rsa = LoadPrivateKey())
                {
                byte[] signedData = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                return Convert.ToBase64String(signedData); // Przekazujemy w Base64
                }
            }

        public bool ValidateLicenseKey(string key, string nip)
            {
            byte[] data = Encoding.UTF8.GetBytes(nip.Trim()); // Musi być identyczne jak w `GenerateLicenseKey()`

            try
                {
                byte[] signedData = Convert.FromBase64String(key); // Odtwarzamy podpis Base64

                using (RSA rsa = LoadPublicKey())
                    {
                    return rsa.VerifyData(data, signedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    }
                }
            catch (FormatException)
                {
                return false; // Jeśli Base64 jest błędne
                }
            }

        /// <summary>
        /// Konwertuje klucz publiczny PEM na obiekt RSA
        /// </summary>
        private RSA ImportPublicKey(string publicKeyPem)
            {
            using (TextReader reader = new StringReader(publicKeyPem))
                {
                PemReader pemReader = new PemReader(reader);
                object pemObject = pemReader.ReadObject();

                if (pemObject == null)
                    throw new Exception("Nie udało się wczytać klucza publicznego. Sprawdź format PEM.");

                if (!(pemObject is RsaKeyParameters rsaParams))
                    throw new Exception("Niepoprawny format klucza publicznego.");

                RSA rsa = RSA.Create();
                rsa.ImportParameters(DotNetUtilities.ToRSAParameters(rsaParams));
                return rsa;
                }
            }

        /// <summary>
        /// Konwertuje klucz prywatny PEM na obiekt RSA
        /// </summary>
        private RSA ImportPrivateKey(string privateKeyPem)
            {
            using (TextReader reader = new StringReader(privateKeyPem))
                {
                PemReader pemReader = new PemReader(reader);
                object pemObject = pemReader.ReadObject();

                if (pemObject == null)
                    throw new Exception("Nie udało się wczytać klucza prywatnego. Sprawdź format PEM.");

                if (!(pemObject is RsaPrivateCrtKeyParameters rsaParams))
                    throw new Exception("Niepoprawny format klucza prywatnego.");

                RSA rsa = RSA.Create();
                rsa.ImportParameters(DotNetUtilities.ToRSAParameters(rsaParams));
                return rsa;
                }
            }
        }
    }
