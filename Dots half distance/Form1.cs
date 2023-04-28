using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dots_half_distance
{
    public partial class Form1 : Form
    {
        private const int minDots = 2;
        private const int baseOpacity = 5;
        private const int dotsOpacity = 3;

        private static int iterations = 0;
        private static int width;
        private static int height;

        private static Bitmap bitmap;
        private static Graphics g;

        private static PointF p = new PointF();
        private static Random random = new Random();
        private static List<PointF> BasePoints = new List<PointF>();

        public Form1()
        {
            InitializeComponent();
            width = pictureBox1.Width;
            height = pictureBox1.Height;

            bitmap = new Bitmap(width, height);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(bitmap);
            timer1.Interval = (int)UpDownTicks.Value;
        }

        private void UpDownTicks_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)UpDownTicks.Value;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Iterations: " + iterations.ToString();
            int index = random.Next(0, BasePoints.Count);

            p.X = (p.X + BasePoints[index].X) / 2;
            p.Y = (p.Y + BasePoints[index].Y) / 2;

            BasePoints.ForEach(p => g.DrawEllipse(Pens.Red, p.X, p.Y, baseOpacity, baseOpacity));
            g.DrawEllipse(Pens.White, p.X, p.Y, dotsOpacity, dotsOpacity);

            iterations++;
            pictureBox1.Image = bitmap;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            iterations = 0;
            g.Clear(Color.Black);
            timer1.Start();
            GenerateRandomBase((int)UpDownPoints.Value);
            p = new PointF((BasePoints[0].X + BasePoints[1].X) / 2, (BasePoints[0].Y + BasePoints[1].Y) / 2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BasePoints.Count < minDots) return;

            iterations = 0;
            g.Clear(Color.Black);
            timer1.Start();
            p = new PointF((BasePoints[0].X + BasePoints[1].X) / 2, (BasePoints[0].Y + BasePoints[1].Y) / 2);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs e2 = (MouseEventArgs)e;

            float mx = e2.X;
            float my = e2.Y;
            BasePoints.Add(new PointF(mx, my));
            g.DrawEllipse(Pens.Red, mx, my, baseOpacity, baseOpacity);
            Refresh();
        }

        public static void GenerateRandomBase(int points)
        {
            //Generate base points
            BasePoints = new List<PointF>();
            for (int i = 0; i < points; i++)
            {
                BasePoints.Add(new PointF(random.Next(0, width), random.Next(0, height)));
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            Refresh();
            timer1.Stop();
            BasePoints = new List<PointF>();
        }
    }
}
