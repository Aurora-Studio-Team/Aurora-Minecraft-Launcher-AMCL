using AuroraMinecarftLauncher.Pages;
using System;
using System.Windows;

// 导入的模块
using System.Windows.Input;

namespace AuroraMinecarftLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            tz.NavigationService.Navigate(new Pages.Home());
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

        // home
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            tz.NavigationService.Navigate(new Pages.Home());
        }

        // links
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tz.NavigationService.Navigate(new Pages.Links());
        }

        // download
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            tz.NavigationService.Navigate(new Pages.Download());
        }

        // setings
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            tz.NavigationService.Navigate(new Pages.Settings());
        }


        // 导航栏 E
    }
}