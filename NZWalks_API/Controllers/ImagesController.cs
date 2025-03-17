using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //api/Images/Upload

        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {

            }

            return BadRequest();
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] {".jpeg" , ".jpg", ".png"};

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Image Size should be less than 10MB");
            }
        }
    }
}
