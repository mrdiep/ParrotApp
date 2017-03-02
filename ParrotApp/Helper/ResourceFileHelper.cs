using System.IO;
using System.Reflection;

namespace ParrotApp.Helper
{
    internal static class ResourceFileHelper
    {
        public static Stream GetStream(string fileName)
        {
            var assembly = typeof(ResourceFileHelper).GetTypeInfo().Assembly;

            return assembly.GetManifestResourceStream("ParrotApp.Resouces." + fileName);
        }

        public static string GetString(string fileName)
        {
            using (var stream = GetStream(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}