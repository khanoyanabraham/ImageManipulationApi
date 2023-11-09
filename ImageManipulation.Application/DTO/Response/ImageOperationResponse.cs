namespace ImageManipulation.Application.DTO.Response
{
    public class ImageOperationResponse : Response
    {
        public string ProcessedImage { get; }

        public ImageOperationResponse(string processedImage,string errorMesasge,int code)
        {
            ErrorCode = code;
            Message=errorMesasge;
            ProcessedImage = processedImage;
        }
    }
}
