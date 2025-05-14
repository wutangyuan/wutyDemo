using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           // Loaded += MainWindow_Loaded;
           //Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            timer?.Stop();
        }

        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        }
        private Timer timer;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void SelfTimer_OnClick(object sender, RoutedEventArgs e)
        {
            SelfAutoTimer();


            var driverInfos = DriveInfo.GetDrives().ToList().FindAll(x =>
                x.IsReady && x.DriveType == DriveType.Removable &&
                x.VolumeLabel.Equals("UPGRIDEDISK", StringComparison.OrdinalIgnoreCase));
        }

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        private void SelfAutoTimer()
        {
            
            Task.Run(async () =>
            {
                await Task.Delay(1000);
            });
            Task.Run(() =>
            {
                var current = DateTime.Now;

                while (!tokenSource.IsCancellationRequested)
                {
                    var temp = DateTime.Now;
                    if ((temp - current).TotalMilliseconds >=1)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                        current = temp;
                    }
                }
            });
        }
    }
}
