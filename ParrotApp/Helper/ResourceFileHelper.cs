using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace ParrotApp.Helper
{
    public class ResourceFileHelper
    {
        internal static Stream GetStream(string fileName)
        {
            var assembly = typeof(ResourceFileHelper).GetTypeInfo().Assembly;

            return assembly.GetManifestResourceStream("ParrotApp.Resouces." + fileName);
        }

        internal static string GetString(string fileName)
        {
            using (var stream = GetStream(fileName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        internal static ImageSource GetImageSource(string fileName)
        {
            return ImageSource.FromStream(() => GetStream(fileName));
        }

        public ImageSource SingerIcon { get; }
        public ImageSource AuthorIcon { get; }
        public ImageSource ChordIcon { get; }
        public ResourceFileHelper()
        {
            SingerIcon = GetImageSource("icon_microphone.png");
            AuthorIcon = GetImageSource("icon_pen.png");
            ChordIcon = GetImageSource("icon_chord.png");
        }
    }
}