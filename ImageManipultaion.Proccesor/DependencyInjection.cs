using ImageManipultaion.Proccesor.Processors;
using ImageManipultaion.Proccesor.Processors.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImageManipultaion.Proccesor
{
    public static class DependencyInjection
    {
        public static void RegisterProcessors(this IServiceCollection collection)
        {
            collection.AddScoped<IImageProcessor, ImageBlurMaker>().AddScoped<IImageProcessor, ImageResizer>();
        }
    }
}
