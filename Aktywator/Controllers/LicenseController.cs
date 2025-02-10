using Aktywator.DTOs;
using Aktywator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aktywator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly IAktywatorService _licenseService;

        public LicenseController(IAktywatorService licenseService)
            {
            _licenseService = licenseService;
            }

        /// <summary>
        /// Generuje klucz aktywacyjny na podstawie NIP
        /// </summary>
        [HttpPost("generate")]
        public IActionResult GenerateLicenseKey([FromBody] LicenseRequest request)
            {
            try
                {
                string licenseKey = _licenseService.GenerateLicenseKey(request.Nip);
                return Ok(new { LicenseKey = licenseKey });
                }
            catch (Exception ex)
                {
                return BadRequest(new { Message = "Błąd generowania klucza", Error = ex.Message });
                }
            }

        /// <summary>
        /// Weryfikuje podany klucz aktywacyjny na podstawie NIP
        /// </summary>
        [HttpPost("validate")]
        public IActionResult ValidateLicenseKey([FromBody] LicenseValidationRequest request)
            {
            try
                {
                bool isValid = _licenseService.ValidateLicenseKey(request.LicenseKey, request.Nip);
                return Ok(new { IsValid = isValid });
                }
            catch (Exception ex)
                {
                return BadRequest(new { Message = "Błąd weryfikacji klucza", Error = ex.Message });
                }
            }
        }
}
