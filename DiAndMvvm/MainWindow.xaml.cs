using Light.GuardClauses;

namespace DiAndMvvm
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel viewModel) : this()
        {
            DataContext = viewModel.MustNotBeNull();
        }
    }
}