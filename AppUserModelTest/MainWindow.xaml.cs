using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppUserModelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            //SetPinning();
        }

        private void SetPinning()
        {
            //return;
            IntPtr hwnd = new WindowInteropHelper(this).Handle;

            // Define the property key for System.AppUserModel.PreventPinning
            NativeWin32.PROPERTYKEY propKey = new NativeWin32.PROPERTYKEY(new Guid("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}"), 9);

            // Get the IPropertyStore for the window
            NativeWin32.IPropertyStore propStore;
            int hr = NativeWin32.SHGetPropertyStoreForWindow(hwnd, ref NativeWin32.IID_IPropertyStore, out propStore);
            if (hr != 0) // if failed
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            try
            {
                // Create a PROPVARIANT with bool value: true
                NativeWin32.PROPVARIANT pv = new NativeWin32.PROPVARIANT();
                pv.SetValue(true);

                // Set the property
                propStore.SetValue(ref propKey, ref pv);

                // We must free the PROPVARIANT
                NativeWin32.PropVariantClear(ref pv);
            }
            finally
            {
                // Release the IPropertyStore
                Marshal.ReleaseComObject(propStore);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetPinning();
        }
    }

    public class NativeWin32
    {
        [DllImport("shell32.dll")]
        public static extern int SHGetPropertyStoreForWindow(IntPtr hwnd, ref Guid riid, out IPropertyStore propertyStore);

        [DllImport("ole32.dll")]
        public static extern int PropVariantClear(ref PROPVARIANT pvar);

        // Define IPropertyStore interface
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
        public interface IPropertyStore
        {
            void GetCount(out uint cProps);
            void GetAt(uint iProp, out PROPERTYKEY pkey);
            void GetValue(ref PROPERTYKEY key, out PROPVARIANT pv);
            void SetValue(ref PROPERTYKEY key, ref PROPVARIANT pv);
            void Commit();
        }

        // Define IID for IPropertyStore
        public static Guid IID_IPropertyStore = new Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99");

        // Define PROPERTYKEY struct
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PROPERTYKEY
        {
            public Guid fmtid;
            public uint pid;

            public PROPERTYKEY(Guid fmtid, uint pid)
            {
                this.fmtid = fmtid;
                this.pid = pid;
            }
        }

        // Define PROPVARIANT structure (simplified, we'll use a simple one for bool)
        [StructLayout(LayoutKind.Explicit)]
        public struct PROPVARIANT
        {
            // We'll only implement the necessary part for boolean
            [FieldOffset(0)] public ushort vt;
            [FieldOffset(8)] public byte boolVal;

            public void SetValue(bool value)
            {
                // VT_BOOL
                vt = 11;
                boolVal = value ? (byte)1 : (byte)0;
            }
        }

    }
}