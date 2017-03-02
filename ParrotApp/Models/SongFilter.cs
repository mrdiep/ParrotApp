using ParrotApp.Data;

namespace ParrotApp.Models
{
    public class SongFilter : ViewModelBase
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        public bool Filter(SongMetadata song)
        {
            return true;
        }
    }
}