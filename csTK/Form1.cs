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
using BezierClass;

namespace csTK
{
    public partial class Form1 : Form
    {
        Bezier3f st;
        public Form1()
        {
            st = new Bezier3f();
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
            Matrix4 modelview = Matrix4.LookAt(Vector3.UnitZ * 10 , Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);

            List<Vector3> a = new List<Vector3>();

            st.ctrlpline = new List<Vector3>();

            st.ctrlpline.Add(new Vector3(1, 2, 0));
            st.ctrlpline.Add(new Vector3(2, 3, 1));
            st.ctrlpline.Add(new Vector3(3, 1, 2));
            st.ctrlpline.Add(new Vector3(5, 1, 2));

            st.MakeLineCurve(a);

            GL.PushMatrix();

            GL.Begin(BeginMode.LineStrip);
            Console.WriteLine("start");
            GL.PointSize(19);
            GL.Color4(Color4.White);
            foreach (var aa in a)
            {
                GL.Vertex3(aa.X, aa.Y, aa.Z);
                Console.WriteLine("x" + aa.X + ", " + "y" + aa.Y + ", " + "z" + aa.Z);
            }
            Console.WriteLine("end");
            GL.End();

            Ground();

            //GL.Begin(BeginMode.Quads);
            //GL.Color4(Color4.White);
            //GL.Vertex3(0.0f, 0.0f, 4.0f);
            //GL.Color4(Color4.Red);
            //GL.Vertex3(-1.0f, -1.0f, 4.0f);
            //GL.Color4(Color4.Lime);
            //GL.Vertex3(1.0f, -1.0f, 4.0f);
            //GL.Color4(Color4.Blue);
            //GL.Vertex3(1.0f, 1.0f, 4.0f);
            //GL.End();

            GL.PopMatrix();
            glControl.SwapBuffers();

        }

        private void Ground()
        {
            float gmax = 50.0f;
            float gmx = 50.0f;
            float gmy = 50.0f;

            GL.Color4(Color4.Blue);
            GL.LineWidth(1);
            GL.Begin(BeginMode.Lines);
            for (float y = -gmy; y < gmy; y++)
            {
                GL.Vertex3(-gmx, y, 0);
                GL.Vertex3(gmx, y, 0);
            }
            for (float x = -gmx; x < gmx; x++)
            {
                GL.Vertex3(x, gmy, 0);
                GL.Vertex3(x, -gmy, 0);
            }
            GL.End();
        }

        private void glControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Size.Width, glControl.Size.Height);
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
