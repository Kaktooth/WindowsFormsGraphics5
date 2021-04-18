using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsGraphics5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class View3D
        {
            public class Point3D
            {
                public double X;
                public double Y;
                public double Z;

                public Point3D(int x, int y, int z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D(float x, float y, float z)
                {
                    X = (double)x;
                    Y = (double)y;
                    Z = (double)z;
                }

                public Point3D(double x, double y, double z)
                {
                    X = x;
                    Y = y;
                    Z = z;
                }

                public Point3D()
                {
                }

                public override string ToString()
                {
                    return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
                }
            }

            public static Point3D RotateX(Point3D point3D, double degrees)
            {
                double rad = (Math.PI * degrees) / 180.0f;
                double x = (point3D.X * Math.Cos(rad)) + (point3D.Z * Math.Sin(rad));
                double z = (point3D.X * -Math.Sin(rad)) + (point3D.Z * Math.Cos(rad));

                return new Point3D(x, point3D.Y, z);
            }

            public static Point3D RotateY(Point3D point3D, double degrees)
            {
                double rad = (Math.PI * degrees) / 180.0;
                double y = (point3D.Y * Math.Cos(rad)) + (point3D.Z * Math.Sin(rad));
                double z = (point3D.Y * -Math.Sin(rad)) + (point3D.Z * Math.Cos(rad));

                return new Point3D(point3D.X, y, z);
            }
            public static Point3D RotateZ(Point3D point3D, double degrees)
            {
                double rad = (Math.PI * degrees) / 180.0;
                double x = (point3D.X * Math.Cos(rad)) + (point3D.Y * Math.Sin(rad));
                double y = (point3D.X * -Math.Sin(rad)) + (point3D.Y * Math.Cos(rad));

                return new Point3D(x, y, point3D.Z);
            }


            public static Point3D Translate(Point3D points3D, Point3D oldOrigin, Point3D newOrigin)
            {
                Point3D difference = new Point3D(newOrigin.X - oldOrigin.X, newOrigin.Y - oldOrigin.Y, newOrigin.Z - oldOrigin.Z);
                points3D.X += difference.X;
                points3D.Y += difference.Y;
                points3D.Z += difference.Z;
                return points3D;
            }
            public static Point3D CalcNormal(Point3D points3D1, Point3D points3D2, Point3D points3D3)
            {
                View3D.Point3D n = new View3D.Point3D(points3D1.Y * points3D3.Z - points3D2.Y * points3D3.Z - points3D1.Y * points3D2.Z - points3D1.Z * points3D3.Y + points3D2.Z * points3D3.Y + points3D1.Z * points3D2.Y,
points3D1.Z * points3D3.X - points3D2.Z * points3D3.X - points3D1.Z * points3D2.X - points3D1.X * points3D3.Z + points3D2.X * points3D3.Z + points3D1.X * points3D2.Z,
points3D1.X * points3D3.Y - points3D2.X * points3D3.Y - points3D1.X * points3D2.Y - points3D1.Y * points3D3.X + points3D2.Y * points3D3.X + points3D1.Y * points3D2.X);

                return n;
            }

            public static Point3D[] RotateX(Point3D[] points3D, double degrees)
            {
                for (int i = 0; i < points3D.Length; i++)
                {
                    points3D[i] = RotateX(points3D[i], degrees);
                }
                return points3D;
            }

            public static Point3D[] RotateY(Point3D[] points3D, double degrees)
            {
                for (int i = 0; i < points3D.Length; i++)
                {
                    points3D[i] = RotateY(points3D[i], degrees);
                }
                return points3D;
            }
            public static Point3D[] RotateZ(Point3D[] points3D, double degrees)
            {
                
                for (int i = 0; i < points3D.Length; i++)
                {
                    points3D[i] = RotateZ(points3D[i], degrees);
                }
                return points3D;
            }
            public static Point3D[] Translate(Point3D[] points3D, Point3D oldOrigin, Point3D newOrigin)
            {
                for (int i = 0; i < points3D.Length; i++)
                {
                    points3D[i] = Translate(points3D[i], oldOrigin, newOrigin);
                }
                return points3D;
            }

        }
        public static View3D.Point3D[] PiramidPoints(int width, int height, int depth)
        {
            View3D.Point3D[] verts = new View3D.Point3D[6];

            verts[0] = new View3D.Point3D(0, 0, 0);
            verts[1] = new View3D.Point3D(width*2, 0, 0);
            verts[2] = new View3D.Point3D(0, height, 0);
            verts[3] = new View3D.Point3D(0, 0, depth*2);
            verts[4] = new View3D.Point3D(width, 0, 0);
            verts[5] = new View3D.Point3D((int)(width/2), height,(int)(depth/2));

            return verts;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                Brush b = Brushes.Black;
                Pen p = Pens.Black;
                PointF[] point3D = new PointF[6];
                int x = Convert.ToInt32(textBox1.Text);
                int y = Convert.ToInt32(textBox3.Text);
                int z = Convert.ToInt32(textBox2.Text);
                int XSize = Convert.ToInt32(textBox4.Text);
                int YSize = Convert.ToInt32(textBox5.Text);
                int dist = Convert.ToInt32(textBox6.Text);
                int Size = Convert.ToInt32(textBox7.Text);
                int XAngle = Convert.ToInt32(trackBar1.Value);
                int YAngle = Convert.ToInt32(trackBar2.Value);
                int ZAngle = Convert.ToInt32(trackBar3.Value);
                View3D.Point3D point0 = new View3D.Point3D(x, y, z);
                View3D.Point3D[] pyramidPoints = PiramidPoints(Size, Size, Size);
                View3D.Point3D pyramidOrigin = new View3D.Point3D((int)(Size / 2), (int)(Size / 2), (int)(Size / 2));
                pyramidPoints = View3D.Translate(pyramidPoints, pyramidOrigin, point0);
                pyramidPoints = View3D.RotateX(pyramidPoints, XAngle);
                pyramidPoints = View3D.RotateY(pyramidPoints, YAngle);
                pyramidPoints = View3D.RotateZ(pyramidPoints, ZAngle);
                pyramidPoints = View3D.Translate(pyramidPoints, point0, pyramidOrigin);
                for (int i = 0; i < point3D.Length; i++)
                {
                    point3D[i].X = ((XSize / 2) + (((float)pyramidPoints[i].X * dist) / ((float)pyramidPoints[i].Z + dist)));
                    point3D[i].Y = (YSize / 2) - (((float)pyramidPoints[i].Y * dist) / ((float)pyramidPoints[i].Z + dist));
                }
                View3D.Point3D n = View3D.CalcNormal(pyramidPoints[1], pyramidPoints[0], pyramidPoints[3]);
                if (n.Z * dist + n.X * pyramidPoints[1].X + n.Y * pyramidPoints[1].Y + n.Z * pyramidPoints[1].Z < 0)
                {
                    //1
                    g.DrawLine(p, point3D[1].X, point3D[1].Y, point3D[0].X, point3D[0].Y);
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[0].X, point3D[0].Y);
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[1].X, point3D[1].Y);
                }
                n = View3D.CalcNormal(pyramidPoints[3], pyramidPoints[0], pyramidPoints[5]);
                if (n.Z * dist + n.X * pyramidPoints[3].X + n.Y * pyramidPoints[3].Y + n.Z * pyramidPoints[3].Z < 0)
                {
                    //2
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[0].X, point3D[0].Y);
                    g.DrawLine(p, point3D[5].X, point3D[5].Y, point3D[0].X, point3D[0].Y);
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[5].X, point3D[5].Y);
                }

                n = View3D.CalcNormal(pyramidPoints[1], pyramidPoints[3], pyramidPoints[5]);
                if (n.Z * dist + n.X * pyramidPoints[1].X + n.Y * pyramidPoints[1].Y + n.Z * pyramidPoints[1].Z < 0)
                {
                    //3
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[5].X, point3D[5].Y);
                    g.DrawLine(p, point3D[5].X, point3D[5].Y, point3D[1].X, point3D[1].Y);
                    g.DrawLine(p, point3D[3].X, point3D[3].Y, point3D[1].X, point3D[1].Y);
                }

                n = View3D.CalcNormal(pyramidPoints[0], pyramidPoints[1], pyramidPoints[5]);
                if (n.Z * dist + n.X * pyramidPoints[0].X + n.Y * pyramidPoints[0].Y + n.Z * pyramidPoints[0].Z < 0)
                {
                    //4
                    g.DrawLine(p, point3D[5].X, point3D[5].Y, point3D[0].X, point3D[0].Y);
                    g.DrawLine(p, point3D[5].X, point3D[5].Y, point3D[1].X, point3D[1].Y);
                    g.DrawLine(p, point3D[1].X, point3D[1].Y, point3D[0].X, point3D[0].Y);
                }
                pictureBox1.Refresh();
            }
            stopwatch.Stop();
            label11.Text = "Runtime: " + stopwatch.Elapsed.ToString();
            GC.Collect();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
