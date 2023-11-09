using ImageManipultaion.Proccesor.Processors.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageManipultaion.Proccesor.Processors
{
    public sealed class ImageResizer : IImageProcessor
    {
        public string Name => "Resizer";

        public async Task<byte[]> ProccessAsync(byte[] imageData, string filterData)
        {
            await Task.Yield();
            if (int.TryParse(filterData, out var value))
            {
                using MemoryStream st = new(imageData);
                using Bitmap originalBitmap = new(st);

                // Resize the image
                Bitmap resizedBitmap = ResizeImageUnsafe(originalBitmap, value, value);
                return ImageToByte2(resizedBitmap);
            }
            return Array.Empty<byte>();
        }
        private static byte[] ImageToByte2(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
        private unsafe Bitmap ResizeImageUnsafe(Bitmap originalBitmap, int newWidth, int newHeight)
        {
            // Create a new Bitmap with the desired size
            Bitmap resizedBitmap = new(newWidth, newHeight);

            // Lock the bits of the original and resized bitmaps
            BitmapData originalData = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData resizedData = resizedBitmap.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // Get the stride (scan width) of the images
            int originalStride = originalData.Stride;
            int resizedStride = resizedData.Stride;

            // Get pointers to the start of the image data
            byte* originalPtr = (byte*)originalData.Scan0.ToPointer();
            byte* resizedPtr = (byte*)resizedData.Scan0.ToPointer();

            // Calculate the scaling factors
            float xScale = (float)originalBitmap.Width / newWidth;
            float yScale = (float)originalBitmap.Height / newHeight;

            // Iterate through each pixel in the resized image
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    // Calculate the corresponding pixel position in the original image
                    int originalX = (int)(x * xScale);
                    int originalY = (int)(y * yScale);

                    // Get the pixel values from the original image
                    byte* originalPixel = originalPtr + originalY * originalStride + originalX * 3;

                    // Set the pixel values in the resized image
                    byte* resizedPixel = resizedPtr + y * resizedStride + x * 3;
                    resizedPixel[0] = originalPixel[0]; // Blue
                    resizedPixel[1] = originalPixel[1]; // Green
                    resizedPixel[2] = originalPixel[2]; // Red
                }
            }

            // Unlock the bits of the original and resized bitmaps
            originalBitmap.UnlockBits(originalData);
            resizedBitmap.UnlockBits(resizedData);

            return resizedBitmap;
        }
    }
}
