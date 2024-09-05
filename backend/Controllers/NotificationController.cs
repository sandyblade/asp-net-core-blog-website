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
    [Route("/api/notification")]
    [Authorize]
    public class NotificationController : Controller
    {
        private INotificationRepository _service;

        public NotificationController(INotificationRepository service)
        {
            _service = service;
        }

        [HttpGet("read/{id}")]
        public IActionResult Read(long Id)
        {
            var user = (User)this.HttpContext.Items["User"];
            Notification notif = _service.GetByUser(user, Id);

            if(notif == null)
            {
                return BadRequest(new { message = "The record with id " + Id + " was not found in our record !!" });
            }

            return Ok(new { status = true, data = notif, message = "ok" });
        }

        [HttpDelete("remove/{id}")]
        public IActionResult Remove(long Id)
        {
            var user = (User)this.HttpContext.Items["User"];
            Notification notif = _service.GetByUser(user, Id);

            if (notif == null)
            {
                return BadRequest(new { message = "The record with id " + Id + " was not found in our record !!" });
            }

            _service.Delete(user, Id);

            return Ok(new { status = true, data = notif, message = "ok" });
        }

        [HttpGet("list")]
        public IActionResult List(int page = 1, int limit = 10, String orderBy = "Id", String OrderDir = "desc", String? Search = null)
        {
            FilterDTO filter = new FilterDTO();
            filter.Page = page;
            filter.Limit = limit;
            filter.OrderBy = orderBy;
            filter.OrderDir = OrderDir;
            filter.Search = Search;
            var user = (User)this.HttpContext.Items["User"];
            var list = _service.GetByUser(user, filter)
                .Select(x => new UserNotificationDTO()
                {
                    Subject = x.Subject,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt
                });
            return Ok(new { status = true, data = list, message = "ok" });
        }

    }
}
