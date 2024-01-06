using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Install;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AuroraMinecarftLauncher.Pages
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Page
    {
        public Download()
        {
            InitializeComponent();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(async () =>
                {
                    GameCoresEntity gameCores = await GameCoreInstaller.GetGameCoresAsync();
                    var releaseVersions = gameCores.Cores.Where(v => v.Type == "release").Select(v => v.Id);
                    Dispatcher.Invoke(() =>
                    {
                        DownloadList.Items.Clear();
                        foreach (var version in releaseVersions)
                        {
                            DownloadList.Items.Add("版本：" + version);
                        }
                    });
                });
            }
            catch
            {
                MessageBox.Show("无法获取版本，请检查网络连接！", "❎错误");
            };
        }
        // D-install
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = DownloadList.SelectedItem.ToString();
                await Task.Run(async () =>
                {
                    GameCoreInstaller list = new(new(".minecraft"), id);
                    var res = await list.InstallAsync();

                    if (res.Success)
                    {
                        MessageBox.Show("安装成功");
                    }
                });
            }
            catch
            {
                MessageBox.Show("安装时出现错误，请检查网络环境，稍后再试。");
            };
        }
    }
}
