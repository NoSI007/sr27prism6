using System.Windows;

namespace SR27_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 3.1  override void OnStartup(StartupEventArgs e)
    /// 3.2  Bootstrapper bootstrapper = new Bootstrapper();
    /// 3.3  bootstrapper.Run();
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

    }
}
