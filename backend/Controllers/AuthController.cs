/**
 * This file is part of the Sandy Andryanto Blog Application.
 *
 * @author     Sandy Andryanto <sandy.andryanto.blade@gmail.com>
 * @copyright  2024
 *
 * For the full copyright and license information,
 * please view the LICENSE.md file that was distributed
 * with this source code.
 */

using backend.Models.DTO;
using backend.Models.Entities;
using backend.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : Controller
    {
        private IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(UserLoginDTO model)
        {
            var response = _userRepository.Authenticate(model);

            if (response == null)
            {
                return Unauthorized(new { message = "These credentials do not match our records." });
            }

            User user = _userRepository.GetByEmail(model.Email, 0);

            if (user.Confirmed == 0)
            {
                return BadRequest(new { message = "You need to confirm your account. We have sent you an activation code, please check your email.!" });
            }

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO model)
        {
            User user = _userRepository.GetByEmail(model.Email, 0);

            if (user != null)
            {
                return BadRequest(new { message = "The email has already been taken.!" });
            }

            _userRepository.Register(model);

            return Ok(new {  message = "You need to confirm your account. We have sent you an activation code, please check your email.!" });

        }

        [HttpGet("confirm/{token}")]
        public IActionResult Confirm(String token)
        {
            User user = _userRepository.GetByConfirmToken(token);

            if (user == null)
            {
                return BadRequest(new { message = "We can't find a user with that  token is invalid.!" });
            }

            _userRepository.Confirmation(token);

            return Ok(new { message = "Your e-mail is verified. You can now login." });
        }

        [HttpPost("email/forgot")]
        public IActionResult Forgot(UserForgotDTO model)
        {
            User user = _userRepository.GetByEmail(model.Email, 0);

            if (user == null)
            {
                return BadRequest(new { message = "We can't find a user with that e-mail address.!" });
            }

            _userRepository.ForgotPassword(model);

            return Ok(new { message = "We have e-mailed your password reset link!" });

        }

        [HttpPost("email/reset/{token}")]
        public IActionResult Reset(String token, UserResetPasswordDTO model)
        {
            User user = _userRepository.GetByResetToken(token, model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "We can't find a user with that e-mail address or password reset token is invalid.!" });
            }

            _userRepository.ResetPassword(token, model);

            return Ok(new { message = "Your password has been reset!" });

        }


    }
}
