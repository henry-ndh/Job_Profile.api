using System.ComponentModel.DataAnnotations;

namespace Udemy.DotNetWebAPI.Models.DTO
{
    public class ImageUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
