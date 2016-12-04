using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class GLControl : UserControl
    {
        Bezier3f st;
        public GLControl()
        {
            st = new Bezier3f();
            
            InitializeComponent();
        }

        private new bool DesignMode
        {
            get
            {
                bool design = base.DesignMode;

                Control parent = this.Parent;

                while (parent != null)
                {
                    ISite site = parent.Site;

                    if (site != null)
                    {
                        design |= site.DesignMode; 
                    }

                    parent = parent.Parent;
                }
                return design;
            }
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

        private void GLControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {

        }

        private void GLControl_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
