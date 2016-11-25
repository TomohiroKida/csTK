using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BezierClass
{
    public class Bezier3f
    {
        // 曲線用制御点
        public List<Vector3> ctrlpline { get; set;}
        // 曲面用制御点
        public List<List<Vector3>> ctrlpcurve { get; set; }
        


        /// <summary>
        /// 曲面　位置ベクトル
        /// </summary>
        /// <param name="u">U方向のパラメータ</param>
        /// <param name="v">V方向のパラメータ</param>
        /// <param name="mode">モード(0, 1, 2) = (null, u, v)</param>
        /// <returns></returns>
        public Vector3 PosiCurve(float u, float v, int mode)
        {
            Bernstein bu = new Bernstein();
            Bernstein bv = new Bernstein();

            if (mode == 1)
            {
                bu.dInit(u);
                bv.Init(v);
            }
            else if (mode == 2)
            {
                bu.Init(u);
                bv.dInit(v);
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
            Pt.X = tmp[0, 0];
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
        /// <summary>
        /// 曲線メッシュ
        /// </summary>
        /// <param name="mesh"></param>
        public void BezierCurveSurfaceMesh(List<Vector3> mesh)
        {
            Vector2 uv = new Vector2(0, 0);
            while (uv.X < Bernstein.ParametricMax)
            {
                while (uv.Y < Bernstein.ParametricMax)
                {
                    mesh.Add(PosiCurve(uv.X, uv.Y, 0));
                    uv.Y += 0.01f;
                    mesh.Add(PosiCurve(uv.X, uv.Y, 1));
                }
                uv.X += 0.01f;
                uv.Y = 0.0f;
            }
            uv.X = 0.0f;
            while (uv.Y < Bernstein.ParametricMax)
            {
                while (uv.X < Bernstein.ParametricMax)
                {
                    mesh.Add(PosiCurve(uv.X, uv.Y, 0));
                    uv.X += 0.01f;
                    mesh.Add(PosiCurve(uv.X, uv.Y, 2));
                }
                uv.Y += 0.01f;
                uv.X = 0.0f;
            }
        }
        /// <summary>
        /// アイソパラメトリック曲線
        /// </summary>
        /// <param name="const_u"></param>
        /// <param name="const_v"></param>
        /// <param name="isopara"></param>
        public void IsoparametricCurve(float const_u, float const_v, List<Vector3> isopara)
        {
            Vector2 uv = new Vector2(0, 0);

            while (uv.Y < Bernstein.ParametricMax)
            {
                isopara.Add(PosiCurve(const_u, uv.Y, 0));
                uv.Y += 0.01f;
                isopara.Add(PosiCurve(const_u, uv.Y, 0));
            }
            while (uv.X < Bernstein.ParametricMax)
            {
                isopara.Add(PosiCurve(uv.X, const_v, 0));
                uv.X += 0.01f;
                isopara.Add(PosiCurve(uv.X, const_v, 0));
            }
        }
        /// <summary>
        /// 接線ベクトル
        /// </summary>
        /// <param name="const_u"></param>
        /// <param name="const_v"></param>
        /// <param name="tangent"></param>
        public void TangentVector(float const_u, float const_v, List<Vector3> tangent)
        {
            // u方向にのびるベクトル
            tangent.Add(PosiCurve(const_u, const_v, 0));
            tangent.Add(PosiCurve(const_u, const_v, 0) + PosiCurve(const_u, const_v, 1));
            // v方向にのびるベクトル
            tangent.Add(PosiCurve(const_u, const_v, 0));
            tangent.Add(PosiCurve(const_u, const_v, 0) + PosiCurve(const_u, const_v, 2));
        }
        /// <summary>
        /// 接平面
        /// </summary>
        /// <param name="const_u"></param>
        /// <param name="const_v"></param>
        /// <param name="tangent"></param>
        public void TangentPlane(float const_u, float const_v, List<Vector3> tangent)
        {
            tangent.Add((PosiCurve(const_u, const_v, 0) + Vector3.Normalize(PosiCurve(const_u, const_v, 1))) - Vector3.Normalize(PosiCurve(const_u, const_v, 2)));
            tangent.Add((PosiCurve(const_u, const_v, 0) + Vector3.Normalize(PosiCurve(const_u, const_v, 1))) + Vector3.Normalize(PosiCurve(const_u, const_v, 2)));
            tangent.Add((PosiCurve(const_u, const_v, 0) - Vector3.Normalize(PosiCurve(const_u, const_v, 1))) + Vector3.Normalize(PosiCurve(const_u, const_v, 2)));
            tangent.Add((PosiCurve(const_u, const_v, 0) - Vector3.Normalize(PosiCurve(const_u, const_v, 1))) - Vector3.Normalize(PosiCurve(const_u, const_v, 2)));
        }
        /// <summary>
        /// 曲面の表
        /// </summary>
        /// <param name="const_u"></param>
        /// <param name="const_v"></param>
        /// <param name="Inverted"></param>
        public void InvertedVector(float const_u, float const_v, List<Vector3> Inverted)
        {
            Inverted.Add(PosiCurve(const_u, const_v, 0));
            Inverted.Add(Vector3.Normalize(// 正規化
                Vector3.Cross(// 外積
                    PosiCurve(const_u, const_v, 1), 
                    PosiCurve(const_u, const_v, 2)
                    )
                    ));
        }
        /// <summary>
        /// 曲線
        /// </summary>
        /// <param name="linevec"></param>
        public void MakeLineCurve(List<Vector3> linevec)
        {
            float t = 0;
            linevec.Clear();
            while (t <= 1)
            {
                linevec.Add(PosiLine(t, true));
                t += 0.1f;
            }
        }
    }
}
