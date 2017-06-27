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

            _container.Register<NavigationShellView>(new PerContainerLifetime())
                      .Register(c => new NavigationShellViewModel(c), new PerContainerLifetime())
                      .Register<INavigationService>(c => c.GetInstance<NavigationShellViewModel>())
                      .Register<MainMenuView>()
                      .Register<MainMenuViewModel>()
                      .Register<ContactsMasterDetailView>()
                      .Register<ContactsMasterDetailViewModel>()
                      .Register<IContactRepository, InMemoryContactRepository>(new PerContainerLifetime());
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = _container.GetInstance<NavigationShellView>();
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