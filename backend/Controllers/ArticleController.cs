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
    [Route("/api/article")]
    public class ArticleController : Controller
    {
        private IArticleRepository _service;

        public ArticleController(IArticleRepository service)
        {
            _service = service;
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
            var list = _service.GetPublsihed(filter)
                .Select(x => new ArticleListDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Image = x.Image,
                    Categories = String.IsNullOrWhiteSpace(x.Categories) ? x.Categories.Split(',').ToList() : new List<String>(),
                    Tags = String.IsNullOrWhiteSpace(x.Tags) ? x.Tags.Split(',').ToList() : new List<String>(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    User = new UserDetailDTO()
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Gender = x.User.Gender,
                        Image = x.User.Image
                    }
                });
            return Ok(new { status = true, data = list, message = "ok" });
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult User(int page = 1, int limit = 10, String orderBy = "Id", String OrderDir = "desc", String? Search = null)
        {
            var user = (User)this.HttpContext.Items["User"];
            FilterDTO filter = new FilterDTO();
            filter.Page = page;
            filter.Limit = limit;
            filter.OrderBy = orderBy;
            filter.OrderDir = OrderDir;
            filter.Search = Search;
            var list = _service.GetByUser(user, filter)
                .Select(x => new ArticleListDTO()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Image = x.Image,
                    Categories = String.IsNullOrWhiteSpace(x.Categories) ? x.Categories.Split(',').ToList() : new List<String>(),
                    Tags = String.IsNullOrWhiteSpace(x.Tags) ? x.Tags.Split(',').ToList() : new List<String>(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    User = new UserDetailDTO()
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Gender = x.User.Gender,
                        Image = x.User.Image
                    }
                });
            return Ok(new { status = true, data = list, message = "ok" });
        }

    }
}
