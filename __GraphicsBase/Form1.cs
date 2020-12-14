using _3DBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        float alpha = 0.0f;
        float b2;
        float a2;
        Vector3 winCenter;
        float scale = 40;

        public Form1()
        {
            InitializeComponent();
            b2 = 2.0f;
            a2 = 1.0f;
            winCenter = new Vector3(canvas.Width / 2.0f,  40.0f, 0.0f);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Surface3D();
        }

        private float X(float u, float v) { return u; }
        private float Y(float u, float v) { return v; }
        private float Z(float u, float v) { return (float)(Math.Cos(b2 * v) * Math.Sin(a2 * u)); }
        private void Surface3D()
        {
            Vector3 V = new Vector3(0.3f, 0.2f, 0.8f);
            Matrix4 projection = Matrix4.Parallel(V);
           
            Matrix4 rotY = Matrix4.RotY(alpha);

            float a = -2.0f * b2 * (float)Math.PI;
            float b = 2.0f * b2 * (float)Math.PI;

            float h1 = (b - a) / 100f;
            float c = 0;
            float d = 2.0f * (float)Math.PI;
            float h2 = (d - c) / 100f;

            float u2 = a;
            float v2;
            while (u2 < b)
            {
                v2 = c;
                Vector3 pv0, pv1;
                Vector3 v0 = new Vector3(X(u2, v2), Y(u2, v2), Z(u2, v2)) * scale;
                while (v2 < d)
                {
                    v2 += h2;
                    Vector3 v1 = new Vector3(X(u2, v2), Y(u2, v2), Z(u2, v2)) * scale;
                    pv0 = projection * (rotY * v0) + winCenter;
                    pv1 = projection * (rotY * v1) + winCenter;
                    g.DrawLine(Pens.Green, pv0, pv1);
                    v0 = v1;
                }
                u2 += h1;
            }

            float v = c;
            float u;
            while (v < d)
            {
                u = a;
                Vector3 pv0, pv1;
                Vector3 v0 = new Vector3(X(u, v), Y(u, v), Z(u, v)) * scale;
                while (u < b)
                {
                    u += h1;
                    Vector3 v1 = new Vector3(X(u, v), Y(u, v), Z(u, v)) * scale;
                    pv0 = projection * (rotY * v0) + winCenter;
                    pv1 = projection * (rotY * v1) + winCenter;
                    g.DrawLine(Pens.Red, pv0, pv1);
                    v0 = v1;
                }
                v += h2;

            }
        }

        #region Mouse handling
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {

        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void rotYScroll_Scroll(object sender, ScrollEventArgs e)
        {
            alpha =  rotYScroll.Value / 100.0f;
            canvas.Refresh(); 
        }
    }
}
