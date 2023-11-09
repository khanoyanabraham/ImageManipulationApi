using ImageManipulation.Application.Configuration;
using ImageManipulation.Application.DTO.Request;
using ImageManipulation.Application.DTO.Response;
using ImageManipulation.Application.ImageServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageManipulation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }
        [HttpPost]
        [Route("modifyimage")]
        public async Task<ImageOperationResponse> ModifyImage(ImageOperationRequest request)
        {
            return await imageService.ProcessImage(request);
        }


    }
}
