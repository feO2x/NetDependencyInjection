using Light.GuardClauses;

namespace DiAndMvvm
{
    public partial class MainMenuView
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        public MainMenuView(MainMenuViewModel viewModel) : this()
        {
            DataContext = viewModel.MustNotBeNull();
        }
    }
}