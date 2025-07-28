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
using System.Text.RegularExpressions;
using Wpf.Ui.Appearance;
namespace Shadertoy_to_Gamemaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   public static class Converter
    {
        public static string SHADER;
        public static bool ef;
        public static bool[] bf=new bool[] { false,false,false,false};
        public static List<string> tmp_spr_file=new List<string>();
       public static string[] Convert(string code,string[] channels,string scr, string[] names,string[] spr=null ) {
            string houz = "";
            bool[] finished = [false, false, false, false, false];
            StringBuilder ev_f = new StringBuilder(), ev_c = new StringBuilder(), ev_d = new StringBuilder(), dp = new StringBuilder();
            switch (scr)
            {
                case "BufferA":
                    houz = "_bfA";
                    ev_c.Append("surface_bfA = surface_create(room_width, room_height);\r\n");
                    ev_f.Append("surface_free(surface_bfA);\r\n");
                    dp.Append("if (!surface_exists(surface_bfA)) surface_bfA = surface_create(room_width, room_height);\r\n");
                    ev_d.Append("surface_set_target(surface_bfA);\r\ndraw_clear_alpha(c_black, 0);\r\n");
                    break;
                case "BufferB":
                    houz = "_bfB";
                    ev_c.Append("surface_bfB = surface_create(room_width, room_height);\r\n");
                    ev_f.Append("surface_free(surface_bfB);\r\n");
                    ev_d.Append("surface_set_target(surface_bfB);\r\ndraw_clear_alpha(c_black, 0);\r\n");
                    dp.Append("if (!surface_exists(surface_bfB)) surface_bfB = surface_create(room_width, room_height);\r\n");
                    break;
                case "BufferC":
                    houz = "_bfC";
                    ev_c.Append("surface_bfC = surface_create(room_width, room_height);\r\n");
                    ev_f.Append("surface_free(surface_bfC);\r\n");
                    ev_d.Append("surface_set_target(surface_bfC);\r\ndraw_clear_alpha(c_black, 0);\r\n");
                    dp.Append("if (!surface_exists(surface_bfC)) surface_bfC = surface_create(room_width, room_height);\r\n");
                    break;
                case "BufferD":
                    houz = "_bfD";
                    ev_c.Append("surface_bfD = surface_create(room_width, room_height);\r\n");
                    ev_f.Append("surface_free(surface_bfD);\r\n");
                    ev_d.Append("surface_set_target(surface_bfD);\r\ndraw_clear_alpha(c_black, 0);\r\n");
                    dp.Append("if (!surface_exists(surface_bfD)) surface_bfD = surface_create(room_width, room_height);\r\n");
                    break;
                case "Image":
                    ev_c.Append("surface = surface_create(room_width, room_height);\r\n");
                    ev_f.Append("surface_free(surface);\r\n");
                    ev_d.Append("surface_set_target(surface);\r\ndraw_clear_alpha(c_black, 0);\r\n");
                    dp.Append("if (!surface_exists(surface)) surface = surface_create(room_width, room_height);\r\n");
                    break;
            }
            StringBuilder pre=new StringBuilder($"varying vec4 v_vColour;\r\nvarying vec2 v_vTexcoord;\r\nvec2 iMouse=vec2(0.,0.);\r\nuniform float iTime;\r\nuniform vec2 iResolution;\r\n");
            if (ef) {
                code = Regex.Replace(code, @"fragCoord\.xy\s*/\s*iResolution.xy", "v_vTexcoord");
            }
            

            // 提取 mainImage 的参数名
            var paramMatch = Regex.Match(code, @"mainImage\s*\(\s*out\s+\w+\s+(\w+)\s*,\s*in\s+vec2\s+(\w+)\s*\)");

            if (paramMatch.Success)
            {
                string outVar = paramMatch.Groups[1].Value; // 比如 fragColor 或 color
                string inVar = paramMatch.Groups[2].Value;  // 比如 fragCoord 或 uv

                // 匹配 mainImage 函数体
                var bodyMatch = Regex.Match(code, @"void\s+mainImage\s*\([^\)]*\)\s*\{(?<body>.*?)\}", RegexOptions.Singleline);

                if (bodyMatch.Success)
                {
                    string body = bodyMatch.Groups["body"].Value;

                    // 替换函数体中的变量名
                    string newBody = body;
                    newBody = Regex.Replace(newBody, $@"\b{Regex.Escape(outVar)}\b", "fragColor");
                    newBody = Regex.Replace(newBody, $@"\b{Regex.Escape(inVar)}\b", "fragCoord");

                    // 替换回原代码
                    code = code.Substring(0, bodyMatch.Groups["body"].Index) +
                           newBody +
                           code.Substring(bodyMatch.Groups["body"].Index + bodyMatch.Groups["body"].Length);
                }
            }


            code = Regex.Replace(code, @"mainImage\(.*?\)", "main()",RegexOptions.Singleline);
            code = code.Replace("fragColor", "gl_FragColor");
            code = code.Replace("fragCoord.xy", "vec2(gl_FragCoord.x, iResolution.y - gl_FragCoord.y)");
            code = code.Replace("fragCoord", "vec2(gl_FragCoord.x, iResolution.y - gl_FragCoord.y)");
            code = code.Replace("fragCoord.y", "(iResolution.y - gl_FragCoord.y)");
            code = code.Replace("texture", "texture2D");
            code = Regex.Replace(code, @"(?<=main\(\)\s*\{).*(?=\})", m => $"\r\n\tgl_FragColor.a = 1.0;{m.Groups[0].Value}\tgl_FragColor *= v_vColour;\r\n",RegexOptions.Singleline);
            var matches = Regex.Matches(code, @"uniform.+;");
            
            ev_d.Append($"shader_set(shd_{SHADER}{houz});\r\n");

            foreach (Match match in matches)
            {
                var key=match.Value;
                if (key.Contains("float"))
                {
                    var create = Regex.Match(key, @"float\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f({create}_uniform,{create});");
                    ev_c.Append(" = 0;\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("int"))
                {
                    var create = Regex.Match(key, @"int\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i({create}_uniform,{create});");
                    ev_c.Append(" = 0;\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");


                }
                else if (key.Contains("vec2"))
                {
                    var create = Regex.Match(key, @"vec2\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("vec3"))
                {
                    var create = Regex.Match(key, @"vec3\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("vec4"))
                {
                    var create = Regex.Match(key, @"vec4\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0,0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("ivec2"))
                {
                    var create = Regex.Match(key, @"ivec2\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");


                }
                else if (key.Contains("ivec3"))
                {
                    var create = Regex.Match(key, @"ivec3\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("ivec4"))
                {
                    var create = Regex.Match(key, @"ivec4\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array({create}_uniform,{create});");
                    ev_c.Append(" = [0,0,0,0];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");

                }
                else if (key.Contains("mat2"))
                {
                    var create = Regex.Match(key, @"mat2\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array({create}_uniform,{create});");
                    ev_c.Append(" = [[0,0],[0,0]];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");


                }
                else if (key.Contains("mat3"))
                {
                    var create = Regex.Match(key, @"mat3\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array({create}_uniform,{create});");
                    ev_c.Append(" = [[0,0,0],[0,0,0],[0,0,0]];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");


                }
                else if (key.Contains("mat4"))
                {
                    var create = Regex.Match(key, @"mat4\s+([\w_]+)\s*;").Groups[1].Value;
                    create = String.Concat(create, houz);
                    ev_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array({create}_uniform,{create});");
                    ev_c.Append(" = [[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]];\r\n");
                    ev_c.Append(String.Format($"{create}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"{create}\");\r\n"));
                    ev_d.Append(s + "\r\n");


                }
            }
            ev_c.Append($"time{houz}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"iTime\");\r\nresolution{houz}_uniform = shader_get_uniform(shd_{SHADER}{houz},\"iResolution\");\r\n");
            ev_d.Append($"shader_set_uniform_f(time{houz}_uniform,current_time/1000);\r\nshader_set_uniform_f_array(resolution{houz}_uniform,[room_width,room_height]);\r\n");
            for (int i = 0; i < 4; i++)
            {
                if (!code.Contains($"iChannel{i}")) continue;
                switch (channels[i])
                {
                    case "基本纹理":
                        code = code.Replace($"iChannel{i}", "gm_BaseTexture");
                        
                        break;
                    case "贴图":
                        
                        code = code.Replace($"iChannel{i}", $"spr_texture{i}");
                        pre.Append($"uniform sampler2D spr_texture{i};\r\n");
                        
                        if (!Regex.IsMatch(names[i], @"^[\w_]+$"))
                        {
                            MessageBox.Show($"贴图名称不规范！");
                            return ["", "", "","",""];
                        }
                        ev_c.Append($"tex_{names[i]} = sprite_get_texture({names[i]}, 0);\r\nspr_texture{i}{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"spr_texture{i}\");\r\n");

                        ev_d.Append($"texture_set_stage(spr_texture{i}{houz}_uniform, tex_{names[i]});\r\n");
                        break;
                    case "表面":
                        code = code.Replace($"iChannel{i}", $"sur_texture{i}");
                        pre.Append($"uniform sampler2D sur_texture{i};\r\n");
                        if (!Regex.IsMatch(names[i], @"^[\w_]+$"))
                        {
                            MessageBox.Show("表面名称不规范！");
                            return ["", "", "","",""];

                        }
                        ev_c.Append($"sur_texture{i}{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"sur_texture{i}\");\r\n");
                        dp.Append($"tex_{names[i]} = surface_get_texture({names[i]});\r\n");
                        ev_d.Append($"texture_set_stage(sur_texture{i}_uniform, tex_{names[i]});\r\n");
                        break;
                    case "游戏画面":
                        code = code.Replace($"iChannel{i}", $"aplsur_texture");
                        if (finished[0]) break;
                        pre.Append($"uniform sampler2D aplsur_texture;\r\n");
                        
                        ev_c.Append($"aplsur{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"alpsur_texture\");\r\n");

                        ev_d.Append($"texture_set_stage(aplsur{houz}_uniform, tex_aplsur);\r\n");
                        dp.Append("tex_aplsur = surface_get_texture(application_surface);\r\n");
                        finished[0] = true;
                        break;
                    case "BufferA":
                        if (!bf[0])
                        {
                            MessageBox.Show("BufferA未编辑！");
                            return ["", "", "", "",""];
                        }
                        code = code.Replace($"iChannel{i}", "bufferA");
                        if(finished[1]) break;
                        pre.Append("uniform sampler2D bufferA;\r\n");
                        ev_c.Append($"bufferA{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"bufferA\");\r\n");

                        ev_d.Append($"texture_set_stage(bufferA{houz}_uniform, tex_bufferA);\r\n");
                        dp.Append("tex_bufferA = surface_get_texture(surface_bfA);\r\n");
                        break;
                    case "BufferB":
                        if (!bf[1])
                        {
                            MessageBox.Show("BufferB未编辑！");
                            return ["", "", "", "",""];
                        }
                        code = code.Replace($"iChannel{i}", "bufferB");
                        pre.Append("uniform sampler2D bufferB;\r\n");
                        ev_c.Append($"bufferB{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"bufferB\");\r\n");

                        ev_d.Append($"texture_set_stage(bufferB{houz}_uniform, tex_bufferB);\r\n");
                        dp.Append("tex_bufferB = surface_get_texture(surface_bfB);\r\n");
                        break;
                    case "BufferC":
                        if (!bf[2])
                        {
                            MessageBox.Show("BufferC未编辑！");
                            return ["", "", "", "",""];
                        }
                        code = code.Replace($"iChannel{i}", "bufferC");
                        pre.Append("uniform sampler2D bufferC;\r\n");
                        ev_c.Append($"bufferC{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"bufferC\");\r\n");

                        ev_d.Append($"texture_set_stage(bufferC{houz}_uniform, tex_bufferC);\r\n");
                        dp.Append("tex_bufferC = surface_get_texture(surface_bfC);\r\n");
                        break;
                    case "BufferD":
                        if (!bf[3])
                        {
                            MessageBox.Show("BufferD未编辑！");
                            return ["", "", "", "",""];
                        }
                        code = code.Replace($"iChannel{i}", "bufferD");
                        pre.Append("uniform sampler2D bufferD;\r\n");
                        ev_c.Append($"bufferD{houz}_uniform = shader_get_sampler_index(shd_{SHADER}{houz}, \"bufferD\");\r\n");

                        ev_d.Append($"texture_set_stage(bufferD{houz}_uniform, tex_bufferD);\r\n");
                        dp.Append("tex_bufferD = surface_get_texture(surface_bfD);\r\n");
                        break;
                }
            }
            if (ef)
            {
                ev_d.Append("draw_surface(application_surface, 0, 0);\r\n");
            }
            else
            {
                ev_d.Append("draw_rectangle(0, 0, room_width, room_height, false);\r\n");
            }
            ev_d.Append("shader_reset();\r\nsurface_reset_target();\r\n");
            if (scr == "Image") {     
                    ev_d.Append("draw_surface(surface, 0, 0);\r\n");
            }
                
            if(scr=="Common")
                return [code,"","","",""];
            code = String.Concat(pre.ToString(), code);
            
            return [code,ev_c.ToString(),ev_d.ToString(),ev_f.ToString(),dp.ToString()];
        }
    }
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
            Color c = Color.FromRgb(3, 157, 91);
            ApplicationAccentColorManager.Apply(c, ApplicationTheme.Dark);
            bg.ImageSource = new BitmapImage(new Uri("BG_Image.png", UriKind.Relative));
        }

      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string shd = t1.Text;
            
            
            ed ee;
            if (Regex.IsMatch(shd, @"^[\w_]+$")) {
                if ((bool)r1.IsChecked)
                {
                    Converter.ef = true;
                    Converter.SHADER = shd;
                    ee =new ed();
                    ee.Show();
                    this.Close();
                    
                }
                else {
                    ee = new ed();
                    Converter.ef = false;
                    Converter.SHADER = shd;
                    ee.Show();
                    this.Close();

                }
            }
            else
            {
                var sm = new Wpf.Ui.Controls.MessageBox();
                MessageBox.Show("Shader名称不合格");
            }

        }
    }
}