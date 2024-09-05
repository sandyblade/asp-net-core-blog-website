
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

using backend.Models.Entities;
using backend.Models.Repositories.Interfaces;

namespace backend.Models.Repositories.Implements
{
    public class ViewerService : IViewerRepository
    {
        private readonly AppDbContext _db;
       
        public ViewerService(AppDbContext db)
        {
            _db = db;
        }

        public void SyncViewer(Article Article, User User)
        {
            Viewer viewer = _db.Viewer.Where(x => x.Article == Article && x.User == User).FirstOrDefault();
            if(viewer == null)
            {
                Viewer NewViewer = new Viewer() { Article = Article, User = User, Status = 0 };
                _db.Viewer.Add(NewViewer);
                _db.SaveChanges();

                int total = _db.Viewer.Where(x => x.Article == Article).Count();
                Article.TotaViewer = total;
                _db.Update(Article);
                _db.SaveChanges();

            }
        }
    }
}
