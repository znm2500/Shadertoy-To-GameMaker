using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// ed.xaml 的交互逻辑
    /// </summary>
    /// 
   
    public partial class ed : Window
    {
        public channel[] ic = new channel[4];
        public channel[] ac = new channel[4];
        public channel[] bbc = new channel[4];
        public channel[] cc = new channel[4];
        public channel[] dc = new channel[4];
        public Convert com = new Convert(new channel[0], "Common"), bufferA, bufferB, bufferC, bufferD, img;
    
        
    
        public ed()
        {
            
            for (int i = 0; i < 4; i++)
            {
                ic[i] = new channel();
                ac[i] = new channel();
                bbc[i] = new channel();
                cc[i] = new channel();
                dc[i] = new channel();
            }

            bufferA = new Convert(ac, "BufferA");
            bufferB = new Convert(bbc, "BufferB");
            bufferC = new Convert(cc, "BufferC");
            bufferD = new Convert(dc, "BufferD");
            img = new Convert(ic, "Image");
            InitializeComponent();
            bg.ImageSource = new BitmapImage(new Uri("BG_Image.png", UriKind.Relative));
            common.Selected += c_Convert;
            image.Selected += im_Convert;
            ba.Selected += ba_Convert;
            bb.Selected += bb_Convert;
            bc.Selected += bc_Convert;
            bd.Selected += bd_Convert;
            I0.Selected += i0_Channel;
            I1.Selected += i1_Channel;
            I2.Selected += i2_Channel;
            I3.Selected += i3_Channel;
            A0.Selected += a0_Channel;
            A1.Selected += a1_Channel;
            A2.Selected += a2_Channel;
            A3.Selected += a3_Channel;
            B0.Selected += b0_Channel;
            B1.Selected += b1_Channel;
            B2.Selected += b2_Channel;
            B3.Selected += b3_Channel;
            C0.Selected += c0_Channel;
            C1.Selected += c1_Channel;
            C2.Selected += c2_Channel;
            C3.Selected += c3_Channel;
            D0.Selected += d0_Channel;
            D1.Selected += d1_Channel;
            D2.Selected += d2_Channel;
            D3.Selected += d3_Channel;
        }
        private void d2_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(dc[2]);
        }
        private void d3_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(dc[3]);
        }
        private void d0_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(dc[0]);
        }
        private void d1_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(dc[1]);
        }
        private void c2_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(cc[2]);
        }
        private void c3_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(cc[3]);
        }
        private void c0_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(cc[0]);
        }
        private void c1_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(cc[1]);
        }
        private void b2_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(bbc[2]);
        }
        private void b3_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(bbc[3]);
        }
        private void b0_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(bbc[0]);
        }
        private void b1_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(bbc[1]);
        }
        private void a2_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ac[2]);
        }
        private void a3_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ac[3]);
        }
        private void a0_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ac[0]);
        }
        private void a1_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ac[1]);
        }
        private void i1_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ic[1]);
        }
        private void i2_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ic[2]);
        }
        private void i3_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ic[3]);
        }
        private void i0_Channel(object sender, RoutedEventArgs e)
        {

            myframe.Navigate(ic[0]);
        }
        private void im_Convert(object sender, RoutedEventArgs e)
        {
            
            if(!(I0.IsSelected|| I1.IsSelected|| I2.IsSelected|| I3.IsSelected))
            myframe.Navigate(img);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (img.output.Text == null) { MessageBox.Show("主Shader代码未输入！");
                return;
            }
            StringBuilder create = new StringBuilder(), clean = new StringBuilder(), draw = new StringBuilder(),dp=new StringBuilder();
            create.Append(bufferA.codes[1]);
            create.Append(bufferB.codes[1]);
            create.Append(bufferC.codes[1]);
            create.Append(bufferD.codes[1]);
            create.Append(img.codes[1]);
            draw.Append(bufferA.codes[2]);
            draw.Append(bufferB.codes[2]);
            draw.Append(bufferC.codes[2]);
            draw.Append(bufferD.codes[2]);
            draw.Append(img.codes[2]);
            clean.Append(bufferA.codes[3]);
            clean.Append(bufferB.codes[3]);
            clean.Append(bufferC.codes[3]);
            clean.Append(bufferD.codes[3]);
            clean.Append(img.codes[3]);
            
                dp.Append(bufferA.codes[4]);
                dp.Append(bufferB.codes[4]);
                dp.Append(bufferC.codes[4]);
                dp.Append(bufferD.codes[4]);
                dp.Append(img.codes[4]);
                dp.Append(draw);
            
            try
            {
                
                
                
                
                Directory.CreateDirectory("temp/objects");
                Directory.CreateDirectory($"temp/objects/obj_{Converter.SHADER}");

                Directory.CreateDirectory("temp/shaders");
                Directory.CreateDirectory("temp/sprites");
                File.Copy("metadata.json", "temp/metadata.json");
                File.Copy("temp.resource_order", "temp/temp.resource_order");
                string vsh = @"//
// Simple passthrough vertex shader
//
attribute vec3 in_Position;                  // (x,y,z)
//attribute vec3 in_Normal;                  // (x,y,z)     unused in this shader.
attribute vec4 in_Colour;                    // (r,g,b,a)
attribute vec2 in_TextureCoord;              // (u,v)

varying vec2 v_vTexcoord;
varying vec4 v_vColour;

void main()
{
    vec4 object_space_pos = vec4( in_Position.x, in_Position.y, in_Position.z, 1.0);
    gl_Position = gm_Matrices[MATRIX_WORLD_VIEW_PROJECTION] * object_space_pos;
    
    v_vColour = in_Colour;
    v_vTexcoord = in_TextureCoord;
}
";
                if (img.codes[0].Contains("main()"))
                    {
                    var code=bufferA.codes[0];
                    
                    if (com.output.Text != null) code = String.Concat(com.output.Text, img.codes[0]);
                        Directory.CreateDirectory($"temp/shaders/shd_{Converter.SHADER}");
                        File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}/shd_{Converter.SHADER}.vsh",vsh);
                       
                    
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}/shd_{Converter.SHADER}.fsh", code);
                    string js = File.ReadAllText(@"shd_temp.yy", Encoding.UTF8);
                    var jo = JObject.Parse(js);
                    jo["name"] = $"shd_{Converter.SHADER}";
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}/shd_{Converter.SHADER}.yy", JsonConvert.SerializeObject(jo, Formatting.Indented));
                    }
                if (bufferA.codes[0].Contains("main()"))
                {
                    var code = bufferA.codes[0];
                    if (com.output.Text != null) code = String.Concat(com.output.Text, bufferA.codes[0]);
                    Directory.CreateDirectory($"temp/shaders/shd_{Converter.SHADER}_bfA");
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfA/shd_{Converter.SHADER}_bfA.vsh", vsh);


                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfA/shd_{Converter.SHADER}_bfA.fsh", code);
                    string js = File.ReadAllText(@"shd_temp.yy", Encoding.UTF8);
                    var jo = JObject.Parse(js);
                    jo["name"] = $"shd_{Converter.SHADER}_bfA";
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfA/shd_{Converter.SHADER}_bfA.yy", JsonConvert.SerializeObject(jo, Formatting.Indented));
                }
                if (bufferB.codes[0].Contains("main()"))
                {
                    var code = bufferB.codes[0];
                    if (com.output.Text != null) code = String.Concat(com.output.Text, bufferB.codes[0]);
                    Directory.CreateDirectory($"temp/shaders/shd_{Converter.SHADER}_bfB");
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfB/shd_{Converter.SHADER}_bfB.vsh", vsh);


                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfB/shd_{Converter.SHADER}_bfB.fsh", code);
                    string js = File.ReadAllText(@"shd_temp.yy", Encoding.UTF8);
                    var jo = JObject.Parse(js);
                    jo["name"] = $"shd_{Converter.SHADER}_bfB";
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfB/shd_{Converter.SHADER}_bfB.yy", JsonConvert.SerializeObject(jo, Formatting.Indented));
                }
                if (bufferC.codes[0].Contains("main()"))
                {
                    var code = bufferC.codes[0];
                    if (com.output.Text != null) code = String.Concat(com.output.Text, bufferC.codes[0]);
                    Directory.CreateDirectory($"temp/shaders/shd_{Converter.SHADER}_bfC");
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfC/shd_{Converter.SHADER}_bfC.vsh", vsh);
     
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfC/shd_{Converter.SHADER}_bfC.fsh", code);
                    string js = File.ReadAllText(@"shd_temp.yy", Encoding.UTF8);
                    var jo = JObject.Parse(js);
                    jo["name"] = $"shd_{Converter.SHADER}_bfC";
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfC/shd_{Converter.SHADER}_bfC.yy", JsonConvert.SerializeObject(jo, Formatting.Indented));
                }
                if (bufferD.codes[0].Contains("main()"))
                {
                    var code = bufferD.codes[0];
                    if (com.output.Text != null) code = String.Concat(com.output.Text, bufferD.codes[0]);
                    Directory.CreateDirectory($"temp/shaders/shd_{Converter.SHADER}_bfD");
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfD/shd_{Converter.SHADER}_bfD.vsh", vsh);
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfD/shd_{Converter.SHADER}_bfD.fsh", code);
                    string js = File.ReadAllText(@"shd_temp.yy", Encoding.UTF8);
                    var jo = JObject.Parse(js);
                    jo["name"] = $"shd_{Converter.SHADER}_bfD";
                    File.WriteAllText($"temp/shaders/shd_{Converter.SHADER}_bfD/shd_{Converter.SHADER}_bfD.yy", JsonConvert.SerializeObject(jo, Formatting.Indented));
                }
                File.WriteAllText($"temp/objects/obj_{Converter.SHADER}/CleanUp_0.gml",clean.ToString());
                File.WriteAllText($"temp/objects/obj_{Converter.SHADER}/Create_0.gml",create.ToString());
                File.WriteAllText($"temp/objects/obj_{Converter.SHADER}/Draw_0.gml",dp.ToString());
                var obj = JObject.Parse(File.ReadAllText(@"obj_temp.yy", Encoding.UTF8));
                obj["name"] = $"obj_{Converter.SHADER}";
                File.WriteAllText($"temp/objects/obj_{Converter.SHADER}/obj_{Converter.SHADER}.yy", JsonConvert.SerializeObject(obj, Formatting.Indented));
                Dictionary<string,string> dic = new Dictionary<string,string>();
                foreach(var chan in ic)
                {
                    if (File.Exists(chan.file_spr) && Regex.IsMatch(chan.name.Text, @"^[\w_]+$"))
                    {
                        dic[chan.name.Text]=chan.file_spr;
                    }
                }

                foreach (var chan in ac)
                {
                    if (File.Exists(chan.file_spr) && Regex.IsMatch(chan.name.Text, @"^[\w_]+$"))
                    {
                        dic[chan.name.Text] = chan.file_spr;
                    }
                }
                foreach (var chan in bbc)
                {
                    if (File.Exists(chan.file_spr) && Regex.IsMatch(chan.name.Text, @"^[\w_]+$"))
                    {
                        dic[chan.name.Text] = chan.file_spr;
                    }
                }
                foreach (var chan in cc)
                {
                    if (File.Exists(chan.file_spr) && Regex.IsMatch(chan.name.Text, @"^[\w_]+$"))
                    {
                        dic[chan.name.Text] = chan.file_spr;
                    }
                }
                foreach (var chan in dc)
                {
                    if (File.Exists(chan.file_spr) && Regex.IsMatch(chan.name.Text, @"^[\w_]+$"))
                    {
                        dic[chan.name.Text] = chan.file_spr;
                    }
                }
                foreach(var pair in dic)
                {

                    Directory.CreateDirectory($"temp/sprites/{pair.Key}");

                    string spr = Guid.NewGuid().ToString();
                    string layer = Guid.NewGuid().ToString();
                    string fid = Guid.NewGuid().ToString();
                    var pic = new BitmapImage(new Uri(pair.Value, UriKind.Absolute));
                    Directory.CreateDirectory($"temp/sprites/{pair.Key}/layers/{spr}");
                    string ext = System.IO.Path.GetExtension(pair.Value);
                    File.Copy(pair.Value, $"temp/sprites/{pair.Key}/{spr}{ext}");
                    File.Copy(pair.Value, $"temp/sprites/{pair.Key}/layers/{spr}/{layer}{ext}");
                    var spy = JObject.Parse(File.ReadAllText(@"spr_temp.yy", Encoding.UTF8));
                    spy["name"] = pair.Key;
                    spy["width"] = pic.PixelWidth;
                    spy["height"] = pic.PixelHeight;
                    spy["bbox_bottom"] = pic.PixelHeight - 1;
                    spy["bbox_right"] = pic.PixelWidth - 1;
                    var jf = (JArray)spy["frames"];
                    var ja = (JObject)jf[0];
                    ja["name"] = spr;
                    jf = (JArray)spy["layers"];
                    ja = (JObject)jf[0];
                    ja["name"] = layer;
                    spy["sequence"]["name"] = pair.Key;
                    JObject ks = (JObject)spy["sequence"]?["tracks"]?[0]?["keyframes"];
                    jf = (JArray)ks["Keyframes"];
                    ja = (JObject)jf[0];
                    ja["id"] = fid;
                    ja["Channels"]["0"]["Id"]["name"] = spr;

                    ja["Channels"]["0"]["Id"]["path"] = $"sprites/{pair.Key}/{pair.Key}.yy";
                    File.WriteAllText($"temp/sprites/{pair.Key}/{pair.Key}.yy", JsonConvert.SerializeObject(spy, Formatting.Indented));

                }
                string josnString = File.ReadAllText(@"temp.yyp", Encoding.UTF8);
                var yyp = JObject.Parse(josnString);
                var resources = (JArray)yyp["resources"];
                var res = (JObject)resources[0];
                res["id"]["name"] = $"obj_{Converter.SHADER}";
                res["id"]["path"] = $"objects/obj_{Converter.SHADER}/obj_{Converter.SHADER}.yy";
                string[] shddirs = Directory.GetDirectories("temp/shaders", "*", SearchOption.TopDirectoryOnly);
                string[] sprdirs = Directory.GetDirectories("temp/sprites", "*", SearchOption.TopDirectoryOnly);
                foreach( var shddir in shddirs)
                {
                    string n = System.IO.Path.GetFileName(shddir);
                    var newResource = new JObject
                    {
                        ["id"] = new JObject
                        {
                            ["name"] = n,
                            ["path"] = $"shaders/{n}/{n}.yy"
                        }
                    };
                    resources.Add(newResource);
                }
                foreach (var shddir in sprdirs)
                {
                    string n = System.IO.Path.GetFileName(shddir);
                    var newResource = new JObject
                    {
                        ["id"] = new JObject
                        {
                            ["name"] = n,
                            ["path"] = $"sprites/{n}/{n}.yy"
                        }
                    };
                    resources.Add(newResource);
                }
                File.WriteAllText($"temp/temp.yyp", JsonConvert.SerializeObject(yyp, Formatting.Indented));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "窗口标题";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "yymps";
            saveFileDialog.Filter = "Gamemaker资源包|*.yymps";
            saveFileDialog.FileName = $"{Converter.SHADER}.yymps";
            if ((bool)saveFileDialog.ShowDialog())
            {
                
                try
                {
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        File.Delete(saveFileDialog.FileName);
                    }
                    ZipFile.CreateFromDirectory("temp",saveFileDialog.FileName);
                    string folder = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                    Process.Start("explorer.exe", folder);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("导出失败！");
                    
                }
                
            }
            DirectoryInfo di = new DirectoryInfo("temp");
            di.Delete(true);
            
        }

        private void c_Convert(object sender, RoutedEventArgs e)
        {
            
            myframe.Navigate(com);
        }
        private void ba_Convert(object sender, RoutedEventArgs e)
        {
            if (!(A0.IsSelected || A1.IsSelected || A2.IsSelected || A3.IsSelected))
                myframe.Navigate(bufferA);
        }
        private void bb_Convert(object sender, RoutedEventArgs e)
        {
            if (!(B0.IsSelected || B1.IsSelected || B2.IsSelected || B3.IsSelected))
                myframe.Navigate(bufferB);
        }
        private void bc_Convert(object sender, RoutedEventArgs e)
        {
            if (!(C0.IsSelected || C1.IsSelected || C2.IsSelected || C3.IsSelected))
                myframe.Navigate(bufferC);
        }
        private void bd_Convert(object sender, RoutedEventArgs e)
        {
            if (!(D0.IsSelected || D1.IsSelected || D2.IsSelected || D3.IsSelected))
                myframe.Navigate(bufferD);
        }
    }
}
