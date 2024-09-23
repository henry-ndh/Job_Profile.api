using System.Net.Mime;
using Udemy.DotNetWebAPI.Data;
using Udemy.DotNetWebAPI.Models.Domain;

namespace Udemy.DotNetWebAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DotNetDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, DotNetDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{image.FileName}{image.FileExtention}";
            image.FilePath = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}




