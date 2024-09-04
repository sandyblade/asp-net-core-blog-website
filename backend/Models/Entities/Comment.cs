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

namespace backend.Models.Entities
{
    [Index(nameof(ArticleId))]
    [Index(nameof(UserId))]
    [Index(nameof(ParentId))]
    [Index(nameof(CreatedAt))]
    [Index(nameof(UpdatedAt))]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("Parent")]
        public Nullable<long> ParentId { get; set; }
        public virtual Comment Parent { get; set; }

        [DisplayName("Article")]
        public long ArticleId { get; set; }
        public required virtual Article Article { get; set; }

        [DisplayName("User")]
        public long UserId { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public required string Body { get; set; }

        public Nullable<System.DateTime> CreatedAt { get; set; } = DateTime.UtcNow;
        public Nullable<System.DateTime> UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual List<Comment> Children { get; set; } = new List<Comment>();

    }
}
