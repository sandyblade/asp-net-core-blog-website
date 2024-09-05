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

using Microsoft.AspNetCore.Mvc;
using backend.Models.Entities;
using backend.Models.Repositories.Interfaces;
using backend.Configs;
using backend.Models.DTO;


namespace backend.Controllers
{
    [ApiController]
    [Route("/api/account")]
    [Authorize]
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        private IActivityRepository _activityRepository;

        public AccountController(IUserRepository userRepository, IActivityRepository activityRepository)
        {
            _userRepository = userRepository;
            _activityRepository = activityRepository;
        }

        [HttpGet("detail")]
        public IActionResult Detail()
        {
            var user = (User)this.HttpContext.Items["User"];
            return Ok(new { status = true, data = user, message = "ok" });
        }

        [HttpPost("update")]
        public IActionResult Update(UserChangeProfileDTO model)
        {
            var user = (User)this.HttpContext.Items["User"];
            var userByEmail = _userRepository.GetByEmail(model.Email, user.Id);
            var userByPhone = _userRepository.GetByPhone(model.Phone, user.Id);

            if(userByEmail != null)
            {
                return BadRequest(new { message = "The e-mail address has already been taken.!" });
            }

            if (userByPhone != null)
            {
                return BadRequest(new { message = "The phone number has already been taken.!" });
            }

            user = _userRepository.ChangeProfile(user, model);
            return Ok(new { status = true, data = user, message = "Yor profile has been changed !!" });
        }

        [HttpPost("password")]
        public IActionResult Password(UserChangePasswordDTO model)
        {
            var user = (User)this.HttpContext.Items["User"];

            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.Password))
            {
                return BadRequest(new { message = "Your password was not updated, since the provided current password does not match.!!" });
            }

            user = _userRepository.ChangePassword(user, model);
            return Ok(new { status = true, data = user, message = "Your profile has been changed !!" });
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormCollection form)
        {
            var user = (User)this.HttpContext.Items["User"];

            if (HttpContext.Request.Form.Files.Count == 0)
            {
                return BadRequest(new { message = "Please select file !" });
            }

            SingleFileDTO model = new SingleFileDTO();
            model.File = HttpContext.Request.Form.Files.FirstOrDefault();

            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = System.Guid.NewGuid().ToString() + "" + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                if (!String.IsNullOrWhiteSpace(user.Image))
                {
                    string fileNameWithPathUser = Path.Combine(path, user.Image);
                    if (System.IO.File.Exists(fileNameWithPathUser))
                    {
                        System.IO.File.Delete(fileNameWithPathUser);
                    }
                }

                _userRepository.ChangeImage(user, fileName);
                model.FileName = "Uploads/" + fileName;
                model.IsSuccess = true;
                model.Message = "File upload successfully";
            }

            return Ok(model);
        }

        [HttpPost("token")]
        public IActionResult Token()
        {
            var user = (User)this.HttpContext.Items["User"];
            var token = _userRepository.generateJwtToken(user);
            return Ok(new { status = true, data = token, message = "Your access token has been generted !!" });
        }

        [HttpGet("activity")]
        public IActionResult Activity(int page = 1, int limit = 10, String orderBy = "Id", String OrderDir = "desc", String? Search = null)
        {
            FilterDTO filter = new FilterDTO();
            filter.Page = page;
            filter.Limit = limit;
            filter.OrderBy = orderBy;
            filter.OrderDir = OrderDir;
            filter.Search = Search;
            var user = (User)this.HttpContext.Items["User"];
            var list = _activityRepository.GetByUser(user, filter)
                .Select(x => new UserActivityDTO() { 
                    Event =x.Event,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                });
            return Ok(new { status = true, data = list, message = "ok" });
        }
    }
}
