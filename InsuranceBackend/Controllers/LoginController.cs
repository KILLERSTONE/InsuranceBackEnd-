using InsuranceBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceBackend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IInsuranceContext _context;

        public LoginController(IInsuranceContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> UserLogin(string username, string password)
        {

            try
            {
                var uId = await _context.UserLogin(username, password);

                if (uId != null) return Ok(uId);
                else return Unauthorized("Invalid username or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occured: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] LoginModel model)
        {
            try
            {
                var uId = await _context.UserLogin(model.Username, model.Password);

                if (uId != null)
                    return Ok(uId);
                else
                    return Unauthorized("Invalid username or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

    }
}
