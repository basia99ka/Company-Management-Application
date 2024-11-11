using SharedLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAccount _userAccountI;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IUserAccount userAccount, ILogger<AuthenticationController> logger)
        {
            this._userAccountI = userAccount;
            _logger = logger;

        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {

            if (user == null)
            {
                _logger.LogWarning("Próba dodania pustego modelu rejestracji.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto rejestrację użytkownika.");
            try
            {
                var result = await _userAccountI.CreateAsync(user);
                _logger.LogInformation("Pomyślnie zarejestrowano użytkownika.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas rejestracji użytkownika.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> SignInAsync(Login user)
        {
            if (user == null)
            {
                _logger.LogWarning("Próba logowania z pustym modelem.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto logowanie użytkownika.");
            try
            {
                var result = await _userAccountI.SignInAsync(user);
                _logger.LogInformation("Pomyślnie zalogowano użytkownika.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas logowania użytkownika.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }

        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null)
            {
                _logger.LogWarning("Próba odświeżenia tokenu z pustym modelem.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto odświeżanie tokenu.");
            try
            {
                var result = await _userAccountI.RefreshTokenAsync(token);
                _logger.LogInformation("Pomyślnie odświeżono token.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas odświeżania tokenu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            _logger.LogInformation("Rozpoczęto pobieranie użytkowników.");
            try
            {
                var users = await _userAccountI.GetUsers();
                if (users == null)
                {
                    _logger.LogWarning("Brak użytkowników do pobrania.");
                    return NotFound("Nie znaleziono użytkowników.");
                }

                _logger.LogInformation("Pomyślnie pobrano użytkowników.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas pobierania użytkowników.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(ManageUser manageUser)
        {
            if (manageUser == null)
            {
                _logger.LogWarning("Próba aktualizacji użytkownika z pustym modelem.");
                return BadRequest("Model jest pusty.");
            }

            _logger.LogInformation("Rozpoczęto aktualizację użytkownika.");
            try
            {
                var result = await _userAccountI.UpdateUser(manageUser);
                _logger.LogInformation("Pomyślnie zaktualizowano użytkownika.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas aktualizacji użytkownika.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var users = await _userAccountI.GetRoles();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Rozpoczęto usuwanie użytkownika o id {UserId}.", id);
            try
            {
                var result = await _userAccountI.DeleteUser(id);
                _logger.LogInformation("Pomyślnie usunięto użytkownika o id {UserId}.", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas usuwania użytkownika o id {UserId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd serwera.");
            }
        }

    }
}
