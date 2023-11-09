using ImageManipultaion.Proccesor.Processors.Interfaces;

namespace ImageManipultaion.Proccesor.Processors
{
    public sealed class ImageBlurMaker : IImageProcessor
    {
        public string Name => "BlurMaker";

        public async Task<byte[]> ProccessAsync(byte[] imageData, string filter)
        {
            System.Diagnostics.Debug.WriteLine("ImageBlur");
            return imageData;
        }
    }
}
