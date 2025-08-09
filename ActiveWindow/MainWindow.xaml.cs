using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActiveWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll")]
        private static extern bool SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);
        // 窗口显示状态常量
        private const int SW_SHOWNOACTIVATE = 4;  // 显示窗口但不激活
        private const int SW_SHOWNA = 8;           // 显示窗口但不激活
        private const int SW_SHOWMINIMIZED = 2;    // 显示最小化窗口
     
        // 窗口定位常量
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;
        // 定义窗口状态常量
        private const int SW_RESTORE = 9; // 激活并显示窗口。如果窗口最小化或最大化，系统将其还原到原始大小和位置。

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var intptr = FindWindow(null, "HideWindow");

            if (intptr != IntPtr.Zero)
            { 
                var isVisible = IsWindowVisible(intptr);
                var showResult = ShowWindow(intptr, SW_RESTORE);

                //uint flags = SWP_NOACTIVATE | SWP_NOZORDER | SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW;
               // SetWindowPos(intptr, IntPtr.Zero, 0, 0, 0, 0, 21);

                isVisible = IsWindowVisible(intptr);
                var setResult = SetForegroundWindow(intptr);
            }

            Show();

        }
    }
}