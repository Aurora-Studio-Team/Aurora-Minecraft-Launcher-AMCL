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
using AuroraMinecarftLauncher.LoginUI;

namespace AuroraMinecarftLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LoginUI.LiXian Lixian = new LoginUI.LiXian();
        LoginUI.WeiRuan WeiRuan = new LoginUI.WeiRuan();

        public static LauncherCore Core = LauncherCore.Create();

        // 配置项
        public MainWindow()
        {
            InitializeComponent();
            // 自动寻找版本
            var versions = Core.GetVersions().ToArray();
            version.ItemsSource = versions;//绑定数据源
            version.DisplayMemberPath = "Id";//设置comboBox显示的为版本Id

            // 有点问题，需要改改，先写正版
            // 自动寻找Java
            var javaInfo = JavaUtil.GetJavas();
            string javaPath = javaInfo.First().JavaPath;
            java.DisplayMemberPath = "JavaLibraryPath";
            java.SelectedValuePath = "JavaLibraryPath";
            java.ItemsSource = javaInfo;

            // 初始选择
            version.SelectedItem = 0;
            java.SelectedItem = 0;
            }

        public void GameStart()
        {
            if (LoginUI.LiXian.OfflineID.Text != string.Empty&&java.Text != string.Empty&&version.Text != string.Empty&& MemoryTextbox.Text != string.Empty)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }
        // 离线
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginControl.Content = new Frame
            {
                Content = Lixian
            };
        }
        // 微软
        private async Task Button_Click_2(object sender, RoutedEventArgs e)
        {
            /*
            var V = MessageBox.Show("确定开始验证您的账户", "验证", MessageBoxButton.OKCancel);
            MicrosoftAuthenticator microsoftAuthenticator = new(MinecaftOAuth.Module.Enum.AuthType.Access)
            {
                ClientId = "ed0e15b9-fa1e-489b-b83d-7a66ff149abd"
            };
            var code = await microsoftAuthenticator.GetDeviceInfo();
            MessageBox.Show(code.UserCode, "你的一次性访问代码 确定开始验证账户");
            Debug.WriteLine("Link:{0} - Code:{1}", code.VerificationUrl, code.UserCode);
            if (V == MessageBoxResult.OK)
            {
                Process.Start(new ProcessStartInfo(code.VerificationUrl)
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            var token = await microsoftAuthenticator.GetTokenResponse(code);
            var user = await microsoftAuthenticator.AuthAsync(x =>
            {
                Debug.WriteLine(x);
            });
            UserInfo = user;
            Debug.WriteLine("欢迎回来, {0}", user.Name);
            */
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

