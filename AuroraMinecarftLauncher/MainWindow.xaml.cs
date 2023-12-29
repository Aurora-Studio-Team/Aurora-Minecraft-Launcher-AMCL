using System;
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

        // 下载&安装路径
        private string downloadPath = ".minecraft/versions/";
        private string installPath = ".minecraft/versions/";


        // 配置项
        public MainWindow()
        {
            InitializeComponent();

            _httpClient = new HttpClient();

            

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
            if (IDTextbox.Text != string.Empty && java.Text != string.Empty && version.Text != string.Empty && MemoryTextbox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = java.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(MemoryTextbox.Text), //最大内存，int类型
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
            string clientId = "a0ceb477-0738-47fa-8c93-52d892aa866a";
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
            if (IDTextbox.Text != string.Empty && java.Text != string.Empty && version.Text != string.Empty && MemoryTextbox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = java.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(MemoryTextbox.Text), //最大内存，int类型
                        
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
            var userInfo = await UnifiedPassAuthenticator.Authenticate(TName.Text, TPassword.Text, ServerID.Text);
            if (IDTextbox.Text != string.Empty && java.Text != string.Empty && version.Text != string.Empty && MemoryTextbox.Text != string.Empty)
            {
                try
                {
                    Core.JavaPath = java.SelectedValue + "\\javaw.exe";
                    var ver = (KMCCC.Launcher.Version)version.SelectedItem;
                    var result = Core.Launch(new LaunchOptions
                    {
                        Version = ver, //Ver为Versions里你要启动的版本名字
                        MaxMemory = Convert.ToInt32(MemoryTextbox.Text), //最大内存，int类型
                        
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

       
        private void jdt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        // D-install
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
        // D-MC
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        // D-Forge
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        // D-OF
        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {

        }
        // D-Fadric
        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {

        }
        // D-OF of Fabric
        private void ComboBox_SelectionChanged_5(object sender, SelectionChangedEventArgs e)
        {

        }
        // D-Fabric API
        private void ComboBox_SelectionChanged_6(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}