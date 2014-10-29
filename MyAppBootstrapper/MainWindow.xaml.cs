using System;
using System.Diagnostics;
using System.Windows;
using AutoUpdaterDotNET;

namespace MyAppBootstrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _changesAddress;

        public MainWindow()
        {
            InitializeComponent();

            Update.IsEnabled = false;
            Later.IsEnabled = false;
            Changes.IsEnabled = false;

            AutoUpdater.CheckForUpdateEvent += OnUpdate;

            //Observable.Timer(TimeSpan.FromMilliseconds(200)).Subscribe(_ => TryUpdate());
            //Observable.Timer(TimeSpan.FromMilliseconds(200)).ObserveOnDispatcher().Subscribe(_ => TryBoot());
        }

        private void OnUpdate(UpdateInfoEventArgs args)
        {
#if false
            if (args.IsUpdateAvailable)
            {
                Application.Current.Dispatcher.Invoke(() => Load(args));
            }
            else
            {
                Application.Current.Dispatcher.Invoke(TryBoot);
            }
#else
            Application.Current.Dispatcher.Invoke(() => Load(args));
#endif
        }

        private void Load(UpdateInfoEventArgs args)
        {
            CurrentVersion.Text = args.InstalledVersion.ToString();
            NewVersion.Text = args.CurrentVersion.ToString();
            Update.IsEnabled = true;
            Later.IsEnabled = true;
            Changes.IsEnabled = true;

            _changesAddress = args.ChangelogURL;
        }

        private static void TryBoot()
        {
            try
            {
                Boot();
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private static void Boot()
        {
            var info = new ProcessStartInfo
            {
                FileName = @"MyApp.exe",
                Arguments = "",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = new Process { StartInfo = info };
            if (!proc.Start())
            {
                throw new ApplicationException("Could not start app process!");
            }
        }

        private void OnUpdateClicked(object sender, RoutedEventArgs e)
        {
            AutoUpdater.DownloadUpdate();
        }

        private void OnLaterClicked(object sender, RoutedEventArgs e)
        {
            TryBoot();
        }

        private void OnCheckClicked(object sender, RoutedEventArgs e)
        {
            AutoUpdater.Start("http://_path_/AppCast.xml");
        }

        private void OnChangesClicked(object sender, RoutedEventArgs e)
        {
            Process.Start(_changesAddress);
        }
    }
}