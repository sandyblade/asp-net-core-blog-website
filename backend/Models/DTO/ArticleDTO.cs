using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO
{
    public class ArticleFormDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10)]
        public required string Content { get; set; }

        public int Status { get; set; } = 0;
        public List<String> Categories = new List<String>();
        public List<String> Tags = new List<String>();

    }

    public class ArticleListDTO
    {
        public long Id { get; set; }
        public required String Title { get; set; }
        public required String Description { get; set; }
        public String? Content { get; set; } = null;
        public String? Image { get; set; } = null;

        public List<String> Categories = new List<String>();

        public List<String> Tags = new List<String>();
        public UserDetailDTO? User { get; set; } = null;
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }

    }
}
