using System;
using System.Collections.Generic;
using System.IO;
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

namespace ImageSourceEx
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        //写一个快速排序的算法
        public static void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int i = left, j = right;
                int x = arr[left];
                while (i < j)
                {
                    while (i < j && arr[j] >= x)
                        j--;
                    if (i < j)
                        arr[i++] = arr[j];
                    while (i < j && arr[i] < x)
                        i++;
                    if (i < j)
                        arr[j--] = arr[i];
                }
                arr[i] = x;
                QuickSort(arr, left, i - 1);
                QuickSort(arr, i + 1, right);
            }
        }



        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ImageSource imageSource = null;

            //用来模拟时间回调后，去获取图片资源
            Task.Run(() =>
            {
                 imageSource = BitmapImageToBitmap();
            });

            await Task.Delay(2000);
            MyImage.Source = imageSource;

        }

        public static BitmapImage BitmapImageToBitmap()
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(@"C:\Users\a\Desktop\祁厅长壁纸.png", UriKind.RelativeOrAbsolute));

            if (bitmapImage.CanFreeze)
            {
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

    }
}
