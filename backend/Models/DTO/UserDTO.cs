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
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 8)]
        public required string Password { get; set; }
    }

    public class UserRegisterDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string PasswordConfirm { get; set; }
    }

    public class UserForgotDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

    }

    public class UserResetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string PasswordConfirm { get; set; }
    }

    public class UserChangePasswordDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public required string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string PasswordConfirm { get; set; }
    }

    public class UserChangeProfileDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Phone]
        [Required]
        public required string Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(2)]
        public required string Gender { get; set; }
        public string? Country { get; set; } = null;
        public string? JobTitle { get; set; } = null;
        public string? Facebook { get; set; } = null;
        public string? Instagram { get; set; } = null;
        public string? Twitter { get; set; } = null;
        public string? LinkedIn { get; set; } = null;
        public string? AboutMe { get; set; } = null;
        public string? Address { get; set; } = null;
    }

    public class UserAuthDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public UserAuthDTO(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;
        }
    }

    public class UserDetailDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }

    public class UserActivityDTO
    {
        public string Event { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
    }

    public class UserNotificationDTO
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
    }
}
