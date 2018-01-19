using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArcheAgeLauncher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipaddress.Text;
            int port = Int32.Parse(this.port.Text);
            string token = userToken.Text;
            string args = "";
            if (turn_slscrn.IsChecked != null)
                args += "-nosplash";

            using (var game = new Process())
            {
                var info = new ProcessStartInfo("archeage.exe");
                info.Arguments = string.Format(" -r +auth_ip {0} -uid {1} -token {2}",
                    ip.ToString()+":"+port.ToString(),
                    uid.Text,
                    token
                    );
                info.Verb = "runas"; //admin
                game.StartInfo = info;
                game.Start();
            }
            

            this.Close();
        }

        private void port_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
