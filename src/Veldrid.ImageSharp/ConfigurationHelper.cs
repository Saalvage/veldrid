using SixLabors.ImageSharp;

namespace Veldrid.ImageSharp
{
    internal class ConfigurationHelper
    {
        private static Configuration CreateConfiguration()
        {
            Configuration config = Configuration.Default.Clone();
            config.PreferContiguousImageBuffers = true;
            return config;
        }

        public static Configuration Configuration { get; } = CreateConfiguration();
    }
}
