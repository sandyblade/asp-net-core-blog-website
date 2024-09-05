
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
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Repositories.Implements
{
    public class CommentService : ICommentRepository
    {
        private readonly AppDbContext _db;
        private readonly IActivityRepository _activityRepository;

        public CommentService(AppDbContext db, IActivityRepository activityRepository)
        {
            _db = db;
            _activityRepository = activityRepository;
        }

        public Comment GetById(long Id)
        {
            Comment comment = _db.Comment.Where(x =>  x.Id == Id).FirstOrDefault();
            return comment == null ? null : comment;
        }

        public Comment Create(User User, long Id, ArticleCommentDTO model)
        {
            bool Replied = false;
            Article article = _db.Article.Include(x=> x.User).Where(x => x.Id == Id).FirstOrDefault();

            if(article == null)
            {
                return null;
            }

            Comment comment = new Comment() { 
                User = User,
                Article = article,
                Body = model.Body
            };

            if(model.ParentId != null)
            {
                Comment parent = _db.Comment.Where(x => x.Id == model.ParentId).FirstOrDefault();

                if(parent != null)
                {
                    comment.Parent = parent;
                    Replied = true;
                }
            }

            article.TotaComment = article.TotaComment + 1;

            String Subject = Replied ? "Reply of comment" : "Add new comment";
            String Description = Replied ? "The user are replied comment of your article with title " + comment.Article.Title : "The user are add comment of your article with title " + comment.Article.Title;

            _activityRepository.SaveActivity(User, Subject, Description);

            if (User != article.User)
            {
                Notification notif = new Notification() { Subject = Subject, Message = Description, User = article.User };
                _db.Add(notif);
            }

            _db.Add(comment);
            _db.Update(article);
            _db.SaveChanges();

            return comment;
        }

        public void Delete(User User, long Id)
        {
            Comment comment = _db.Comment.Include(x=> x.Article).Where(x => x.Id == Id && x.User == User).FirstOrDefault();

            if(comment != null)
            {
                if(comment.Article != null)
                {
                    _activityRepository.SaveActivity(User, "Delete Comment Article", "The user are deleted comment of article with title " + comment.Article.Title);

                    Article article = comment.Article;
                    article.TotaComment = article.TotaComment - 1;
                    _db.Update(article);
                }
                _db.Remove(comment);
                _db.SaveChanges();
            }

        }

        public List<ArticleCommentListDTO> GetByArticle(Article Article)
        {
            List<Comment> comments = _db.Comment.Include(x=> x.Article).Include(x=> x.User).Where(x => x.Article == Article).OrderByDescending(x => x.Id).ToList();
            List<ArticleCommentListDTO> list = new List<ArticleCommentListDTO>();

            foreach(var row in comments)
            {
                ArticleCommentListDTO cm = new ArticleCommentListDTO();
                cm.Id = row.Id;
                cm.ParentId = row.ParentId;
                cm.CreatedAt = row.CreatedAt;
                cm.Body = row.Body;
                cm.User = new UserDetailDTO()
                {
                    FirstName = row.User.FirstName,
                    LastName = row.User.LastName,
                    Gender = row.User.Gender,
                    Image = row.User.Image
                };
                list.Add(cm);
            }

            return generateCommentTree(list);
        }

        private List<ArticleCommentListDTO> generateCommentTree(List<ArticleCommentListDTO> comments, Nullable<long> ParentId = null)
        {
            List<ArticleCommentListDTO> branch = new List<ArticleCommentListDTO>();

            foreach (var element in comments)
            {

                if (element.ParentId == ParentId)
                {
                    List<ArticleCommentListDTO> children = this.generateCommentTree(comments, element.Id);
                    if (children.Count > 0)
                    {
                        element.Children = children;
                    }
                    else
                    {
                        element.Children = new List<ArticleCommentListDTO>();
                    }
                    branch.Add(element);
                }
            }

            return branch;
        }
    }
}
