using System.Security.Policy;
using System.Text;
using System.Threading.Channels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text;
            if (radioButton1.Checked)
            {
                code = code.Replace("fragCoord.xy/iResolution.xy", "v_vTexcoord");
                code = code.Replace("fragCoord.xy / iResolution.xy", "v_vTexcoord");
            }
            code = code.Replace("mainImage( out vec4 fragColor, in vec2 fragCoord )", "main()");
            code = "varying vec4 v_vColour;\r\nvarying vec2 v_vTexcoord;\r\nuniform float iTime;" + "\r\nvec2 iResolution=vec2(640.,480.);\r\nvec2 iMouse=vec2(0.,0.);\r\n" + code;
            code = code.Replace("fragColor =", "gl_FragColor = v_vColour *");
            code = code.Replace("fragColor", "gl_FragColor");

            code = code.Replace("fragCoord", "gl_FragCoord");
            code = code.Replace("texture", "texture2D");


            code = code.Replace("iChannel0", "gm_BaseTexture");
            textBox3.Text = code;
            string[] uniforms = code.Split(';');
            string draws = "", create = "";
            string SHADER = textBox4.Text;
            var sb_c=new StringBuilder();
            var sb_d=new StringBuilder();
            foreach (string key in uniforms)
            {
                if (key.Contains("uniform float") && !key.Contains("uniform float iTime"))
                {

                    create = key.Substring(key.IndexOf("uniform float") + 14, key.Length - (key.IndexOf("uniform float") + 14));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform int"))
                {
                    create = key.Substring(key.IndexOf("uniform int") + 12, key.Length - (key.IndexOf("uniform int") + 12));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");


                }
                else if (code.Contains("uniform vec2"))
                {
                    create = key.Substring(key.IndexOf("uniform vec2") + 13, key.Length - (key.IndexOf("uniform vec2") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform vec3"))
                {
                    create = key.Substring(key.IndexOf("uniform vec3") + 13, key.Length - (key.IndexOf("uniform vec3") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform vec4"))
                {
                    create = key.Substring(key.IndexOf("uniform vec4") + 13, key.Length - (key.IndexOf("uniform vec4") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_f_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform ivec2"))
                {
                    create = key.Substring(key.IndexOf("uniform ivec2") + 14, key.Length - (key.IndexOf("uniform ivec2") + 14));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");


                }
                else if (code.Contains("uniform ivec3"))
                {
                    create = key.Substring(key.IndexOf("uniform ivec3") + 14, key.Length - (key.IndexOf("uniform ivec3") + 14));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform ivec4"))
                {
                    create = key.Substring(key.IndexOf("uniform ivec4") + 14, key.Length - (key.IndexOf("uniform ivec4") + 14));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_i_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");

                }
                else if (code.Contains("uniform mat2"))
                {
                    create = key.Substring(key.IndexOf("uniform mat2") + 13, key.Length - (key.IndexOf("uniform mat2") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");


                }
                else if (code.Contains("uniform mat3"))
                {
                    create = key.Substring(key.IndexOf("uniform mat3") + 13, key.Length - (key.IndexOf("uniform mat3") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");


                }
                else if (code.Contains("uniform mat4"))
                {
                    create = key.Substring(key.IndexOf("uniform mat4") + 13, key.Length - (key.IndexOf("uniform mat4") + 13));
                    sb_c.Append(create);
                    string s = string.Format($"shader_set_uniform_matrix_array(shader_get_uniform({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");


                }
                else if (code.Contains("uniform sampler2D"))
                {
                    create = key.Substring(key.IndexOf("uniform sampler2D") + 18, key.Length - (key.IndexOf("uniform sampler2D") + 18));
                    sb_c.Append(create);
                    string s = string.Format($"texture_set_stage(shader_get_sampler_index({SHADER},\"{create}\"),{create});");
                    sb_c.Append(" = 0;\r\n");
                    sb_d.Append(s + "\r\n");
                }
            }
            create= sb_c.ToString();
            draws=sb_d.ToString();
            if (radioButton2.Checked)
            {
               
                    textBox2.Text = string.Format($"shader_set({SHADER}); \r\nshader_set_uniform_f(shader_get_uniform({SHADER},\"iTime\"),current_time/1000); \r\n{draws}draw_rectangle(0,0,640,480,0); \r\nshader_reset(); ");
                
               
            }
            else if (radioButton1.Checked)
            {

                
                    textBox2.Text = string.Format($"shader_set({SHADER}); \r\nshader_set_uniform_f(shader_get_uniform({SHADER},\"iTime\"),current_time/1000); \r\n{draws}draw_surface(application_surface,0,0); \r\nshader_reset(); ");
               

            }
            textBox5.Text = create;
        }
    }
}
