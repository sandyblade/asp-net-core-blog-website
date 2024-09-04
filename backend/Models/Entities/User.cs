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

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Entities
{
    [Index(nameof(Email))]
    [Index(nameof(Phone))]
    [Index(nameof(Password))]
    [Index(nameof(Confirmed))]
    [Index(nameof(ResetToken))]
    [Index(nameof(ConfirmToken))]
    [Index(nameof(Image))]
    [Index(nameof(FirstName))]
    [Index(nameof(LastName))]
    [Index(nameof(Gender))]
    [Index(nameof(Country))]
    [Index(nameof(Facebook))]
    [Index(nameof(Instagram))]
    [Index(nameof(LinkedIn))]
    [Index(nameof(Twitter))]
    [Index(nameof(CreatedAt))]
    [Index(nameof(UpdatedAt))]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(180)")]
        public required string Email { get; set; }


        [Column(TypeName = "varchar(64)")]
        public string ?Phone { get; set; } = null;

        [Required]
        [Column(TypeName = "varchar(255)")]
        [JsonIgnore]
        public required string Password { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Image { get; set; } = null;

        [Column(TypeName = "varchar(191)")]
        public string? FirstName { get; set; } = null;

        [Column(TypeName = "varchar(191)")]
        public string? LastName { get; set; } = null;

        [Column(TypeName = "varchar(2)")]
        public string? Gender { get; set; } = null;

        [Column(TypeName = "varchar(191)")]
        public string? Country { get; set; } = null;

        [Column(TypeName = "varchar(255)")]
        public string? JobTitle { get; set; } = null;

        [Column(TypeName = "varchar(255)")]
        public string? Facebook { get; set; } = null;

        [Column(TypeName = "varchar(255)")]
        public string? Instagram { get; set; } = null;

        [Column(TypeName = "varchar(255)")]
        public string? Twitter { get; set; } = null;

        [Column(TypeName = "varchar(255)")]
        public string? LinkedIn { get; set; } = null;

        [Column(TypeName = "text")]
        public string? Address { get; set; } = null;

        [Column(TypeName = "text")]
        public string? AboutMe { get; set; } = null;

        [Column(TypeName = "varchar(36)")]
        public string? ResetToken { get; set; } = null;

        [Column(TypeName = "varchar(36)")]
        public string? ConfirmToken { get; set; } = null;

        [Required]
        [Column(TypeName = "smallint")]
        public int Confirmed { get; set; } = 0;

        public Nullable<System.DateTime> CreatedAt { get; set; } = DateTime.UtcNow;
        public Nullable<System.DateTime> UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Viewer> Viewers { get; set; } = new List<Viewer>();
    }
}
