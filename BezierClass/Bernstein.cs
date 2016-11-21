using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierClass
{
    public class Bernstein
    {
        private float _b0, _b1, _b2, _b3;
        public void Init(float t)
        {
            _b0 = BS0(t);
            _b1 = BS1(t);
            _b2 = BS2(t);
            _b3 = BS3(t);
        }
        public void dInit(float t)
        {
            _b0 = dBS0(t);
            _b1 = dBS1(t);
            _b2 = dBS2(t);
            _b3 = dBS3(t);
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
