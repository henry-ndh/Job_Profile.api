
using Udemy.DotNetWebAPI.Models.Domain;

namespace Udemy.DotNetWebAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
