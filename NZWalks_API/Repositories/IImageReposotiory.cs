using System.Net;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IImageReposotiory
    {
        Task<Image> UploadAsync(Image image);
    }
}
