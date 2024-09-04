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
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Models.Entities
{
    [Index(nameof(UserId))]
    [Index(nameof(Subject))]
    [Index(nameof(CreatedAt))]
    [Index(nameof(UpdatedAt))]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("User")]
        public long UserId { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public required string Subject { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public required string Message { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; } = DateTime.UtcNow;
        public Nullable<System.DateTime> UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
