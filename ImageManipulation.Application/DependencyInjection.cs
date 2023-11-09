using ImageManipulation.Application.ImageServices;
using ImageManipulation.Application.ImageServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImageManipulation.Application
{
    public static class DependencyInjection
    {
        public static void RegisterApplication(this IServiceCollection collection)
        {
            collection.AddScoped<IImageService, ImageService>();

        }
    }
}
