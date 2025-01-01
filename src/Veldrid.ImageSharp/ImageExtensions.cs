using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Veldrid.ImageSharp
{
    public static class ImageExtensions
    {
        public static Span<Rgba32> GetPixelSpan(this Image<Rgba32> image)
        {
            if (image.DangerousTryGetSinglePixelMemory(out Memory<Rgba32> pixelData))
            {
                return pixelData.Span;
            }

            int bufferSize = image.Width * image.Height;
            Rgba32[] buffer = new Rgba32[bufferSize];
            Span<Rgba32> span = new Span<Rgba32>(buffer);
            image.CopyPixelDataTo(span);
            return span;
        }
    }
}
