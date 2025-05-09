
using Mobappg4v2.ViewModel;

namespace Mobappg4v2.Model
{
    public class TabPageModel : BaseViewModel
    {
        public TabPageModel(string name, int id, bool isSelected)
        {
            this.Name = name;
            this.Id = id;
            IsSelected = isSelected;
        }
        public string Name { private set; get; }
        public int Id { private set; get; }

        private bool _IsSelected = false;
        public bool IsSelected
        {
            get => _IsSelected;
            set => SetProperty(ref _IsSelected, value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}