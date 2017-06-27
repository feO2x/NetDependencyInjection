using System.Threading.Tasks;
using System.Windows;
using LightInject;

namespace DiAndMvvm
{
    public partial class App
    {
        private readonly ServiceContainer _container;

        public App()
        {
            _container = new ServiceContainer();

            _container.Register<MainWindow>(new PerContainerLifetime())
                      .Register(c => new MainWindowViewModel(c), new PerContainerLifetime())
                      .Register<INavigationService>(c => c.GetInstance<MainWindowViewModel>())
                      .Register<MainMenuView>()
                      .Register<MainMenuViewModel>()
                      .Register<ContactsMasterDetailView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = _container.GetInstance<MainWindow>();
            MainWindow.Show();
            await Task.Delay(500);
            var navigationService = _container.GetInstance<INavigationService>();
            navigationService.NavigateToMainMenu();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }
    }
}