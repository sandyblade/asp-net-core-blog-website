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
    public interface IActivityRepository
    {
        Activity SaveActivity(User User, String Event, String Description);

        List<Activity> GetByUser(User user, FilterDTO filter);
    }
}
