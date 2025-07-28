using Microsoft.Win32;
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

namespace Shadertoy_to_Gamemaker
{
    /// <summary>
    /// channel.xaml 的交互逻辑
    /// </summary>
    public partial class channel : Page
    {
        public string file_spr = "";
        
        public channel()
        {
            InitializeComponent();
            List<string> list = ["无","基本纹理","贴图", "表面","游戏画面", "BufferA", "BufferB", "BufferC", "BUfferD"];
            combo.ItemsSource = list;
            
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combo.SelectedItem.ToString()=="贴图") { import.IsEnabled = true;
                name.PlaceholderText = "贴图命名";
                name.IsEnabled = true;
                name.Visibility = Visibility.Visible;
                import.Visibility = Visibility.Visible;
            }
            else if(combo.SelectedItem.ToString() == "表面"){
                import.IsEnabled = false;
                name.PlaceholderText = "表面命名";
                name.IsEnabled = true;
                name.Visibility = Visibility.Visible;
                import.Visibility = Visibility.Hidden;
            } 
            else { import.IsEnabled = false;
                show.Source = null;
                file_spr = "";
                name.IsEnabled= false;
                name.Text = "";
                name.Visibility = Visibility.Hidden;
                import.Visibility = Visibility.Hidden;
            }
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件 (*.png;*.gif;*.jpg;*.jepg;*.tiff;*.ico;*.tif;*.bmp;*.2bp;*.mdi;*.svg;*.psd)|*.png;*.gif;*.jpg;*.jepg;*.tiff;*.ico;*.tif;*.bmp;*.2bp;*.mdi;*.svg;*.psd";
            if (ofd.ShowDialog() == true)
            {
                file_spr = ofd.FileName;
                show.Source = new BitmapImage(new Uri(file_spr));
            }
        }
    }
}
