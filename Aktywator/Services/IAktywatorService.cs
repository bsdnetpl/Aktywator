namespace Aktywator.Services
    {
    public interface IAktywatorService
        {
        string GenerateLicenseKey(string nip);
        bool ValidateLicenseKey(string key, string nip);
        }
    }