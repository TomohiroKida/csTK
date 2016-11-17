using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace csTK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Quads);
            GL.Color4(Color4.White);
            GL.Vertex3(-1.0f, 1.0f, 4.0f);
            GL.Color4(Color4.Red);
            GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color4(Color4.Lime);
            GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color4(Color4.Blue);
            GL.Vertex3(1.0f, 1.0f, 4.0f);
            GL.End();

            glControl.SwapBuffers();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0,0,glControl.Size.Width, glControl.Size.Height);
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                (float)glControl.Size.Width / (float)glControl.Size.Height,
                1.0f, 
                64.0f);
            GL.LoadMatrix(ref projection);
        }
    }
}
