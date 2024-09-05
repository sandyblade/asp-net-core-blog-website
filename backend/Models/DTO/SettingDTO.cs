

using System.ComponentModel.DataAnnotations;

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

namespace backend.Models.DTO
{
    public class SettingDTO
    {
        public string Secret { get; set; }
    }

    public class SingleFileDTO
    {
        [Required(ErrorMessage = "Please enter file name")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }

    public class FilterDTO
    {
        public FilterDTO()
        {
            Page = 1;
            Limit = 10;
            Offset = ((Page - 1) * Limit);
            OrderBy = "Id";
            OrderDir = "Desc";
        }

        public int Page { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string OrderBy { get; set; }
        public string OrderDir { get; set; }
        public string? Search { get; set; } = null;
    }

}
