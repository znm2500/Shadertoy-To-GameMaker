using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Shadertoy_to_Gamemaker
{
    /// <summary>
    /// Warning.xaml 的交互逻辑
    /// </summary>
    public partial class Warning : Window
    {
        public Warning()
        {
            InitializeComponent();
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
                mainWindow.Closed += (s, e) => Close();
            bg.ImageSource = new BitmapImage(new Uri("BG_Image.png", UriKind.Relative));
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Hyperlink link = sender as Hyperlink;
                // 激活的是当前默认的浏览器
                Process.Start(new ProcessStartInfo("explorer.exe",link.NavigateUri.AbsoluteUri));
                
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
