using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BezierClass
{
    class Bezier3f
    {
        // 曲線用制御点
        List<Vector3> ctrlpline;
        // 曲面用制御点
        List<List<Vector3>> ctrlpcurve;


        /// <summary>
        /// 曲面　位置ベクトル
        /// </summary>
        /// <param name="u">U方向のパラメータ</param>
        /// <param name="v">V方向のパラメータ</param>
        /// <param name="direction">方向(true, false) = (u, v)</param>
        /// <param name="v">V方向のパラメータ</param>
        /// <returns></returns>
        public Vector3 PosiCurve(float u, float v, bool direction)
        {
            Bernstein bu = new Bernstein();
            Bernstein bv = new Bernstein();

            if (direction)
            {
                bu.dInit(u);
                bv.Init(v);
            }
            else
            {
                bu.Init(u);
                bv.Init(v);
            }

            Matrix4 cr_u = new Matrix4();
            Matrix4 cr_v = new Matrix4();
            Matrix4 cp = new Matrix4();
            Matrix4 tmp = new Matrix4();

            Vector3 Pt = new Vector3();

            for (int i = 0; i < 4; i++)
            {
                cr_u[0, i] = bu.b[i];
                cr_v[i, 0] = bv.b[i];
            }

            // x
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    cp[i, j] = ctrlpcurve[i][j].X;
            tmp = cr_u * cp;
            tmp = tmp * cr_v;
            Pt.X = tmp.[0, 0];
            // y
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    cp[i, j] = ctrlpcurve[i][j].Y;
            tmp = cr_u * cp;
            tmp = tmp * cr_v;
            Pt.Y = tmp[0, 0];
            // z
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    cp[i, j] = ctrlpcurve[i][j].Z;
            tmp = cr_u * cp;
            tmp = tmp * cr_v;
            Pt.Z = tmp[0, 0];


            return Pt;
        }

        /// <summary>
        /// 曲線　位置ベクトル
        /// </summary>
        /// <param name="t">パラメータ</param>
        /// <param name="dimension">(true, false) = (normal, 一階微分)</param>
        /// <returns></returns>
        public Vector3 PosiLine(float t, bool dimension)
        {
            Bernstein bt = new Bernstein();
            if (dimension) bt.Init(t);
            else bt.dInit(t);

            Matrix4 cr_t = new Matrix4();
            Matrix4 cp = new Matrix4();
            Matrix4 tmp = new Matrix4();
            Vector3 Pt = new Vector3();

            for (int i = 0; i < 4; i++)
            {
                cr_t[0, i] = bt.b[i];
            }

            // x
            for (int i = 0; i < 4; i++)
                cp[i, 0] = ctrlpline[i].X;
            tmp = cr_t * cp;
            Pt.X = tmp[0, 0];
            // y
            for (int i = 0; i < 4; i++)
                cp[i, 0] = ctrlpline[i].Y;
            tmp = cr_t * cp;
            Pt.Y = tmp[0, 0];
            // z
            for (int i = 0; i < 4; i++)
                cp[i, 0] = ctrlpline[i].Z;
            tmp = cr_t * cp;
            Pt.Z = tmp[0, 0];

            return Pt;
        }

        public void BezierCurveSurfaceMesh(List<Vector3> mesh)
        {
            Vector2 uv = new Vector2(0, 0);
            while (uv.X < Bernstein.ParametricMax)
            {
                while (uv.Y < Bernstein.ParametricMax)
                {
                    mesh.Add(PosiCurve(uv.X, uv.Y, true));
                    uv.Y += 0.01f;
                    mesh.Add(PosiCurve(uv.X, uv.Y, false));
                }
                uv.X += 0.01f;
                uv.Y = 0.0f;
            }
            uv.X = 0.0f;
            while (uv.Y < Bernstein.ParametricMax)
            {
                while (uv.X < Bernstein.ParametricMax)
                {
                    mesh.Add(PosiCurve(uv.X, uv.Y, true));
                    uv.X += 0.01f;
                    mesh.Add(PosiCurve(uv.X, uv.Y, false));
                }
                uv.Y += 0.01f;
                uv.X = 0.0f;
            }
        }
    }
}
