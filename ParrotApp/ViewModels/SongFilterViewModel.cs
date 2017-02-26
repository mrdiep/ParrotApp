using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotApp.Models
{
    public class SongFilterViewModel : ViewModelBase
    {
        string name;
        public string Name { get {
                return name;
            } set {
                name = value;
                RaiseChange();
            } }

        private void RaiseChange()
        {
            throw new NotImplementedException();
        }
    }
}
