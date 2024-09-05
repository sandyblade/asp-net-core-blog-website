
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
using System.Text.RegularExpressions;
using System.Linq.Dynamic.Core;

namespace backend.Models.Repositories.Implements
{
    public class ArticleService : IArticleRepository
    {
        private readonly AppDbContext _db;
        private readonly IActivityRepository _activityRepository;
        private readonly INotificationRepository _notificationRepository;

        public ArticleService(AppDbContext db, IActivityRepository activityRepository, INotificationRepository notificationRepository)
        {
            _db = db;
            _activityRepository = activityRepository;
            _notificationRepository = notificationRepository;
        }

        public Article GetBySlug(String Slug)
        {
            Article article = _db.Article.Where(x => x.Slug == Slug).FirstOrDefault();
            return article == null ? null : article;
        }

        public Article CreateOrUpdate(User User, ArticleFormDTO model, long Id)
        {
            Article article = new Article() {
                Title = model.Title,
                Slug = GenerateSlug(model.Title),
                Description = model.Description,
                Content = model.Content,
                User = User
            };

            if(Id > 0)
            {
                article = _db.Article.Where(x => x.Id == Id).FirstOrDefault();
                article.Title = model.Title;
                article.Slug = GenerateSlug(model.Title);
                article.Description = model.Description;
                article.Content = model.Content;
            }

            article.Status = model.Status;

            if(Id == 0)
            {
                _db.Article.Add(article);
            }
            else
            {
                _db.Article.Update(article);
            }

            if(model.Categories.Count > 0)
            {
                article.Categories = String.Join(",", model.Categories);
            }

            if (model.Tags.Count > 0)
            {
                article.Tags = String.Join(",", model.Tags);
            }

            _db.SaveChanges();

            if (User != null)
            {
                if(Id == 0)
                {
                    _activityRepository.SaveActivity(User, "Create New Article", "The user create new article with title " + article.Title);
                }
                else
                {
                    _activityRepository.SaveActivity(User, "Edit Article", "The user edited article with title " + article.Title);
                }
            }

            return article;
        }

        public void Delete(User user, long Id)
        {
            Article article = _db.Article.Where(x => x.User == user && x.Id == Id).FirstOrDefault();

            if(article != null)
            {
                _db.Remove(article);
                _db.SaveChanges();

                _activityRepository.SaveActivity(user, "Delete Article", "The user deleted article with title " + article.Title);
            }

        }

        public List<Article> GetByUser(User user, FilterDTO filter)
        {
            var attr = typeof(Article).GetProperty(filter.OrderBy);
            var data = _db.Article.AsQueryable();
            data = data.Where(x => x.User == user);

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                data = data.Where(x => x.Title.Contains(filter.Search) || x.Description.Contains(filter.Search) || x.Categories.Contains(filter.Search) || x.Tags.Contains(filter.Search));
            }

            return data.OrderBy(filter.OrderBy + " " + filter.OrderDir).Skip(filter.Offset).Take(filter.Limit).ToList();
        }

        public List<Article> GetPublsihed(FilterDTO filter)
        {
            var attr = typeof(Article).GetProperty(filter.OrderBy);
            var data = _db.Article.AsQueryable();
            data = data.Where(x => x.Status == 1);

            if (!String.IsNullOrWhiteSpace(filter.Search))
            {
                data = data.Where(x => x.Title.Contains(filter.Search) || x.Description.Contains(filter.Search) || x.Categories.Contains(filter.Search) || x.Tags.Contains(filter.Search));
            }

            return data.OrderBy(filter.OrderBy + " " + filter.OrderDir).Skip(filter.Offset).Take(filter.Limit).ToList();
        }

        private  string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

    }
}
