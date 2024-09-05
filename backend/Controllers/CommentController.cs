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
    [Route("/api/comment")]
    public class CommentController : Controller
    {
        private ICommentRepository _service;
        private IArticleRepository _article;

        public CommentController(ICommentRepository service, IArticleRepository article)
        {
            _service = service;
            _article = article;
        }


        [HttpGet("list/{id}")]
        public IActionResult List(long Id)
        {
            Article article = _article.GetById(Id);

            if (article == null)
            {
                return BadRequest(new { message = "The article with id '" + Id + "' was not found.!!" });
            }

            var payload = _service.GetByArticle(article);
            return Ok(new { status = true, data = payload, message = "ok" });
        }


        [HttpPost("create/{id}")]
        [Authorize]
        public IActionResult Create(long Id, ArticleCommentDTO model)
        {
            var user = (User)this.HttpContext.Items["User"];
            Article article = _article.GetById(Id);

            if(article == null)
            {
                return BadRequest(new { message = "The article with id '" + Id + "' was not found.!!" });
            }

            var payload = _service.Create(user, Id, model);

            return Ok(new { status = true, data = payload, message = "ok" });

        }

        [HttpDelete("remove/{id}")]
        [Authorize]
        public IActionResult remove(long Id)
        {
            var user = (User)this.HttpContext.Items["User"];
            Comment comment = _service.GetById(Id);

            if (comment == null)
            {
                return BadRequest(new { message = "The comment with id '" + Id + "' was not found.!!" });
            }

            _service.Delete(user, Id);

            return Ok(new { status = true, data = comment, message = "ok" });

        }

    }
}
