using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;
using Wpf.Ui.Controls;

namespace Shadertoy_to_Gamemaker
{
    /// <summary>
    /// Common.xaml 的交互逻辑
    /// </summary>
    public partial class Convert : Page
    {
        public channel[] channels;
        public string scr;
        public string[] codes=["","","","",""];
        public Convert(channel[] channels, string scr)
        {
            InitializeComponent();
            this.channels = channels;
            this.scr = scr;
         
        }

        private void convert_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(input.Document.ContentStart, input.Document.ContentEnd);
            string[] cn = new string[channels.Length];
            string[] c = new string[] { "", "", "", "" ,""};
            string[] d = new string[channels.Length];
            for (int i = 0; i < channels.Length; i++)
            {

                cn[i] = channels[i].combo.SelectedItem.ToString();
                d[i] = channels[i].file_spr;
                if (channels[i].combo.SelectedItem.ToString() == "贴图" || channels[i].combo.SelectedItem.ToString() == "表面")
                {
                    if (channels[i].name.Text != null)
                    {
                        c[i] = channels[i].name.Text;
                    }
                }
                if (cn[i] == "表面" && c[i] == "aplsur") {
                    System.Windows.MessageBox.Show("命名与游戏画面冲突！");
                    return; }
            }
            switch (scr) {
                case "BufferA":
                    if (textRange.Text.Contains("main")) Converter.bf[0] = true;
                    else Converter.bf[0] = false;
                    break;
                case "BufferB":
                    if(textRange.Text.Contains("main")) Converter.bf[1] = true;
                    else Converter.bf[1] = false;
                    break;
                case "BufferC":
                    if(textRange.Text.Contains("main")) Converter.bf[2] = true;
                    else Converter.bf[2]= false;
                        break;
                case "BufferD":
                    if(textRange.Text.Contains("main")) Converter.bf[3] = true;
                    else Converter.bf[3]= false;
                        break;
            }

         
            codes = Converter.Convert(textRange.Text,  cn, scr,c,d);
            output.Text = codes[0];
            
        }
    }
}
