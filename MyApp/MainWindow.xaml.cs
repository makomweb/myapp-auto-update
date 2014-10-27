using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Version.Text = string.Format("{0}", new AssemblyInfoHelper(typeof(MainWindow)).AssemblyVersion);
        }
    }
}
