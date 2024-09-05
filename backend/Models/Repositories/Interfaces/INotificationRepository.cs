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

namespace backend.Models.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Notification GetByUser(User user, long Id);

        void Delete(User user, long Id);

        List<Notification> GetByUser(User user, FilterDTO filter);
    }
}
