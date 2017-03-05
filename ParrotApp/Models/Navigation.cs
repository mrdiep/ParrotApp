using Xamarin.Forms;

namespace ParrotApp.Models
{
    public class Navigation
    {
        public Navigation(string icon, string name)
        {
            Icon = Helper.ResourceFileHelper.GetImageSource(icon);
            ViewName = name;
        }

        public ImageSource Icon { get; set; }
        public string ViewName { get; set; }
    }
}