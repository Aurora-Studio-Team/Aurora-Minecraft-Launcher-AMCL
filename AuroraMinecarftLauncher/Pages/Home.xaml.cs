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

namespace AuroraMinecarftLauncher.Pages
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Window
    {
        public static LaunchConfig LaunchConfig { get; } = new LaunchConfig();
        public Account UserInfo { get; private set; }

        public static LauncherCore Core = LauncherCore.Create();
        public Home()
        {
            InitializeComponent();
            
            // 自动寻找版本
            var versions = Core.GetVersions().ToArray();
            version.ItemsSource = versions;//绑定数据源
            version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

            // 自动寻找Java
            var javaInfo = JavaUtil.GetJavas();
            string javaPath = javaInfo.First().JavaPath;
            Settings.JavaList.DisplayMemberPath = "JavaLibraryPath";
            Settings.JavaList.SelectedValuePath = "JavaLibraryPath";
            Settings.JavaList.ItemsSource = javaInfo;

            // 初始选择
            version.SelectedItem = 1;
            Settings.JavaList.SelectedItem = 1;
        }

        // 启动页-离线启动
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IDTextbox.Text != string.Empty && Settings.JavaList.Text != string.Empty && version.Text != string.Empty && Settings.MemoryBox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = Settings.JavaList.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(Settings.MemoryBox.Text), //最大内存，int类型
                        Authenticator = new KMCCC.Authentication.OfflineAuthenticator(IDTextbox.Text), //离线启动，ZhaiSoul那儿为你要设置的游戏名
                        //Authenticator = new YggdrasilLogin("邮箱", "密码", false), // 正版启动，最后一个为是否twitch登录
                        Mode = LaunchMode.MCLauncher,

                    });
                    // 错误提示
                    if (!result.Success)
                    {
                        switch (result.ErrorType)
                        {
                            case ErrorType.NoJAVA:
                                MessageBox.Show("Java错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.AuthenticationFailed:
                                MessageBox.Show("登录时发生错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            case ErrorType.UncompressingFailed:
                                MessageBox.Show("文件错误！详细信息：" + result.ErrorMessage, "错误！");
                                break;
                            default:
                                MessageBox.Show(result.ErrorMessage, "错误！");
                                break;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("启动失败！", "错误！");
                }
            }
            else
            {
                MessageBox.Show("信息不完整！", "错误！");
            }
        }

        // 启动页-微软登录
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var auth = new MicrosoftAuthentication("a0ceb477-0738-47fa-8c93-52d892aa866a");
            var deviceCodeInfo = await auth.RetrieveDeviceCodeInfo();
            Process.Start("explorer.exe", deviceCodeInfo.VerificationUri);
            MessageBox.Show("请在浏览器中输入您的用户验证代码：" + deviceCodeInfo.UserCode, "Microsoft验证");
            var tokenInfo = await auth.GetTokenResponse(deviceCodeInfo);
            var userInfo = await auth.MicrosoftAuthAsync(tokenInfo, x =>
            {
                Console.WriteLine(x);
            });
            MessageBox.Show("欢迎回来！" + userInfo.Name, "欢迎");
            userName.Text = userInfo.Name;
        }
        // 启动页-正版启动
        private void ZB_Start(object sender, RoutedEventArgs e)
        {
            LaunchConfig config = new LaunchConfig
            {
                Account = UserInfo.AccessToken, //账户信息的获取请使用验证器，使用方法请跳转至验证器文档查看
                GameWindowConfig = new GameWindowConfig
                {
                    Width = 600,
                    Height = 800,
                    IsFullscreen = false
                },
                JvmConfig = new JvmConfig(Settings.JavaList.SelectedValue + "\\javaw.exe")
                {
                    MaxMemory = int.Parse(Settings.MemoryBox.Text),
                    MinMemory = 0
                },
                IsEnableIndependencyCore = true//是否启用版本隔离
            };
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
        private async void TStart(object sender, RoutedEventArgs e)
        {
            var auth = new UnifiedPassAuthenticator(TName.Text, TPassword.Text, ServerID.Text);
            var userInfo = await auth.UnifiedPassAuthAsync();
        }
    }
}
