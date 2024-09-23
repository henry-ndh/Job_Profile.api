using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Udemy.DotNetWebAPI.Models.Domain;
using Udemy.DotNetWebAPI.Models.DTO;
using Udemy.DotNetWebAPI.Repositories;

namespace Udemy.DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO imageUploadDTO)
        {
            ValidateImageFile(imageUploadDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageDomain = new Image
            {
                FileName = imageUploadDTO.FileName,
                File = imageUploadDTO.File,
                FileDescription = imageUploadDTO.FileDescription,
                FileExtention = Path.GetExtension(imageUploadDTO.File.FileName),
                FileSizeInBytes = imageUploadDTO.File.Length.ToString(),
            };
            var img = await _imageRepository.Upload(imageDomain);
            return Ok(img);
        }

        private void ValidateImageFile(ImageUploadDTO requestImage)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var maxFileSize = 10485760;
            if (!allowedExtensions.Contains(Path.GetExtension(requestImage.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file extension");
            }
            if (requestImage.File.Length > maxFileSize)
            {
                ModelState.AddModelError("File", "File size is too large");
            }
        }

    }
}
