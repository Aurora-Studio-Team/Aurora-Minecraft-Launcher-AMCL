using StarLight_Core.Authentication;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StarLight_Core.Authentication;
using StarLight_Core.Models.Authentication;
using SquareMinecraftLauncher.Online;

using Natsurainko.FluentCore.Extension.Windows.Module.Authenticator;
using Natsurainko.FluentCore.Module.Authenticator;
using static System.Formats.Asn1.AsnWriter;
using PInvoke;


namespace AuroraMinecarftLauncher.LoginUI
{
    /// <summary>
    /// WeiRuan.xaml 的交互逻辑
    /// </summary>
    public partial class WeiRuan : Page
    {
        public static TextBlock WeiRuanID_N { get; set; }
        public static Image WeiRuanID_I { get; set; }
        public WeiRuan()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
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

            userName.Text = userInfo.Name;
            // userImge.Source = new BitmapImage(new Uri("@https://crafatar.com/avatars/"+userInfo.Name));
        }
    }
}
