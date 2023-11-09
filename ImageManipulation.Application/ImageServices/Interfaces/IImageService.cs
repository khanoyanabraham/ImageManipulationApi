using ImageManipulation.Application.DTO.Request;
using ImageManipulation.Application.DTO.Response;

namespace ImageManipulation.Application.ImageServices.Interfaces
{
    public interface IImageService
    {
        Task<ImageOperationResponse> ProcessImage(ImageOperationRequest request);
    }
}
