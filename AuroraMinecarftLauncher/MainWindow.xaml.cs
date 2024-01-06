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
    }
}