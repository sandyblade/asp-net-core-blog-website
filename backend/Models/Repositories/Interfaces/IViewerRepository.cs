

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
    public interface IViewerRepository
    {
        void SyncViewer(Article Article, User User);
    }
}
