using Microsoft.Management.Infrastructure;
using System.Management;
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

namespace TestWmi
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

        private void BtnWmi_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // 创建 WMI 查询
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT SerialNumber FROM Win32_BIOS");

                // 遍历查询结果
                foreach (ManagementObject obj in searcher.Get())
                {
                    string serialNumber = obj["SerialNumber"]?.ToString();

                    if (!string.IsNullOrEmpty(serialNumber))
                    {
                        Console.WriteLine($"BIOS 序列号: {serialNumber}");
                        return;
                    }
                }

                Console.WriteLine("未找到 BIOS 序列号信息");
            }
            catch (ManagementException ex)
            {
                Console.WriteLine($"WMI 查询错误: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WMI 查询异常错误: {ex.Message}");
            }

        }

        private void BtnCim_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // 创建本地CIM会话
                using (var session = CimSession.Create(null))
                {
                    // 查询Win32_BIOS类
                    var instances = session.QueryInstances(@"root\cimv2", "WQL", "SELECT SerialNumber FROM Win32_BIOS");
                    foreach (var instance in instances)
                    {
                        var serialNumber = instance.CimInstanceProperties["SerialNumber"].Value?.ToString();
                        Console.WriteLine($"BIOS 序列号: {serialNumber}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
            }

        }
    }
}