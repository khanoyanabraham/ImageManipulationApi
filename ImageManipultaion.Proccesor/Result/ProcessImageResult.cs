namespace ImageManipultaion.Proccesor.Result
{
    public class ProcessImageResult
    {
        public byte[] ImageData { get; }
        public ProcessImageResult(byte[] iamge)
        {
            this.ImageData = iamge;
        }
    }
}
