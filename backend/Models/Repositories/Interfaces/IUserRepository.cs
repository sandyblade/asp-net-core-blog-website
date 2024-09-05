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
    public interface IUserRepository
    {
        void CreateInitial();
        UserAuthDTO Authenticate(UserLoginDTO model);
        User GetById(long Id);
        User GetByEmail(String Email, long Id);
        User GetByPhone(String Phone, long Id);
        User GetByConfirmToken(String Token);
        User GetByResetToken(String Token, String Email);
        User Register(UserRegisterDTO model);
        User Confirmation(String Token);
        User ForgotPassword(UserForgotDTO model);
        User ResetPassword(String Token, UserResetPasswordDTO model);
        User ChangePassword(User user, UserChangePasswordDTO model);
        User ChangeProfile(User user, UserChangeProfileDTO model);
        void ChangeImage(User user, String Path);
        String generateJwtToken(User user);

    }
}
