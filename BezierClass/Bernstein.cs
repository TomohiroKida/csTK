using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierClass
{
    public class Bernstein
    {
        private const int PARAMETRIC_MAX = 1;

        public static int ParametricMax
        {
            get { return PARAMETRIC_MAX; }
        }
        
        public float[] b = new float[4];
        public void Init(float t)
        {
            b[0] = BS0(t);
            b[1] = BS1(t);
            b[2] = BS2(t);
            b[3] = BS3(t);
        }
        public void dInit(float t)
        {
            b[0] = dBS0(t);
            b[1] = dBS1(t);
            b[2] = dBS2(t);
            b[3] = dBS3(t);
        }

        private float BS0(float t) { return (float)Math.Pow((1 - t), 3); }
        private float BS1(float t) { return 3 * t * (float)Math.Pow((1 - t), 2); ; }
        private float BS2(float t) { return 3 * (float)Math.Pow(t, 2) * (1 - t); }
        private float BS3(float t) { return (float)Math.Pow(t, 3); }
        private float dBS0(float t) { return -3 * (float)Math.Pow((1 - t), 2); }
        private float dBS1(float t) { return 3 * (1 - 4 * t + 3 * (float)Math.Pow(t, 2)); }
        private float dBS2(float t) { return 3 * t * (2 - 3 * t); }
        private float dBS3(float t) { return 3 * (float)Math.Pow(t, 2); }
    }
}
