using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Containers;
using WebApi.DAL;
using WebApi.DAL.DB;
using WebApi.Models;
using WebApi.Models.Forms;
using WebApi.Tools;
using WebApi.Tools.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Validator validator;
        private readonly IJwtService jwtService;

        private readonly UserContainer userContainer;

        public UserController(DatabaseContext context, IJwtService jwtService)
        {
            validator = new Validator();

            this.jwtService = jwtService;
            userContainer = new UserContainer(new UserDAL(context));
        }

        // POST: UserController/Register
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterRequest request)
        {
            string inputValidationError = CheckRegisterInputs(request.username, request.email, request.password, request.passwordRepeat);

            if (inputValidationError != string.Empty)
                return BadRequest(inputValidationError);
            else if (userContainer.UsernameExists(request.username) == true)
                return Conflict("Username is already taken.");
            else if (userContainer.EmailExists(request.email) == true)
                return Conflict("An user with this email already exists.");
            else
                return CreateUser(request.username, request.email, request.password);
        }

        private string CheckRegisterInputs(string username, string email, string password, string passwordRepeat)
        {
            if (password != passwordRepeat)
                return "Passwords do not match.";
            else if (!validator.ValidateUsername(username))
                return "Username format is invalid.";
            else if (!validator.ValidateEmail(email))
                return "Email format is invalid.";
            else if (!validator.ValidatePassword(password))
                return "Password format is invalid.";
            else
                return string.Empty;
        }

        private OkObjectResult CreateUser(string username, string email, string password)
        {
            string hashedPassword = new PasswordHasher<string>().HashPassword(username, password);

            User user = new()
            {
                username = username,
                email = email,
                passwordHash = hashedPassword,
                createdAt = DateTime.Now
            };

            userContainer.CreateUser(user);

            return Ok("User created.");
        }

        // POST: UserController/Login
        [HttpPost("login")]
        public async Task<IActionResult>Login([FromBody] LoginRequest request)
        {
            if (!validator.ValidatePassword(request.password))
                return BadRequest("Password format is invalid.");
            else if (userContainer.UsernameEmailExists(request.usernameEmail) == false)
                return NotFound("Username or email does not exist.");
            else if (IsValidLogin(request.usernameEmail, request.password) == false)
                return Unauthorized("Combination of email/username and password is incorrect.");
            else
                return await SetSignedIn(request.usernameEmail);
        }

        private bool IsValidLogin(string usernameEmail, string password)
        {
            PasswordHasher<string> hasher = new();
            string hashedPassword = userContainer.FetchPassword(usernameEmail);

            PasswordVerificationResult result = hasher.VerifyHashedPassword(null, hashedPassword, password);

            if (result == PasswordVerificationResult.Success)
                return true;
            else
                return false;
        }

        public async Task<IActionResult> SetSignedIn(string usernameEmail)
        {
            User user = userContainer.GetUserDetails(usernameEmail);
            String token = jwtService.GenerateToken(user);

            return Ok(new {token});
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { userId });
        }
    }
}