namespace ImageManipultaion.Proccesor.Processors.Interfaces
{
    public interface IImageProcessor
    {
        string Name { get; }
        Task<byte[]> ProccessAsync(byte[] imageData, string filterData);
    }
}
