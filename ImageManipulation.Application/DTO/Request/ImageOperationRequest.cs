namespace ImageManipulation.Application.DTO.Request
{
    public class ImageOperationRequest
    {
        public string ImageData { get; set; }

        public List<ImageFilter> Filters { get; set; }
    }
}
