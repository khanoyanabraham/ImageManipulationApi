using ImageManipulation.Application.Configuration;
using ImageManipulation.Application.DTO.Request;
using ImageManipulation.Application.DTO.Response;
using ImageManipulation.Application.ImageServices.Interfaces;
using ImageManipultaion.Proccesor.Processors.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ImageManipulation.Application.ImageServices
{
    public sealed class ImageService : IImageService
    {
        private readonly IEnumerable<IImageProcessor> imageProcessor;
        private readonly IConfiguration configration;

        public ImageService(IEnumerable<IImageProcessor> imageProcessor, IConfiguration configuration)
        {
            this.imageProcessor = imageProcessor;
            this.configration = configuration;
        }

        public async Task<ImageOperationResponse> ProcessImage(ImageOperationRequest request)
        {
            if (string.IsNullOrEmpty(request.ImageData))
                return new ImageOperationResponse("", "Empty Image", 11);
            try
            {
                List<IImageProcessor> imageProcessors = new();

                var configurations = configration.GetSection("ImagePlugins").GetSection("Plugins").Get<List<ImageConfiguration>>();

                if (configurations != null)
                {

                    configurations.RemoveAll(x => !x.Active);
                    for (var i = 0; i < request.Filters.Count; i++)
                    {
                        var filterItem = request.Filters[i];
                        if (configurations.Exists(x => x.Name == filterItem.Name))
                        {
                            var processor = imageProcessor.FirstOrDefault(x => x.Name == filterItem.Name);
                            if (processor != null)
                            {
                                imageProcessors.Add(processor);
                            }
                        }
                    }
                }
                var imageParts = request.ImageData.Split(',').ToList<string>();
                byte[] Image = Convert.FromBase64String(imageParts[1]);
                foreach (var processor in imageProcessors)
                {
                    var filter = request.Filters.First(x => x.Name == processor.Name);
                    Image = await processor.ProccessAsync(Image, filter.Data);
                }
                return new ImageOperationResponse(Convert.ToBase64String(Image), "", 0);
            }
            catch (Exception ex)
            {
                //logger
                return new ImageOperationResponse("Invalid Image", ex.Message, 10);
            }
        }

    }
}
