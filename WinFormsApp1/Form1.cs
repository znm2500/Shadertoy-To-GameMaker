using System.Threading.Channels;

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
            string code=textBox1.Text;
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
            if (radioButton2.Checked)
            {
                string SHADER=textBox4.Text;
                textBox2.Text = string.Format($"shader_set({SHADER}); \r\nshader_set_uniform_f(shader_get_uniform({SHADER},\"iTime\"),current_time/1000); \r\ndraw_rectangle(0,0,640,480,0); \r\nshader_reset(); ");
            }
            else if (radioButton1.Checked) {
                string SHADER = textBox4.Text;
                textBox2.Text = string.Format($"shader_set({SHADER}); \r\nshader_set_uniform_f(shader_get_uniform({SHADER},\"iTime\"),current_time/1000); \r\ndraw_surface(application_surface,0,0); \r\nshader_reset(); ");
            }
        }
    }
}
