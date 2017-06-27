using Light.GuardClauses;

namespace DiAndMvvm
{
    public partial class NavigationShellView
    {
        public NavigationShellView()
        {
            InitializeComponent();
        }

        public NavigationShellView(NavigationShellViewModel viewModel) : this()
        {
            DataContext = viewModel.MustNotBeNull();
        }
    }
}