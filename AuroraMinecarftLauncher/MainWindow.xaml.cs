using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// 导入的模块
using KMCCC.Launcher;
using KMCCC.Authentication;
using KMCCC.Modules.JVersion;
using KMCCC.Modules.Minecraft;
using Panuon.UI.Silver;
using StarLight_Core.Utilities;
using StarLight_Core.Models.Utilities;
using Newtonsoft.Json.Linq;
using static System.Formats.Asn1.AsnWriter;
using AuroraMinecarftLauncher.LoginUI;
using StarLight_Core.Authentication;
using System.Diagnostics;
using MinecraftOAuth.Module;
using Natsurainko.FluentCore.Extension.Windows.Service;
using MinecraftLaunch.Modules.Models.Launch;
using MinecraftLaunch.Modules.Models.Auth;
using Natsurainko.FluentCore.Module.Authenticator;
using MinecraftLaunch.Modules.Installer;
using MinecraftLaunch.Modules.Models.Install;

namespace AuroraMinecarftLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {
        public static LaunchConfig LaunchConfig { get; } = new LaunchConfig();
        public Account UserInfo {get; private set; }

        public static LauncherCore Core = LauncherCore.Create();

        // 配置项
        public MainWindow()
        {
            InitializeComponent();
            // 自动寻找版本
            var versions = Core.GetVersions().ToArray();
            version.ItemsSource = versions;//绑定数据源
            version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

            // 自动寻找Java
            var javaInfo = JavaUtil.GetJavas();
            string javaPath = javaInfo.First().JavaPath;
            java.DisplayMemberPath = "JavaLibraryPath";
            java.SelectedValuePath = "JavaLibraryPath";
            java.ItemsSource = javaInfo;

            // 初始选择
            version.SelectedItem = 1;
            java.SelectedItem = 1;
        }

        public void GameStart()
        {
            if (LoginUI.LiXian.OfflineID.Text != string.Empty && java.Text != string.Empty && version.Text != string.Empty && MemoryTextbox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = java.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(MemoryTextbox.Text), //最大内存，int类型
                        Authenticator = new KMCCC.Authentication.OfflineAuthenticator(LoginUI.LiXian.OfflineID.Text), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                        // Authenticator = new YggdrasilLogin("邮箱", "密码", true), // 正版启动，最后一个为是否twitch登录
                        Mode = LaunchMode.MCLauncher, //启动模式，这个我会在后面解释有哪几种
                                                      // Server = new ServerInfo { Address = "服务器IP地址", Port = "服务器端口" }, //设置启动游戏后，自动加入指定IP的服务器，可以不要
                                                      // Size = new WindowSize { Height = 768, Width = 1280 } //设置窗口大小，可以不要
                    });
                    // 错误提示
                    if (!result.Success)
                    {
                        switch (result.ErrorType)
                        {
                            case ErrorType.NoJAVA:
                                MessageBoxX.Show("Java错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.AuthenticationFailed:
                                MessageBoxX.Show("登录时发生错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.UncompressingFailed:
                                MessageBoxX.Show("文件错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            default:
                                MessageBoxX.Show(result.ErrorMessage, "错误！");
                                break;
                        }
                    }
                }
                catch
                {
                    MessageBoxX.Show("启动失败！", "错误！");
                }
            }
            else
            {
                MessageBoxX.Show("信息不完整！", "错误！");
            }
        }


        private void javaCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void MemoryTextbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void userName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        // 启动页-离线启动
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }

        // 启动页-微软登录
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string clientId = "e1e383f9-59d9-4aa2-bf5e-73fe83b15ba0";
            var deviceCodeInfo = await MicrosoftAuthentication.RetrieveDeviceCodeInfo(clientId);
            System.Diagnostics.Process.Start("explorer.exe", deviceCodeInfo.VerificationUri);
            MessageBox.Show("在浏览器中输入您的验证代码：" + deviceCodeInfo.UserCode, "Microsoft登录");
            var tokenInfo = await MicrosoftAuthentication.GetTokenResponse(deviceCodeInfo);
            var userInfo = await MicrosoftAuthentication.MicrosoftAuthAsync(tokenInfo, x =>
            {
                Console.WriteLine(x);

            });
            MessageBox.Show("欢迎回来！" + userInfo.Name, "欢迎");
            userName.Text = userInfo.Name;
            // userImge.Source = new BitmapImage(new Uri("@https://crafatar.com/avatars/"+userInfo.Name));
        }

        // 启动页-正版启动
        private void ZB_Start(object sender, RoutedEventArgs e)
        {
            
        }

        // 下载页-刷新
        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
        }
        // 下载页-安装
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }

        private void IDT(object sender, TextChangedEventArgs e)
        {

        }
        // LittleSKin-Email
        private void LEamil_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // LittleSkin-Password
        private void LPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // LittleSkin-Start
        private void LStart(object sender, RoutedEventArgs e)
        {
           
        }
        //统一通行证
        // 服务器ID
        private void ServerID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // 邮箱
        private void TEamil_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // 密码
        private void TPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        // 启动
        private void TStart(object sender, RoutedEventArgs e)
        {

        }
    }
}