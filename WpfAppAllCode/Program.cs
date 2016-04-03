using System;
using System.Linq;
using System.Windows;

namespace WpfAppAllCode
{
    class Program : Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            Program app = new Program();
            app.Startup += AppStartUp;
            app.Exit += AppExit;

            app.Run();
        }

        static void AppExit(object sender, ExitEventArgs e)
        {
            MessageBox.Show("App has exited");
        }

        static void AppStartUp(object sender, StartupEventArgs e)
        {
            Current.Properties["GodMode"] = false;

            if (e.Args.Any(arg => arg.ToLower() == "/godmode"))
            {
                Current.Properties["GodMode"] = true;
            }

            var mainWindow = new MainWindow("My better WPF App!!!", 200, 300);

            mainWindow.Show();
        }
    }
}
