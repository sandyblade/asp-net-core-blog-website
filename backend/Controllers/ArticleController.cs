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
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/article")]
    public class ArticleController : Controller
    {
        private IArticleRepository _service;
        private readonly IViewerRepository _viewerRepository;

        public ArticleController(IArticleRepository service, IViewerRepository viewerRepository)
        {
            _service = service;
            _viewerRepository = viewerRepository;
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
                    Categories = !String.IsNullOrWhiteSpace(x.Categories) ? x.Categories.Split(',').ToList() : new List<String>(),
                    Tags = !String.IsNullOrWhiteSpace(x.Tags) ? x.Tags.Split(',').ToList() : new List<String>(),
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
                    Categories = !String.IsNullOrWhiteSpace(x.Categories) ? x.Categories.Split(',').ToList() : new List<String>(),
                    Tags = !String.IsNullOrWhiteSpace(x.Tags) ? x.Tags.Split(',').ToList() : new List<String>(),
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


        [HttpPost("create")]
        [Authorize]
        public IActionResult Create(ArticleFormDTO model)
        {
            var user = (User)this.HttpContext.Items["User"];
            var slug = _service.GenerateSlug(model.Title);
            var check = _service.GetBySlug(slug, 0);

            if (check != null)
            {
                return BadRequest(new { message = "The article with title '" + model.Title + "' has already been taken.!" });
            }

            _service.CreateOrUpdate(user, model, 0);
            return Ok(new { status = true, data = model, message = "ok" });
        }

        [HttpGet("read/{slug}")]
        [Authorize]
        public IActionResult Read(String slug)
        {
            var article = _service.GetBySlug(slug, 0);
            var user = (User)this.HttpContext.Items["User"];

            if (article == null)
            {
                return BadRequest(new { message = "The article with slug '" + slug + "' was not found.!!" });
            }
            else
            {
                ArticleListDTO payload = new ArticleListDTO()
                {
                    Id = article.Id,
                    Title = article.Title,
                    Description = article.Description,
                    Content = article.Content,
                    Image = article.Image,
                    Categories = !String.IsNullOrWhiteSpace(article.Categories) ? article.Categories.Split(',').ToList() : new List<String>(),
                    Tags = !String.IsNullOrWhiteSpace(article.Tags) ? article.Tags.Split(',').ToList() : new List<String>(),
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt,
                    User = new UserDetailDTO()
                    {
                        FirstName = article.User.FirstName,
                        LastName = article.User.LastName,
                        Gender = article.User.Gender,
                        Image = article.User.Image
                    }
                };

                _viewerRepository.SyncViewer(article, user);

                return Ok(new { status = true, data = payload, message = "ok" });
            }

        }

        [HttpPut("update/{id}")]
        [Authorize]
        public IActionResult Update(long Id, ArticleFormDTO model)
        {
            var user = (User)this.HttpContext.Items["User"];
            var slug = _service.GenerateSlug(model.Title);
            var check = _service.GetBySlug(slug, Id);

            if (check != null)
            {
                return BadRequest(new { message = "The article with title '" + model.Title + "' has already been taken.!" });
            }

            _service.CreateOrUpdate(user, model, Id);
            return Ok(new { status = true, data = model, message = "ok" });
        }

        [HttpDelete("remove/{id}")]
        [Authorize]
        public IActionResult Remove(long Id)
        {
            var user = (User)this.HttpContext.Items["User"];
            Article article = _service.GetById(Id);

            if (article == null)
            {
                return BadRequest(new { message = "The record with id " + Id + " was not found in our record !!" });
            }

            _service.Delete(user, Id);

            return Ok(new { status = true, data = article, message = "ok" });
        }

        [HttpGet("words")]
        [Authorize]
        public IActionResult Words(int max = 10)
        {
            List<String> payloads = new List<String>();

            for(int i = 1; i <= max; i++)
            {
                payloads.Add(Faker.Lorem.Sentence(1));
            }

            payloads = payloads.Distinct().ToList();
            payloads.Sort();

            return Ok(new { status = true, data = payloads, message = "ok" });
        }

        [HttpPost("upload/{id}")]
        [Authorize]
        public IActionResult Upload(long Id, IFormCollection form)
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

                _service.Upload(Id, user, fileName);
                model.FileName = "Uploads/" + fileName;
                model.IsSuccess = true;
                model.Message = "File upload successfully";
            }

            return Ok(model);
        }

    }
}
