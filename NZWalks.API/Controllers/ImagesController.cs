using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {

            if (request == null || request.File == null)
            {
                ModelState.AddModelError("File", "Please upload an image file.");
                return BadRequest(ModelState);
            }

            var file = request.File;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("File", "Unsupported file format. Allowed: .jpg, .jpeg, .png, .gif");
            }

            long maxFileSize = 10 * 1024 * 1024; // 10MB

            if (file.Length > maxFileSize)
            {
                ModelState.AddModelError("File", "File size exceeds the 10MB limit.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            //  CREATE DOMAIN MODEL
            

            var imageDomainModel = new Models.Domain.Image
            {
                File = file,
                FileName = Path.GetFileNameWithoutExtension(file.FileName),
                FileExtension = extension,
                FileDescription = request.FileDescription
            };

            // Save to repository
            await imageRepository.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }
    }
}
