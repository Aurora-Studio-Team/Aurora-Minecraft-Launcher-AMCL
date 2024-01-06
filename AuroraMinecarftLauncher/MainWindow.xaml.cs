using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

// 导入的模块
using KMCCC.Launcher;
using StarLight_Core.Utilities;
using StarLight_Core.Authentication;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Models.Auth;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using MinecraftLaunch.Modules.Models.Install;
using System.Threading.Tasks;
using MinecraftLaunch.Modules.Installer;
using System.Windows.Input;

namespace AuroraMinecarftLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static LaunchConfig LaunchConfig { get; } = new LaunchConfig();
        public Account UserInfo {get; private set; }

        public static LauncherCore Core = LauncherCore.Create();

        private HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();

        }

        // Title S
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            zxh.Click += (s, e) =>
            {
                WindowState = WindowState.Minimized;
            };
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            gb.Click += (s, e) =>
            {
                Close();
            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        // Title E

        // 导航栏 S

        // 导航栏 E
    }
}