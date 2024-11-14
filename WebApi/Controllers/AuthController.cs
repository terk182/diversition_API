using App.Auth.Models;
using App.Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserService _userService;

        public AuthController(JwtTokenService jwtTokenService, UserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///   
        ///     {
        ///        "Username": "terk",
        ///        "Password": "1234"
        ///     }
        ///
        /// </remarks>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // ดึงข้อมูลผู้ใช้ทั้งหมด
            var users = _userService.GetAllUsers();

            // ตรวจสอบชื่อผู้ใช้และรหัสผ่าน
            var user = users.FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // สร้างและคืนค่า JWT Token
            var token = _jwtTokenService.GenerateToken(user.Username);
            return Ok(new { Token = token, userId = user.Id });
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // ตรวจสอบว่าชื่อผู้ใช้ซ้ำหรือไม่
            if (_userService.UsernameExists(user.Username))
            {
                return BadRequest("Username already exists.");
            }

            // เพิ่มผู้ใช้ใหม่
            _userService.AddUser(user);
            return Ok("User registered successfully.");
        }
    }
}
