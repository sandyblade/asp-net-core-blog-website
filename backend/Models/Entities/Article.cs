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
    [Index(nameof(UserId))]
    [Index(nameof(Title))]
    [Index(nameof(Slug))]
    [Index(nameof(Description))]
    [Index(nameof(TotaViewer))]
    [Index(nameof(TotaComment))]
    [Index(nameof(Status))]
    [Index(nameof(CreatedAt))]
    [Index(nameof(UpdatedAt))]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [DisplayName("User")]
        public long UserId { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public required string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public required string Slug { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public required string Content { get; set; }

        [Column(TypeName = "text")]
        public string? Categories { get; set; } = null;

        [Column(TypeName = "text")]
        public string? Tags { get; set; } = null;

        [Required]
        public int TotaViewer { get; set; } = 0;

        [Required]
        public int TotaComment { get; set; } = 0;

        [Column(TypeName = "smallint")]
        public int Status { get; set; } = 0;

        public Nullable<System.DateTime> CreatedAt { get; set; } = DateTime.UtcNow;
        public Nullable<System.DateTime> UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Viewer> Viewers { get; set; } = new List<Viewer>();

    }
}
