using ParrotApp.Models;

namespace ParrotApp.ViewModels
{
    public class NavigationViewModel
    {
        public Navigation[] Navigations { get; }

        public NavigationViewModel()
        {
            Navigations = new Navigation[]
            {
                new Navigation("icon_chord.png", "BÀI HÁT"),
                new Navigation("icon_microphone.png", "CA SĨ"),
                new Navigation("icon_pen.png", "NHẠC SĨ"),
                new Navigation("icon_chord.png", "YÊU THÍCH"),
                new Navigation("icon_chord.png", "CỦA TÔI"),
            };
        }
    }
}