

using backend.Models.DTO;
using backend.Models.Entities;

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
namespace backend.Models.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetById(long Id);
        Comment Create(User User, long Id, ArticleCommentDTO model);
        List<ArticleCommentListDTO> GetByArticle(Article Article);
        void Delete(User User, long Id);

    }
}
