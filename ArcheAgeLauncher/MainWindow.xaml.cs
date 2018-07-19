using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace ArcheAgeLauncher
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ip = ipaddress.Text;
            var port = int.Parse(this.port.Text);
            var token = userToken.Text;
            var args = "";
            if (turn_slscrn.IsChecked == true)
            {
                args += "-nosplash";
            }

            using (var game = new Process())
            {
                var info = new ProcessStartInfo("archeage.exe");
                info.Arguments = string.Format(args + " -r +auth_ip {0} -uid {1} -token {2}",
                    ip + ":" + port,
                    uid.Text,
                    token
                );
                info.Verb = "runas"; //admin
                game.StartInfo = info;
                game.Start();
            }
            Close();
        }

        private void port_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}