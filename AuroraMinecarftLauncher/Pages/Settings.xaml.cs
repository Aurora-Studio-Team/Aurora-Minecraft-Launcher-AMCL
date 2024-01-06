using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Page
    {
        public static ComboBox JavaList { get; set; }
        
        public static TextBox MemoryBox { get; set; }
        
        public Settings()
        {
            InitializeComponent();

            Url1.Navigate("https://afdian.net/a/thzstudent");

            JavaList = Java;
            MemoryBox = MemoryTextbox;
        }

        public class Data1
        {
            public int java { get; set; }
            public int MemoryTextbox { get; set; }
        }

        private void ULA_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://amcl.thzstudent.top/doc/ula.html");
        }
        // GitHub
        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/Aurora-Studio-Team/Aurora-Minecarft-Launcher-AMCL");
        }
        // Gitee
        private void Gitee_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://gitee.com/THZtx/Aurora-Minecarft-Launcher-AMCL");
        }
        // GW
        private void GW_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://amcl.thzstudent.top");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }
    }
}
