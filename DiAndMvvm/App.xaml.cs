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
                      .Register<MainWindowViewModel>(new PerContainerLifetime());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = _container.GetInstance<MainWindow>();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }
    }
}